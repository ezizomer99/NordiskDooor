using MySql.Data.MySqlClient;
using System.Data;
namespace bacit_dotnet.MVC.DataAccess;

    public class SqlConnector { 
    
        private readonly IConfiguration config;

        public SqlConnector(IConfiguration config)
        {
            this.config = config;
        }

        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(config.GetConnectionString("MariaDb"));
        }

    }