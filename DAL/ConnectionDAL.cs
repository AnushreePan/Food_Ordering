namespace Food_Ordering.DAL
{
    public class ConnectionDAL
    {
        public static string SQL_Connection = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("SQL_Food");
    }
}
