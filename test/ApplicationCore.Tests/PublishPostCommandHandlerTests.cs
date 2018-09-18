//using System.Threading;
//using System.Threading.Tasks;
//using Blog.ApplicationCore.Common;
//using Blog.ApplicationCore.Features.Commands.PublishPost;
//using Blog.Domain.Entities;
//using Blog.Infrastructure.Data;
//using Moq;
//using Xunit;

//namespace ApplicationCore.Tests
//{
//    public class PublishPostCommandHandlerTests
//    {

//        [Fact(DisplayName ="IntegrationTest")]
//        public async Task PublishPostReturnsPublishedTrue()
//        {
//            var post = new Post()
//            {
//                PostId = "5b9910a06f59e8fc4c6b632a",
//                Published = false,
//                DatePublished = null
//            };
            
//            var repoMock = new Mock<IPostRepository>();
//            repoMock.Setup(d => d.Get(It.IsAny<string>())).Returns(Task.FromResult(post));

//            var handler = new PublishPostCommandHandler(repoMock.Object);
//            var result = await handler.Handle(new PublishPostCommand()
//            {
//               PostId = "5b9910a06f59e8fc4c6b632a"
//            },
//                CancellationToken.None);

//            Assert.True(result.Published);
//        }

//    }
//}
