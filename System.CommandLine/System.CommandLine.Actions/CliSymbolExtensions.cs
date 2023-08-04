using System.CommandLineApp;

namespace System.CommandLine.Actions
{
    public static class CliSymbolExtensions
    {
        public static void SetAction(this System.CommandLineApp.CliCommand command, Func<CliParseResult, int> action)
        {
            System.CommandLineApp.CliSymbol? symbol = command.Parent;
            for (; symbol is not null && symbol is not ActionCli; symbol = symbol.Parent) ;

            if (symbol is ActionCli cli)
            {
                cli.SetAction(command, action);
            }
        }
    }
}
