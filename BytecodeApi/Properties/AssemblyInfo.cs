global using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

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
[assembly: InternalsVisibleTo("BytecodeApi.Wpf.Cui")]

#if DEBUG
[assembly: InternalsVisibleTo("Playground.Console")]
[assembly: InternalsVisibleTo("Playground.Wpf")]
#endif