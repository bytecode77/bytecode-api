using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Markup;

[assembly: AssemblyTitle("BytecodeApi")]
[assembly: AssemblyProduct("BytecodeApi")]
[assembly: Guid("2d3ac17b-38a1-40f7-94a5-c94ed0eaef86")]

[assembly: InternalsVisibleTo("BytecodeApi.Cryptography")]
[assembly: InternalsVisibleTo("BytecodeApi.FileFormats")]
[assembly: InternalsVisibleTo("BytecodeApi.FileIcons")]
[assembly: InternalsVisibleTo("BytecodeApi.GeoIP")]
[assembly: InternalsVisibleTo("BytecodeApi.GeoIP.ASN")]
[assembly: InternalsVisibleTo("BytecodeApi.GeoIP.City")]
[assembly: InternalsVisibleTo("BytecodeApi.UI")]
#if DEBUG
[assembly: InternalsVisibleTo("Playground.Console")]
[assembly: InternalsVisibleTo("Playground.Wpf")]
#endif

[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.Comparers")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.Data")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.Extensions")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.IO")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.IO.Cli")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.IO.Debugging")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.IO.FileSystem")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.IO.Http")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.IO.Interop")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.IO.SystemInfo")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.IO.Wmi")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.Mathematics")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.Text")]
[assembly: XmlnsDefinition("https://schemas.bytecode77.com/2019/xaml/api", "BytecodeApi.Threading")]
[assembly: XmlnsPrefix("https://schemas.bytecode77.com/2019/xaml/api", "api")]