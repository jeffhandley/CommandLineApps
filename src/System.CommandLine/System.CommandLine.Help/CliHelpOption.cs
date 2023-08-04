namespace System.CommandLine
{
    public partial class CliHelpOption : CliOption<bool>
    {
        public CliHelpOption() : base("help")
        {
            ShortNames = new[] { 'h', '?' };
        }

        public bool IsNeeded(CliParseResult result) => result.HasErrors || result.GetOption<bool>(this.Name);

        public void Show(CliParseResult result)
        {
            Console.WriteLine("GENERATED HELP WOULD BE SHOWN");
        }

        public bool ShowIfNeeded(CliParseResult result)
        {
            if (IsNeeded(result))
            {
                if (result.HasErrors)
                {
                    // Show errors and help
                    Show(result);
                    return true;
                }
                else if (result.GetOption<bool>(this.Name))
                {
                    Show(result);
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
            else if (result.GetOption<bool>(this.Name))
            {
                // Show help
                Show(result);
                return true;
            }

            return false;
        }
    }
}
