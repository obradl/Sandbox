using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.Domain.Entities
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public string Lead { get; set; }
        public bool Published { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
        public DateTime DateCreated { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
        public DateTime? DatePublished { get; set; }
    }
}
