
namespace DENMAP_SERVER.Entity.dto
{
    internal class CommentDTO
    {
        public int Id { get; set; }
        public User User { get; set; }
        public double Rating { get; set; }
        public string Message { get; set; }
        public int PostId { get; set; }
        public DateTime CreatedAt { get; set; }

        public CommentDTO(int id, User user, double rating, string message, int postId, DateTime createdAt)
        {
            Id = id;
            User = user;
            Rating = rating;
            Message = message;
            PostId = postId;
            CreatedAt = createdAt;
        }

        public CommentDTO(Comment comment, User user)
        {
            Id = comment.Id;
            User = user;
            Rating = comment.Rating;
            Message = comment.Message;
            PostId = comment.PostId;
            CreatedAt = comment.CreatedAt;
        }
    }
}
