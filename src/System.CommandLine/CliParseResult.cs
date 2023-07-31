namespace System.CommandLine
{
    public struct CliParseResult
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
}
