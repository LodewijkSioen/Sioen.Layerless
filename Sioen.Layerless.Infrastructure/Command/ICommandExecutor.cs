using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sioen.Layerless.Infrastructure.Command
{
    public interface ICommandExecutor
    {
        void Execute(Command command);
        T Execute<T>(Command<T> command);
    }
}
