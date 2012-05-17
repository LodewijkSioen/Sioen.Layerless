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
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Logic.Commands
{
    public class BuildDatabaseCommand : Command
    {
        public NHibernate.Cfg.Configuration Config { get; set; }

        public override void Execute()
        {
            SchemaHelper.DropDatabase(Config);
            SchemaHelper.BuildDatabase(Config);
        }
    }
}
