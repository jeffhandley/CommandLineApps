namespace System.CommandLineApp
{
    public static class CliParser
    {
        public static CliParseResult Parse(IEnumerable<string> args) => new CliParseResult(args);

        public static CliParseResult Parse(this Cli cli, IEnumerable<string> args) => new CliParseResult(cli, args);
    }
}
