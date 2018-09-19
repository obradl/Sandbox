﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Domain.Entities;

namespace Blog.Infrastructure.Data
{
    public interface IPostRepository
    {
        Task<Post> Get(string id);
        Task Update(Post post);
        Task Insert(Post post);
        Task<IEnumerable<Post>> GetAll(bool published);
        Task Delete(string id);
        Task AddRating(string id, int rating);
    }
}
