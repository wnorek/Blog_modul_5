using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IConfiguration _configuration;

        public AuthorRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Author> AddAsync(Author newAuthor)
        {
            var sql = string.Format("INSERT INTO Authors (Name, Surname, Email) VALUES ('{0}', '{1}', '{2}')",newAuthor.Name,newAuthor.Surname,newAuthor.Email);

            using(var connection = new SqlConnection(_configuration.GetConnectionString("BloggerCS")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql);
                return newAuthor;
            }
        }

        public async Task DeleteAsync(Author author)
        {
            var sql = string.Format("DELETE FROM Authors WHERE Id = '{0}'",author.Id);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("BloggerCS")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql);
                await Task.CompletedTask;
            }
        }

        public IQueryable<Author> GetAllAuthors()
        {
            var sql = "SELECT * FROM Authors";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("BloggerCS")))
            {
                connection.Open();
                var result = connection.Query<Author>(sql);
                return result.AsQueryable();
            }
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var sql = string.Format("SELECT * FROM Authors WHERE Id = '{0}'", id);

            using (var connection = new SqlConnection(_configuration.GetConnectionString("BloggerCS")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Author>(sql);
                return result;
            }
        }

        public async Task UpdateAsync(Author updateAuthor)
        {
                var sql = string.Format("UPDATE Authors SET Email='{0}'  WHERE Id = '{1}'",updateAuthor.Email, updateAuthor.Id);
                
                using (var connection = new SqlConnection(_configuration.GetConnectionString("BloggerCS")))
                {
                    connection.Open();
                    var result = await connection.ExecuteAsync(sql);
                    await Task.CompletedTask;
                }
        }
    }
}
