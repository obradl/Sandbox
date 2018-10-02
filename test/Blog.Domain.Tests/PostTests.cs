using Blog.Domain.Entities;
using Xunit;

namespace Blog.Domain.Tests
{
    public class PostTests
    {
        [Fact]
        public void PublishPostReturnPublishedTrue()
        {
            var post = new Post("title", "oystein", "text text", "lead");
            post.Publish();

            Assert.True(post.Published);
            Assert.NotNull(post.DatePublished);
        }

        [Fact]
        public void UnPublishPostReturnPublishedFalse()
        {
            var post = new Post("title", "oystein", "text text", "lead");
            post.UnPublish();

            Assert.False(post.Published);
            Assert.Null(post.DatePublished);
        }

        [Fact]
        public void CalculateRatingReturnsNull()
        {
            var post = new Post("title", "ob", "Full text", "Lead");
            var rating = post.CalculateAverageRating();

            Assert.Null(rating);
        }

        [Fact]
        public void AddRatingThenCalculateRatingReturns1()
        {
            var post = new Post("title", "ob", "Full text", "Lead");
            post.AddRating(1);

            var rating = post.CalculateAverageRating();

            Assert.Equal(1, rating);
        }
    }
}