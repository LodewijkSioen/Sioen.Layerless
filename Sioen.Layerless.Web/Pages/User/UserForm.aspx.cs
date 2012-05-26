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
            switch (RouteData.Values["action"].ToString())
            {
                case "new":
                    Form.DefaultMode = FormViewMode.Insert;
                    break;
                case "edit":
                    Form.DefaultMode = FormViewMode.Edit;
                    break;
                default:
                    Form.DefaultMode = FormViewMode.ReadOnly;
                    break;
            }
        }

        public Sioen.Layerless.Logic.Entities.User SelectUser([RouteData]Guid? id)
        {
            return id.HasValue ? 
                Db.Get<Sioen.Layerless.Logic.Entities.User>(id.Value) :
                new Logic.Entities.User();
        }

        public void InsertUser(Sioen.Layerless.Logic.Entities.User user)
        {

        }

        public void UpdateUser(Guid id)
        {
            var user = Db.Get<Sioen.Layerless.Logic.Entities.User>(id);
            TryUpdateModel(user);

            Db.Save(user);

            Response.RedirectToRoute("UserList");
        }

        public void DeleteUser(Guid id)
        {
            var user = Db.Get<Sioen.Layerless.Logic.Entities.User>(id);
            TryUpdateModel(user);

            Response.RedirectToRoute("UserList");
        }
    }
}