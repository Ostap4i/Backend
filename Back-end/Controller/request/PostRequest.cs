
namespace DENMAP_SERVER.Controller.request
{
    internal class PostRequest
    {
        public int userId { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string content { get; set; }
        public int genreId { get; set; }
    }
}
