using BytecodeApi.Extensions;
using BytecodeApi.GeoIP.Properties;
using BytecodeApi.IO;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;

namespace BytecodeApi.GeoIP
{
	/// <summary>
	/// Class for GeoIP lookup of IPv4 and IPv6 addresses. Lookup operations are performed on a local database and do not require an online API.
	/// The database is part of BytecodeApi.GeoIP.dll and is a variant of GeoLite2 Country (https://dev.maxmind.com/geoip/geoip2/geolite2).
	/// </summary>
	public static class GeoIPLookup
	{
		private static readonly IPRange[] Ranges;
		private static readonly IPRange6[] Ranges6;
		/// <summary>
		/// Gets a <see cref="DateTime" /> value indicating, when this database was last updated.
		/// </summary>
		public static DateTime DatabaseTimeStamp { get; private set; }
		/// <summary>
		/// Gets a collection of all <see cref="Country" /> objects.
		/// </summary>
		public static ReadOnlyCollection<Country> Countries { get; private set; }

		static GeoIPLookup()
		{
			using (BinaryReader reader = new BinaryReader(new MemoryStream(Compression.Decompress(Resources.GeoIP)), Encoding.UTF8))
			{
				DatabaseTimeStamp = new DateTime(reader.ReadInt64());
				Country[] countries = new Country[reader.ReadByte()];
				Ranges = new IPRange[reader.ReadInt32()];
				Ranges6 = new IPRange6[reader.ReadInt32()];

				for (int i = 0; i < countries.Length; i++)
				{
					countries[i] = new Country(reader.ReadString(), ReadTwoCharacterCode(), reader.ReadString(), ReadTwoCharacterCode(), reader.ReadBoolean());
				}
				for (int i = 0; i < Ranges.Length; i++)
				{
					Ranges[i] = new IPRange(countries[reader.ReadByte()], reader.ReadUInt32(), reader.ReadUInt32());
				}
				for (int i = 0; i < Ranges6.Length; i++)
				{
					Ranges6[i] = new IPRange6(countries[reader.ReadByte()], reader.ReadBytes(16), reader.ReadBytes(16));
				}

				Countries = countries.ToReadOnlyCollection();

				string ReadTwoCharacterCode() => new[] { (char)reader.ReadByte(), (char)reader.ReadByte() }.AsString();
			}
		}

		/// <summary>
		/// Performs a GeoIP lookup on an IPv4 or IPv6 address and returns the <see cref="Country" /> object as the result. If the IP address lookup failed, <see langword="null" /> is returned.
		/// </summary>
		/// <param name="ipAddress">An <see cref="IPAddress" /> object to perform the lookup on.</param>
		/// <returns>
		/// The <see cref="Country" /> object as the result, or <see langword="null" />, if the IP address lookup failed.
		/// </returns>
		public static Country Lookup(IPAddress ipAddress)
		{
			Check.ArgumentNull(ipAddress, nameof(ipAddress));

			if (GeoIPHelper.TryConvertIPv4(ipAddress, out uint address))
			{
				foreach (IPRange range in Ranges)
				{
					if (address >= range.From && address <= range.To)
					{
						return range.Country;
					}
				}
			}
			else if (GeoIPHelper.TryConvertIPv6(ipAddress, out byte[] addressBytes))
			{
				foreach (IPRange6 range in Ranges6)
				{
					bool found = true;

					for (int i = 0; i < 16; i++)
					{
						if (addressBytes[i] < range.From[i] || addressBytes[i] > range.To[i])
						{
							found = false;
							break;
						}
					}

					if (found) return range.Country;
				}
			}

			return null;
		}
	}
}