using Biblia.App.Interfaces;
using Biblia.App.Servicos;
using Biblia.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;

namespace Biblia.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddScoped<IBibliaApp, BibliaApp>();
            services.AddScoped<IBibliaRepository, BibliaRepository>();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Bíblia API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Email = "marcio281@gmail.com",
                        Name = "Marcio Costa"
                    },
                    Description = "Biblia sagrada com seis versões em português"
                });

                //var basePath = AppContext.BaseDirectory;
                //var assemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                //var fileName = Path.GetFileName(assemblyName + ".xml");
                ////Set the comments path for the swagger json and ui.

                //c.IncludeXmlComments(Path.Combine(basePath, fileName));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(c =>
            {
                c.AllowAnyHeader();
                c.AllowAnyMethod();
                c.AllowAnyOrigin();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "../swagger/v1/swagger.json";
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
