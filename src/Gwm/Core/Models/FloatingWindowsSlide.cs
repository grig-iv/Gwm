using Functional.Maybe;
using gwm.Core.Domain.Enums;
using gwm.Core.Services;

namespace gwm.Core.Models;

public class FloatingWindowsSlide : ISlide
{
    private readonly IDisplay _ownerDisplay;
    private readonly IDesktopService _desktopService;

    public FloatingWindowsSlide(IDisplay ownerDisplay, IDesktopService desktopService)
    {
        _ownerDisplay = ownerDisplay;
        _desktopService = desktopService;
    }

    public void Hide()
    {
    }

    public void Show()
    {
        FocusTopWindow();
    }

    private void FocusTopWindow()
    {
        _ownerDisplay.GetWindows()
            .FirstMaybe(x => x.GetState() is WindowState.Maximize or WindowState.Normal)
            .Do(x => x.Focus());
    }

    public override string ToString()
    {
        return $"FloatingWindowSlide of {_ownerDisplay}";
    }
}