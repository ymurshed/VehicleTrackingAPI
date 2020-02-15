using System;
using System.Linq;
using System.Reflection;
using System.Text;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using VehicleTracker.Api.Configure;
using VehicleTracker.Api.Filters;
using VehicleTracker.Business.Utility;

namespace VehicleTracker.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private void RegisterServices(IServiceCollection services)
        {
            Configurator.ConfigureAppSettingsServices(services, Configuration);
            Configurator.ConfigureDbServices(services);
            Configurator.ConfigureGraphQlServices(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RegisterServices(services);

            // Add GraphQL
            services.AddGraphQL(o => { o.ExposeExceptions = false; }).AddGraphTypes();

            // Add mediator
            services.AddMediatR(typeof(Startup));
            var businessAssembly = AppDomain.CurrentDomain.Load("VehicleTracker.Business");
            services.AddMediatR(businessAssembly);

            // Add Swagger
            services.AddSwaggerGen(options =>
            {
                var version = Assembly.GetEntryAssembly()?.GetName().Version.ToString();
                options.ResolveConflictingActions(x => x.First());
                options.SwaggerDoc(version, new OpenApiInfo {Title = Constants.SwaggerTitle, Version = version});
            });

            // Add Jwt
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration[Constants.Issuer],
                        ClockSkew = TimeSpan.FromMinutes(Convert.ToDouble(Configuration[Constants.ExpireTimeInMins])),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[Constants.SecretKey]))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            var payload = new JObject
                            {
                                ["error"] = Constants.Unauthorized + context.Error,
                                ["error_description"] = Constants.UnauthorizedError + context.ErrorDescription,
                                ["error_uri"] = context.ErrorUri
                            };

                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return context.Response.WriteAsync(payload.ToString());
                        }
                    };
                });

            // Add admin user policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.AdminUserPolicy, policy => policy.RequireClaim(Constants.AdminUserPolicy));
            });

            // Add MVC
            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(LoggingFilter));
                config.Filters.Add(typeof(GlobalExceptionFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Use Serilog extension to write logs in file
            loggerFactory.AddFile("Logs/api-{Date}.txt");

            // Specify the Swagger JSON endpoint
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var version = Assembly.GetEntryAssembly()?.GetName().Version.ToString();
                var versionName = $"/swagger/{version}/swagger.json";
                options.SwaggerEndpoint(versionName, Constants.SwaggerTitle);
                options.DocumentTitle = Constants.SwaggerTitle;
            });

            app.UseAuthentication();

            app.UseGraphQL<ISchema>();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions {Path = "/ui/playground" });
            
            app.UseMvc();
        }
    }
}
