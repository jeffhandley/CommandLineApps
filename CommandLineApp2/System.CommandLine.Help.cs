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
    }

    public class CliHelpOption : CliOption<bool>
    {
        public CliHelpOption() : base("help", 'h')
        {
        }

        public bool ShowIfNeeded(CliParseResult cli) => false;
    }
}