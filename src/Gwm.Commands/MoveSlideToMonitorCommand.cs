using Gwm.Commands.Enums;

namespace Gwm.Commands;

public class MoveSlideToMonitorCommand : AbstractCommand
{
    public MonitorMovement Direction { get; init; }
}