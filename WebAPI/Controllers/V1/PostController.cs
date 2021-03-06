using Application.DTO;
using Application.Interfaces;
using Application.Validators;
using Infrastructure.Identity;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Attributes;
using WebAPI.Filters;
using WebAPI.Helpers;
using WebAPI.Wrappers;

namespace WebAPI.Controllers.V1
{   
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [SwaggerOperation(Summary = "Retrieves sort fields")]
        [HttpGet("[action]")]
        public IActionResult GetSortFields()
        {
            return Ok(SortingHelper.GetSortField().Select(x => x.Key));
        }

        [SwaggerOperation(Summary ="Retrieves paged posts")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationFilter paginationFilter, [FromQuery] SortingFilter sortingFilter,
                                              [FromQuery] string filteredBy="")
        {
            var validPaginationFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
            var validSortingFilter = new SortingFilter(sortingFilter.SortField, sortingFilter.Ascending);

            var posts = await _postService.GetAllPostsAsync(validPaginationFilter.PageNumber, validPaginationFilter.PageSize,
                                                            validSortingFilter.SortField, validSortingFilter.Ascending,
                                                            filteredBy);

            var totalRecords = await _postService.GetAllCountAsync(filteredBy);

            return Ok(PaginationHelper.CreatePagedResponse(posts,validPaginationFilter, totalRecords));
        }

        [SwaggerOperation(Summary ="Retrieves all posts")]
        [Authorize(Roles = UserRoles.Admin)]        
        [EnableQuery]
        [HttpGet("[action]")]
        public IQueryable<PostDto> GetAll()
        {
            return _postService.GetAllPosts();
        }

        [SwaggerOperation(Summary = "Retrieves post")]
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            
            if(post==null)
            {
                return NotFound();
            }

            return Ok(new Response<PostDto>(post));
        }

        [SwaggerOperation(Summary = "Retrieve posts by phrase")]
        [HttpGet("Search/{phrase}")]
        public async Task<IActionResult> Get(string phrase)
        {
            var posts = await _postService.GetPostsByPhraseAsync(phrase);

            return Ok(posts);
        }

        [ValidateFilter]
        [SwaggerOperation(Summary = "Create new post")]
        [Authorize(Roles = UserRoles.User)]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePostDto newPost)
        {          
            var post = await _postService.AddNewPostAsync(newPost, User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Created($"api/posts/{post.Id}", new Response<PostDto>(post));
        }

        [SwaggerOperation(Summary = "Update post")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdatePostDto updatePost)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(updatePost.Id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            if(!userOwnsPost)
            {
                return BadRequest(new Response(false, "You do not own this post"));
            }
            await _postService.UpdatePostAsync(updatePost);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Delete post")]
        [Authorize(Roles = UserRoles.AdminOrUser)]
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var userOwnsPost = await _postService.UserOwnsPostAsync(id, User.FindFirstValue(ClaimTypes.NameIdentifier));

            var isAdmin = User.IsInRole(UserRoles.Admin);

            if (!isAdmin && !userOwnsPost)
            {
                return BadRequest(new Response(false, "You do not own this post"));
            }

            await _postService.DeletePostAsync(id);
            return NoContent();
        }
    }
}
