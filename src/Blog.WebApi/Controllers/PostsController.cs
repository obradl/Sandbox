using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.ApplicationCore.Common.Dto;
using Blog.ApplicationCore.Features.Post.Commands.CreatePost;
using Blog.ApplicationCore.Features.Post.Commands.DeletePost;
using Blog.ApplicationCore.Features.Post.Commands.EditPost;
using Blog.ApplicationCore.Features.Post.Commands.PublishPost;
using Blog.ApplicationCore.Features.Post.Commands.UnPublishPost;
using Blog.ApplicationCore.Features.Post.Queries.GetPosts;
using Blog.ApplicationCore.Features.Post.Queries.GetSinglePost;
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
        public async Task<ActionResult<List<PostDto>>> Get([FromQuery]bool published = false)
        {
            var posts = await _mediator.Send(new GetPostsQuery { PublishedOnly = published });
            return Ok(posts);
        }

        /// <summary>
        /// Get a single post by id
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
        /// Make the post unavailable for public
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        [HttpPut("{id}/unpublish")]
        public async Task<ActionResult<PostDto>> UnPublish([FromRoute]string id)
        {
            var publishedPost = await _mediator.Send(new UnPublishPostCommand { PostId = id });
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
        /// Update an existing post
        /// </summary>
        /// <param name="post">Post</param>
        /// <param name="id">Post id</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<PostDto>> Put([FromBody]EditPostDto post, [FromRoute]string id)
        {
            var updatedPost = await _mediator.Send(new EditPostCommand { Post = post, PostId = id});
            return Ok(updatedPost);
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
