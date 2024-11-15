using DENMAP_SERVER.Entity;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DENMAP_SERVER.Repository
{
    internal class CommentRepository
    {
        public int addComment(DbConnection connection, int userId, double rating, string message, int postId, DateTime createdAt)
        {
            string query = $"INSERT INTO comments (post_id, user_id, rating, message, created_at) " +
                $"VALUES ( @postId, @userId, @rating, @message, @createdAt); SELECT LAST_INSERT_ID();";
            int id = 0;
            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@rating", rating);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@postId", postId);
                cmd.Parameters.AddWithValue("@createdAt", createdAt);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return id;
        }


        public List<Comment> getCommentsByUserID(DbConnection connection, int userId)
        {
            List<Comment> comments = new List<Comment>();

            string query = $"SELECT * FROM comments WHERE user_id = {userId}";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Comment comment = new Comment(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToString(reader["message"]),
                            Convert.ToInt32(reader["post_id"]),
                            Convert.ToDateTime(reader["created_at"])
                        );
                        comments.Add(comment);
                    }
                }
            }

            return comments;
        }

        public List<Comment> getCommentsByPostID(DbConnection connection, int postId)
        {
            List<Comment> comments = new List<Comment>();

            string query = $"SELECT * FROM comments WHERE post_id = {postId}";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Comment comment = new Comment(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToString(reader["message"]),
                            Convert.ToInt32(reader["post_id"]),
                            Convert.ToDateTime(reader["created_at"])
                        );
                        comments.Add(comment);
                    }
                }
            }

            return comments;
        }

        public void deleteComment(DbConnection connection, int id)
        {
            string query = $"DELETE FROM comments WHERE id = {id}";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public Comment getCommentById(DbConnection connection, int id)
        {
            string query = $"SELECT * FROM comments WHERE id = {id}";

            Comment comment = null;
            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        comment = new Comment(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToString(reader["message"]),
                            Convert.ToInt32(reader["post_id"]),
                            Convert.ToDateTime(reader["created_at"])
                        );
                    }
                }
            }
            return comment;
        }

        public void deleteCommentsByPostId(DbConnection connection, int postId)
        {
            string query = $"DELETE FROM comments WHERE post_id = {postId}";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
