using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Domain.Entities.Cosmos;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Installers
{
    public class CosmosInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var cosmosStoreSettings = new CosmosStoreSettings(
                configuration["CosmosSettings:DataBaseName"],
                configuration["CosmosSettings:AccountUri"],
                configuration["CosmosSettings:AccountKey"],
                new ConnectionPolicy { ConnectionMode=ConnectionMode.Direct, ConnectionProtocol=Protocol.Tcp});

            services.AddCosmosStore<CosmosPost>(cosmosStoreSettings);
        }
    }
}
