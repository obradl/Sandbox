using System;
using System.Threading;
using System.Threading.Tasks;
using Blog.ApplicationCore.Features.Commands.Post.PublishPost;
using Blog.Domain.Entities;
using Blog.Infrastructure.Data;
using Moq;
using Xunit;

namespace ApplicationCore.Tests
{
    public class PublishPostCommandHandlerTests : IClassFixture<TestFixture>
    {
        private readonly TestFixture _fixture;

        public PublishPostCommandHandlerTests(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task PublishPostReturnsPublishedTrue()
        {
            var post = new Post()
            {
                Id = "5b9910a06f59e8fc4c6b632a",
                Published = false,
                DatePublished = null,
                DateCreated = DateTime.Now
            };

            var repoMock = new Mock<IPostRepository>();
            repoMock.Setup(d => d.Get(It.IsAny<string>())).Returns(Task.FromResult(post));

            var handler = new PublishPostCommandHandler(repoMock.Object, _fixture.Mapper);
            var result = await handler.Handle(new PublishPostCommand()
            {
                PostId = "5b9910a06f59e8fc4c6b632a"
            },
                CancellationToken.None);

            Assert.True(result.Published);
        }

    }
}
