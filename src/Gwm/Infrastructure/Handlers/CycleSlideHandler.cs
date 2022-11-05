using Gwm.Commands;
using Gwm.Commands.Enums;
using gwm.Core.Models;

namespace gwm.Infrastructure.Handlers;

public class CycleSlideHandler : AbstractHandler<CycleSlideCommand>
{
    public CycleSlideHandler(ControlPanel controlControlPanel) : base(controlControlPanel)
    {
    }

    public override void Handle(CycleSlideCommand command)
    {
        var slideTray = ControlPanel.CurrentSlideTray;
        var prevSlide = slideTray.CurrentSlide;

        if (command.Direction == SlideMovement.Down)
            slideTray.CycleDown();
        else
            slideTray.CycleUp();

        var currSlide = slideTray.CurrentSlide;
        Logger.Debug($"Cycle slide: {prevSlide} -> {currSlide}");
    }
}