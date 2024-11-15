using DENMAP_SERVER.Entity;
using DENMAP_SERVER.Repository;
using MySql.Data.MySqlClient;

namespace DENMAP_SERVER.Service
{
    internal class GenreService
    {
        string connectionString = "Server=34.116.253.154;Port=3306;Database=chat_database;Uid=app_user;Pwd=&X9fT#7vYqZ$4LpR;";

        private GenreRepository genreRepository = new GenreRepository();

        public int AddGenre(string name)
        {
            int id = 0;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    id = genreRepository.addGenre(connection, name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
            return id;
        }

        public bool updateGenre(int id, string name)
        {
            bool result = false;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    result = genreRepository.updateGenre(connection, id, name);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            return result;
        }

        public List<Genre> GetAllGenres()
        {
            List<Genre> genres = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    genres = genreRepository.getAllGenres(connection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            return genres;
        }

        public Genre GetGenreById(int id)
        {
            Genre genre = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    genre = genreRepository.getGenreById(connection, id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }

            if (genre == null)
            {
                throw new Exception("Genre with id: " + id + " not found");
            }

            return genre;

        }

        public List<Genre> GetGenresByIds(List<int> ids)
        {
            List<Genre> genres = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                try
                {
                    genres = genreRepository.GetGenresByIds(connection, ids);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database error.\nError:" + ex.Message);
                    throw new Exception("Server error");
                }
            }
            return genres;
        }
    }
}
