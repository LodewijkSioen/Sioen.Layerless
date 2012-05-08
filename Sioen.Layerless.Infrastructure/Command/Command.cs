namespace Sioen.Layerless.Infrastructure.Command
{
    public abstract class Command
    {
        public abstract void Execute();
    }

    public abstract class Command<T>
    {
        public T ReturnValue { get; protected set; }
    }
}
