using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CommandLine
{
    public static partial class CliParser
    {
        public static CliParseResult ParseOptions(string[] args, Cli cli) => default(CliParseResult);
    }

    public partial struct CliParseResult
    {
        public bool HasCommand(string name) => false;
        public bool HasDirective(CliDirective directive) => false;
    }

    public partial class Cli
    {
        public CliCommand AddCommand(string name) => new CliCommand(name);
    }

    public partial class CliCommand : Cli
    {
        public string Name { get; set; }

        public CliCommand(string name) => Name = name;
    }
}
