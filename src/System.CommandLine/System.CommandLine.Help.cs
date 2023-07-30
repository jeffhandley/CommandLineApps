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
            cli.Add(help);

            return help;
        }
    }

    public partial class CliHelpOption : CliOption<bool>
    {
        public CliHelpOption() : base("help")
        {
            ShortNames = new[] { 'h', '?' };
        }

        public bool IsNeeded(CliParseResult result) => result.HasErrors || result.HasFlag(this.Name);

        public bool ShowIfNeeded(CliParseResult result)
        {
            if (IsNeeded(result))
            {
                if (result.HasErrors)
                {
                    // Show errors and help
                    return true;
                }
                else if (result.HasFlag(this.Name))
                {
                    // Show help
                    return true;
                }
            }

            return false;
        }

        public bool ShowIfNeeded(CliParseResult result, out int exitCode)
        {
            exitCode = 0;

            if (result.HasErrors)
            {
                // Show errors and the option for getting help
                exitCode = 1;
                return true;
            }
            else if (result.HasFlag(this.Name))
            {
                // Show help
                return true;
            }

            return false;
        }
    }
}
