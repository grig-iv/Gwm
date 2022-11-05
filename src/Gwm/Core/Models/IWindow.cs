using gwm.Core.Domain.Enums;

namespace gwm.Core.Models;

public interface IWindow 
{
    IDisplay GetDisplay();

    WindowState GetState();
    
    void Minimize();
    void Maximize();
    void Normalize();
    
    void Focus();
    void Unfocus();
    string GetTitleName();
}