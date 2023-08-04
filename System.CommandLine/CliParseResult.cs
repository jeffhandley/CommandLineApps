using System.CommandLine;
using System.Linq;

namespace System.CommandLineApp
{
    public struct CliParseResult
    {
        private IReadOnlyList<string> _args;
        private CliRootCommand RootCommand;
        private Dictionary<string, System.CommandLine.CliCommand> Commands = new();
        private Dictionary<string, System.CommandLine.CliOption> Options = new();
        private ushort ArgumentCount = 0;

        public CliParseResult() => throw new ArgumentNullException("args");
        public CliParseResult(IEnumerable<string> args)
        {
            _args = args.ToList();

            RootCommand = new CliRootCommand { TreatUnmatchedTokensAsErrors = false };
            RootCommand.Options.Clear();
        }

        public CliParseResult(Cli cli, IEnumerable<string> args) : this(args)
        {
            foreach (var command in cli.Commands)
            {
                AddCommand(command.Name);
            }
        }

        public T GetOption<T>(string name, char? abbr = null) => GetOptionValue<T>(name, Enumerable.Empty<string>(), new[] { name[0] })!;
        public T GetOption<T>(CliOption<T> option) => GetOptionValue<T>(option.Name, option.Aliases, option.ShortNames);

        public IEnumerable<T> GetArguments<T>(ushort minArgs, ushort maxArgs = 0) => GetArgumentValues<T>(minArgs, maxArgs);
        public IEnumerable<T> GetArguments<T>(CliArgument<T> argument) => GetArguments<T>(argument.MinArgs, argument.MaxArgs);

        public bool HasDirective(string name) => _args.Contains($"[{name}]");
        public bool HasDirective(CliDirective directive) => HasDirective(directive.Name);

        public bool HasCommand(CliCommand command) => HasCommand(command.Name);
        public bool HasErrors { get; } = false;

        public bool HasCommand(string name)
        {
            if (Commands.TryGetValue(name, out var command))
            {
                var result = RootCommand.Parse(_args);
                return result.CommandResult.Command.Name == command.Name;
            }

            AddCommand(name);

            return HasCommand(name);
        }

        private void AddCommand(string name)
        {
            var cliCommand = new System.CommandLine.CliCommand(name) { TreatUnmatchedTokensAsErrors = false };
            RootCommand.Add(cliCommand);
            Commands.Add(name, cliCommand);
        }

        private T GetOptionValue<T>(string name, IEnumerable<string> aliases, IEnumerable<char> shortNames)
        {
            if (Options.TryGetValue(name, out var option))
            {
                var result = RootCommand.Parse(_args);
                return result.GetValue<T>((System.CommandLine.CliOption<T>)option!) ?? default(T)!;
            }

            AddOption<T>(name, aliases, shortNames);

            return GetOptionValue<T>(name, aliases, shortNames);
        }

        private void AddOption<T>(string name, IEnumerable<string> aliases, IEnumerable<char> shortNames)
        {
            List<string> allAliases = new[] { $"-{name[0]}" }.Union(aliases.Select(a => $"--{a}")).Union(shortNames.Select(a => $"-{a}")).ToList();

            var cliOption = new System.CommandLine.CliOption<T>($"--{name}", allAliases.ToArray()) { Recursive = true };
            RootCommand.Add(cliOption);
            Options.Add(name, cliOption);
        }

        private IEnumerable<T> GetArgumentValues<T>(ushort minArgs, ushort maxArgs)
        {
            var result = RootCommand.Parse(_args);
            var argsExcludingSubsequentOptions = result.Tokens.Select(t => t.Value).ToList().Except(result.UnmatchedTokens).Union(result.UnmatchedTokens.TakeWhile(a => !a.StartsWith("-")));

            var argument = new System.CommandLine.CliArgument<IEnumerable<T>>($"arg{ArgumentCount++}")
            {
                Arity = ArgumentArity.ZeroOrMore
            };

            RootCommand.Add(argument);

            foreach (var command in Commands.Values)
            {
                command.Add(argument);
            }

            result = RootCommand.Parse(argsExcludingSubsequentOptions.ToList());

            return result.GetValue(argument) ?? Enumerable.Empty<T>();
        }

        public OptionValueGetter this[string name] => new OptionValueGetter(this, name);
        public ArgumentValueGetter Arguments => new ArgumentValueGetter(this);

        public OptionValueGetter GetOption(string name) => new OptionValueGetter(this, name);
        public ArgumentValueGetter GetArguments() => new ArgumentValueGetter(this);

        public struct OptionValueGetter
        {
            private CliParseResult _result;
            private string _name;

            internal OptionValueGetter(CliParseResult result, string name)
            {
                _result = result;
                _name = name;
            }

            public static implicit operator bool(OptionValueGetter getter) => getter._result.GetOption<bool>(getter._name);
            public static implicit operator string(OptionValueGetter getter) => getter._result.GetOption<string>(getter._name);
            public static implicit operator int(OptionValueGetter getter) => getter._result.GetOption<int>(getter._name);
            public static implicit operator int?(OptionValueGetter getter) => getter._result.GetOption<int?>(getter._name);
        }

        public struct ArgumentValueGetter
        {
            private CliParseResult _result;
            internal ArgumentValueGetter(CliParseResult result) => _result = result;

            public static implicit operator string[](ArgumentValueGetter getter) => getter._result.GetArguments<string>(minArgs: 1).ToArray();
        }
    }
}
