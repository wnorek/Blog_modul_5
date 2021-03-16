using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthorService
    {
        IQueryable<AuthorDto> GetAllAuthors();
        Task<AuthorDto> GetAuthorByIdAsync(int id);

        Task<AuthorDto> AddNewAuthorAsync(CreateAuthorDto newAuthor);
        Task UpdateAuthorAsync(UpdateAuthorDto updateAuthor);
        Task DeleteAuthorAsync(int id);
    }
}
