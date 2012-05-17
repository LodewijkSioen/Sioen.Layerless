using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sioen.Layerless.Infrastructure.Command;

namespace Sioen.Layerless.Logic.Commands
{
    public class CreateDatabaseFile : Command
    {
        public override void Execute()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CE"].ConnectionString;
            var connection = new SqlConnectionStringBuilder(connectionString);
            var path = ReplaceDataPath(connection.DataSource);
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            using (var engine = new SqlCeEngine(connectionString))
            {
                engine.CreateDatabase();
            }
        }

        private string ReplaceDataPath(string datasource)
        {
            return datasource.Replace("|DataDirectory|", AppDomain.CurrentDomain.GetData("DataDirectory").ToString());

        }
    }
}
