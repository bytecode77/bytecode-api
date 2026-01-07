using System.Runtime.Versioning;

namespace BytecodeApi.IO;

/// <summary>
/// Represents a <see langword="static" /> set of <see cref="string" /> objects representing common file system paths.
/// </summary>
[SupportedOSPlatform("windows")]
public static class KnownPaths
{
	/// <summary>
	/// Represents the path to the Windows hosts file. This field is read-only.
	/// <para>Example: C:\Windows\System32\drivers\etc\hosts</para>
	/// </summary>
	public static readonly string HostsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\hosts");
	/// <summary>
	/// Represents the path to the Windows services file. This field is read-only.
	/// <para>Example: C:\Windows\System32\drivers\etc\services</para>
	/// </summary>
	public static readonly string ServicesFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"drivers\etc\services");
}