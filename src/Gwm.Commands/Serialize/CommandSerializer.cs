using System.Text;
using Newtonsoft.Json;

namespace Gwm.Commands.Serialize;

public static class CommandSerializer
{
    private static readonly IEnumerable<Type> CommandTypes;

    static CommandSerializer()
    {
        CommandTypes = typeof(CommandSerializer).Assembly
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(AbstractCommand)) && !t.IsAbstract)
            .ToList();
    }

    public static byte[] Serialize(AbstractCommand command)
    {
        var jsonCmd = JsonConvert.SerializeObject(command, Formatting.None);
        return Encoding.ASCII.GetBytes(jsonCmd);
    }

    public static AbstractCommand Deserialize(Span<byte> rawCommand)
    {
        var jsonCmd = Encoding.ASCII.GetString(rawCommand);
        var commandBase = JsonConvert.DeserializeObject<CommandBase>(jsonCmd)!;
        var commandType = CommandTypes.FirstOrDefault(t => t.Name == commandBase.TypeName);
        if (commandType is null)
            throw new Exception($"Unknown command type '{commandBase.TypeName}'");
        
        return (AbstractCommand)JsonConvert.DeserializeObject(jsonCmd, commandType)!;
    }

    private class CommandBase
    {
        [JsonProperty("Type")] public string TypeName { get; set; }
    }
}