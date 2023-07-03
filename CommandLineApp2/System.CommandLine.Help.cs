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

        public bool IsNeeded(CliParseResult result) => result.HasErrors || result.HasOption(this.Name);

        public bool ShowIfNeeded(CliParseResult result)
        {
            if (result.HasErrors)
            {
                // Show errors and the option for getting help
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