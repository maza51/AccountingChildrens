using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AccountingChildrens.Application;
using AccountingChildrens.Application.Interfases;
using AccountingChildrens.Application.Services;
using AccountingChildrens.Domain;
using AccountingChildrens.Domain.Repositories;
using AccountingChildrens.Infrastructure.Data;
using AccountingChildrens.Infrastructure.Data.Repository;
using AccountingChildrens.Infrastructure.NLog;
using AccountingChildrens.WebApi.Moddleware;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;

namespace AccountingChildrens.WebApi
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AppMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<ILoggerManager, LoggerManager>();

            services.AddTransient<IChildrenRepository, ChildrenRepository>();
            services.AddTransient<IEducatorRepository, EducatorRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();

            services.AddTransient<IChildrenService, ChildrenService>();
            services.AddTransient<IEducatorService, EducatorServices>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IChildrenGroupService, ChildrenGroupService>();
            services.AddTransient<IEducatorGroupService, EducatorGroupService>();

            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddDbContext<AppDbContext>(options => options
                .UseSqlite(_configuration.GetConnectionString("AppDbContext"), b =>
                b.MigrationsAssembly("AccountingChildrens.Infrastructure")));

            //services.AddDbContext<AppDbContext>(options => options
            //    .UseNpgsql(_configuration.GetConnectionString("AppDbContextPostgreSQL"), b =>
            //    b.MigrationsAssembly("AccountingChildrens.Infrastructure")));

            services.AddMvc(options => options.EnableEndpointRouting = false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseRouting();

            app.UseMvc();

            app.UseMvcWithDefaultRoute();
        }
    }
}

