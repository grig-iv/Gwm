using Gwm.Commands;
using gwm.Core.Models;
using Serilog;

namespace gwm.Infrastructure.Handlers;

public abstract class AbstractHandler<T> where T : AbstractCommand
{
    protected AbstractHandler(ControlPanel controlControlPanel)
    {
        ControlPanel = controlControlPanel;
    }

    protected ControlPanel ControlPanel { get; }
    protected ILogger Logger => ControlPanel.Logger;


    public abstract void Handle(T command);
}