using Ecommerce.Services.Collection;
using Ecommerce.Services.Helpers;
using Ecommerce.Services.Utils;
using Ecommerce.Utils.Settings;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ecommerce
{
    public class ApplicationStartup
    {
        private readonly IConfiguration _configuration;

        public ApplicationStartup(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ServicesDependencyInjection(_configuration);
            services.AddHttpContextAccessor();
            services.AddCorsConfigurationExtensions();
            services.AddControllers();
            //services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddHttpClient<HttpClientWrapper>();

            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));
            services.Configure<DolarApiSettings>(_configuration.GetSection("DolarApi"));

            services.AddMvc().AddJsonOptions(options => 
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                // Configuración para usar CamelCase en la serialización
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                // Configuración para ignorar los nombres de propiedades definidos en JsonPropertyName al deserializar
                options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dolar Rate API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dolar Rate API v1"));
            }

            app.AddCorsConfigurationExtensions();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}