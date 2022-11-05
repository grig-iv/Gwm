using Gwm.Commands;
using gwm.Core.Services;

namespace gwm.Infrastructure.Handlers;

public class CommandHandler : ICommandHandler
{
    private readonly Dictionary<Type, object> _handlers;

    public CommandHandler()
    {
        _handlers = new Dictionary<Type, object>();
    }

    public CommandHandler RegisterHandler<T>(AbstractHandler<T> handler) where T : AbstractCommand
    {
        _handlers[typeof(T)] = handler;
        return this;
    }

    public void Handle(AbstractCommand command)
    {
        var handler = _handlers[command.GetType()];
        typeof(AbstractHandler<>)
            .MakeGenericType(command.GetType())
            .GetMethod(nameof(AbstractHandler<AbstractCommand>.Handle))
            ?.Invoke(handler, new[] { command });
    }
}