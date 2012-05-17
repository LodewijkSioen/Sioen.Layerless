using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;
using Sioen.Layerless.Infrastructure.Command;
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Infrastructure.Web
{
    public abstract class BasePage : Page
    {
        protected BasePage()
            : base()
        {
            Dispatcher = new EventDispatcher(this);
        }

        public IQueryExecutor Db { get; set; }
        public ICommandExecutor Command { get; set; }
        public EventDispatcher Dispatcher { get; private set; }
        
        public override ViewStateMode ViewStateMode
        {
            get { return ViewStateMode.Disabled; }
            set { }
        }        
    }

    
}
