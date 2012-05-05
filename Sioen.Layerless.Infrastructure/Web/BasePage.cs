using System.Web.UI;
using Sioen.Layerless.Infrastructure.Data;

namespace Sioen.Layerless.Infrastructure.Web
{
    public abstract class BasePage : Page
    {
        public IQueryExecutor Db { get; set; }
    }
}
