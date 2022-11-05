using Functional.Maybe;
using gwm.Core.Models;

namespace gwm.Infrastructure.Extensions;

public static class ControlPanelExtension
{
    public static IEnumerable<ISlide> Slides(this ControlPanel controlPanel)
    {
        return controlPanel.SlideTrays.SelectMany(s => s);
    }

    public static Maybe<IDisplay> GetMonitorBySlide(this ControlPanel controlPanel, ISlide slide)
    {
        return controlPanel.SlideTrays
            .FirstMaybe(tray => tray.Contains(slide))
            .Select(tray => controlPanel.GetMonitorBy(tray));
    }
}