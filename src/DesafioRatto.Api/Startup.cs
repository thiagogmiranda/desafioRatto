using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioRatto.Dominio.Repositorio;
using DesafioRatto.Dominio.Servicos;
using DesafioRatto.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace DesafioRatto.Api
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

            services.AddDbContext<DesafioRattoContext>(options => options.UseInMemoryDatabase("DesafioRatto"));
            services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
            services.AddTransient<IEnderecoRepositorio, EnderecoRepositorio>();
            services.AddTransient<AdicionarClienteServico>();
            services.AddTransient<EditarClienteServico>();
            services.AddTransient<AdicionarEnderecoServico>();
            services.AddTransient<EditarEnderecoServico>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                    {
                        Title = "Ratto API",
                        Version = "v1",
                        Description = "Desafio Ratto",
                        Contact = new OpenApiContact
                        {
                            Name = "Thiago G. Miranda",
                            Url = new Uri("https://github.com/thiagogmiranda")
                        }
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ratto API V1");
            });
        }
    }
}
