namespace System.CommandLine.Actions
{
    public static class CliSymbolExtensions
    {
        public static void SetAction(this CliCommand command, Func<CliParseResult, int> action)
        {
            CliSymbol? symbol = command.Parent;
            for (; symbol is not null && symbol is not ActionCli; symbol = symbol.Parent) ;

            if (symbol is ActionCli cli)
            {
                cli.SetAction(command, action);
            }
        }
    }
}
