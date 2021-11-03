using BytecodeApi.Text;
using System.Diagnostics;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Represents a hosts file entry.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class HostsFileEntry
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<HostsFileEntry>("IPAddress = {0}, HostName = {1}", new QuotedString(IPAddress), new QuotedString(HostName));
		/// <summary>
		/// Gets the IP address of the hosts file entry.
		/// </summary>
		public string IPAddress { get; internal set; }
		/// <summary>
		/// Gets the hostname of the hosts file entry.
		/// </summary>
		public string HostName { get; internal set; }

		internal HostsFileEntry()
		{
		}
	}
}