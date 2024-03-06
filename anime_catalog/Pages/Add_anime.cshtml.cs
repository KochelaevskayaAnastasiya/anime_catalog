using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Web;
using System.Xml.Linq;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;

namespace anime_catalog.Pages
{
    public class Add_animeModel : PageModel
    {
        private DataBase database = new DataBase();

        public string[] ganres;
        public string[] tags;
        public string[] ages;

        [BindProperty]
        public IFormFile Photo { get; set; }

        public void OnGet()
        {
            ganres = getMasGenre();
            tags = getMasTag();
            ages = getMasAge();
        }

        public string[] getMasGenre()
        {
            List<string> records = new List<string>();
            string queryString = "SELECT [Genre] FROM [Genre]";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string data = reader[0].ToString();
                records.Add(data);
            }
            reader.Close();
            database.closeConnection();

            return records.ToArray();
        }
        public string[] getMasTag()
        {
            List<string> records = new List<string>();
            string queryString = "SELECT [Tag] FROM [Tag]";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string data = reader[0].ToString();
                records.Add(data);
            }
            reader.Close();
            database.closeConnection();

            return records.ToArray();
        }
        public string[] getMasAge()
        {
            List<string> records = new List<string>();
            string queryString = "SELECT [Age] FROM [Age]";

            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                string data = reader[0].ToString();
                records.Add(data);
            }
            reader.Close();
            database.closeConnection();

            return records.ToArray();
        }
        public int CountTitleBD()
        {
            string queryString = "SELECT COUNT(*) FROM [Title];";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            int l = 0;
            while (reader.Read())
            {
                l = int.Parse(reader[0].ToString());
            }
            reader.Close();
            database.closeConnection();
            return l;
        }

        public int GetIdAge(string age)
        {
            string queryString = "SELECT ID_age FROM [Age] WHERE Age = '" + age + "';";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            int l = 0;
            while (reader.Read())
            {
                l = int.Parse(reader[0].ToString());
            }
            reader.Close();
            database.closeConnection();
            return l;
        }

        public int GetIdGenre(string genre)
        {
            string queryString = "SELECT ID_genre FROM [Genre] WHERE Genre = '" + genre + "';";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            int l = 0;
            while (reader.Read())
            {
                l = int.Parse(reader[0].ToString());
            }
            reader.Close();
            database.closeConnection();
            return l;
        }

        public int GetIdTag(string tag)
        {
            string queryString = "SELECT ID_tag FROM [Tag] WHERE Tag = '" + tag + "';";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            int l = 0;
            while (reader.Read())
            {
                l = int.Parse(reader[0].ToString());
            }
            reader.Close();
            database.closeConnection();
            return l;
        }
        public bool SaveInBD_genre(int id_title, string[] genres)
        {
            for (int i = 0; i < genres.Length; i++)
            {
                int id_genre = GetIdGenre(genres[i]);
                string queryString = "INSERT INTO[ID_anime-genre] VALUES("
                + id_title + ","
                + id_genre
                + ");";
                database.openConnection();
                SqlCommand command_insert = new SqlCommand(queryString, database.getConnection());
                int number = command_insert.ExecuteNonQuery();
                Console.WriteLine("Добавлен: {0} в ID_anime-Genre", number);
                database.closeConnection();
            }
            return true;
        }

        public bool SaveInBD_tags(int id_title, string[] tags)
        {
            for (int i = 0; i < tags.Length; i++)
            {
                int id_tag = GetIdTag(tags[i]);
                string queryString = "INSERT INTO[ID_anime-tag] VALUES("
                + id_title + ","
                + id_tag
                + ");";
                database.openConnection();
                SqlCommand command_insert = new SqlCommand(queryString, database.getConnection());
                int number = command_insert.ExecuteNonQuery();
                Console.WriteLine("Добавлен: {0} в ID_anime-Tag", number);
                database.closeConnection();
            }
            return true;
        }
        
        public async Task SavePhoto(int id_title)
        {
            string filePath = Path.Combine("D:/University/8sem/ORO/anime_catalog/anime_catalog/wwwroot/images/posters", id_title +".jpeg");
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await Photo.CopyToAsync(fileStream);
            }

        }
        
        public async Task<bool> SaveInBD(string name, string description, int year, double score,
            string auther, int count, string[] check_genres, string[] check_tags, string action)
        {
            //Title
            int id_title = CountTitleBD() + 1;
            int id_age = GetIdAge(action);
            string sc = "" + score;
            sc = sc.Replace(',', '.');
            string queryString = "INSERT INTO[Title] VALUES(" 
                + id_title + ",'"
                + name + "','"
                + description + "',"
                + year + ","
                + sc + ",'"
                + auther + "',"
                + count + ","
                + id_age
                + ");";
            database.openConnection();
            SqlCommand command_insert = new SqlCommand(queryString, database.getConnection());
            int number = command_insert.ExecuteNonQuery();
            Console.WriteLine("Добавлен: {0} в Title", number);
            database.closeConnection();


            //Genre tags
            SaveInBD_genre(id_title, check_genres);
            SaveInBD_tags(id_title, check_tags);
            await SavePhoto(id_title);

            return true;
        }

        [HttpPost]
        public IActionResult OnPost()
        {
            string name = Request.Form["name"];
            string description = Request.Form["description"];
            int year = int.Parse(Request.Form["year"]);

            string sc = Request.Form["score"];
            sc = sc.Replace('.', ',');
            double score = double.Parse(sc);


            string auther = Request.Form["auther"];
            int count = int.Parse(Request.Form["count"]);
            var check_genres = Request.Form["check1[]"].ToString().Split(',');
            var check_tags = Request.Form["check2[]"].ToString().Split(',');
            for (int i = 0; i < check_genres.Length; i++)
            {
                check_genres[i] = HttpUtility.HtmlDecode(check_genres[i]);
            }
            for (int i = 0; i < check_tags.Length; i++)
            {
                check_tags[i] = HttpUtility.HtmlDecode(check_tags[i]);
            }
            var action = Request.Form["action"];
            SaveInBD(name, description, year, score,
             auther, count, check_genres, check_tags, action
                );
            return RedirectToPage("Add_anime");
        }
    }
}
