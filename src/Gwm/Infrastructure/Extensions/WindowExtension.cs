using gwm.Core.Models;
using gwm.Infrastructure.Models;

namespace gwm.Infrastructure.Extensions;

public static class WindowExtension
{
    public static WindowSlide ToSlide(this IWindow window) => new WindowSlide(window);
}