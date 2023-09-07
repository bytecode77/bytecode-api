using System.Diagnostics;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Represents a hosts file entry.
/// </summary>
[DebuggerDisplay($"{nameof(HostsFileEntry)}: IPAddress = {{IPAddress}}, HostName = {{HostName}}")]
public sealed class HostsFileEntry
{
	/// <summary>
	/// Gets the IP address of the hosts file entry.
	/// </summary>
	public string IPAddress { get; internal set; } = null!;
	/// <summary>
	/// Gets the hostname of the hosts file entry.
	/// </summary>
	public string HostName { get; internal set; } = null!;

	internal HostsFileEntry()
	{
	}
}