using IOT_DeviceManager.API.DeviceClient;
using IOT_DeviceManager.API.DeviceClient.MqttClient;
using IOT_DeviceManager.API.Repositories;
using IOT_DeviceManager.API.Repositories.DbContexts;
using IOT_DeviceManager.API.Services;
using IOT_DeviceManager.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace IOT_DeviceManager.API.Helpers.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddControllersWithInputOutputFormattersAndConfigureAPIBehaviour(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson(
                    setupAction =>
                    {
                        setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    // Add support for correct error response when input model is invalid
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetails = new ValidationProblemDetails(context.ModelState)
                        {
                            Type = "https://iotdevicemanager/api/modelvalidationproblem",
                            Title = "One or more model validation errors occurred.",
                            Status = StatusCodes.Status422UnprocessableEntity,
                            Detail = "See the errors property for details.",
                            Instance = context.HttpContext.Request.Path
                        };

                        problemDetails.Extensions.Add("traceId", context.HttpContext.TraceIdentifier);

                        return new UnprocessableEntityObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });
            return services;
        }

        public static IServiceCollection AddRepositoryService(this IServiceCollection services)
        {
            services.AddDbContext<DeviceRepositoryContext>(options =>
            {
                options.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=IOT_DeviceManagerDB;Trusted_Connection=True;");
            }, ServiceLifetime.Transient, ServiceLifetime.Transient);
            services.AddTransient<IDeviceRepository, DeviceInMemoryRepository>();
            return services;
        }

        public static IServiceCollection AddDeviceClientHostedService(this IServiceCollection services)
        {
            services.AddTransient<IDeviceClient, MqttDeviceClient>();
            services.AddSingleton<DeviceClientService>();
            services.AddSingleton<IHostedService>(serviceProvider => serviceProvider.GetService<DeviceClientService>());
            return services;
        }

        public static IServiceCollection AddCalculationService(this IServiceCollection services)
        {
            return services.AddTransient<ICalculationService, CalculationService>();
        }
    }
}
