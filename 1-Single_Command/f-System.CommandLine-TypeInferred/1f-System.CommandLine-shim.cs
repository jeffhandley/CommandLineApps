using System.Collections.ObjectModel;
using System.CommandLine.Parsing;

public class Cli : System.CommandLine.CliRootCommand
{
    public ParseResult Parse(string[] args)
    {
        return new ParseResult(base.Parse(args));
    }

    public CliOption<T> AddOption<T>(string name, bool required = false)
    {
        var option = new CliOption<T>($"--{name}", new[] { $"-{name[0]}" }) { Required = required };
        Options.Add(option);

        return option;
    }

    public CliArgument<T> AddArgument<T>()
    {
        var argName = $"arg{Arguments.Count}";
        var argument = new CliArgument<T>(argName);
        Arguments.Add(argument);

        return argument;
    }
}

public class ParseResult
{
    private System.CommandLine.ParseResult _result;
    public ParseResult(System.CommandLine.ParseResult result) => _result = result;

    public OptionValueGetter this[CliOption option] => new(_result.GetResult(option), option);
    public ArgumentValueGetter this[CliArgument argument] => new(_result.GetResult(argument), argument);

    public struct OptionValueGetter
    {
        private OptionResult? _result;
        private CliOption _option;

        internal OptionValueGetter(OptionResult? result, CliOption option)
        {
            _result = result;
            _option = option;
        }

        public static T EnforceRequired<T>(CliOption option)
        {
            if (option.Required)
            {
                throw new ArgumentNullException(option.Name);
            }

            return default!;
        }

        public static implicit operator bool(OptionValueGetter getter) => getter._result?.GetValueOrDefault<bool>() ?? EnforceRequired<bool>(getter._option);
        public static implicit operator string(OptionValueGetter getter) => getter._result?.GetValueOrDefault<string>() ?? EnforceRequired<string>(getter._option);
        public static implicit operator int?(OptionValueGetter getter) => getter._result?.GetValueOrDefault<int?>() ?? EnforceRequired<int?>(getter._option);
        public static implicit operator int(OptionValueGetter getter) => getter._result?.GetValueOrDefault<int>() ?? EnforceRequired<int>(getter._option);
    }

    public struct ArgumentValueGetter
    {
        private ArgumentResult? _result;
        private CliArgument _argument;

        internal ArgumentValueGetter(ArgumentResult? result, CliArgument argument)
        {
            _result = result;
            _argument = argument;
        }

        public static implicit operator string[](ArgumentValueGetter getter) => getter._result?.GetValueOrDefault<string[]>() ?? default!;
    }
}