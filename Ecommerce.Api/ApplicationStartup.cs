using Ecommerce.Services.Collection;
using Ecommerce.Services.Utils;
using Ecommerce.Utils.Settings;
using Microsoft.OpenApi.Models;
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
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));

            services.AddMvc().AddJsonOptions(options => 
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EcommerceAPI", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EcommerceAPI v1"));
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