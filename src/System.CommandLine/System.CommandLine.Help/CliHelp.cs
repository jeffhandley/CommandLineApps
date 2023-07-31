namespace System.CommandLine
{
    public static partial class CliHelp
    {
        public static bool ShowIfNeeded(CliParseResult result)
        {
            var help = new CliHelpOption();
            return help.ShowIfNeeded(result);
        }

        public static int ShowError(CliParseException parseException)
        {
            // Show errors and help
            return 1;
        }

        public static bool ShowIfNeeded(CliParseResult result, out int exitCode)
        {
            var help = new CliHelpOption();
            return help.ShowIfNeeded(result, out exitCode);
        }

        public static CliHelpOption AddHelp(this Cli cli)
        {
            var help = new CliHelpOption();
            cli.AddOption(help);

            return help;
        }
    }
}
