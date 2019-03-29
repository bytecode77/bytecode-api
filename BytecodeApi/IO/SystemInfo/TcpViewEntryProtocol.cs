using System.ComponentModel;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Specifies the connection type of a <see cref="TcpViewEntry" /> object.
	/// </summary>
	public enum TcpViewEntryProtocol
	{
		/// <summary>
		/// The TCPv4 protocol is used.
		/// </summary>
		[Description("TCPv4")]
		Tcp4,
		/// <summary>
		/// The TCPv6 protocol is used.
		/// </summary>
		[Description("TCPv6")]
		Tcp6,
		/// <summary>
		/// The UDPv4 protocol is used.
		/// </summary>
		[Description("UDPv4")]
		Udp4,
		/// <summary>
		/// The UDPv6 protocol is used.
		/// </summary>
		[Description("UDPv6")]
		Udp6
	}
}