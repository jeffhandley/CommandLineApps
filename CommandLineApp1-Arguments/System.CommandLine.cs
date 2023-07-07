using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CommandLine
{
    public static partial class CliParser
    {
        public static CliParseResult ParseArguments(string[] args) => default;
    }

    public partial struct CliParseResult
    {
        public T GetArgument<T>() => default!;
        public IEnumerable<T> GetArguments<T>(ushort minArgs = 0, ushort maxArgs = 0) => Enumerable.Empty<T>();
    }
}
