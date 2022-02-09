using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPCoreLesson4AdditionalProject;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASPCoreLesson4AdditionalTask
{
    public class Startup
    {
        IConfiguration appConfiguration;

        public Startup(IHostEnvironment env)
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath);
            builder.AddJsonFile("library.json");

            appConfiguration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            RouteBuilder builder = new RouteBuilder(app);

            builder.MapRoute("Library", async context =>
            {
                await context.Response.WriteAsync("Hello!");
            });

            builder.MapRoute("Library/Books", async context =>
            {
                IEnumerable<Book> books = LibraryManager.GetBooks(appConfiguration);
                foreach (Book item in books)
                    await context.Response.WriteAsync($"Book name: {item.BookName}\n");
            });

            builder.MapRoute("Library/Profile", async context =>
            {
                IEnumerable<Student> students = LibraryManager.GetAllStudents(appConfiguration);
                foreach (var s in students)
                    await context.Response.WriteAsync($"ID: {s.StudentID}, name: {s.Name}\n");
            });

            builder.MapRoute("Library/Profile/{id?}", async context =>
            {
                string indexStr = context.GetRouteData().Values["id"]?.ToString();

                if (int.TryParse(indexStr, out int index))
                {
                    Student student = LibraryManager.GetStudentByID(index, appConfiguration);
                    if (student == null)
                        await context.Response.WriteAsync("This user doesnt exist.");
                    else
                        await context.Response.WriteAsync($"ID = {student.StudentID}, Name = {student.Name}");
                }
                else
                    await context.Response.WriteAsync("ID must be a number");

            });

            app.UseRouter(builder.Build());

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Default page");
            });

        }


    }

}
