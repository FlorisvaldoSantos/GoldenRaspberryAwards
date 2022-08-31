using AutoMapper;
using Domain;
using Domain.Entities;
using Infrasctruture.Context;
using Infrasctruture.Repository;
using Infrasctruture.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Service;
using Service.Services;
using Service.Services.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenRaspberryAwards
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
            /*AutoMepper - Configuração */
            #region AutoMapper-Config
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DTOMapperProfile());
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
            #endregion

            services.AddControllers();
            
            /* Adicionando as camadas ao serviço para utilizar a injeção de dependência */
            services.AddScoped<IProducerRepository, ProducerRepository>();
            services.AddScoped<ILoadInformation, LoadInformation>();
            services.AddScoped<IProducerBaseService<ProducerDTO>, ProducerBaseService<ProducerDTO>>();

             services.AddDbContext<DataBaseContext>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GoldenRaspberryAwards", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoadInformation LoadInformation)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GoldenRaspberryAwards v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStatusCodePages();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            LoadInformation.Initialize();
        }
                     

    }
}
