
namespace DENMAP_SERVER.Entity.dto
{
    internal class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Image { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Post> Posts { get; set; }


        public UserDTO(int id, string name, string password, string image, double rating, string description, List<Comment> comments, List<Post> posts)
        {
            Id = id;
            Name = name;
            Password = password;
            Image = image;
            Rating = rating;
            Description = description;
            Comments = comments;
            Posts = posts;
        }

        public UserDTO(User user, List<Comment> comments, List<Post> posts)
        {
            Id = user.Id;
            Name = user.Name;
            Password = user.Password;
            Image = user.Image;
            Rating = user.Rating;
            Description = user.Description;
            Comments = comments;
            Posts = posts;
        }
    }
}
