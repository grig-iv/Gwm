using Functional.Maybe;
using gwm.Core.Models;

namespace gwm.Core.Services;

public interface IWindowsProvider
{
    Maybe<IWindow> GetFocusedWindow();
}