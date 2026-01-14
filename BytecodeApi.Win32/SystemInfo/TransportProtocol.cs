using System.ComponentModel;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Specifies a transport protocol.
/// </summary>
public enum TransportProtocol
{
	/// <summary>
	/// The TCP protocol is used.
	/// </summary>
	[Description("TCP")]
	Tcp,
	/// <summary>
	/// The UDP protocol is used.
	/// </summary>
	[Description("UDP")]
	Udp
}