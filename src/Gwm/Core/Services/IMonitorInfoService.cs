using gwm.Core.Models;

namespace gwm.Core.Services;

public interface IMonitorInfoService
{
    IEnumerable<IDisplay> GetDisplays();
    IDisplay GetMainDisplay();
}