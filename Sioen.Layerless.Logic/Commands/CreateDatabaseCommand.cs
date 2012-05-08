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
    public class CreateDatabaseCommand : Command
    {
        public override void Execute()
        {
            NHibernateConfigurator.DropDatabase();
            NHibernateConfigurator.BuildDatabase();
        }
    }
}
