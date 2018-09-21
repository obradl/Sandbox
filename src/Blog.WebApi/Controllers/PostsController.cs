using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Features.Post.CreatePost;
using Blog.ApplicationCore.Features.Post.DeletePost;
using Blog.ApplicationCore.Features.Post.EditPost;
using Blog.ApplicationCore.Features.Post.GetPosts;
using Blog.ApplicationCore.Features.Post.GetSinglePost;
using Blog.ApplicationCore.Features.Post.PublishPost;
using Blog.ApplicationCore.Features.Post.RatePost;
using Blog.ApplicationCore.Features.Post.UnPublishPost;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Blog.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        ///     Get posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> Get([FromQuery] bool published = false)
        {
            var posts = await _mediator.Send(new GetPostsQuery(published));
            return Ok(posts);
        }

        /// <summary>
        ///     Get a single post by id
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSingle")]
        public async Task<ActionResult<PostDto>> GetSingle([FromRoute] string id)
        {
            var posts = await _mediator.Send(new GetSinglePostQuery(id));
            return Ok(posts);
        }

        /// <summary>
        ///     Make the post available for public
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        [HttpPut("{id}/publish")]
        public async Task<ActionResult<PostDto>> Publish([FromRoute] string id)
        {
            var publishedPost = await _mediator.Send(new PublishPostCommand(id));
            return Ok(publishedPost);
        }

        /// <summary>
        ///     Rate a post. Range: 1-5
        /// </summary>
        /// <param name="id">Post id</param>
        /// <param name="rating">Rating</param>
        /// <returns></returns>
        [HttpPut("{id}/rate")]
        public async Task<ActionResult> Rate([FromRoute] string id, [FromBody] int rating)
        {
            await _mediator.Send(new RatePostCommand(id, rating));
            return Ok();
        }

        /// <summary>
        ///     Make the post unavailable for public
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        [HttpPut("{id}/unpublish")]
        public async Task<ActionResult<PostDto>> UnPublish([FromRoute] string id)
        {
            var publishedPost = await _mediator.Send(new UnPublishPostCommand(id));
            return Ok(publishedPost);
        }

        /// <summary>
        ///     Create a new post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PostDto>> Post([FromBody] CreatePostDto post)
        {
            var createdPost = await _mediator.Send(new CreatePostCommand(post));
            return CreatedAtRoute("GetSingle", new {createdPost.Id}, createdPost);
        }

        /// <summary>
        ///     Update an existing post
        /// </summary>
        /// <param name="post">Post</param>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<PostDto>> Put([FromBody] EditPostDto post, [FromRoute] string id)
        {
            var updatedPost = await _mediator.Send(new EditPostCommand(id, post));
            return Ok(updatedPost);
        }

        /// <summary>
        ///     Delete a post
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            await _mediator.Send(new DeletePostCommand(id));
            return Ok();
        }
    }
}