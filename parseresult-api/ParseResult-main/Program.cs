using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.Reflection;

static string GetName(string prefix, string name, Type type) => string.IsNullOrEmpty(prefix) ? $"{name} : {type.ToString()}" : $"{prefix}.{name} : {type.ToString()}";
static string CombinePrefix(string prefix, string name) => string.IsNullOrEmpty(prefix) ? name : $"{prefix}.{name}";
static string GetDerivedPrefix(string prefixedName, Type? type = null) => type is null ? prefixedName : $"{prefixedName}[{type.Name}]";
static bool IsSuppressed(MethodInfo method) => method.IsSpecialName || method.Name == "Equals" || method.Name == "GetHashCode" || method.Name == "ToString";

static void ShowDerivedTypes(Type type, string prefixedName)
{
    foreach (var derived in type.Assembly.GetTypes().Where(t => t.BaseType == type))
    {
        ShowType(derived, GetDerivedPrefix(prefixedName, derived));
    }

}

static void ShowType(Type type, string prefix = "")
{
    foreach (var prop in type.GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly).OrderBy(p => p.Name))
    {
        var (descend, ascend) = prop switch
        {
            { PropertyType: { Name: nameof(CliAction) } } => (typeof(CliAction), true),
            { Name: nameof(ParseResult.CommandResult), PropertyType: { Name: nameof(CommandResult) } } => (typeof(CommandResult), true),
            { PropertyType: { Name: nameof(CliToken) } } => (typeof(CliToken), true),
            { DeclaringType: { Name: nameof(CommandResult) }, Name: nameof(CommandResult.Children) } => (typeof(SymbolResult), true),
            { DeclaringType: { Name: nameof(ParseResult) }, Name: nameof(ParseResult.Tokens) } => (typeof(CliToken), false),
            { Name: nameof(CliArgument) } => (typeof(CliArgument), true),
            _ => (null, false)
        };

        Console.WriteLine(GetName(prefix, prop.Name, prop.PropertyType));

        var nextPrefix = CombinePrefix(prefix, prop.Name);

        if (descend is not null)
        {
            ShowType(descend, GetDerivedPrefix(nextPrefix, descend));
        }

        if (ascend)
        {
            ShowDerivedTypes(prop.PropertyType, nextPrefix);
        }
    }

    foreach (var method in type.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly)
        .Where(m => !IsSuppressed(m))
        .OrderBy(m => m.Name))
    {
        var (descend, ascend) = method switch
        {
            { Name: nameof(ParseResult.GetResult), ReturnType: { Name: nameof(ArgumentResult) } } => (typeof(ArgumentResult), true),
            { Name: nameof(ParseResult.GetResult), ReturnType: { Name: nameof(OptionResult) } } => (typeof(OptionResult), true),
            { Name: nameof(ParseResult.GetResult), ReturnType: { Name: nameof(DirectiveResult) } } => (typeof(DirectiveResult), true),
            { DeclaringType: { Name: nameof(ParseResult) }, Name: nameof(ParseResult.GetResult), ReturnType: { Name: nameof(CommandResult) } } => (typeof(CommandResult), false),
            _ => (null, false)
        };

        Console.WriteLine(GetName(prefix, $"{method.Name}()", method.ReturnType));

        if (descend is not null)
        {
            ShowType(descend, GetDerivedPrefix(CombinePrefix(prefix, $"{method.Name}()[{descend.Name}]")));
        }
    }
}

var cmdline = System.Reflection.Assembly.Load("System.CommandLine")!;
var parseresult = cmdline.GetType("System.CommandLine.ParseResult")!;

ShowType(parseresult);
