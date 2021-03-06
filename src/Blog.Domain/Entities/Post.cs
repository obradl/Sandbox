﻿using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Blog.Domain.Entities
{
    public class Post
    {
        public Post(string title, string author, string body, string lead)
        {
            Title = title;
            Author = author;
            Body = body;
            Lead = lead;
            DateCreated = DateTime.Now;
            SetUpdatedTime();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Body { get; private set; }
        public string Lead { get; private set; }
        public bool Published { get; private set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
        public DateTime DateCreated { get; private set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
        public DateTime? DatePublished { get; private set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = BsonType.DateTime)]
        public DateTime Updated { get; private set; }

        [BsonIgnore] public List<PostRating> Ratings { get; set; }

        public void Publish()
        {
            Published = true;
            DatePublished = DateTime.Now;
            SetUpdatedTime();
        }

        public void UnPublish()
        {
            Published = false;
            DatePublished = null;
            SetUpdatedTime();
        }

        public int? CalculateAverageRating()
        {
            if (Ratings != null && Ratings.Any()) return Ratings.Sum(d => d.Rating) / Ratings.Count();

            return null;
        }

        public void SetLead(string lead)
        {
            Lead = lead;
        }

        public void SetBody(string body)
        {
            Body = body;
        }

        public void SetAuthor(string author)
        {
            Author = author;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        private void SetUpdatedTime()
        {
            Updated = DateTime.Now;
        }

        public PostRating AddRating(int rating)
        {
            if (Ratings == null)
            {
                Ratings = new List<PostRating>();
            }

            var postRating = new PostRating(Id, rating);
            Ratings.Add(postRating);

            return postRating;
        }
    }
}