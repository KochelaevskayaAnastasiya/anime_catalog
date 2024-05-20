using System.Data.SqlClient;

namespace anime_catalog
{
    public class DataBase
    {
        SqlConnection con = new SqlConnection("Data source=.\\SQLEXPRESS;database=Anime; Integrated security=true;");
        //SqlConnection con = new SqlConnection("workstation id=Anime_catalog.mssql.somee.com;packet size=4096;user id=Kochelka_SQLLogin_1;pwd=33tdrbs6ui;data source=Anime_catalog.mssql.somee.com;persist security info=False;initial catalog=Anime_catalog;TrustServerCertificate=True");
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
