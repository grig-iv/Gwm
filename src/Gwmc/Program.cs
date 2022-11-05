using System.Net;
using System.Net.Sockets;
using Gwm.Commands;
using Gwm.Commands.Enums;
using Gwm.Commands.Serialize;

var cmd = GetCommand();
if (cmd is null)
    NotifyUnknownCommand();
else
    SendCommand(cmd);

AbstractCommand? GetCommand()
{
    return string.Join(" ", args) switch
    {
        "toggle-capture" => new ToggleCaptureCommand(),
        "switch-to-last" => new SwitchToLastCommand(),
        "cycle-slide down" => new CycleSlideCommand { Direction = SlideMovement.Down },
        "cycle-slide up" => new CycleSlideCommand { Direction = SlideMovement.Up },
        "cycle-monitor prev" => new CycleMonitorCommand { Direction = MonitorMovement.Prev },
        "cycle-monitor next" => new CycleMonitorCommand { Direction = MonitorMovement.Next },
        "move-slide down" => new MoveSlideCommand { Direction = SlideMovement.Down },
        "move-slide up" => new MoveSlideCommand { Direction = SlideMovement.Up },
        "move-slide-to-monitor prev" => new MoveSlideToMonitorCommand { Direction = MonitorMovement.Prev },
        "move-slide-to-monitor next" => new MoveSlideToMonitorCommand { Direction = MonitorMovement.Next },
        _ => null,
    };
}

void SendCommand(AbstractCommand command)
{
    var client = new TcpClient();
    var socket = client.Client;
    var endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8698);
    var rawCmd = CommandSerializer.Serialize(command);
    client.Connect(endPoint);
    socket.Send(rawCmd);
}

void NotifyUnknownCommand()
{
    Console.WriteLine($"ERROR! Unknown command '{string.Join(" ", args)}'");
}
