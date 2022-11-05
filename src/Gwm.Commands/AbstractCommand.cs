namespace Gwm.Commands;

public abstract class AbstractCommand
{
    public string Type => GetType().Name;
}