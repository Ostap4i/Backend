using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Repository;
using MySql.Data.MySqlClient;

namespace DENMAP_SERVER.Service
{
    internal class UserService
    {
        string connectionString = "Server=34.116.253.154;Port=3306;Database=chat_database;Uid=app_user;Pwd=&X9fT#7vYqZ$4LpR;";

        private UserRepository userRepository = new UserRepository();
        private PostRepository postRepository = new PostRepository();
        private PostRatesRepository postRateRepository = new PostRatesRepository();

        public int RegisterUser(string username, string password, string image, string description)
        {
            int id = 0;
            User user = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    user = userRepository.getUserByName(connection, username);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }

                if (user != null)
                {
                    throw new Exception("User with name: " + username + " already exists");
                }

                try
                {
                    id = userRepository.addUser(connection, username, password, image, description);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
            return id;
        }

        public User getUserByName(string name)
        {
            User user = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    user = userRepository.getUserByName(connection, name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            if (user == null)
            {
                throw new Exception("User with name: " + name + " not found");
            }


            return user;
        }

        public int loginUser(string name, string password)
        {
            User user = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                System.Console.WriteLine("User service, starting login");
                try
                {
                    user = userRepository.getUserByName(connection, name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            if (user == null)
            {
                System.Console.WriteLine("User with name: " + name + " not found");
                throw new Exception("User with name: " + name + " not found");
            }

            if (!user.Password.Equals(password))
            {
                System.Console.WriteLine("Incorrect password");
                throw new Exception("Incorrect password");
            }

            return user.Id;
        }

        public User GetUserById(int id)
        {
            User user = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    user = userRepository.getUserById(connection, id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            if (user == null)
            {
                throw new Exception("User with id: " + id + " not found");
            }

            return user;
        }

        public List<User> GetUsersByIds(List<int> ids)
        {
            List<User> users = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    users = userRepository.GetUsersByIds(connection, ids);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            if (users == null)
            {
                throw new Exception("Users not found. Ids: " + ids);
            }

            return users;
        }


        public int UpdateUser(int id, string name, string password, string image, string description)
        {
            User user = GetUserById(id);
            int result = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    result = userRepository.updateUser(connection, id, name, password, image, description);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            return result;
        }

        public int UpdateUserRating(int id, double rating)
        {
            int result = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    result = userRepository.updateUserRating(connection, id, rating);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            return result;
        }
        public void ReCalculateUserRating(int id)
        {
            double rating = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    List<PostRate> postsRates = postRateRepository.getPostRatesByUserPostIDs(connection, id);
                    foreach (PostRate rate in postsRates)
                    {
                        rating += rate.Rating;
                    }
                    rating /= postsRates.Count;


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            UpdateUserRating(id, rating); ;
        }
    }
}
