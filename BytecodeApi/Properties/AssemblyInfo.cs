global using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: Guid("d3793ffa-fc22-4089-8225-76b14deea8d2")]

[assembly: InternalsVisibleTo("BytecodeApi.CommandLineParser")]
[assembly: InternalsVisibleTo("BytecodeApi.ConsoleUI")]
[assembly: InternalsVisibleTo("BytecodeApi.Cryptography")]
[assembly: InternalsVisibleTo("BytecodeApi.CsvParser")]
[assembly: InternalsVisibleTo("BytecodeApi.IniParser")]
[assembly: InternalsVisibleTo("BytecodeApi.LanguageGenerator")]
[assembly: InternalsVisibleTo("BytecodeApi.Lexer")]
[assembly: InternalsVisibleTo("BytecodeApi.Penetration")]
[assembly: InternalsVisibleTo("BytecodeApi.PEParser")]
[assembly: InternalsVisibleTo("BytecodeApi.PEResources")]
[assembly: InternalsVisibleTo("BytecodeApi.Rest")]
[assembly: InternalsVisibleTo("BytecodeApi.Win32")]
[assembly: InternalsVisibleTo("BytecodeApi.Wmi")]
[assembly: InternalsVisibleTo("BytecodeApi.Wpf")]

#if DEBUG
[assembly: InternalsVisibleTo("Playground.Console")]
[assembly: InternalsVisibleTo("Playground.Wpf")]
#endif