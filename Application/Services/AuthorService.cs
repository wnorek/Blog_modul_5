using Application.DTO;
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
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;


        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        public async Task<AuthorDto> AddNewAuthorAsync(CreateAuthorDto newAuthor)
        {
            if (string.IsNullOrEmpty(newAuthor.Email))
            {
                throw new Exception("Author can not have empty email.");
            }

            var author = _mapper.Map<Author>(newAuthor);
            var result = await _authorRepository.AddAsync(author);
            return _mapper.Map<AuthorDto>(result);
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            await _authorRepository.DeleteAsync(author);
        }

        public IQueryable<AuthorDto> GetAllAuthors()
        {
            var author = _authorRepository.GetAllAuthors();
            return _mapper.ProjectTo<AuthorDto>(author);
        }

        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var author = await _authorRepository.GetAuthorByIdAsync(id);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task UpdateAuthorAsync(UpdateAuthorDto updateAuthor)
        {
            var existingPost = await _authorRepository.GetAuthorByIdAsync(updateAuthor.Id);
            var post = _mapper.Map(updateAuthor, existingPost);
            await _authorRepository.UpdateAsync(post);
        }
    }
}
