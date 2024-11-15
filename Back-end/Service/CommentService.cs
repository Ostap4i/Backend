using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Repository;
using MySql.Data.MySqlClient;

namespace DENMAP_SERVER.Service
{
    internal class CommentService
    {
        string connectionString = "Server=34.116.253.154;Port=3306;Database=chat_database;Uid=app_user;Pwd=&X9fT#7vYqZ$4LpR;";

        private CommentRepository commentRepository = new CommentRepository();

        public int AddComment(int userId, double rating, string message, int postId)
        {
            int id = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    id = commentRepository.addComment(connection, userId, rating, message, postId, DateTime.Now);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
            return id;
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            List<Comment> comments = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    comments = commentRepository.getCommentsByPostID(connection, postId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
            return comments;
        }

        public List<Comment> GetCommentsByUserId(int userId)
        {
            List<Comment> comments = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    comments = commentRepository.getCommentsByUserID(connection, userId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
            return comments;
        }

        public void DeleteComment(int id)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    commentRepository.deleteComment(connection, id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
        }

        public Comment GetCommentById(int id)
        {
            Comment comment = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    comment = commentRepository.getCommentById(connection, id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
            return comment;
        }

        public void DeleteCommentsByPostId(int postId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    commentRepository.deleteCommentsByPostId(connection, postId);
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
