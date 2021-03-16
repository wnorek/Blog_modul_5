using Application.DTO.Cosmos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities.Cosmos;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CosmosPostService : ICosmosPostService
    {
        private readonly ICosmosRepository _postRepository;
        private readonly IMapper _mapper;

        public CosmosPostService(ICosmosRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<CosmosPostDto> AddNewPostAsync(CreateCosmosPostDto newPost)
        {
            if (string.IsNullOrEmpty(newPost.Title))
            {
                throw new Exception("Post can not have empty title.");
            }

            var post = _mapper.Map<CosmosPost>(newPost);
            var result = await _postRepository.AddAsync(post);
            return _mapper.Map<CosmosPostDto>(result);
        }

        public async Task DeletePostAsync(string id)
        {
            var post = await _postRepository.GetByIDAsync(id);
            await _postRepository.DeleteAsync(post);
        }

        public async Task<IEnumerable<CosmosPostDto>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CosmosPostDto>>(posts);
        }

        public async Task<CosmosPostDto> GetPostByIdAsync(string id)
        {
            var post = await _postRepository.GetByIDAsync(id);
            return _mapper.Map<CosmosPostDto>(post);
        }

        public async Task<IEnumerable<CosmosPostDto>> GetPostsByPhraseAsync(string phrase)
        {
            IEnumerable<CosmosPost> posts;

            if (string.IsNullOrEmpty(phrase) || string.IsNullOrWhiteSpace(phrase))
            {
                posts = await _postRepository.GetAllAsync();
            }
            else
            {
                posts = await _postRepository.GetByPhraseAsync(phrase);
            }

            return _mapper.Map<IEnumerable<CosmosPostDto>>(posts);
        }

        public async Task UpdatePostAsync(UpdateCosmosPostDto updatePost)
        {
            var existingPost = await _postRepository.GetByIDAsync(updatePost.Id);
            var post = _mapper.Map(updatePost, existingPost);
            await _postRepository.UpdateAsync(post);
        }

    }
}
