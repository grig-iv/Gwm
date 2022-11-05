using gwm.Core.Models;
using gwm.Core.Services;
using Serilog;

namespace gwm.Core;

public class Gwm
{
    private readonly ICommandReceiver _receiver;
    private readonly ILogger _logger;
    private readonly ICommandHandler _handler;
    private readonly ControlPanel _controlPanel;

    public Gwm(ICommandReceiver receiver, ICommandHandlerBuilder handlerBuilder, 
        IDesktopService desktopService, ILogger logger)
    {
        _receiver = receiver;
        _logger = logger;
        _controlPanel = new ControlPanel(desktopService, logger);
        _handler = handlerBuilder.Build(_controlPanel);
    }

    public void Run()
    {
        foreach (var command in _receiver.Receive())
        {
            // _logger.Information($"Receive command '{command.GetType().Name}'");
            _handler.Handle(command);
        }
    }
}