using System.CommandLine.Parsing;
namespace System.CommandLine;

internal static class CommandLineExtensions
{
    public static bool ShouldTerminate(this ParseResult parseResult)
    {
        return parseResult.Action is not null;
    }

    public static int Terminate(this ParseResult parseResult, string? message = null)
    {
        if (message is not null)
        {
            Console.Error.WriteLine(message);
            return 1;
        }
        
        return parseResult.Invoke();
    }

    public static T GetOption<T>(this ParseResult parseResult, string name)
    {
        return parseResult.GetValue((CliOption<T>)(
            parseResult.CommandResult.Command.Options.FirstOrDefault(o => o.Name == $"--{name}") ??
            parseResult.RootCommandResult.Command.Options.First(o => o.Name == $"--{name}")
        ))!;
    }

    public static T GetArgument<T>(this ParseResult parseResult, string name)
    {
        return parseResult.GetValue((CliArgument<T>)(
            parseResult.CommandResult.Command.Arguments.FirstOrDefault(o => o.Name == name) ??
            parseResult.RootCommandResult.Command.Arguments.First(o => o.Name == name)
        ))!;
    }
}
