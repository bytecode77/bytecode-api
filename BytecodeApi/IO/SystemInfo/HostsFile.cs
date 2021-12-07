using BytecodeApi.Extensions;
using BytecodeApi.IO.FileSystem;
using System.IO;
using System.Linq;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Provides information about the hosts file in %SYSTEMROOT%\drivers\etc\hosts.
	/// </summary>
	public static class HostsFile
	{
		/// <summary>
		/// Retrieves all entries from the Windows hosts file and returns a <see cref="HostsFileEntry" />[] containing the IP address and the hostnames. This includes invalid entries. The IP address and the DNS address are separated by the first whitespace character. Empty lines, lines with no whitespace characters and comments are ignored.
		/// </summary>
		/// <returns>
		/// A <see cref="HostsFileEntry" />[] with all entries form the Windows hosts file.
		/// </returns>
		public static HostsFileEntry[] GetEntries()
		{
			return File
				.ReadAllLines(KnownPaths.HostsFile)
				.Select(line => line.Trim().Replace('\t', ' '))
				.Where(line => !line.StartsWith("#"))
				.Where(line => line.Contains(" "))
				.Select(line => new HostsFileEntry
				{
					IPAddress = line.SubstringUntil(" ").Trim(),
					HostName = line.SubstringFrom(" ").Trim()
				})
				.ToArray();
		}
	}
}