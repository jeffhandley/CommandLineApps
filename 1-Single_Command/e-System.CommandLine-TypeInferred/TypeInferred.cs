internal static class ParseResultContainer
{
    internal static System.CommandLine.ParseResult? ParseResult;
}

public class CliOption<T> : System.CommandLine.CliOption<T>
{
    public CliOption(string name, params string[] aliases) : base(name, aliases) { }

    public static implicit operator T(CliOption<T> option)
    {
        if (ParseResultContainer.ParseResult is null)
        {
            throw new InvalidOperationException("Cannot retrieve the option value until parsing is completed.");
        }

        return ParseResultContainer.ParseResult.GetValue(option) ?? default(T)!;
    }

    public static implicit operator int(CliOption<T> option)
    {
        if (ParseResultContainer.ParseResult is null)
        {
            throw new InvalidOperationException("Cannot retrieve the option value until parsing is completed.");
        }

        CliOption baseOption = option;
        return ParseResultContainer.ParseResult.GetResult(baseOption)!.GetValueOrDefault<int>();
    }
}

public class CliArgument<T> : System.CommandLine.CliArgument<T>
{
    public CliArgument(string name) : base(name) { }

    public static implicit operator T(CliArgument<T> argument)
    {
        if (ParseResultContainer.ParseResult is null)
        {
            throw new InvalidOperationException("Cannot retrieve the argument value until parsing is completed.");
        }

        return ParseResultContainer.ParseResult.GetValue(argument) ?? default(T)!;
    }
}

public class CliRootCommand : System.CommandLine.CliRootCommand
{
    public ParseResult Parse(IEnumerable<string> args)
    {
        ParseResultContainer.ParseResult = base.Parse(args.ToList());

        return ParseResultContainer.ParseResult;
    }
}
