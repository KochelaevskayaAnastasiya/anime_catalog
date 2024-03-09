using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace anime_catalog.Pages
{
    public class AnimeModel : PageModel
    {
        DataBase database = new DataBase();

        public string name;
        public string discription_anime;
        public string year;
        public string score;
        public string author;
        public string count;
        public string genres;
        public string tags;
        public string age;
        public int id;



        public string GetAge(int id)
        {
            string queryString = "SELECT Age FROM [Age] WHERE ID_age ='" + id + "';";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string l = "";
            while (reader.Read())
            {
                l = reader[0].ToString();
            }
            reader.Close();
            database.closeConnection();
            return l;
        }
        public string GetGenre(int id)
        {
            string queryString = "SELECT Genre FROM [Genre] WHERE ID_genre = '" + id + "';";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string l = "";
            while (reader.Read())
            {
                l = reader[0].ToString();
            }
            reader.Close();
            database.closeConnection();
            return l;
        }

        public string GetTag(int id)
        {
            string queryString = "SELECT Tag FROM [Tag] WHERE ID_tag = '" + id + "';";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string l = "";
            while (reader.Read())
            {
                l = reader[0].ToString();
            }
            reader.Close();
            database.closeConnection();
            return l;
        }

        public string GetGenresStr(List<int> genresId)
        {
            string l = "Жанры: ";
            for (int i = 0; i < genresId.Count; i++)
            {
                if (l != "Жанры: ") { l += ", "; }
                l += GetGenre(genresId[i]);
            }

            return l;
        }
        public string GetGenres(int id)
        {
            List<int> genres = new List<int>();
            string queryString = "SELECT [ID_genre] FROM [ID_anime-genre] WHERE ID_anime=" + id + ";";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            int l = 0;
            while (reader.Read())
            {
                l = int.Parse(reader[0].ToString());
                genres.Add(l);
            }
            reader.Close();
            database.closeConnection();
            return GetGenresStr(genres);
        }
        public string GetTagesStr(List<int> TagesId)
        {
            string l = "Тэги: ";
            for (int i = 0; i < TagesId.Count; i++)
            {
                if (l != "Тэги: ") { l += ", "; }
                l += GetTag(TagesId[i]);
            }

            return l;
        }
        public string GetTages(int id)
        {
            List<int> tages = new List<int>();

            string queryString = "SELECT [ID_tag] FROM [ID_anime-tag] WHERE ID_anime=" + id + ";";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            int l = 0;
            while (reader.Read())
            {
                l = int.Parse(reader[0].ToString());
                tages.Add(l);
            }
            reader.Close();
            database.closeConnection();
            return GetTagesStr(tages);
        }

        public string GetCount(int id)
        {
            string queryString = "SELECT [Count_ser] FROM [Title] WHERE ID_anime=" + id + ";";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string l = "Количество серий: ";
            while (reader.Read())
            {
                l += reader[0].ToString();
            }
            reader.Close();
            database.closeConnection();
            return l;
        }
        public string GetYear(int id)
        {
            string queryString = "SELECT [Year] FROM [Title] WHERE ID_anime=" + id + ";";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string l = "Год выпуска: ";
            while (reader.Read())
            {
                l += reader[0].ToString();
            }
            reader.Close();
            database.closeConnection();
            return l;
        }
        public string GetDiscription(int id)
        {
            string queryString = "SELECT [Discription] FROM [Title] WHERE ID_anime=" + id + ";";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string l = "";
            while (reader.Read())
            {
                l += reader[0].ToString();
            }
            reader.Close();
            database.closeConnection();
            return l;
        }

        public string GetAgeAnime(int id)
        {
            string queryString = "SELECT [ID_age] FROM [Title] WHERE ID_anime=" + id + ";";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            int k = 0;
            while (reader.Read())
            {
                k = int.Parse(reader[0].ToString());
            }

            reader.Close();
            database.closeConnection();
            return "Возрастное ограничение: " + GetAge(k);
        }


        public string GetScore(int id)
        {
            string queryString = "SELECT [Score] FROM [Title] WHERE ID_anime=" + id + ";";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string l = "Оценка: ";
            while (reader.Read())
            {
                l += reader[0].ToString();
            }
            reader.Close();
            database.closeConnection();
            return l;
        }

        public string GetName(int id)
        {
            string queryString = "SELECT [Name] FROM [Title] WHERE ID_anime=" + id + ";";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string l = "";
            while (reader.Read())
            {
                l = reader[0].ToString();
            }
            reader.Close();
            database.closeConnection();
            return l;
        }

        public string GetAuthor(int id)
        {
            string queryString = "SELECT [Author] FROM [Title] WHERE ID_anime=" + id + ";";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string l = "Студия: ";
            while (reader.Read())
            {
                l += reader[0].ToString();
            }
            reader.Close();
            database.closeConnection();
            return l;
        }
        public void OnGet(int id)
        {
            this.id = id;
            name = GetName(id);
            discription_anime = GetDiscription(id);
            year = GetYear(id);
            score = GetScore(id);
            author = GetAuthor(id);
            count = GetCount(id);
            genres = GetGenres(id);
            tags = GetTages(id);
            age = GetAgeAnime(id);
        }
    }
}
