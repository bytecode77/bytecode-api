using BytecodeApi.Extensions;
using BytecodeApi.IO;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Provides a snapshot of the services file in %systemroot%\drivers\etc\services.
/// </summary>
public sealed class ProtocolMapping
{
	private static readonly Regex ServicesFileRegex = new(@"(.*) +([0-9]+)\/(tcp|udp)", RegexOptions.IgnoreCase);
	/// <summary>
	/// Gets all protocol mapping entries.
	/// </summary>
	public ReadOnlyCollection<ProtocolMappingEntry> Entries { get; private init; }

	private ProtocolMapping(IEnumerable<ProtocolMappingEntry> entries)
	{
		Entries = entries.ToReadOnlyCollection();
	}
	/// <summary>
	/// Creates a new <see cref="ProtocolMapping" /> instance and loads all protocol mapping entries from the services file in %systemroot%\drivers\etc\services.
	/// </summary>
	/// <returns>
	/// The <see cref="ProtocolMapping" /> this method creates.
	/// </returns>
	public static ProtocolMapping Load()
	{
		return new
		(
			File
				.ReadAllLines(KnownPaths.ServicesFile)
				.Select(line => line.Trim())
				.Where(line => !line.StartsWith('#'))
				.Select(line => ServicesFileRegex.Match(line))
				.Where(match => match.Success)
				.Select(match => new ProtocolMappingEntry
				{
					Protocol = match.Groups[3].Value.Equals("udp", StringComparison.OrdinalIgnoreCase) ? TransportProtocol.Udp : TransportProtocol.Tcp,
					Port = Convert.ToInt32(match.Groups[2].Value.Trim()),
					Name = match.Groups[1].Value.Trim()
				})
				.ToArray()
		);
	}
}