using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Repository;
using MySql.Data.MySqlClient;

namespace DENMAP_SERVER.Service
{
    internal class PostService
    {
        string connectionString = "Server=34.116.253.154;Port=3306;Database=chat_database;Uid=app_user;Pwd=&X9fT#7vYqZ$4LpR;";

        private PostRepository postRepository = new PostRepository();
        private CommentRepository commentRepository = new CommentRepository();
        private PostRatesRepository postRatesRepository = new PostRatesRepository();

        public int AddPost(int userId, string title, string image, string content, int genreId)
        {
            int id = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    id = postRepository.addPost(connection, userId, title, image, content, 0, DateTime.Now, genreId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
            return id;
        }

        public Post GetPostById(int id)
        {
            Post post = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    post = postRepository.getPostByID(connection, id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            if (post == null)
            {
                throw new Exception("Post with id: " + id + " not found");
            }

            return post;
        }

        public List<Post> GetPostsByUserId(int userId)
        {
            List<Post> posts = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    posts = postRepository.getPostsByUserID(connection, userId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            return posts;
        }

        public List<Post> GetAllPosts()
        {
            List<Post> posts = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    posts = postRepository.getAllPosts(connection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            return posts;
        }

        public int UpdatePost(int id, string title, string image, string content, int genreId)
        {
            int result = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    result = postRepository.updatePost(connection, id, title, image, content, genreId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            return result;
        }

        public int UpdatePostRating(int id, double rating)
        {
            int result = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    result = postRepository.updatePostRating(connection, id, rating);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            return result;
        }

        public double GetPostRating(int id)
        {
            double rating = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    rating = postRepository.getPostRating(connection, id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            return rating;
        }

        public void ReCalculatePostRating(int id)
        {
            double rating = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    List<PostRate> postRates = postRatesRepository.getPostRatesByPostID(connection, id);
                    foreach (PostRate postRate in postRates)
                    {
                        rating += postRate.Rating;
                    }
                    rating /= postRates.Count;

                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            UpdatePostRating(id, rating); ;
        }

        public List<Post> GetPostsByGenreId(int genreId)
        {
            List<Post> posts = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    posts = postRepository.getPostsByGenreID(connection, genreId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            return posts;
        }

        public void DeletePost(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    postRepository.deletePost(connection, id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
        }
    }
}
