using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AAR.Infrastructure.Extension;

namespace AAR.Infrastructure
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var generalConfig = _configuration.GetSection("GeneralConfig");
            var serviceNamingConvention = generalConfig["ServiceNamingConvention"]; // used for reflection DI
            var serilogConfig = _configuration.GetSection("Serilog");
            var applicationAssemblyName = generalConfig["ApplicationAssembly"];

            ServiceExtension.AddMisc(services, applicationAssemblyName);
            ServiceExtension.AddCommandServices(services, serviceNamingConvention);
            ServiceExtension.AddQueryServices(services, serviceNamingConvention);
            ServiceExtension.AddCommandDecorators(services);
            ServiceExtension.AddQueryDecorators(services);
            ServiceExtension.AddRepositories(services);
            ServiceExtension.AddSerilog(serilogConfig);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigureExtension.ConfigureEnvironment(app, env, _configuration);
            ConfigureExtension.ConfigureApp(app);
        }
    }
}
