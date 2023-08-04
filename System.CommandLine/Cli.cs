namespace System.CommandLine
{
    public partial class Cli : CliCommand
    {
        public Cli() : base(string.Empty) { }
        public CliDirective AddDirective(CliDirective directive) => directive;
    }
}
