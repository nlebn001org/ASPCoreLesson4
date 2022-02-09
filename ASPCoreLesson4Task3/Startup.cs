using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPCoreLesson4Task3
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            RouteBuilder builder = new RouteBuilder(app);
            builder.MapGet("{controller}/{action}/{id}", async context =>
            {
                string controller = context.GetRouteData().Values["controller"].ToString();
                string action = context.GetRouteData().Values["action"].ToString();
                string id = context.GetRouteData().Values["id"].ToString();

                if (controller.All(char.IsDigit) && action.All(char.IsDigit) && id.All(char.IsDigit))
                    await context.Response.WriteAsync($"{controller}/{action}/{id}");
                else
                    await context.Response.WriteAsync("Error");
            });

            app.UseRouter(builder.Build());

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Homepage");
            });
        }
    }
}
