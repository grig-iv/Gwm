using Functional.Maybe;
using gwm.Core.Models;
using gwm.Core.Services;
using gwm.Infrastructure.Models;
using Vanara.PInvoke;

namespace gwm.Infrastructure.Services;

public class DesktopService : IDesktopService
{
    public IEnumerable<IDisplay> GetDisplays()
    {
        var monitors = new List<IDisplay>();
        
        User32.EnumDisplayMonitors(HDC.NULL, null, (monitorHandle, _, _, _) =>
        {
            monitors.Add(new Display(monitorHandle));
            return true;
        }, IntPtr.Zero);
        
        return monitors;
    }

    public IDisplay GetMainDisplay()
    {
        var monitorHandle = User32.MonitorFromPoint( new POINT(0, 0), User32.MonitorFlags.MONITOR_DEFAULTTOPRIMARY);
        return new Display(monitorHandle);
    }

    public Maybe<IWindow> GetFocusedWindow()
    {
        return User32.GetForegroundWindow().ToMaybe()
            .Where(handle => handle != HWND.NULL)
            .Select(handle => new Window(handle) as IWindow);
    }
}