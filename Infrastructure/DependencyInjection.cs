using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICosmosRepository, CosmosPostRepository>();
            services.AddScoped<ICosmosPostService, CosmosPostService>();
            services.AddScoped<IPictureRepository, PictureRepository>();
            services.AddScoped<IAttachmentRepository, AttachmentRepository>();

            return services;
        }
    }
}
