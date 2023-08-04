namespace System.CommandLineApp
{
    public abstract class CliOption : CliSymbol
    {
        public CliOption(string name, IEnumerable<char> shortNames, IEnumerable<string> aliases)
        {
            Name = name;
            ShortNames = shortNames;
            Aliases = aliases;
        }

        public string Name { get; set; }
        public IEnumerable<string> Aliases { get; set; }
        public IEnumerable<char> ShortNames { get; set; }
        public string? Description { get; set; }
    }

    public partial class CliOption<T> : CliOption
    {
        public CliOption(string name) : base(name, Enumerable.Empty<char>(), Enumerable.Empty<string>()) { }
        public CliOption(string name, char shortName) : base(name, new[] { shortName }, Enumerable.Empty<string>()) { }
        public CliOption(string name, IEnumerable<string> aliases) : base(name, Enumerable.Empty<char>(), aliases) { }
        public CliOption(string name, char shortName, IEnumerable<string> aliases) : base(name, new[] { shortName }, aliases) { }
        public CliOption(string name, IEnumerable<char> shortNames, IEnumerable<string> aliases) : base(name, shortNames, aliases) { }
    }
}
