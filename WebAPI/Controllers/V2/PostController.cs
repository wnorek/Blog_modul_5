using Application.DTO;
using Application.DTO.Cosmos;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.V2
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly ICosmosPostService _postService;
        public PostController(ICosmosPostService postService)
        {
            _postService = postService;
        }

        [SwaggerOperation(Summary ="Retrieves all posts")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [SwaggerOperation(Summary = "Retrieves post")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            
            if(post==null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [SwaggerOperation(Summary = "Create new post")]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCosmosPostDto newPost)
        {
            var post = await _postService.AddNewPostAsync(newPost);
            return Created($"api/posts/{post.Id}", post);
        }

        [SwaggerOperation(Summary = "Update post")]
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UpdateCosmosPostDto updatePost)
        {
            await _postService.UpdatePostAsync(updatePost);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete post")]
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
    }
}
