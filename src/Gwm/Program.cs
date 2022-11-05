// See https://aka.ms/new-console-template for more information

using System.Net;
using gwm.Infrastructure.Services;
using Serilog;

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

var desktopService = new DesktopService();

var gwm = new gwm.Core.Gwm(
    new CommandReceiver(IPAddress.Any, 8698, logger),
    new CommandHandlerBuilder(desktopService),
    desktopService,
    logger);

gwm.Run();