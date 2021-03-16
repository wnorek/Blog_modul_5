using Application.DTO;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IPostService
    {
        IQueryable<PostDto> GetAllPosts();
        Task<IEnumerable<PostDto>> GetAllPostsAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy);
        Task<int> GetAllCountAsync(string filterBy);
        Task<PostDto> GetPostByIdAsync(int id);
        Task<IEnumerable<PostDto>> GetPostsByPhraseAsync(string phrase);

        Task<PostDto> AddNewPostAsync(CreatePostDto newPost, string userId);
        Task UpdatePostAsync(UpdatePostDto updatePost);
        Task DeletePostAsync(int id);

        Task<bool> UserOwnsPostAsync(int postId, string userId);
    }
}
