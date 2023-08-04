namespace System.CommandLine.ModelBinding
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
    public class ArgumentAttribute : Attribute
    {
        public ArgumentAttribute() { }
        public ArgumentAttribute(ushort minArgs) => MinArgs = minArgs;
        public ArgumentAttribute(ushort minArgs, ushort maxArgs) : this(minArgs) => MaxArgs = maxArgs;

        public ushort MinArgs { get; private set; }
        public ushort MaxArgs { get; private set; }
    }
}
