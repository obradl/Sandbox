using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.Domain.Entities
{
    public class Comment
    {
        public Comment(string author, string body, string postId)
        {
            Author = author;
            Body = body;
            DateCreated = DateTime.Now;
            PostId = postId;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; }

        public string Author { get; }
        public string Body { get; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
        public DateTime DateCreated { get; }
    }
}