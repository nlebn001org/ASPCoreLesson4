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

namespace ASPCoreLesson4Task1
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

            builder.MapRoute("{controller}/{action}/{id}", async context =>
            {
                string controllerValue = context.GetRouteData().Values["controller"]?.ToString();
                string actionValue = context.GetRouteData().Values["action"]?.ToString();
                string idValue = context.GetRouteData().Values["id"]?.ToString();

                await context.Response.WriteAsync($"Controler: {controllerValue}, Action: {actionValue}, ID: {idValue}");

            });

            app.UseRouter(builder.Build());

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Home page");
            });

        }
    }
}
