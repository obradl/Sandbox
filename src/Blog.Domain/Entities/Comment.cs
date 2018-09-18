using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.Domain.Entities
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; }

        public string Author { get; set; }
        public string Body { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
        public DateTime DateCreated { get; set; }
    }
}