using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.Domain.Entities
{
    public class PostRating
    {
        public PostRating(string postId, int rating)
        {
            Rating = rating;
            PostId = postId;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; }

        public int Rating { get; private set; }
    }
}