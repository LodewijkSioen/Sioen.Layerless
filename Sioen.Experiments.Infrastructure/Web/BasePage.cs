using System.Web.UI;
using Sioen.Experiments.Infrastructure.Data;

namespace Sioen.Experiments.Infrastructure.Web
{
    public abstract class BasePage : Page
    {
        public IQueryExecutor Db { get; set; }
    }
}
