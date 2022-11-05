using Gwm.Commands;
using Gwm.Commands.Enums;
using gwm.Core.Models;

namespace gwm.Infrastructure.Handlers;

public class CycleMonitorHandler : AbstractHandler<CycleMonitorCommand>
{
    public CycleMonitorHandler(ControlPanel controlControlPanel) : base(controlControlPanel)
    {
    }

    public override void Handle(CycleMonitorCommand command)
    {
        var trays = ControlPanel.SlideTrays;
        if (command.Direction == MonitorMovement.Prev)
            trays = trays.Reverse();

        ControlPanel.CurrentSlideTray = trays
            .SkipWhile(tray => tray != ControlPanel.CurrentSlideTray).Skip(1)
            .FirstOrDefault(trays.First());
        
        ControlPanel.CurrentSlideTray.CurrentSlide.Show();
    }
}