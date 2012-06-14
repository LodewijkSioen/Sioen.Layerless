using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.SqlCommand;

namespace Sioen.Layerless.Tests.Data
{
    public class LogSqlInterceptor : EmptyInterceptor, IInterceptor
    {
        public static string LastExecutedQuery;

        SqlString IInterceptor.OnPrepareStatement(SqlString sql)
        {
            LastExecutedQuery = sql.ToString();

            return sql;
        }        
    }
}
