using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.ApplicationCore.Features.Post.CreatePost;
using Blog.ApplicationCore.Features.Post.EditPost;
using Blog.ApplicationCore.Features.Post.GetPosts;
using Blog.ApplicationCore.Features.Post.GetSinglePost;
using Blog.ApplicationCore.Features.Post.PublishFuturePost;
using Blog.ApplicationCore.Features.Post.PublishPost;
using Blog.ApplicationCore.Features.Post.RatePost;
using Blog.ApplicationCore.Features.Post.UnPublishPost;
using Blog.ApplicationCore.Features.Post.Utils.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Blog.WebApi.Controllers
{
    [ApiController]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PostDto>> GetSingle([FromRoute] string id)
        {
            var posts = await _mediator.Send(new DeletePostCommand(id));
            return Ok(posts);
        }

        /// <summary>
        ///     Make the post available for public
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        [HttpPut("{id}/publish")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PostDto>> Put([FromBody] EditPostDto post, [FromRoute] string id)
        {
            var updatedPost = await _mediator.Send(new EditPostCommand(id, post));
            return Ok(updatedPost);
        }

        /// <summary>
        ///     Publish a post in future
        /// </summary>
        /// <param name="id">Post id</param>
        /// <param name="publishDate">Publish date</param>
        /// <returns></returns>
        [HttpPut("{id}/publishfuture")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<PostDto>> PutFuturePublish([FromRoute] string id, [FromBody]DateTime publishDate)
        {
           await _mediator.Send(new PublishFuturePostCommand(id, publishDate));
           return Ok();
        }

        /// <summary>
        ///     Delete a post
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            await _mediator.Send(new ApplicationCore.Features.Post.DeletePost.DeletePostCommand(id));
            return Ok();
        }
    }
}