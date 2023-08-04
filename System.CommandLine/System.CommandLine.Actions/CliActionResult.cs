namespace System.CommandLine.Actions
{
    public class CliActionResult
    {
        public CliActionResult(CliParseResult result, CliSymbol? invokedSymbol, int exitCode)
        {
            ParseResult = result;
            InvokedSymbol = invokedSymbol;
            ExitCode = exitCode;
        }

        public CliParseResult ParseResult { get; private init; }
        public CliSymbol? InvokedSymbol { get; private init; }
        public int ExitCode { get; private init; }
    }
}
