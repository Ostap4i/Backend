using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DENMAP_SERVER.Service
{
    internal class PostRateService
    {
        string connectionString = "Server=34.116.253.154;Port=3306;Database=chat_database;Uid=app_user;Pwd=&X9fT#7vYqZ$4LpR;";

        private PostRatesRepository postRatesRepository = new PostRatesRepository();

        public int AddPostRate(int postId, int userId, double rating)
        {
            int id = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    id = postRatesRepository.addPostRate(connection, postId, userId, rating, DateTime.Now);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
            return id;
        }

        public PostRate GetPostRateByUserIDAndPostID(int userId, int postId)
        {
            PostRate postRate = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    postRate = postRatesRepository.getPostRateByUserIDAndPostID(connection, userId, postId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
            return postRate;
        }

       public List<PostRate> GetPostRatesByPostID(int postId)
        {
            List<PostRate> postRates = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    postRates = postRatesRepository.getPostRatesByPostID(connection, postId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
            return postRates;
        }
    }
}
