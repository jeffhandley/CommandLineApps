namespace System.CommandLineApp
{
    public partial class CliDirective : CliSymbol
    {
        public string Name { get; set; }
        public CliDirective(string name) => Name = name;
    }
}
