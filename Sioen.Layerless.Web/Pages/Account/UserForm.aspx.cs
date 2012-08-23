using System;
using System.Web.ModelBinding;
using System.Web.UI.WebControls;
using Sioen.Layerless.Infrastructure.Web;
using Sioen.Layerless.Logic.Entities;
using Omu.ValueInjecter;

namespace Sioen.Layerless.Web.Pages.Account
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
            var user = new UserModel();
            if(id.HasValue) user.InjectFrom(Db.Get<User>(id.Value));
            return user;
        }

        public void InsertUser(UserModel user)
        {
            var entity = new Logic.Entities.User
            {
                UserName = user.UserName
            };

            Db.Save(entity);

            Response.RedirectToRoute("UserList");
        }

        public void UpdateUser(UserModel user)
        {
            var entity = Db.Get<User>(user.Id);
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