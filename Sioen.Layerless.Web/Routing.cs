using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Sioen.Layerless.Web
{
    public static class Routing
    {
        public static void DefineRoutes(RouteCollection routes)
        {
            routes.MapPageRoute("Default", "", "~/Pages/Default.aspx");
            routes.MapPageRoute("UserList", "users/", "~/Pages/User/UserList.aspx");;
            routes.MapPageRoute("UserAction", "user/{action}/{*id}", "~/Pages/User/UserDetail.aspx");

            //routes.MapPageRoute("Customer", "customer/{id}", "~/Pages/Customer/Index.aspx");
        }
    }
}