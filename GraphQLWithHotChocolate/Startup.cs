using System.Text.Json;
using System.Text.Json.Serialization;
using GraphQLWithHotChocolate.Context;
using GraphQLWithHotChocolate.DemoData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GraphQLWithHotChocolate {
    public class Startup {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices (IServiceCollection services) { }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseRouting ();
            var options = new JsonSerializerOptions()
            {
                MaxDepth = 0,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true,
            };
            app.UseEndpoints (endpoints => {
                endpoints.MapGet ("/", async context => {
                    await context.Response.WriteAsync ("Hello World!");
                });
                // CREACION DE DATOS DEMO
                endpoints.MapGet ("/createDemoData", async context => {
                    var demoData = new DemoDataCreator ();
                    var resp = demoData.CreateDemoData ();
                    string json = JsonSerializer.Serialize(resp, options);
                    await context.Response.WriteAsync (json);
                });
                // CONSULTA DE DATOS REST
                endpoints.MapGet ("/getBlogs", async context => {
                    using (var db = new DemoContext ()) {
                        var result = db.Blogs.Include (blog => blog.Posts).ThenInclude(post => post.Author).ToListAsync ();
                        string json = JsonSerializer.Serialize (result, options);
                        await context.Response.WriteAsync (json);
                    }
                });
            });
        }
    }
}