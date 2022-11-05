using gwm.Core.Models;
using gwm.Core.Services;
using gwm.Infrastructure.Handlers;

namespace gwm.Infrastructure.Services;

public class CommandHandlerBuilder : ICommandHandlerBuilder
{
    private readonly IWindowsProvider _windowsProvider;

    public CommandHandlerBuilder(IWindowsProvider windowsProvider)
    {
        _windowsProvider = windowsProvider;
    }

    public ICommandHandler Build(ControlPanel controlPanel)
    {
        return new CommandHandler()
            .RegisterHandler(new ToggleCaptureHandler(controlPanel, _windowsProvider))
            .RegisterHandler(new CycleSlideHandler(controlPanel))
            .RegisterHandler(new CycleMonitorHandler(controlPanel));
    }
}