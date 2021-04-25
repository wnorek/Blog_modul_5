using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.HealthChecks;

namespace WebAPI.Installers
{
    public class HealthChecksInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<BloggerContext>("Database");

            services.AddHealthChecks()
                .AddCheck<ResponseTimeHealthChcek>("Network speed test");

            services.AddHealthChecksUI()
                .AddInMemoryStorage();
                
         
                
        }
    }
}
