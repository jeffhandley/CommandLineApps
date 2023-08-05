public class CliRootCommand : System.CommandLine.CliRootCommand
{
    public void SetAction(Action<ParseResult> action)
    {
        base.SetAction(result => action(new(result)));
    }
}

public class ParseResult
{
    private System.CommandLine.ParseResult _result;
    public ParseResult(System.CommandLine.ParseResult result) => _result = result;

    public OptionValueGetter this[CliOption option] => new(_result, option);
    public ArgumentValueGetter this[CliArgument argument] => new(_result, argument);

    public struct OptionValueGetter
    {
        private System.CommandLine.ParseResult _result;
        private CliOption _option;

        internal OptionValueGetter(System.CommandLine.ParseResult result, CliOption option)
        {
            _result = result;
            _option = option;
        }

        public static implicit operator bool(OptionValueGetter getter) => getter._result.GetValue((CliOption<bool>)getter._option);
        public static implicit operator string(OptionValueGetter getter) => getter._result.GetValue((CliOption<string>)getter._option)!;
        
        public static implicit operator int(OptionValueGetter getter)
        {
            if (getter._option is CliOption<int?>)
            {
                return getter._result.GetValue((CliOption<int?>)getter._option)!.Value;
            }

            return getter._result.GetValue((CliOption<int>)getter._option);
        }

        public static implicit operator int?(OptionValueGetter getter) => getter._result.GetValue<int?>((CliOption<int?>)getter._option);
    }

    public struct ArgumentValueGetter
    {
        private System.CommandLine.ParseResult _result;
        private CliArgument _argument;

        internal ArgumentValueGetter(System.CommandLine.ParseResult result, CliArgument argument)
        {
            _result = result;
            _argument = argument;
        }

        public static implicit operator string[](ArgumentValueGetter getter) => getter._result.GetValue<string[]>((CliArgument<string[]>)getter._argument)!;
    }
}