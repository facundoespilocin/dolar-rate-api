using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ecommerce.DataAccessLayer.Factory;
using Ecommerce.DataAccessLayer.Repositories.Interfaces;
using Ecommerce.DataAccessLayer.Repositories;
using Ecommerce.Services.Interfaces;
using Ecommerce.Utils.EmailService;
using Ecommerce.DataAccessLayer.Aspect;

namespace Ecommerce.Services.Collection
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
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IMiscService, MiscService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<IOrdersService, OrdersService>();

            // Repositories
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IMiscRepository, MiscRepository>();
            services.AddTransient<IProductsRepository, ProductsRepository>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();
            services.AddTransient<ICustomersRepository, CustomersRepository>();
            services.AddTransient<IOrdersRepository, OrdersRepository>();
        }
    }
}