using DENMAP_SERVER.Entity;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DENMAP_SERVER.Repository
{
    internal class PostRepository
    {
        public int addPost(MySqlConnection connection, int userId, string title, string image, 
            string content, double rating, DateTime createdAt, int genreId)
        {
            string query = $"INSERT INTO posts (user_id, title, image, content, rating, created_at, genre_id) " +
                $"VALUES (@userId, @title, @image, @content, @rating, @createdAt, @genreId); SELECT LAST_INSERT_ID();";
            int id = 0;
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@rating", rating);
                cmd.Parameters.AddWithValue("@createdAt", createdAt);
                cmd.Parameters.AddWithValue("@genreId", genreId);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return id;
        }


        public List<Post> getPostsByUserID(DbConnection connection, int userId)
        {
            List<Post> posts = new List<Post>();
            string query = $"SELECT * " +
                           $"FROM posts " +
                           $"WHERE user_id = @userId";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Post post = new Post(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToString(reader["title"]),
                            Convert.ToString(reader["image"]),
                            Convert.ToString(reader["content"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToDateTime(reader["created_at"]),
                            Convert.ToInt32(reader["genre_id"])
                        );

                        posts.Add(post);
                    }
                }
            }
            Console.WriteLine(posts.Count);

            return posts;
        }

        public Post getPostByID(DbConnection connection, int Id)
        {
            Post post = null;
            string query = $"SELECT * " +
                           $"FROM posts " +
                           $"WHERE id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@Id", Id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        post = new Post(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToString(reader["title"]),
                            Convert.ToString(reader["image"]),
                            Convert.ToString(reader["content"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToDateTime(reader["created_at"]),
                            Convert.ToInt32(reader["genre_id"])
                        );
                        Console.WriteLine("id:" + Convert.ToInt32(reader["id"]) +
                            " user_id: " + Convert.ToInt32(reader["user_id"]) +
                            " title: " + Convert.ToString(reader["title"]) +
                            " image: " + Convert.ToString(reader["image"]) +
                            " content: " + Convert.ToString(reader["content"]) +
                            " rating: " + Convert.ToDouble(reader["rating"]) +
                            " created_at: " + Convert.ToDateTime(reader["created_at"]));
                        Console.WriteLine("id:" + post.Id + " user_id: " + post.UserId + " title: " 
                            + post.Title + " image: " + post.Image + " content: " + post.Content + " rating: " 
                            + post.Rating + " created_at: " + post.CreatedAt);
                    }
                }
            }
            return post;
        }

        public List<Post> getAllPosts(DbConnection connection)
        {
            List<Post> posts = new List<Post>();
            string query = $"SELECT * " +
                           $"FROM posts ";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        Post post = new Post(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToString(reader["title"]),
                            Convert.ToString(reader["image"]),
                            Convert.ToString(reader["content"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToDateTime(reader["created_at"]),
                            Convert.ToInt32(reader["genre_id"])
                        );

                        posts.Add(post);
                    }
                }
            }
            return posts;
        }

        public int updatePost(DbConnection connection, int id, string title, string image, 
            string content, int genreId)
        {
            string query = $"UPDATE posts " +
                           $"SET title = @title, " +
                               $"image = @image, " +
                               $"content = @content, " +
                               $"genre_id = @genreId " +
                           $"WHERE id = @Id";

            int result = 0;
            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@genreId", genreId);
                cmd.Parameters.AddWithValue("@Id", id);

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        public int updatePostRating(DbConnection connection, int id, double rating)
        {
            string query = $"UPDATE posts " +
                           $"SET rating = @rating " +
                           $"WHERE id = @Id";

            int result = 0;
            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@rating", rating);
                cmd.Parameters.AddWithValue("@Id", id);

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        public double getPostRating(DbConnection connection, int id)
        {
            double rating = 0.0;
            string query = $"SELECT rating " +
                           $"FROM posts " +
                           $"WHERE id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (!reader.IsDBNull(reader.GetOrdinal("rating")))
                        {
                            rating = Convert.ToDouble(reader["rating"]);
                        }
                    }
                }
            }
            return rating;
        }

        public List<Post> getPostsByGenreID(DbConnection connection, int genreId)
        {
            List<Post> posts = new List<Post>();
            string query = $"SELECT * " +
                           $"FROM posts " +
                           $"WHERE genre_id = @genreId";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@genreId", genreId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Post post = new Post(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToString(reader["title"]),
                            Convert.ToString(reader["image"]),
                            Convert.ToString(reader["content"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToDateTime(reader["created_at"]),
                            Convert.ToInt32(reader["genre_id"])
                        );

                        posts.Add(post);
                    }
                }
            }
            return posts;
        }

        public void deletePost(DbConnection connection, int id)
        {
            string query = $"DELETE FROM posts WHERE id = {id}";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}
