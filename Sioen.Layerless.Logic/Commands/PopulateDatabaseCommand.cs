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
            var users = new List<User> {
                new User{ UserName = "Admin" },
                new User { UserName = "John" },
                new User { UserName = "Paul" },
                new User { UserName = "Ringo" },
                new User { UserName = "George" },
                new User { UserName = "Kurt" },
                new User { UserName = "Krist" },
                new User { UserName = "Dave" },
                new User { UserName = "Thom" },
                new User { UserName = "Jonny" },
                new User { UserName = "Ed" },
                new User { UserName = "Colin" },
                new User { UserName = "Phil" },
                new User { UserName = "Steve" },
                new User { UserName = "Glen" },
                new User { UserName = "Sid" }
            };

            users.ForEach(u => Db.Save(u));
        }
    }
}
