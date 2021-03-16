﻿using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDto> AddNewPostAsync(CreatePostDto newPost, string userId)
        {
            if(string.IsNullOrEmpty(newPost.Title))
            {
                throw new Exception("Post can not have empty title.");
            }

            var post = _mapper.Map<Post>(newPost);
            post.UserId = userId;
            var result = await _postRepository.AddAsync(post);
            return _mapper.Map<PostDto>(result);
        }

        public async Task DeletePostAsync(int id)
        {
            var post = await _postRepository.GetByIDAsync(id);
            await _postRepository.DeleteAsync(post);
        }

        public async Task<int> GetAllCountAsync(string filterBy)
        {
            return await _postRepository.GetAllCountAsync(filterBy);
        }

        public IQueryable<PostDto> GetAllPosts()
        {
            var posts = _postRepository.GetAll();
            return _mapper.ProjectTo<PostDto>(posts);
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy)
        {
            var posts = await _postRepository.GetAllAsync(pageNumber, pageSize, sortField, ascending, filterBy);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetByIDAsync(id);
            return  _mapper.Map<PostDto>(post);
        }

        public async Task<IEnumerable<PostDto>> GetPostsByPhraseAsync(string phrase)
        {
            IEnumerable<Post> posts;

            if (string.IsNullOrEmpty(phrase) || string.IsNullOrWhiteSpace(phrase))
            {
                posts =await  _postRepository.GetAllAsync(1,5, "Id", true, "");
            }
            else
            {
                posts = await _postRepository.GetByPhraseAsync(phrase);
            }

            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task UpdatePostAsync(UpdatePostDto updatePost)
        {
            var existingPost = await _postRepository.GetByIDAsync(updatePost.Id);
            var post = _mapper.Map(updatePost, existingPost);
            await _postRepository.UpdateAsync(post);
        }

        public async Task<bool> UserOwnsPostAsync(int postId, string userId)
        {
            var post = await _postRepository.GetByIDAsync(postId);

            if(post ==null)
            {
                return false;
            }

            if(post.UserId !=userId)
            {
                return false;
            }

            return true;
        }
    }
}
