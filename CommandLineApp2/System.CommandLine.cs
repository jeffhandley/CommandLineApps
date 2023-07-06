using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CommandLine
{
    public partial struct CliParseResult
    {
        public CliParseResult() { }
        public bool HasErrors { get; } = false;
    }

    public partial class CliOption<T>
    {
        public string Name { get; set; }
        public IEnumerable<string> Aliases { get; set; }
        public CliOption(string name)
        {
            Name = name;
            Aliases = Enumerable.Empty<string>();
        }
        public CliOption(string name, char abbr) : this(name)
        {
            Aliases = new string[] { abbr.ToString() };
        }
    }

    public partial class CliDirective
    {
        public string Name { get; set; }
        public CliDirective(string name) => Name = name;
    }

}
