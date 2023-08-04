using System.CommandLine;

internal static class CliParser
{
    public static CliParseResult Parse(IEnumerable<string> args)
    {
        return new CliParseResult(args);
    }
}

internal class CliParseResult
{
    public CliParseResult(IEnumerable<string> args)
    {
        _args = args.ToList();
    }

    private IReadOnlyList<string> _args;
    private CliRootCommand RootCommand = new() { TreatUnmatchedTokensAsErrors = false };
    private ParseResult? Result;
    private Dictionary<string, CliOption> Options = new();
    private ushort ArgumentCount = 0;

    public T GetOption<T>(string name)
    {
        if (Result is not null && Options.TryGetValue(name, out var option))
        {
            return Result.GetValue<T>((CliOption<T>)option!) ?? default(T)!;
        }

        var cliOption = new CliOption<T>($"--{name}", new[] { $"-{name[0]}" });
        RootCommand.Options.Add(cliOption);
        Options.Add(name, cliOption);
        Result = RootCommand.Parse(_args);
        
        return GetOption<T>(name);
    }

    public IEnumerable<T> GetArguments<T>(ushort minArgs = 0, ushort maxArgs = 0)
    {
        var argument = new CliArgument<IEnumerable<T>>($"arg{ArgumentCount++}")
        {
            Arity = new ArgumentArity(minArgs, maxArgs > 0 ? maxArgs : ushort.MaxValue)
        };

        RootCommand.Arguments.Add(argument);

        var remainingArgs = Result is null ? _args : Result.UnmatchedTokens;
        Result = RootCommand.Parse(remainingArgs.TakeWhile(a => !a.StartsWith("-")).ToList());

        return Result.GetValue(argument) ?? Enumerable.Empty<T>();
    }
}
