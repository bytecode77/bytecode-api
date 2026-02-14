using System.Diagnostics;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Represents a protocol mapping entry.
/// </summary>
[DebuggerDisplay($"{nameof(ProtocolMappingEntry)}: Protocol = {{Protocol}}, Port = {{Port}}, Name = {{Name,nq}}")]
public sealed class ProtocolMappingEntry
{
	/// <summary>
	/// Gets the protocol that is associated with this instance.
	/// </summary>
	public TransportProtocol Protocol { get; internal set; }
	/// <summary>
	/// Gets the port that is associated with this instance.
	/// </summary>
	public int Port { get; internal set; }
	/// <summary>
	/// Gets the service or protocol name that is associated with this instance.
	/// </summary>
	public string Name { get; internal set; } = null!;

	internal ProtocolMappingEntry()
	{
	}
}