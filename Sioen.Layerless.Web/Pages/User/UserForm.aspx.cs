using System;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using Sioen.Layerless.Infrastructure.Web;

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

        public UserModel SelectUser([RouteData]Guid? id)
        {
            return id.HasValue ?
                Db.Get<Sioen.Layerless.Logic.Entities.User>(id.Value).To<UserModel>() :
                new UserModel();
        }

        public void InsertUser(Sioen.Layerless.Logic.Entities.User user)
        {
            var entity = new Logic.Entities.User
            {
                UserName = user.UserName
            };

            Db.Save(user);

            Response.RedirectToRoute("UserList");
        }

        public void UpdateUser(UserModel user)
        {
            var entity = Db.Get<Sioen.Layerless.Logic.Entities.User>(user.Id);
            entity.UserName = user.UserName;

            Db.Save(entity);

            Response.RedirectToRoute("UserList");
        }

        public void DeleteUser(Guid id)
        {
            var user = Db.Get<Logic.Entities.User>(id);

            Db.Delete(user);
            
            Response.RedirectToRoute("UserList");
        }
    }
}