namespace DollarInfo.Api
{
    public class Startup
    {
        private readonly ApplicationStartup _applicationStartup;

        public Startup(IConfiguration configuration)
        {
            _applicationStartup = new ApplicationStartup(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            _applicationStartup.ConfigureServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _applicationStartup.Configure(app, env);
        }
    }
}