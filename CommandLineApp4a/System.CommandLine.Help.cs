namespace System.CommandLine
{
    public static partial class CliHelp
    {
        public static bool ShowIfNeeded(CliParseResult result, out int exitCode)
        {
            var help = new CliHelpOption();
            return help.ShowIfNeeded(result, out exitCode);
        }

        public static CliHelpOption AddHelp(this Cli cli)
        {
            var help = new CliHelpOption();
            cli.Add(help);

            return help;
        }
    }

    public partial class CliHelpOption : CliOption<bool>
    {
        public bool ShowIfNeeded(CliParseResult result, out int exitCode)
        {
            exitCode = 0;

            if (result.HasErrors)
            {
                // Show errors and the option for getting help
                exitCode = 1;
                return true;
            }
            else if (result.HasOption(this.Name))
            {
                // Show help
                return true;
            }

            return false;
        }
    }
}
