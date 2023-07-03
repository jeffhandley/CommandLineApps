namespace System.CommandLine
{
    public static class CliHelp
    {
        public static CliHelpOption AddHelp(this Cli cli)
        {
            var help = new CliHelpOption();
            cli.AddOption(help);

            return help;
        }

        public static bool ShowIfNeeded(CliParseResult result) => ShowIfNeeded(result, out _);

        public static bool ShowIfNeeded(CliParseResult result, out int exitCode)
        {
            var help = new CliHelpOption();
            return help.ShowIfNeeded(result, out exitCode);
        }
    }

    public class CliHelpOption : CliOption<bool>
    {

        public CliHelpOption() : base("help", 'h')
        {

        }

        public bool IsNeeded(CliParseResult result) => result.HasErrors || result.HasOption(this.Name);

        public bool ShowIfNeeded(CliParseResult result) => ShowIfNeeded(result, out _);

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
