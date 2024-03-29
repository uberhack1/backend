﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UberHack.API.Contracts;
using UberHack.API.Entities;
using UberHack.API.Repository;
using Swashbuckle.AspNetCore.Swagger;
using UberHack.API.Hub;

namespace UberHack.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<UberHackDbContext>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API UberHack1", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            services.AddSignalR();

            services.AddTransient<IBaseRepository<Bairro>, BaseRepository<Bairro>>();
            services.AddTransient<IBaseRepository<Chat>, BaseRepository<Chat>>();
            services.AddTransient<IBaseRepository<Empresa>, BaseRepository<Empresa>>();
            services.AddTransient<IBaseRepository<Faculdade>, BaseRepository<Faculdade>>();
            services.AddTransient<IBaseRepository<Mensagem>, BaseRepository<Mensagem>>();
            services.AddTransient<IBaseRepository<Usuario>, BaseRepository<Usuario>>();
            services.AddTransient<IBaseRepository<ChatUsuarios>, BaseRepository<ChatUsuarios>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json",
                    "API UberHack1");
            });
            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/chathub");
            });
            app.UseCors((o) =>
            {
                o.AllowCredentials();
                o.AllowAnyHeader();
                o.AllowAnyMethod();
                o.AllowAnyOrigin();
            });

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
