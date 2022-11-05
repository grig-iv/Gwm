using System.Net;
using System.Net.Sockets;
using Gwm.Commands;
using Gwm.Commands.Serialize;
using gwm.Core.Services;
using Serilog;

namespace gwm.Infrastructure.Services;

public class CommandReceiver : ICommandReceiver
{
    private readonly TcpListener _server;
    private readonly ILogger _logger;

    public CommandReceiver(IPAddress ipAddress, ushort port, ILogger logger)
    {
        _server = new TcpListener(ipAddress, port);
        _logger = logger;
    }

    public IEnumerable<AbstractCommand> Receive()
    {
        _server.Start();

        while (true)
        {
            yield return AcceptClientAndReceiveCommand();
        }
    }

    private AbstractCommand AcceptClientAndReceiveCommand()
    {
        try
        {
            using var client = _server.AcceptTcpClient();
            using var socket = client.Client;
            return ReceiveFromSocket(socket);
        }
        catch (Exception e)
        {
            _logger.Error(e, "Command receiving exception");
            return AcceptClientAndReceiveCommand();
        }
    }

    private static AbstractCommand ReceiveFromSocket(Socket socket)
    {
        var buff = new byte[1024];
        socket.Receive(buff, SocketFlags.None);
        return CommandSerializer.Deserialize(buff);
    }
}