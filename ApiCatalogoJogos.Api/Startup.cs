using ApiCatalogoJogos.Data.IRepository;
using ApiCatalogoJogos.Data.Repository;
using ApiCatalogoJogos.Service.InterfaceService;
using ApiCatalogoJogos.Service.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using static ApiCatalogoJogos.Api.Controllers.CicloDeVidaIDController;

namespace ApiCatalogoJogos.Api
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
            services.AddScoped<IJogoService, JogoService>();
            services.AddScoped<IJogoRepository, JogoSqlServerRepository>();

            #region Ciclo de Vida

            services.AddSingleton<IExemploSingleton, ExemploCicloVida>();
            services.AddScoped<IExemploScoped, ExemploCicloVida>();
            services.AddTransient<IExemploTransient, ExemploCicloVida>();

            #endregion


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ApiCatalogoJogos.Api", Version = "v1" });

                var basePath = AppDomain.CurrentDomain.BaseDirectory;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                c.IncludeXmlComments(Path.Combine(basePath, fileName));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCatalogoJogos.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
