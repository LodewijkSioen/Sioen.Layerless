using System;
using Sioen.Layerless.Logic.Commands;
using Sioen.Layerless.Logic.Entities;
using Sioen.Layerless.Infrastructure.Web;

namespace Sioen.Layerless.Web.Pages
{
    public partial class Home : BasePage
    {
        public string CreateDatabase()
        {
            Command.Execute(new CreateDatabaseCommand());
            Command.Execute(new PopulateDatabaseCommand());

            return GetRouteUrl("Default", null);
        }
    }
}