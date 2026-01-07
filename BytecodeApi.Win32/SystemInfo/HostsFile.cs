using BytecodeApi.Extensions;
using BytecodeApi.IO;
using System.Collections.ObjectModel;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Provides a snapshot of the hosts file in %SYSTEMROOT%\drivers\etc\hosts.
/// </summary>
public sealed class HostsFile
{
	/// <summary>
	/// Gets all hosts file entries.
	/// </summary>
	public ReadOnlyCollection<HostsFileEntry> Entries { get; }

	private HostsFile(IEnumerable<HostsFileEntry> entries)
	{
		Entries = entries.ToReadOnlyCollection();
	}
	/// <summary>
	/// Creates a new <see cref="HostsFile" /> instance and loads all entries from the hosts file.
	/// </summary>
	/// <returns>
	/// The <see cref="HostsFile" /> this method creates.
	/// </returns>
	public static HostsFile Load()
	{
		return new
		(
			File
				.ReadAllLines(KnownPaths.HostsFile)
				.Select(line => line.Trim().Replace('\t', ' '))
				.Where(line => !line.StartsWith('#'))
				.Where(line => line.Contains(' '))
				.Select(line => new HostsFileEntry
				{
					IPAddress = line.SubstringUntil(' ').Trim(),
					HostName = line.SubstringFrom(' ').Trim()
				})
				.ToArray()
		);
	}
}