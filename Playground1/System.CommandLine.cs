using System.Runtime.CompilerServices;

namespace System.CommandLine;

internal class Parser
{
    public static void ParseArgs(string[] args, IEnumerable<CliSymbol> symbols)
    {

    }

    internal static void ParseArgs<T1>(string[] args, out T1 arg1, [CallerArgumentExpression(nameof(arg1))] string arg1Name = null!)
    {
        Console.WriteLine($"arg1Name: {arg1Name}");
        arg1 = default(T1)!;
    }
}

public class CliSymbol
{

}

public class CliOption<T> : CliSymbol
{
    public CliOption(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public string? Key { get; set; }
}

public class CliArgument<T> : CliSymbol
{
    public string? Key { get; set; }
}
