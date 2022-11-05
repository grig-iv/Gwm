using gwm.Core.Models;

namespace gwm.Infrastructure.Models;

public readonly struct WindowSlide : ISlide
{
    public WindowSlide(IWindow window)
    {
        Window = window;
    }

    public IWindow Window { get; }

    public void Hide()
    {
        Window.Minimize();
    }

    public void Show()
    {
        Window.Maximize();
    }

    public override string ToString()
    {
        return $"WindowSlide of {Window.GetTitleName()}";
    }
}