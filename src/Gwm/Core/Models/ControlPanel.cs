using Functional.Maybe;
using gwm.Core.Services;
using Serilog;

namespace gwm.Core.Models;

public class ControlPanel
{
    private readonly List<(IDisplay monitor, SlideTray slideTray)> _monitorSlides;
    
    public ControlPanel(IDesktopService desktopService, ILogger logger)
    {
        Logger = logger;
        _monitorSlides = desktopService
            .GetDisplays()
            .Select(m => (m, new SlideTray(new FloatingWindowsSlide(m, desktopService))))
            .ToList();

        var initMonitor = desktopService
            .GetFocusedWindow()
            .Select(window => window.GetDisplay())
            .OrElse(desktopService.GetMainDisplay());
        CurrentSlideTray = GetSlideTrayBy(initMonitor);
    }
    
    public ILogger Logger { get; }

    public IEnumerable<SlideTray> SlideTrays => _monitorSlides.Select(x => x.slideTray);
    public IEnumerable<IDisplay> Monitors => _monitorSlides.Select(x => x.monitor);

    public SlideTray GetSlideTrayBy(IDisplay display) => _monitorSlides.First(x => x.monitor.Equals(display)).slideTray;
    public IDisplay GetMonitorBy(SlideTray slideTray) => _monitorSlides.First(x => x.slideTray.Equals(slideTray)).monitor;
    
    public SlideTray CurrentSlideTray { get; set; }
}