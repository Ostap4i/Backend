using System;
using System.Collections.Generic;
using System.Linq;

namespace DENMAP_SERVER.Entity
{
    internal class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Rating { get; set; }
        public string Message { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; }

        public Comment(int id, int userId, double rating, string message, int postId, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Rating = rating;
            Message = message;
            PostId = postId;
            CreatedAt = createdAt;
        }
    }
}
