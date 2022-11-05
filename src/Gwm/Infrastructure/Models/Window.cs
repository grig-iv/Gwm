using System.Text;
using gwm.Core.Domain.Enums;
using gwm.Core.Models;
using Vanara.PInvoke;

namespace gwm.Infrastructure.Models;

public struct Window : IWindow
{
    private User32.WINDOWINFO _info;

    public Window(HWND windowHandle)
    {
        Handle = windowHandle;
        _info = new User32.WINDOWINFO();
    }
    
    public HWND Handle { get; }

    public User32.WINDOWINFO Info
    {
        get
        {
            if (_info.cbSize == 0)
                User32.GetWindowInfo(Handle, ref _info);
            return _info;
        }
    }

    public bool IsVisible => Info.dwStyle.HasFlag(User32.WindowStyles.WS_VISIBLE);
    public bool IsPopUp => Info.dwStyle.HasFlag(User32.WindowStyles.WS_POPUP) || Info.dwStyle.HasFlag(User32.WindowStyles.WS_POPUPWINDOW);

    public IDisplay GetDisplay()
    {
        var monitorHandle = User32.MonitorFromWindow(Handle, User32.MonitorFlags.MONITOR_DEFAULTTONEAREST);
        return new Display(monitorHandle);
    }

    public WindowState GetState()
    {
        var placement = GetPlacement();
        switch (placement.showCmd)
        {
            case ShowWindowCommand.SW_SHOWNORMAL:
                return WindowState.Normal;
            case ShowWindowCommand.SW_SHOWMAXIMIZED:
                return WindowState.Maximize;
            case ShowWindowCommand.SW_SHOWMINIMIZED:
            case ShowWindowCommand.SW_MINIMIZE:
                return WindowState.Minimize;
            default:
                return WindowState.Other;
        }
    } 

    public void Minimize()
    {
        User32.ShowWindow(Handle, ShowWindowCommand.SW_SHOWMINIMIZED);
    }

    public void Maximize()
    {
        User32.ShowWindow(Handle, ShowWindowCommand.SW_SHOWMAXIMIZED);
    }

    public void Normalize()
    {
        User32.ShowWindow(Handle, ShowWindowCommand.SW_SHOWNORMAL);
    }

    public void Focus()
    {
        Console.WriteLine(GetTitleName());
        User32.SetForegroundWindow(Handle);
    }

    public void Unfocus()
    {
        User32.SetForegroundWindow(
            User32.GetDesktopWindow()
        );
    }

    public string GetTitleName()
    {
        var titleLength = User32.GetWindowTextLength(Handle);
        var titleSb = new StringBuilder(titleLength + 1);
        User32.GetWindowText(Handle, titleSb, titleLength + 1);
        return titleSb.ToString();
    }

    public User32.WINDOWPLACEMENT GetPlacement()
    {
        var placement = new User32.WINDOWPLACEMENT();
        User32.GetWindowPlacement(Handle, ref placement);
        return placement;
    }
}