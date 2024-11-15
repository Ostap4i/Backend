
namespace DENMAP_SERVER.Entity
{
    internal class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string Content { get; set; }
        public double Rating { get; set; }
        public DateTime CreatedAt { get; set; }
        public int GenreId { get; set; }

        public Post(int id, int user, string title, string image, string content, double rating, DateTime createdAt, int genreId)
        {
            Id = id;
            UserId = user;
            Title = title;
            Image = image;
            Content = content;
            Rating = rating;
            CreatedAt = createdAt;
            GenreId = genreId;
        }
    }
}
