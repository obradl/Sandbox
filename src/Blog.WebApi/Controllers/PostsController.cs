using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Features.Commands.Post.CreatePost;
using Blog.ApplicationCore.Features.Commands.Post.DeletePost;
using Blog.ApplicationCore.Features.Commands.Post.PublishPost;
using Blog.ApplicationCore.Features.Queries.Post.GetPosts;
using Blog.ApplicationCore.Features.Queries.Post.GetSinglePost;
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
        /// Get posts
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> Get()
        {
            var posts = await _mediator.Send(new GetPostsQuery());
            return Ok(posts);
        }

        /// <summary>
        /// Get a single post
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSingle")]
        public async Task<ActionResult<PostDto>> GetSingle([FromRoute]string id)
        {
            var posts = await _mediator.Send(new GetSinglePostQuery {PostId = id });
            return Ok(posts);
        }

        /// <summary>
        /// Make the post available for public
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        [HttpPut("{id}/publish")]
        public async Task<ActionResult<PostDto>> Publish([FromRoute]string id)
        {
            var publishedPost = await _mediator.Send(new PublishPostCommand {PostId = id });
            return Ok(publishedPost);
        }

        /// <summary>
        /// Create a new post
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PostDto>> Post([FromBody]CreatePostDto post)
        {
            var createdPost = await _mediator.Send(new CreatePostCommand {Post = post});
            return CreatedAtRoute("GetSingle", new {createdPost.Id } ,createdPost);
        }

       /// <summary>
       /// Delete a post
       /// </summary>
       /// <param name="id">Post id</param>
       /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute]string id)
        {
            await _mediator.Send(new DeletePostCommand {PostId = id});
            return Ok();
        }

    }
}
