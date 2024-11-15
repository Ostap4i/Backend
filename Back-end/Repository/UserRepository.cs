using MySql.Data.MySqlClient;
using DENMAP_SERVER.Entity;
using System.Data.Common;

namespace DENMAP_SERVER.Repository
{
    internal class UserRepository
    {
        public int addUser(DbConnection connection, string name, string password, string image, string description)
        {
            string query = $"INSERT INTO users (name, password, image, rating, description) " +
                $"VALUES (@name, @password, @image, @rating, @description); SELECT LAST_INSERT_ID();";
            int id = 0;
            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@image", image);
                cmd.Parameters.AddWithValue("@rating", 0);
                cmd.Parameters.AddWithValue("@description", description);
                id = Convert.ToInt32(cmd.ExecuteScalar());
            }
            return id;
        }


        public User getUserByName(DbConnection connection, string name)
        {
            User user = null;
            string query = $"SELECT * " +
                           $"FROM users " +
                           $"WHERE name = @name";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@name", name);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToString(reader["name"]),
                            Convert.ToString(reader["password"]),
                            Convert.ToString(reader["image"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToString(reader["description"])
                        );
                    }
                }
            }
            return user;
        }

        public User getUserById(DbConnection connection, int id)
        {
            User user = null;
            string query = $"SELECT * " +
                           $"FROM users " +
                           $"WHERE id = @Id";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToString(reader["name"]),
                            Convert.ToString(reader["password"]),
                            Convert.ToString(reader["image"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToString(reader["description"])
                        );
                    }
                }
            }
            return user;
        }

        public int updateUser(DbConnection connection, int id, string name, string password, 
            string image, string description)
        {
            string query = $"UPDATE users " +
                           $"SET name = @Name, " +
                               $"password = @Password, " +
                               $"image = @Image, " +
                               $"description = @Description " +
                           $"WHERE id = @Id";

            int result = 0;
            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@Image", image);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Id", id);

                result = cmd.ExecuteNonQuery();
            }
            return result;
        }

        public List<User> GetUsersByIds(DbConnection connection, List<int> ids)
        {
            List<User> users = new List<User>();
            if (ids.Count == 0)
            {
                return users;
            }

            string idsString = string.Join(",", ids);

            string query = $"SELECT * FROM users WHERE id IN ({idsString})";

            using (MySqlCommand cmd = new MySqlCommand(query, (MySqlConnection)connection))
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        User user = new User(
                            Convert.ToInt32(reader["id"]),
                            Convert.ToString(reader["name"]),
                            Convert.ToString(reader["password"]),
                            Convert.ToString(reader["image"]),
                            Convert.ToDouble(reader["rating"]),
                            Convert.ToString(reader["description"])
                        );
                        users.Add(user);
                    }
                }
            }

            return users;
        }

        public int updateUserRating(DbConnection connection, int id, double rating)
        {
            string query = $"UPDATE users " +
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
    }
}

