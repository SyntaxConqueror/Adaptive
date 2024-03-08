using LR7.Models;
using LR7.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LR7.Controllers
{
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("comments/")]
        public async Task<IActionResult> Get()
        {
            var comments = await _commentService.GetCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("comments/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound();
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentModel comment)
        {
            var createdComment = await _commentService.CreateCommentAsync(comment);
            return CreatedAtAction(nameof(Get), new { id = createdComment.Id }, createdComment);
        }

        [HttpPut("comments/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CommentModel comment)
        {
            var updatedComment = await _commentService.UpdateCommentAsync(id, comment);
            if (updatedComment == null)
                return NotFound();
            return Ok(updatedComment);
        }

        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _commentService.DeleteCommentAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
