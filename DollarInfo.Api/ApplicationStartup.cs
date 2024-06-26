using Amazon;
using Amazon.Runtime;
using Amazon.SimpleEmail;
using DollarInfo.Services.Collection;
using DollarInfo.Services.Helpers;
using DollarInfo.Services.Utils;
using DollarInfo.Utils.Settings;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DollarInfo
{
    public class ApplicationStartup
    {
        private readonly IConfiguration _configuration;

        public ApplicationStartup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ServicesDependencyInjection(_configuration);
            services.AddHttpContextAccessor();
            services.AddCors();

            services.AddControllers();
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddHttpClient<HttpClientWrapper>();

            services.Configure<ExternalApiSettings>(_configuration.GetSection("ExternalApi"));
            services.Configure<OriginsSettings>(_configuration.GetSection("OriginsSettings"));

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dollar Info API", Version = "v1" });
            });

            // AWS SES Configuration
            var awsOptions = _configuration.GetAWSOptions();
            awsOptions.Credentials = new BasicAWSCredentials(_configuration["AWS:AccessKey"], _configuration["AWS:SecretKey"]);
            awsOptions.Region = RegionEndpoint.GetBySystemName(_configuration["AWS:Region"]);

            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<IAmazonSimpleEmailService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dollar Info API v1"));

            app.UseCors(builder =>
                builder.WithOrigins(ConfigureOrigins())
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                // Health Checks Route
                endpoints.MapGet("/health", async context =>
                {
                    await context.Response.WriteAsync("Healthy");
                });
            });
        }

        public string[] ConfigureOrigins()
        {
            var origins = _configuration.GetSection("DolarInfo").Get<OriginsSettings>();

            return [origins.DomainUrl, origins.LocalhostUrl, origins.BaseUrl];
        }
    }
}
