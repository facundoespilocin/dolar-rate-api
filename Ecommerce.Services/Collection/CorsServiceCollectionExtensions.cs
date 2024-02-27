using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Services.Collection
{
    public static class CorsServiceCollectionExtensions
    {
        public const string CORS_POLICY = "_myAllowSpecificOrigins";

        public static void AddCorsConfigurationExtensions(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    name: CORS_POLICY,
                    builder => {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    }
                );
            });
        }

        public static void AddCorsConfigurationExtensions(this IApplicationBuilder app)
        {
            app.UseCors(CORS_POLICY);

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
            );
        }
    }
}