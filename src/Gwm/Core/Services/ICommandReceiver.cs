using Gwm.Commands;

namespace gwm.Core.Services;

public interface ICommandReceiver
{
    IEnumerable<AbstractCommand> Receive();
}