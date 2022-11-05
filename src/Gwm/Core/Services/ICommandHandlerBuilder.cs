using gwm.Core.Models;

namespace gwm.Core.Services;

public interface ICommandHandlerBuilder
{
    ICommandHandler Build(ControlPanel controlPanel);
}