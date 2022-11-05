using Gwm.Commands.Enums;

namespace Gwm.Commands;

public class MoveSlideCommand : AbstractCommand
{
    public SlideMovement Direction { get; init; }
}