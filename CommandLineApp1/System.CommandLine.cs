﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.CommandLine
{
    public class CliParser
    {
        public static CliParseResult Parse(string[] args) => default(CliParseResult);
    }

    public struct CliParseResult
    {
        public T GetOption<T>(string name) => default(T)!;
        public T GetOption<T>(string name, char abbr) => default(T)!;
        public bool HasOption(string name) => false;
        public bool HasOption(string name, char abbr) => false;
        public IEnumerable<string> GetArgument(string name, ushort minArgs = 0, ushort maxArgs = 0) => Enumerable.Empty<string>();
        public IEnumerable<T> GetArgument<T>(ushort minArgs = 0, ushort maxArgs = 0) => Enumerable.Empty<T>();
    }
}
