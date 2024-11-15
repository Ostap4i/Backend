using DENMAP_SERVER.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Repository
{
    internal class PostRatesRepository
    {
        public int addPostRate(DbConnection connection, int postId, int userId, double rating, DateTime createdAt)
        {
            string query = $"INSERT INTO post_rates (post_id, user_id, rating, created_at) " +
                $"VALUES ( @postId, @userId, @rating, @createdAt); SELECT LAST_INSERT_ID();";
            int id = 0;
            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@rating", rating);
                cmd.Parameters.AddWithValue("@postId", postId);
                cmd.Parameters.AddWithValue("@createdAt", createdAt);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return id;
        }


        public PostRate getPostRateByUserIDAndPostID(DbConnection connection, int userId, int postId)
        {
            string query = $"SELECT * FROM post_rates WHERE user_id = {userId} AND post_id = {postId}";

            PostRate postRate = null;
            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        postRate = new PostRate(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToInt32(reader["post_id"]),
                            Convert.ToDateTime(reader["created_at"])
                        );
                    }
                }
            }

            return postRate;
        }

        public List<PostRate> getPostRatesByPostID(DbConnection connection, int postId)
        {
            string query = $"SELECT * FROM post_rates WHERE post_id = {postId}";

            List<PostRate> postRates = new List<PostRate>();
            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PostRate postRate = new PostRate(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToInt32(reader["post_id"]),
                            Convert.ToDateTime(reader["created_at"])
                        );

                        postRates.Add(postRate);
                    }
                }
            }

            return postRates;
        }

        public List<PostRate> getPostRatesByUserPostIDs(DbConnection connection, int userId)
        {
            string query = $"SELECT * FROM post_rates WHERE post_id IN (SELECT id FROM posts WHERE user_id = {userId})";

            List<PostRate> postRates = new List<PostRate>();
            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PostRate postRate = new PostRate(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToInt32(reader["user_id"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToInt32(reader["post_id"]),
                            Convert.ToDateTime(reader["created_at"])
                        );

                        postRates.Add(postRate);
                    }
                }
            }

            return postRates;
        }
    }
}
