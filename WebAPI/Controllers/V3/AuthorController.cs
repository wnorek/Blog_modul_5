using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V3
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiVersion("3.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;   
        }

        [SwaggerOperation(Summary = "Retrieves all authors")]
        [HttpGet]
        public IActionResult Get()
        {
            var authors = _authorService.GetAllAuthors();
            return Ok(authors);
        }

        [SwaggerOperation(Summary = "Retrieves author")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return Ok(new Response<AuthorDto>(author));
        }

        [SwaggerOperation(Summary = "Create new author")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAuthorDto newAuthor)
        {
            var author = await _authorService.AddNewAuthorAsync(newAuthor);
            return Created($"api/author/{author.Id}", new Response<AuthorDto>(author));
        }

        [SwaggerOperation(Summary = "Update author")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateAuthorDto updateAuthor)
        {
            await _authorService.UpdateAuthorAsync(updateAuthor);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete author")]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAuthorAsync(id);
            return NoContent();
        }
    }
}
