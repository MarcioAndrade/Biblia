using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;

using Biblia.App.Interfaces;
using Biblia.App.Servicos;
using Biblia.Repositorio;

namespace Biblia.API;
public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

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

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bíblia API v1");
            c.RoutePrefix = "swagger"; // Deixa o Swagger disponível em /swagger
        });

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
