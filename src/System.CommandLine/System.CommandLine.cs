using System.Collections.ObjectModel;

namespace System.CommandLine
{
    public static partial class CliParser
    {
        public static CliParseResult Parse(IEnumerable<string> args) => new CliParseResult { args = args };
        public static CliParseResult Parse(this Cli cli, IEnumerable<string> args) => new CliParseResult { args = args };
    }

    public partial struct CliParseResult
    {
        internal IEnumerable<string> args { get; init; } = Enumerable.Empty<string>();
        public CliParseResult() { }
        public T GetOption<T>(string name, char? abbr = null) => default!;
        public T GetOption<T>(CliOption<T> option) => default(T)!;
        public IEnumerable<T> GetArguments<T>(ushort minArgs, ushort maxArgs = 0) => Enumerable.Empty<T>();
        public IEnumerable<T> GetArguments<T>(CliArgument<T> argument) => Enumerable.Empty<T>();
        public bool HasDirective(string name) => args.Contains($"[{name}]");
        public bool HasDirective(CliDirective directive) => HasDirective(directive.Name);
        public bool HasCommand(string name) => args.Contains(name);
        public bool HasCommand(CliCommand command) => HasCommand(command.Name);
        public bool HasOption(string name) => args.Contains($"--{name}");
        public bool HasOption(CliOption option) => HasOption(option.Name);
        public bool HasErrors { get; } = false;
    }

    public abstract partial class CliSymbol
    {
        public CliSymbol? Parent { get; set; }
    }

    public partial class Cli : CliCommand
    {
        public Cli() : base(string.Empty) { }
        public CliDirective AddDirective(CliDirective directive) => directive;
    }

    public partial class CliCommand : CliSymbol
    {
        public CliCommand(string name) => Name = name;
        public string Name { get; set; }
        public string? Description { get; set; }
        public CliCommand AddCommand(CliCommand command)
        {
            command.Parent = this;
            return command;
        }

        public CliCommand AddCommand(string name) => AddCommand(new CliCommand(name));

        public CliArgument<T> AddArgument<T>(CliArgument<T> argument)
        {
            argument.Parent = this;
            return argument;
        }

        public CliArgument<T> AddArguments<T>(ushort minArgs = 0, ushort maxArgs = 0) => AddArgument(new CliArgument<T>(minArgs, maxArgs));

        public CliOption<T> AddOption<T>(CliOption<T> option)
        {
            option.Parent = this;
            return option;
        }

        public CliOption<T> AddOption<T>(string name, char abbr) => AddOption(new CliOption<T>(name, abbr));
    }

    public abstract class CliOption : CliSymbol
    {
        public CliOption(string name, IEnumerable<string> aliases, IEnumerable<char> shortNames)
        {
            Name = name;
            Aliases = aliases;
            ShortNames = shortNames;
        }

        public string Name { get; set; }
        public IEnumerable<string> Aliases { get; set; }
        public IEnumerable<char> ShortNames { get; set; }
        public string? Description { get; set; }
    }

    public partial class CliOption<T> : CliOption
    {
        public CliOption(string name) : base(name, Enumerable.Empty<string>(), Enumerable.Empty<char>()) { }
        public CliOption(string name, char shortName) : base(name, Enumerable.Empty<string>(), new[] { shortName }) { }
        public CliOption(string name, IEnumerable<string> aliases) : base(name, aliases, Enumerable.Empty<char>()) { }
        public CliOption(string name, IEnumerable<string> aliases, char shortName) : base(name, aliases, new[] { shortName }) { }
        public CliOption(string name, IEnumerable<string> aliases, IEnumerable<char> shortNames) : base(name, aliases, shortNames) { }
    }

    public partial class CliDirective : CliSymbol
    {
        public string Name { get; set; }
        public CliDirective(string name) => Name = name;
    }

    public partial class CliParseException : Exception { }

    public partial class CliArgument<T> : CliSymbol
    {
        public string? Description { get; set; }
        public ushort MinArgs { get; set; }
        public ushort MaxArgs { get; set; }
        public CliArgument(ushort minArgs = 0, ushort maxArgs = 0)
        {
            MinArgs = minArgs;
            MaxArgs = maxArgs;
        }
    }
}
