using Gwm.Commands;
using gwm.Core.Models;

namespace gwm.Infrastructure.Handlers;

public class SwitchToLastHandler : AbstractHandler<SwitchToLastCommand>
{
    public SwitchToLastHandler(ControlPanel controlControlPanel) : base(controlControlPanel)
    {
    }

    public override void Handle(SwitchToLastCommand command)
    {
    }
}