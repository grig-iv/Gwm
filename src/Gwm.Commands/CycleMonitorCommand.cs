using Gwm.Commands.Enums;

namespace Gwm.Commands;

public class CycleMonitorCommand : AbstractCommand
{
    public MonitorMovement Direction { get; init; }
}