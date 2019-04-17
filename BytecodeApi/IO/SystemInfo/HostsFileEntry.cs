using BytecodeApi.Extensions;
using BytecodeApi.IO.FileSystem;
using System.IO;
using System.Linq;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Represents a hosts file entry from the Windows hosts file in %SYSTEMROOT%\drivers\etc\hosts.
	/// </summary>
	public sealed class HostsFileEntry
	{
		/// <summary>
		/// Gets or sets the IP address of the hosts file entry.
		/// </summary>
		public string IPAddress { get; set; }
		/// <summary>
		/// Gets or sets the hostname of the hosts file entry.
		/// </summary>
		public string HostName { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="HostsFileEntry" /> struct with the specified IP address and hostame.
		/// </summary>
		/// <param name="ipAddress">A <see cref="string" /> specifying the IP address of the hosts file entry.</param>
		/// <param name="hostName">A <see cref="string" /> specifying the hostname of the hosts file entry.</param>
		public HostsFileEntry(string ipAddress, string hostName)
		{
			IPAddress = ipAddress;
			HostName = hostName;
		}
		/// <summary>
		/// Retrieves all entries from the Windows hosts file and returns a <see cref="HostsFileEntry" />[] containing the IP address and the hostnames. This includes invalid entries. The IP address and the DNS address are separated by the first whitespace character. Empty lines, lines with no whitespace characters and comments are ignored.
		/// </summary>
		/// <returns>
		/// A <see cref="HostsFileEntry" />[] with all entries form the Windows hosts file.
		/// </returns>
		public static HostsFileEntry[] GetHostsFileEntries()
		{
			return File
				.ReadAllLines(KnownPaths.HostsFile)
				.Select(line => line.Trim().Replace('\t', ' '))
				.Where(line => !line.StartsWith("#"))
				.Where(line => line.Contains(' '))
				.Select(line => new HostsFileEntry(line.SubstringUntil(" ").Trim(), line.SubstringFrom(" ").Trim()))
				.ToArray();
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + IPAddress + ", " + HostName + "]";
		}
	}
}