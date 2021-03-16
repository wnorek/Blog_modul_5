using Cosmonaut;
using Cosmonaut.Extensions;
using Domain.Entities.Cosmos;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CosmosPostRepository : ICosmosRepository
    {
        private readonly ICosmosStore<CosmosPost> _cosmosStore;
        public CosmosPostRepository(ICosmosStore<CosmosPost> cosmosStore)
        {
            _cosmosStore = cosmosStore;
        }

        public async Task<CosmosPost> AddAsync(CosmosPost post)
        {
            post.Id = Guid.NewGuid().ToString();
            return await _cosmosStore.AddAsync(post);
        }

        public async Task DeleteAsync(CosmosPost post)
        {
            await _cosmosStore.RemoveAsync(post);
        }

        public async Task<IEnumerable<CosmosPost>> GetAllAsync()
        {
            var posts = await _cosmosStore.Query().ToListAsync();
            return posts;
        }

        public async Task<CosmosPost> GetByIDAsync(string id)
        {
            return await _cosmosStore.FindAsync(id);
        }

        public async Task<IEnumerable<CosmosPost>> GetByPhraseAsync(string phrase)
        {
            var posts = _cosmosStore.Query().Where(x => x.Title.ToLower().Contains(phrase.Trim().ToLower())).ToListAsync();
            return await posts;
        }

        public async Task UpdateAsync(CosmosPost post)
        {
            await _cosmosStore.UpdateAsync(post);
        }
    }
}
