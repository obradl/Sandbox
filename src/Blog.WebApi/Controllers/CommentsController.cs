using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.ApplicationCore.Features.Comment.CreateComment;
using Blog.ApplicationCore.Features.Comment.DeleteComment;
using Blog.ApplicationCore.Features.Comment.GetCommentsForPost;
using Blog.ApplicationCore.Features.Comment.Utils.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CommentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Create a new comment associated with a post
        /// </summary>
        /// <param name="comment">Comment</param>
        /// <param name="postId">Post id</param>
        /// <returns></returns>
        [HttpPost("~/api/posts/{postId}/comments")]
        public async Task<ActionResult<CommentDto>> Post([FromBody] CreateCommentDto comment, [FromRoute] string postId)
        {
            var createdComment = await _mediator.Send(new CreateCommentCommand(postId, comment));
            return Ok(createdComment);
        }

        /// <summary>
        ///     Get comments for a given post
        /// </summary>
        /// <returns></returns>
        [HttpGet("~/api/posts/{postId}/comments")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsForPost([FromRoute] string postId)
        {
            var comments = await _mediator.Send(new GetCommentsForPostQuery(postId));
            return Ok(comments);
        }

        /// <summary>
        ///     Delete a comment
        /// </summary>
        /// <param name="id">Comment id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            await _mediator.Send(new DeleteCommentCommand(id));
            return Ok();
        }
    }
}