using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Castle.MicroKernel;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Sioen.Layerless.Infrastructure.Web;

namespace Sioen.Layerless.Web
{
    public class Global : System.Web.HttpApplication, IContainerAccessor
    {
        private static IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            _container = new WindsorContainer()            
                .Install(FromAssembly.InThisApplication());

            Routing.DefineRoutes(RouteTable.Routes);
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            if (Context.Handler is BasePage)
            {
                _container.Kernel.InjectProperties(Context.Handler);
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            
        }

        protected void Application_End(object sender, EventArgs e)
        {
            _container.Dispose();
        }

        public IWindsorContainer Container
        {
            get { return _container; }
        }
    }
}