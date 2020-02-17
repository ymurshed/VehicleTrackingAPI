using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using VehicleTracker.Business.GraphQL.DataProvider;
using VehicleTracker.Business.GraphQL.Query;
using VehicleTracker.Business.GraphQL.Schema;
using VehicleTracker.Business.Services;
using VehicleTracker.Contracts.GraphQL;
using VehicleTracker.Contracts.Models.AppSettingsModels;

namespace VehicleTracker.Api.Configure
{
    public class Configurator
    {
        public static void ConfigureAppSettingsServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfig>(configuration.GetSection(nameof(JwtConfig)));
            services.Configure<GoogleMapApiConfig>(configuration.GetSection(nameof(GoogleMapApiConfig)));
            services.Configure<VehicleTrackerDbConfig>(configuration.GetSection(nameof(VehicleTrackerDbConfig)));
            services.Configure<DummyAdminUser>(configuration.GetSection(nameof(DummyAdminUser)));
            services.AddSingleton<IJwtConfig>(provider => provider.GetRequiredService<IOptions<JwtConfig>>().Value);
            services.AddSingleton<IGoogleMapApiConfig>(provider => provider.GetRequiredService<IOptions<GoogleMapApiConfig>>().Value);
            services.AddSingleton<IVehicleTrackerDbConfig>(provider => provider.GetRequiredService<IOptions<VehicleTrackerDbConfig>>().Value);
            services.AddSingleton<IDummyAdminUser>(provider => provider.GetRequiredService<IOptions<DummyAdminUser>>().Value);
        }

        public static void ConfigureGraphQlServices(IServiceCollection services)
        {
            // GraphQL Model
            services.AddSingleton<RegistrationInfoType>();
            services.AddSingleton<TrackingInfoType>();
            services.AddSingleton<TrackingHistoryType>();
            services.AddSingleton<LocationType>();

            // Data Provider
            services.AddSingleton<RegistrationDataProvider>();
            services.AddSingleton<TrackingDataProvider>();

            // Query
            services.AddSingleton<VehicleTrackerQuery>();

            // Schema
            services.AddSingleton<ISchema, VehicleTrackerSchema>();

            // Activate schema
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
        }

        public static void ConfigureDbServices(IServiceCollection services)
        {
            services.AddSingleton<IRegistrationService, RegistrationService>();
            services.AddSingleton<ITrackingService, TrackingService>();
        }
    }
}
