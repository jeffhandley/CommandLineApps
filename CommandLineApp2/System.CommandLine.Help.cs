namespace System.CommandLine
{
    public static partial class CliHelp
    {
        public static bool ShowIfNeeded(CliParseResult result)
        {
            var help = new CliHelpOption();
            return help.ShowIfNeeded(result);
        }
    }

    public partial class CliHelpOption : CliOption<bool>
    {

        public CliHelpOption() : base("help", 'h') { }
        public bool IsNeeded(CliParseResult result) => result.HasErrors || result.HasOption(this.Name);

        public bool ShowIfNeeded(CliParseResult result)
        {
            if (IsNeeded(result))
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
            }

            return false;
        }
    }
}
