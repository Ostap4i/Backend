
namespace DENMAP_SERVER.Entity.dto
{
    internal class PostDTO
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
        public List<CommentDTO> Comments { get; set; }
        public DateTime CreatedAt { get; set; }
        public Genre Genre { get; set; }

        public PostDTO(int id, User user, string title, string image, string content, double rating, List<CommentDTO> comments, DateTime createdAt)
        {
            Id = id;
            User = user;
            Title = title;
            Image = image;
            Content = content;
            Rating = rating;
            Comments = comments;
            CreatedAt = createdAt;
        }

        public PostDTO(Post post, User user, List<CommentDTO> comments, Genre genre)
        {
            Id = post.Id;
            User = user;
            Title = post.Title;
            Image = post.Image;
            Content = post.Content;
            Rating = post.Rating;
            Comments = comments;
            CreatedAt = post.CreatedAt;
            Genre = genre;
        }
        public PostDTO(Post post, User user, Genre genre)
        {
            Id = post.Id;
            User = user;
            Title = post.Title;
            Image = post.Image;
            Content = post.Content;
            Rating = post.Rating;
            Comments = new List<CommentDTO>();
            CreatedAt = post.CreatedAt;
            Genre = genre;
        }
    }
}
