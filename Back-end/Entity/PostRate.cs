using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Entity
{
    internal class PostRate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public double Rating { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; }

        public PostRate(int id, int userId, double rating, int postId, DateTime createdAt)
        {
            Id = id;
            UserId = userId;
            Rating = rating;
            PostId = postId;
            CreatedAt = createdAt;
        }
    }
}
