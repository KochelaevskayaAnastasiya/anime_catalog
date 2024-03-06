using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace anime_catalog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        DataBase database = new DataBase();

        public IActionResult OnGetData()
        {
            return new JsonResult(GetAnimes());
        }
        public List<string> GetAnimes()
        {
            List<string> animes = new List<string>();
            string queryString = "SELECT * FROM [Title] ORDER BY [Year] DESC;";
            SqlCommand command = new SqlCommand(queryString, database.getConnection());
            database.openConnection();
            SqlDataReader reader = command.ExecuteReader();
            int k = 0;
            while (reader.Read() && k < 10)
            {
                string str = reader[0].ToString() + '$' + reader[1].ToString();
                animes.Add(str);
                k++;
            }
            reader.Close();
            database.closeConnection();
            return animes;
        }

        public void OnGet()
        {
        }
    }
}