using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CommandLine
{
    public class Cli
    {
        public CliCommand AddCommand(string name) => new CliCommand(name);
        public CliOption<T> AddOption<T>(CliOption<T> option) => option;
        public CliOption<bool> AddOption(string name) => new CliOption<bool>(name);
        public CliOption<bool> AddOption(string name, char abbr) => new CliOption<bool>(name, abbr);
        public CliOption<T> AddOption<T>(string name) => new CliOption<T>(name);
        public CliOption<T> AddOption<T>(string name, char abbr) => new CliOption<T>(name, abbr);
        public CliGroup AddGroup(CliGroupType type) => new CliGroup(type);
        public CliArgument<T> AddArgument<T>(string name, ushort minArgs = 0, ushort maxArgs = 0) => new CliArgument<T>(name, minArgs, maxArgs);
        public CliDirective AddDirective(CliDirective directive) => directive;
    }

    public class CliCommand : Cli
    {
        public string Name { get; set; }

        public CliCommand(string name) => Name = name;
    }

    public class CliOption<T>
    {
        public string Name { get; set; }
        public char Abbr { get; set; }
        public Type OptionType => typeof(T);
        public CliOption(string name) => Name = name;
        public CliOption(string name, char abbr) : this(name) => Abbr = abbr;
    }

    public class CliArgument<T>
    {
        public string Name { get; set; }
        public ushort MinArgs { get; set; }
        public ushort MaxArgs { get; set; }
        public Type ArgumentType => typeof(T);
        public CliArgument(string name, ushort minArgs = 0, ushort maxArgs = 0)
        {
            Name = name;
            MinArgs = minArgs;
            MaxArgs = maxArgs;
        }
    }

    public class CliDirective
    {
        public string Name { get; set; }
        public CliDirective(string name) => Name = name;
    }

    public enum CliGroupType
    {
        Any,
        All,
        MutuallyExclusive
    }

    public class CliGroup : Cli
    {
        public CliGroupType Type { get; set; }
        public CliGroup(CliGroupType type) => Type = type;
    }

    public class CliParser
    {
        public static CliParseResult Parse(string[] args) => default(CliParseResult);
        public static CliParseResult Parse(string[] args, Cli cli) => default(CliParseResult);
    }

    public struct CliParseResult
    {
        private bool _hasErrors = false;

        public CliParseResult(IEnumerable<string> errors)
        {
            _hasErrors = errors.Any();
        }

        public bool HasErrors { get => _hasErrors; }
        public T GetOption<T>(CliOption<T> option) => default(T)!;
        public T GetOption<T>(string name) => default(T)!;
        public T GetOption<T>(string name, char abbr) => default(T)!;
        public CliCommand? InvokedCommand { get; private set; }
        public bool IsInvoked(CliCommand command) => false;
        public bool IsInvoked(string name) => false;
        public bool HasOption<T>(CliOption<T> option) => false;
        public bool HasOption(string name) => false;
        public bool HasOption(string name, char abbr) => false;
        public IEnumerable<string> GetArgument(string name, ushort minArgs = 0, ushort maxArgs = 0) => Enumerable.Empty<string>();
        public IEnumerable<T> GetArgument<T>(string name, ushort minArgs = 0, ushort maxArgs = 0) => Enumerable.Empty<T>();
        public IEnumerable<T> GetArgument<T>(CliArgument<T> argument) => Enumerable.Empty<T>();
        public bool HasDirective(CliDirective directive) => false;
        public bool HasDirective(string directive) => false;
    }
}
