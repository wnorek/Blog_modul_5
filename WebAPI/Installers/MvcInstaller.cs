﻿using Application;
using Application.Interfaces;
using Application.Mapping;
using Application.Services;
using Domain.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddApplication();
            services.AddInfrastructure();
            services.AddControllers()
                .AddJsonOptions(options => 
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            services.AddApiVersioning(x=>
                {
                    x.DefaultApiVersion = new ApiVersion(1, 0);
                    x.AssumeDefaultVersionWhenUnspecified = true;
                    x.ReportApiVersions = true;
                    x.ApiVersionReader = new HeaderApiVersionReader("x-ApiVersionReader-version");
                });

            services.AddAuthorization();

            services.AddTransient<UserResolverService>();
            services.AddOData();

        }
    }
}