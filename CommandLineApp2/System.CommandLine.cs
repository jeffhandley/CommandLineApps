using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CommandLine
{
    public class Cli
    {
        public CliOption<T> AddOption<T>(CliOption<T> option) => option;
        public CliOption<T> AddOption<T>(string name) => new CliOption<T>(name);
        public CliOption<T> AddOption<T>(string name, char abbr) => new CliOption<T>(name, abbr);
        public CliGroup AddGroup(CliGroupType type) => new CliGroup(type);
        public CliArgument<T> AddArgument<T>(short minArgs = 0, short maxArgs = 0) => new CliArgument<T>(minArgs, maxArgs);
        public CliDirective AddDirective(CliDirective directive) => directive;
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
        public short MinArgs { get; set; }
        public short MaxArgs { get; set; }
        public Type ArgumentType => typeof(T);
        public CliArgument(short minArgs = 0, short maxArgs = 0)
        {
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
        public T GetOption<T>(CliOption<T> option) => default(T)!;
        public T GetOption<T>(string name) => default(T)!;
        public T GetOption<T>(string name, char abbr) => default(T)!;
        public bool HasOption<T>(CliOption<T> option) => false;
        public bool HasOption(string name) => false;
        public bool HasOption(string name, char abbr) => false;
        public IEnumerable<T> GetArgument<T>(CliArgument<T> argument) => Enumerable.Empty<T>();
        public IEnumerable<T> GetArgument<T>() => Enumerable.Empty<T>();
        public bool HasDirective(CliDirective directive) => false;
        public bool HasDirective(string directive) => false;
    }
}
