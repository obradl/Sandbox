using System;

namespace Blog.ApplicationCore.Common.Dto
{
    public class PostDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public string Lead { get; set; }
        public bool Published { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DatePublished { get; set; }
        public int? Rating { get; set; }
    }
}