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
            // Db service configuration
            services.Configure<VehicleTrackerDbSettings>(Configuration.GetSection(nameof(VehicleTrackerDbSettings)));
            services.AddSingleton<IVehicleTrackerDbSettings>(sp =>sp.GetRequiredService<IOptions<VehicleTrackerDbSettings>>().Value);
            services.AddSingleton<IRegistrationService, RegistrationService>();
            services.AddSingleton<ITrackingService, TrackingService>();

            services.AddMediatR(typeof(Startup));

            // Adding Swagger
            services.AddSwaggerGen(options =>
            {
                var version = Assembly.GetEntryAssembly()?.GetName().Version.ToString();
                options.ResolveConflictingActions(x => x.First());
                options.SwaggerDoc(version, new OpenApiInfo {Title = Common.Constants.SwaggerTitle, Version = version});
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
                options.SwaggerEndpoint(versionName, Common.Constants.SwaggerTitle);
                options.DocumentTitle = Common.Constants.SwaggerTitle;
            }); // specifying the Swagger JSON endpoint.

            app.UseMvc();
        }
    }
}
