using LR7.Models;
using LR7.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LR7.Controllers
{
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("posts/")]
        public async Task<IActionResult> Get()
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(posts);
        }

        [HttpGet("posts/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostModel post)
        {
            var createdPost = await _postService.CreatePostAsync(post);
            return CreatedAtAction(nameof(Get), new { id = createdPost.Id }, createdPost);
        }

        [HttpPut("posts/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PostModel post)
        {
            var updatedPost = await _postService.UpdatePostAsync(id, post);
            if (updatedPost == null)
                return NotFound();
            return Ok(updatedPost);
        }

        [HttpDelete("posts/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postService.DeletePostAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
