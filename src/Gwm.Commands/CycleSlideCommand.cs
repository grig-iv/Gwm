using Gwm.Commands.Enums;

namespace Gwm.Commands;

public class CycleSlideCommand : AbstractCommand
{
    public SlideMovement Direction { get; init; }
}