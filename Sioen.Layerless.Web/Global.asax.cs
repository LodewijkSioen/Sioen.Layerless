using System;
using System.Web.Routing;
using AutoMapper;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Sioen.Layerless.Infrastructure.Data;
using Sioen.Layerless.Infrastructure.Web;
using Sioen.Layerless.Logic.Entities;
using Sioen.Layerless.Logic.Mappings;
using Sioen.Layerless.Web.Pages.Account;

namespace Sioen.Layerless.Web
{
    public class Global : System.Web.HttpApplication, IContainerAccessor
    {
        private static IWindsorContainer _container;

        protected void Application_Start(object sender, EventArgs e)
        {
            _container = new WindsorContainer()
                .Install(FromAssembly.InThisApplication());

            _container.Resolve<NHibernate.Cfg.Configuration>()
                .ForWeb()
                .ForSqlServerCE("CE")
                .WithMappingsFromAssemblyOf<UserMapping>();

            Routing.DefineRoutes(RouteTable.Routes);

            Mapper.CreateMap<User, UserModel>();
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