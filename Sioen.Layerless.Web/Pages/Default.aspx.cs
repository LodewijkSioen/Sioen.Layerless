using System;
using Sioen.Layerless.Logic.Commands;
using Sioen.Layerless.Logic.Entities;
using Sioen.Layerless.Infrastructure.Web;
using System.Web.ModelBinding;

namespace Sioen.Layerless.Web.Pages
{
    public partial class Home : BasePage
    {
        public string CreateDatabase()
        {
            Command.Execute(new BuildDatabaseCommand());
            Command.Execute(new PopulateDatabaseCommand());

            return GetRouteUrl("Default", null);
        }

        public string TestBinding([Control()]string hiddenId)
        {
            return hiddenId;
        }
    }
}