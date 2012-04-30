using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sioen.Experiments.Tests.Data
{
    public static class Database
    {
        public static void Create()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CE"].ConnectionString;
            var connection = new SqlConnectionStringBuilder(connectionString);
            if (File.Exists(connection.DataSource))
            {
                File.Delete(connection.DataSource);
            }

            using (var engine = new SqlCeEngine(connectionString))
            {
                engine.CreateDatabase();
            }
        }        
    }
}
