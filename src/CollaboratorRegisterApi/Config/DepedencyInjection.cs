using CollaboratorRegisterApi.Interfaces.Repositories;
using CollaboratorRegisterApi.Interfaces.Services;
using CollaboratorRegisterApi.Repositories;
using CollaboratorRegisterApi.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace CollaboratorRegisterApi.config
{
    [ExcludeFromCodeCoverage]
    public static class DepedencyInjection
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICollaboratorService, CollaboratorService>();
            services.AddTransient<ICollaboratorPhoneService, CollaboratorPhoneService>();

            var connectionString = Environment.GetEnvironmentVariable("SqlConnectionTesteZup");
            services.AddTransient<ICollaboratorRepository>(repo => new CollaboratorRepository(connectionString));
            services.AddTransient<ICollaboratorPhoneRepository>(repo => new CollaboratorPhoneRepository(connectionString));
            return services;
        }
    }
}
