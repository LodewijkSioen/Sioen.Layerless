using Castle.MicroKernel;

namespace Sioen.Layerless.Infrastructure.Command
{
    public class CommandExecutor : ICommandExecutor
    {
        private IKernel _kernel;

        public CommandExecutor(IKernel kernel)
        {
            _kernel = kernel;
        }

        public void Execute(Command command)
        {
            _kernel.InjectProperties(command);
            command.Execute();
        }

        public T Execute<T>(Command<T> command)
        {
            Execute(command as Command);
            return command.ReturnValue;
        }
    }
}
