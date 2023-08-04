using System.CommandLineApp;

namespace System.CommandLine.Actions
{
    public class CliActionResult
    {
        public CliActionResult(CliParseResult result, System.CommandLineApp.CliSymbol? invokedSymbol, int exitCode)
        {
            ParseResult = result;
            InvokedSymbol = invokedSymbol;
            ExitCode = exitCode;
        }

        public CliParseResult ParseResult { get; private init; }
        public System.CommandLineApp.CliSymbol? InvokedSymbol { get; private init; }
        public int ExitCode { get; private init; }
    }
}
