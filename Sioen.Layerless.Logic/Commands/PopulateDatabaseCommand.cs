using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sioen.Layerless.Logic.Entities;
using Sioen.Layerless.Infrastructure.Command;
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Logic.Commands
{
    public class PopulateDatabaseCommand : Command
    {
        public IQueryExecutor Db { get; set; }

        public override void Execute()
        {
            var Admin = new User
            {
                UserName = "Admin"
            };

            Db.Save(Admin);
        }
    }
}
