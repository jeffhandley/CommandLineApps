namespace System.CommandLine.ModelBinding
{
    public class ModelBoundResult<TCommands>
    {
        public ModelBoundResult(CliParseResult result, TCommands commands)
        {
            ParseResult = result;
            Commands = commands;
        }

        public CliParseResult ParseResult { get; private init; }

        public TCommands Commands { get; private init; }

        public static implicit operator CliParseResult(ModelBoundResult<TCommands> result) => result.ParseResult;
    }
}
