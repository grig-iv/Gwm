using Functional.Maybe;
using Gwm.Commands;
using gwm.Core.Models;
using gwm.Core.Services;
using gwm.Infrastructure.Extensions;

namespace gwm.Infrastructure.Handlers;

public class ToggleCaptureHandler : AbstractHandler<ToggleCaptureCommand>
{
    private readonly IWindowsProvider _windowsProvider;

    public ToggleCaptureHandler(ControlPanel controlControlPanel, IWindowsProvider windowsProvider) : base(controlControlPanel)
    {
        _windowsProvider = windowsProvider;
    }

    public override void Handle(ToggleCaptureCommand command)
    {
        _windowsProvider
            .GetFocusedWindow()
            .Do(window =>
            {
                if (IsCaptured(window))
                    Release(window);
                else
                    Capture(window);
            });
    }

    private bool IsCaptured(IWindow window)
    {
        return ControlPanel.Slides()
            .Contains(window.ToSlide());
    }

    private void Capture(IWindow window)
    {
        var windowMonitor = window.GetDisplay();
        var monitorSlides = ControlPanel.GetSlideTrayBy(windowMonitor);
        var newSlide = window.ToSlide();
        monitorSlides.Add(newSlide);
        monitorSlides.Show(newSlide);
        ControlPanel.CurrentSlideTray = monitorSlides;
    }

    private void Release(IWindow window)
    {
        var windowSlide = window.ToSlide();
        ControlPanel.SlideTrays
            .First(tray => tray.Contains(windowSlide))
            .Remove(windowSlide);
        window.Normalize();
    }
}