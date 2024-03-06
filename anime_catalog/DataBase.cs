using System.Data.SqlClient;

namespace anime_catalog
{
    public class DataBase
    {
        SqlConnection con = new SqlConnection("Data source=.\\SQLEXPRESS;database=Anime; Integrated security=true;");
        public void openConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
        }

        public void closeConnection()
        {
            if (con.State == System.Data.ConnectionState.Open)
            {
                con.Close();
            }
        }

        public SqlConnection getConnection()
        {
            return con;
        }
    }
}
