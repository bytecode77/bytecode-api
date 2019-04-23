using BytecodeApi.Extensions;
using BytecodeApi.IO.FileSystem;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BytecodeApi.IO.SystemInfo
{
	/// <summary>
	/// Provides a snapshot from the services file in %systemroot%\drivers\etc\services.
	/// </summary>
	public sealed class ProtocolMappingEntry
	{
		private static readonly Regex ServicesFileRegex = new Regex(@"(.*) +([0-9]+)\/(tcp|udp)", RegexOptions.IgnoreCase);
		/// <summary>
		/// Gets the protocol that is associated with this instance.
		/// </summary>
		public ProtocolMappingProtocol Protocol { get; private set; }
		/// <summary>
		/// Gets the port that is associated with this instance.
		/// </summary>
		public int Port { get; private set; }
		/// <summary>
		/// Gets the service or protocol name that is associated with this instance.
		/// </summary>
		public string Name { get; private set; }

		private ProtocolMappingEntry()
		{
		}
		/// <summary>
		/// Reads the services file in %systemroot%\drivers\etc\services and returns a <see cref="ProtocolMappingEntry" />[] object with information about all protocols found in this file.
		/// </summary>
		/// <returns>
		/// A <see cref="ProtocolMappingEntry" />[] object with information about all protocols found in the services file.
		/// </returns>
		public static ProtocolMappingEntry[] GetProtocolMappingEntries()
		{
			return File
				.ReadAllLines(KnownPaths.ServicesFile)
				.Select(line => line.Trim())
				.Where(line => !line.StartsWith("#"))
				.Select(line => ServicesFileRegex.Match(line))
				.Where(match => match.Success)
				.Select(match => new ProtocolMappingEntry
				{
					Protocol = match.Groups[3].Value.Equals("udp", SpecialStringComparisons.IgnoreCase) ? ProtocolMappingProtocol.Udp : ProtocolMappingProtocol.Tcp,
					Port = Convert.ToInt32(match.Groups[2].Value.Trim()),
					Name = match.Groups[1].Value.Trim()
				})
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
			return "[" + Port + ", " + Protocol.GetDescription() + ", " + Name + "]";
		}
	}
}