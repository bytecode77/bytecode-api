using BytecodeApi.Text;
using System.Diagnostics;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Represents a protocol mapping entry.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class ProtocolMappingEntry
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<ProtocolMappingEntry>("Protocol = {0}, Port = {1}, Name = {2}", Protocol, Port, new QuotedString(Name));
		/// <summary>
		/// Gets the protocol that is associated with this instance.
		/// </summary>
		public ProtocolMappingProtocol Protocol { get; internal set; }
		/// <summary>
		/// Gets the port that is associated with this instance.
		/// </summary>
		public int Port { get; internal set; }
		/// <summary>
		/// Gets the service or protocol name that is associated with this instance.
		/// </summary>
		public string Name { get; internal set; }

		internal ProtocolMappingEntry()
		{
		}
	}
}