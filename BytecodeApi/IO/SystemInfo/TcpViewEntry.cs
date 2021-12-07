using BytecodeApi.Text;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Represents a TCPView entry.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class TcpViewEntry
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<TcpViewEntry>("Protocol = {0}, Local: {1}, Remote: {2}", Protocol, new QuotedString(LocalAddress + ":" + (LocalProtocolName ?? LocalPort.ToString())), new QuotedString(RemoteAddress == null ? null : RemoteAddress + ":" + (RemoteProtocolName ?? RemotePort?.ToString())));
		/// <summary>
		/// Gets the protocol that is associated with this instance.
		/// </summary>
		public TcpViewEntryProtocol Protocol { get; internal set; }
		/// <summary>
		/// Gets the local IP address that is associated with this instance.
		/// </summary>
		public IPAddress LocalAddress { get; internal set; }
		/// <summary>
		/// Gets the local port that is associated with this instance.
		/// </summary>
		public int LocalPort { get; internal set; }
		/// <summary>
		/// Gets the name of the protocol of the local port that is associated with this instance, if the <see cref="TcpView.ResolveProtocolNames" /> parameter was set to <see langword="true" />.
		/// </summary>
		public string LocalProtocolName { get; internal set; }
		/// <summary>
		/// Gets the remote IP address that is associated with this instance.
		/// </summary>
		public IPAddress RemoteAddress { get; internal set; }
		/// <summary>
		/// Gets the remote port that is associated with this instance.
		/// </summary>
		public int? RemotePort { get; internal set; }
		/// <summary>
		/// Gets the name of the protocol of the remote port that is associated with this instance, if the <see cref="TcpView.ResolveProtocolNames" /> parameter was set to <see langword="true" />.
		/// </summary>
		public string RemoteProtocolName { get; internal set; }
		/// <summary>
		/// Gets the TCP state that is associated with this instance and returns <see langword="null" />, if this <see cref="TcpViewEntry" /> is a UDP connection.
		/// </summary>
		public TcpState? TcpState { get; internal set; }
		/// <summary>
		/// Gets the PID of the process that is associated with this instance.
		/// </summary>
		public int ProcessId { get; internal set; }

		internal TcpViewEntry()
		{
		}
	}
}