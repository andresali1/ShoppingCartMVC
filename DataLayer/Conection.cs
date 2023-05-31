using System.Configuration;

namespace DataLayer
{
    public class Conection
    {
        public static string connStr = ConfigurationManager.ConnectionStrings["connStr"].ToString();
    }
}
