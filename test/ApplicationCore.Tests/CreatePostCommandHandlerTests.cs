//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Blog.ApplicationCore.Features.Commands.CreatePost;
//using Blog.Domain.Entities;
//using Blog.Infrastructure.Data;
//using Moq;
//using Xunit;

//namespace ApplicationCore.Tests
//{
//    public class CreatePostCommandHandlerTests
//    {

//        [Fact]
//        public async Task CreatePostReturnsNotNull()
//        {
//            var posts = new List<Post>();
//            var repoMock = new Mock<IPostRepository>();
//            repoMock.Setup(d => d.Insert(It.IsAny<Post>())).Returns(Task.CompletedTask).Callback((Post d) =>
//            {
//                d.PostId = "1";
//                posts.Add(d);
//            });

//            repoMock.Setup(d => d.Get(It.IsAny<string>())).Returns((string s) =>
//            {
//                return Task.FromResult(posts.First(d => d.PostId == s));
//            });

//            var handler = new CreatePostCommandHandler(repoMock.Object);
//            var result = await handler.Handle(new CreatePostCommand()
//            {
//                Post = new CreatePostDto()
//                {
//                    Author = "Mitt navn",
//                    Body = "asd sa d dsa dsa",
//                    Lead = " asddsa  dsa dsa",
//                    Title = "asd sads"
//                }
//            },
//                CancellationToken.None);

//            Assert.NotNull(result);
//        }


//    }
//}
