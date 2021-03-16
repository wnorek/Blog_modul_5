using Domain.Entities.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICosmosRepository
    {
        Task<IEnumerable<CosmosPost>> GetAllAsync();
        Task<CosmosPost> GetByIDAsync(string id);
        Task<IEnumerable<CosmosPost>> GetByPhraseAsync(string phrase);
        Task<CosmosPost> AddAsync(CosmosPost post);
        Task UpdateAsync(CosmosPost post);
        Task DeleteAsync(CosmosPost post);
    }
}
