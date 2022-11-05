using Gwm.Commands;

namespace gwm.Core.Services;

public interface ICommandHandler
{
    void Handle(AbstractCommand command);
}