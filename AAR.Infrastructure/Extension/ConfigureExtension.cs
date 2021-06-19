using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AAR.Infrastructure.Extension
{
    public static class ConfigureExtension
    {
        public static void ConfigureEnvironment(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            var swaggerConfig = configuration.GetSection("Swagger");
            var swaggerUrl = swaggerConfig["Endpoint"];
            var swaggerName = swaggerConfig["Title"] + " " + swaggerConfig["Versionas"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint(swaggerUrl, swaggerName));
            }
        }

        public static void ConfigureApp(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();
        }
    }
}
