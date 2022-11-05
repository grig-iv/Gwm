namespace gwm.Core.Models;

public interface IDisplay
{
    IEnumerable<IWindow> GetWindows();
}