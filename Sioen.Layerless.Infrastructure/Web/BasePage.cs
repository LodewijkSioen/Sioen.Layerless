using System.Web.UI;
using System.Web.UI.WebControls;
using Sioen.Layerless.Infrastructure.Command;
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Infrastructure.Web
{
    public abstract class BasePage : Page
    {
        public IQueryExecutor Db { get; set; }
        public ICommandExecutor Command { get; set; }

        public override ViewStateMode ViewStateMode
        {
            get { return ViewStateMode.Disabled; }
            set { }
        }

        protected void OnCommand(object sender, CommandEventArgs e)
        {
            var method = GetType().GetMethod(e.CommandName);
            var redirect = method.Invoke(this, null);

            if (redirect is string)
            {
                Response.Redirect(redirect as string, true);
            }
        }
    }
}
