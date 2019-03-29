using System.ComponentModel;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Specifies the protocol of a <see cref="ProtocolMappingEntry" /> object.
	/// </summary>
	public enum ProtocolMappingProtocol
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
}