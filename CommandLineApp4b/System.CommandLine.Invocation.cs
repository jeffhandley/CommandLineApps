namespace System.CommandLine
{
    public static partial class CliInvocation
    {
        public static bool Invoke(this Cli cli, string[] args, Dictionary<CliCommand, Func<CliParseResult, int>> actions)
        {
            cli.AddHelp();
            cli.AddCompletion();

            var result = cli.Parse(args);

            if (CliCompletion.ShowIfNeeded(result)) return true;
            if (CliHelp.ShowIfNeeded(result)) return true;

            if (result.InvokedCommand is not null && actions.TryGetValue(result.InvokedCommand, out var action))
            {
                action(result);
                return true;
            }

            return false;
        }
    }
}
