using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sioen.Layerless.Infrastructure.Web;

namespace Sioen.Layerless.Web.Pages.User
{
    public partial class UserList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public IQueryable<Sioen.Layerless.Logic.Entities.User> ListUsers()
        {
            return Db.Query<Sioen.Layerless.Logic.Entities.User>();
        }
    }
}