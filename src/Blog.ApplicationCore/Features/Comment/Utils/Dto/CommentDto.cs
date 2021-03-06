﻿using System;

namespace Blog.ApplicationCore.Features.Comment.Utils.Dto
{
    public class CommentDto
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
        public DateTime DateCreated { get; set; }
    }
}