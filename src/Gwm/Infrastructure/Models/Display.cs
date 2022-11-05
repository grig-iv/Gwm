using gwm.Core.Models;
using Vanara.PInvoke;

namespace gwm.Infrastructure.Models;

public class Display : IDisplay
{
    private readonly HMONITOR _handle;

    public Display(HMONITOR monitorHandle)
    {
        _handle = monitorHandle;
    }

    public IEnumerable<IWindow> GetWindows()
    {
        var result = new List<IWindow>();
        
        User32.EnumWindows((winHandle, _) =>
        {
            var window = new Window(winHandle);
            if (IsDesktopWindows(window) && IsOnDisplay(window))
                result.Add(window);

            return true;
        }, IntPtr.Zero);

        return result;
    }

    public override string ToString()
    {
        return $"Monitor '{_handle}'";
    }

    private static bool IsDesktopWindows(Window window)
    {
        if (!window.IsVisible || window.IsPopUp)
            return false;

        var showStatus = window.GetPlacement().showCmd;
        return new[]
        {
            ShowWindowCommand.SW_MAXIMIZE,
            ShowWindowCommand.SW_SHOWMAXIMIZED,
            ShowWindowCommand.SW_NORMAL,
            ShowWindowCommand.SW_SHOWNORMAL,
            ShowWindowCommand.SW_MINIMIZE,
            ShowWindowCommand.SW_SHOWMINIMIZED,
        }.Any(x => x == showStatus);
    }

    private bool IsOnDisplay(Window window)
    {
        return window.GetDisplay().Equals(this);
    }

    public override bool Equals(object? obj) => obj is Display other && other._handle == _handle;
    public override int GetHashCode() => _handle.GetHashCode();
}