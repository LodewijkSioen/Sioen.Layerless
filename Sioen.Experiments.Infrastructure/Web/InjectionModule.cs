using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Castle.Windsor;

namespace Sioen.Experiments.Infrastructure.Web
{
    public class InjectionModule : IHttpModule
    {
        private IWindsorContainer _container;

        public void Init(HttpApplication context)
        {
            _container = (context as IContainerAccessor).Container;

            context.PreRequestHandlerExecute += (s, e) => {
                if (context.Context.Handler is BasePage)
                {
                    _container.Kernel.InjectProperties(HttpContext.Current.Handler);
                }
            };
        }

        public void Dispose()
        {

        }
    }
}
