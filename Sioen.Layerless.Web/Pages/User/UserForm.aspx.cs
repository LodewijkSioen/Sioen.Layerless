using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sioen.Layerless.Infrastructure.Web;
using System.Web.ModelBinding;

namespace Sioen.Layerless.Web.Pages.User
{
    public partial class UserForm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RouteData.Values["action"].ToString() == "new")
            {
                Form.DefaultMode = FormViewMode.Insert;
            }
        }

        public Sioen.Layerless.Logic.Entities.User SelectUser([RouteData]Guid id)
        {
            return Db.Get<Sioen.Layerless.Logic.Entities.User>(id);
        }        
    }
}