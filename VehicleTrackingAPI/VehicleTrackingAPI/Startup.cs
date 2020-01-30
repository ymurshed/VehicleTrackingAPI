using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VehicleTrackingAPI.Models.AppSettingsModels;
using VehicleTrackingAPI.Services;
using MediatR;
using Microsoft.OpenApi.Models;
using VehicleTrackingAPI.Utility;

namespace VehicleTrackingAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add AppSettings config
            services.Configure<JwtConfig>(Configuration.GetSection(nameof(JwtConfig)));
            services.Configure<VehicleTrackerDbConfig>(Configuration.GetSection(nameof(VehicleTrackerDbConfig)));
            services.Configure<DummyAdminUser>(Configuration.GetSection(nameof(DummyAdminUser)));
            services.AddSingleton<IJwtConfig>(provider => provider.GetRequiredService<IOptions<JwtConfig>>().Value);
            services.AddSingleton<IVehicleTrackerDbConfig>(provider => provider.GetRequiredService<IOptions<VehicleTrackerDbConfig>>().Value);
            services.AddSingleton<IDummyAdminUser>(provider => provider.GetRequiredService<IOptions<DummyAdminUser>>().Value);
            
            // Add Db services
            services.AddSingleton<IRegistrationService, RegistrationService>();
            services.AddSingleton<ITrackingService, TrackingService>();

            services.AddMediatR(typeof(Startup));

            // Adding Swagger
            services.AddSwaggerGen(options =>
            {
                var version = Assembly.GetEntryAssembly()?.GetName().Version.ToString();
                options.ResolveConflictingActions(x => x.First());
                options.SwaggerDoc(version, new OpenApiInfo {Title = Constants.SwaggerTitle, Version = version});
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Using Swagger
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var version = Assembly.GetEntryAssembly()?.GetName().Version.ToString();
                var versionName = $"/swagger/{version}/swagger.json";
                options.SwaggerEndpoint(versionName, Constants.SwaggerTitle);
                options.DocumentTitle = Constants.SwaggerTitle;
            }); // specifying the Swagger JSON endpoint.

            app.UseMvc();
        }
    }
}
