using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Data.SqlClient;
using static System.Formats.Asn1.AsnWriter;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Components;
using System.Reflection.PortableExecutable;

namespace anime_catalog.Pages
{
    public class CatalogModel : PageModel
    {
        DataBase database = new DataBase();

        public string[] ganres;
        public string[] tags;
        public string[] ages;

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
        //умный поиск
        public IActionResult OnGetSearch(string query)
        {
            // Логика поиска

            return new ContentResult
            {
                Content = GetListSearch(query), // Предполагается, что productInfo уже представляет собой HTML-код
                ContentType = "text/html"
            };
        }
        public string GetListSearch(string query)
        {
            List<string> search = new List<string>();
            string res = "";
            if (query != null && query != "")
            {
                string queryString = "SELECT [Name] FROM [Title] WHERE [Name] LIKE N'" + query + "%';";

                SqlCommand command = new SqlCommand(queryString, database.getConnection());
                database.openConnection();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string data = reader[0].ToString();
                    search.Add(data);
                }
                reader.Close();
                database.closeConnection();

                res = "<p>";
                res += string.Join("<br>", search) + "<p>";
            }
            return res;
        }


        //модальное окно
        public IActionResult OnGetGetProductInfo(int id)
        {
            // Загрузка информации о товаре по ID
            var productInfo = GetProductInfo(id);

            // Возвращение информации о товаре в виде HTML или JSON
            return new ContentResult
            {
                Content = productInfo, // Предполагается, что productInfo уже представляет собой HTML-код
                ContentType = "text/html"
            };
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
        public string GetDiscription (int id)
        {
            string queryString = "SELECT [Discription] FROM [Title] WHERE ID_anime="+id+";";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            string l = "Описание: ";
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
        private string GetProductInfo(int id)
        {
            // Логика получения информации о товаре
            return "<p>" + GetDiscription(id) + "<br><br>" + GetGenres(id)+ "<br>" + GetTages(id) + "<br>" + GetCount(id) + "<br>" + GetYear(id) + "<br>" + GetScore(id) + "<br>" + GetAgeAnime(id) + "</p>"; 
        }

        //пагинация
        public IActionResult OnGetNextPage(int pageNumber, string str_filt)
        {
            // Получение данных для следующей страницы
            var data = GetDataForPage(pageNumber, str_filt);

            // Возвращение частичного представления с данными
            return new JsonResult(data);
        }

        private List<string> GetDataForPage(int pageNumber, string str_filt)
        {
            // Логика получения данных для указанной страницы
            // Здесь должен быть ваш код для получения данных
            List<string> strings = GetAnimes(pageNumber,str_filt);
            List<string> res = new List<string>();
            for (int i = (pageNumber-1)*6; i < pageNumber*6+1; i++)
            {
                if(strings.Count<=i) { break; }
                res.Add(strings[i]);
            }
            return res;
        } 

        public string GetStrGenre4(int k1, int k2, int k3, int k4)
        {
            return "ID_anime in(SELECT [ID_anime-genre].[ID_anime] FROM ([ID_anime-genre] INNER JOIN [ID_anime-genre] AS [ID_anime-genre1] ON [ID_anime-genre].[ID_anime]=[ID_anime-genre1].[ID_anime]) INNER JOIN [ID_anime-genre] AS [ID_anime-genre2] ON [ID_anime-genre1].[ID_anime]=[ID_anime-genre2].[ID_anime] INNER JOIN [ID_anime-genre] AS [ID_anime-genre3] ON [ID_anime-genre2].[ID_anime]=[ID_anime-genre3].[ID_anime] WHERE [ID_anime-genre].[ID_genre]="+k1+" AND [ID_anime-genre1].[ID_genre]="+k2+" AND [ID_anime-genre2].[ID_genre]="+k3+" AND [ID_anime-genre3].[ID_genre]="+k4+") ";
        }

        
        public List<string> GetAnimes(int pageNumber, string str_filt)
        {

            //разбираем фильтры
            string[] filts = str_filt.Split('$');
               //упорядочить по
            string[] filts2 = filts[0].Split(',');

            string chacnm_genre = "";
            int genre1 = 0;
            int genre2 = 0;
            int genre3 = 0;
            int genre4 = 0;
            if (filts[1] != "" && filts[1] != null)
            {
                string[] filts_genre = filts[1].Split(',');
                if (filts_genre[0] == "1") { genre1 = 1; } else { genre1 = 0; }
                if (filts_genre[1] == "1") { genre2 = 2; } else { genre2 = 0; }
                if (filts_genre[2] == "1") { genre3 = 3; } else { genre3 = 0; }
                if (filts_genre[3] == "1") { genre4 = 4; } else { genre4 = 0; }

                if (genre1 == 0 && genre2 == 0 && genre3 == 0 && genre4 == 0) { chacnm_genre = ""; }
                else
                {

                    // Находим максимум из первых двух чисел
                    int max1 = Math.Max(genre1, genre2);

                    // Находим максимум из максимума первой пары чисел и третьего числа
                    int max2 = Math.Max(max1, genre3);

                    // Находим максимум из максимума второй пары чисел и четвертого числа
                    int max3 = Math.Max(max2, genre4);

                    if (genre1 == 0) { genre1 = max3; }
                    if (genre2 == 0) { genre2 = max3; }
                    if (genre3 == 0) { genre3 = max3; }
                    if (genre4 == 0) { genre4 = max3; }
                    chacnm_genre = " ID_anime in (SELECT [ID_anime-genre].[ID_anime] FROM ([ID_anime-genre] INNER JOIN [ID_anime-genre] AS [ID_anime-genre1] ON [ID_anime-genre].[ID_anime]=[ID_anime-genre1].[ID_anime]) INNER JOIN [ID_anime-genre] AS [ID_anime-genre2] ON [ID_anime-genre1].[ID_anime]=[ID_anime-genre2].[ID_anime] INNER JOIN [ID_anime-genre] AS [ID_anime-genre3] ON [ID_anime-genre2].[ID_anime]=[ID_anime-genre3].[ID_anime] WHERE [ID_anime-genre].[ID_genre]=" + genre1+" AND [ID_anime-genre1].[ID_genre]="+ genre2+" AND [ID_anime-genre2].[ID_genre]="+ genre3 + " AND [ID_anime-genre3].[ID_genre]="+ genre4+ ")";
                        
                }
            }
            //тэги
            string chacnm_tags = "";
            int tags1 = 0;
            int tags2 = 0;

            if (filts[2] != "" && filts[2] != null)
            {
                string[] filts_tags = filts[2].Split(',');
                if (filts_tags[0] =="1") { tags1 = 1; } else { tags1 = 0; }
                if (filts_tags[1] =="1") { tags2 = 2; } else { tags2 = 0; }

                if (tags1 == 0 && tags2 == 0) { chacnm_tags = ""; } 
                else
                {
                    if (tags1 == 0 && tags2 == 2) { tags1 = 2; }
                    else
                    {
                        if (tags1 == 1 && tags2 == 0) { tags2 = 1; }
                        
                    }
                    chacnm_tags = "ID_anime in (SELECT ID_anime FROM [ID_anime-tag] WHERE ID_tag ="+ tags1 + " AND ID_anime in (SELECT ID_anime FROM [ID_anime-tag] WHERE ID_tag = "+ tags2 + "))";
                }
            }
            //количество
            string chacnm_count = "";
            int chacnm_count1 = 0;
            int chacnm_count2 = 0;

            if (filts[3] != "" && filts[3] != null)
            {
                string[] filts_count = filts[3].Split(',');
                if (int.TryParse(filts_count[0], out _) && int.TryParse(filts_count[1], out _))
                {
                    chacnm_count1 = int.Parse(filts_count[0]);
                    chacnm_count2 = int.Parse(filts_count[1]);
                    if (chacnm_count1!=0 && chacnm_count2!=0)
                    {
                        chacnm_count = "[Count_ser] >= " + chacnm_count1 + " AND[Count_ser] <= " + chacnm_count2;

                    }
                }
            }
            //год
            string chacnm_ear = "";
            int chacnm_ear1 = 0;
            int chacnm_ear2 = 0;

            if (filts[4] != "" && filts[4] != null)
            {
                string[] filts_ear = filts[4].Split(',');
                if (int.TryParse(filts_ear[0], out _) && int.TryParse(filts_ear[1], out _))
                {
                    chacnm_ear1 = int.Parse(filts_ear[0]);
                    chacnm_ear2 = int.Parse(filts_ear[1]);
                    chacnm_ear = "[Year] >= "+ chacnm_ear1 + " AND [Year] <= "+ chacnm_ear2;
                }
            }
            //оценка
            string chacnm_scor = "";
            int chacnm_scor1 = 0;
            int chacnm_scor2 = 0;

            if (filts[5] != "" && filts[5] != null)
            {
                string[] filts_scor = filts[5].Split(',');
                if (filts_scor[0].All(char.IsDigit) && filts_scor[1].All(char.IsDigit) && filts_scor[1]!="" && filts_scor[1]!=null && filts_scor[0] != "" && filts_scor[0] != null)
                {
                    chacnm_scor1 = int.Parse(filts_scor[0]);
                    chacnm_scor2 = int.Parse(filts_scor[1]);
                    chacnm_scor = "[Score] >= "+ chacnm_scor1 + " AND[Score] <= "+ chacnm_scor2;
                }
            }
            //Рейтинг
            string chacnm_age = "";

            if (filts[6] != "" && filts[6] != null)
            {
                string[] filts_age = filts[6].Split(',');
                for (int i = 0; i < filts_age.Length; i++)
                {
                    if (filts_age[i] == "1")
                    {

                        if (chacnm_age != "")
                        {
                            chacnm_age += " OR ";
                        }
                        chacnm_age += "[ID_age] = " + (i + 1);
                    }

                }
                if (chacnm_age != "")
                {
                    chacnm_age = "(" + chacnm_age + ")";
                }
            }
            string cdjqcndf = chacnm_age;
            if (cdjqcndf != "" && chacnm_count != "") cdjqcndf += " AND ";
            cdjqcndf += chacnm_count;

            if (cdjqcndf != "" && chacnm_ear != "") cdjqcndf += " AND ";
            cdjqcndf += chacnm_ear;

            if (cdjqcndf != "" && chacnm_scor != "") cdjqcndf += " AND ";
            cdjqcndf += chacnm_scor;

            if (cdjqcndf != "" && chacnm_tags != "") cdjqcndf += " AND ";
            cdjqcndf += chacnm_tags;

            if (cdjqcndf != "" && chacnm_genre != "") cdjqcndf += " AND ";
            cdjqcndf += chacnm_genre;
            


            if (cdjqcndf != "")
            {
                cdjqcndf = " WHERE " + cdjqcndf;
            }
            string queryString = "SELECT* FROM[Title]" + cdjqcndf + " ORDER BY["+filts2[0]+ "] "+filts2[1]+";";


            //разобрали фильтры

            List<string> animes = new List<string>();
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            int k = 1;
            while (reader.Read() && k <= 6*pageNumber)
            {
                string str = reader[0].ToString() + '$' + reader[1].ToString();
                animes.Add(str);
                k++;
            }
            reader.Close();
            database.closeConnection();
            return animes;
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
        public void OnGet()
        {
            ganres = getMasGenre();
            tags = getMasTag();
            ages = getMasAge();
        }
    }
}
