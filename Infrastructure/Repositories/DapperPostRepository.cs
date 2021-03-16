using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Repositories
{
    public class DapperPostRepository : IPostRepository
    {
        private readonly BloggerContext _context;
        private readonly string ConnectionString;
        public DapperPostRepository(BloggerContext context)
        {
            _context = context;
            ConnectionString = _context.Database.GetConnectionString();
        }

        public async Task<Post> AddAsync(Post post)
        {
            post.Created = DateTime.Now;
            post.CreatedBy = _context.GetUser();

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = await connection.ExecuteAsync("dbo.AddPost @Title, @Content, @Created, @UserId",
                                                            new { post.Title, post.Content, post.Created, post.UserId });
                
                return post;
            }
        }

        public async Task DeleteAsync(Post post) 
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = await connection.ExecuteAsync("dbo.DeletePost @id", new { post.ID });
                await Task.CompletedTask;
            }
        }

        public IQueryable<Post> GetAll()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.Query<Post>("dbo.GetAll");
                return result.AsQueryable();
            }
        }

        public Task<IEnumerable<Post>> GetAllAsync(int pageNumber, int pageSize, string sortField, bool ascending, string filterBy)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetAllCountAsync(string filterBy)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.ExecuteAsync("dbo.GetPostByPhrase @filter", new { filter= "%" + filterBy + "%" });
                return await result;
            }
        }

        public async Task<Post> GetByIDAsync(int id)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Post>("dbo.GetPostById @id", new { id });
                return result;
            }
        }

        public async Task<IEnumerable<Post>> GetByPhraseAsync(string phrase)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = connection.QueryAsync<Post>("dbo.GetPostByPhrase @phrase_db", new { phrase_db = "%"+phrase+"%" });
                return await result;
            }
        }

        public async Task UpdateAsync(Post post)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                var result = await connection.ExecuteAsync("dbo.UpdatePost @Content, @id", new {post.Content, post.ID });
                await Task.CompletedTask;
            }
        }
    }
}
