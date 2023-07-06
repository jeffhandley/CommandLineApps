using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CommandLine
{
    public partial struct CliParseResult
    {
        public T GetOption<T>(CliOption<T> option) => default(T)!;
        public bool HasCommand(CliCommand command) => false;
        public IEnumerable<T> GetArgument<T>(CliArgument<T> argument) => Enumerable.Empty<T>();
    }

    public partial class Cli
    {
        public CliOption<T> Add<T>(CliOption<T> option) => option;
        public CliOption<T> AddOption<T>(string name) => Add(new CliOption<T>(name));
        public CliOption<T> AddOption<T>(string name, char abbr) => Add(new CliOption<T>(name, abbr));
        public CliGroup AddGroup(CliGroupType type) => new CliGroup(type);
        public CliArgument<T> Add<T>(CliArgument<T> argument) => argument;
        public CliArgument<T> AddArgument<T>(string name, ushort minArgs = 0, ushort maxArgs = 0) => Add(new CliArgument<T>(name, minArgs, maxArgs));
        public CliDirective Add(CliDirective directive) => directive;
    }

    public partial class CliArgument<T>
    {
        public string Name { get; set; }
        public ushort MinArgs { get; set; }
        public ushort MaxArgs { get; set; }
        public CliArgument(string name, ushort minArgs = 0, ushort maxArgs = 0)
        {
            Name = name;
            MinArgs = minArgs;
            MaxArgs = maxArgs;
        }
    }

    public enum CliGroupType
    {
        Any,
        All,
        MutuallyExclusive
    }

    public partial class CliGroup : Cli
    {
        public CliGroupType Type { get; set; }
        public CliGroup(CliGroupType type) => Type = type;
    }

    public static partial class CliParser
    {
        public static CliParseResult Parse(this Cli cli, string[] args) => default(CliParseResult);
    }
}
