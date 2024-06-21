using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DollarInfo.DAL.Factory;
using DollarInfo.DAL.Repositories.Interfaces;
using DollarInfo.DAL.Repositories;
using DollarInfo.Services.Interfaces;
using DollarInfo.Utils.EmailService;
using DollarInfo.DAL.Aspect;

namespace DollarInfo.Services.Collection
{
    public static class ContainerServiceCollectionExtensions
    {
        public static void ServicesDependencyInjection(this IServiceCollection services, IConfiguration _configuration)
        {
            // Configuration
            services.AddSingleton(provider => (IConfigurationRoot)_configuration);

            // Factories
            services.AddTransient<IConnectionFactory, ConnectionFactory>();

            // Services
            services.AddScoped<ICurrentUserAspect, CurrentUserAspect>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IRatesService, RatesService>();

            // Repositories
            services.AddTransient<IMiscRepository, MiscRepository>();
        }
    }
}