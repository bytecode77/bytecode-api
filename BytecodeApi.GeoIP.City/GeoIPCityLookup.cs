using BytecodeApi.Extensions;
using BytecodeApi.GeoIP.City.Properties;
using BytecodeApi.IO;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;

namespace BytecodeApi.GeoIP.City
{
	/// <summary>
	/// Class for GeoIP city lookup of IPv4 and IPv6 addresses. Lookup operations are performed on a local database and do not require an online API.
	/// The database is part of BytecodeApi.GeoIP.dll and is a variant of GeoLite2 City (https://dev.maxmind.com/geoip/geoip2/geolite2).
	/// </summary>
	public static class GeoIPCityLookup
	{
		private static readonly CityRange[] Ranges;
		private static readonly CityRange6[] Ranges6;
		/// <summary>
		/// Gets a collection of all <see cref="City" /> objects.
		/// </summary>
		public static ReadOnlyCollection<City> Cities { get; private set; }

		static GeoIPCityLookup()
		{
			using (BinaryReader reader = new BinaryReader(new MemoryStream(Compression.Decompress(Resources.GeoIP_City)), Encoding.UTF8))
			{
				City[] cities = new City[reader.ReadInt32()];
				Ranges = new CityRange[reader.ReadInt32()];
				Ranges6 = new CityRange6[reader.ReadInt32()];

				for (int i = 0; i < cities.Length; i++)
				{
					byte country = reader.ReadByte();
					cities[i] = new City(country == byte.MaxValue ? null : GeoIPLookup.Countries[country], reader.ReadString().ToNullIfEmpty(), reader.ReadString().ToNullIfEmpty(), reader.ReadString().ToNullIfEmpty(), reader.ReadString().ToNullIfEmpty(), reader.ReadString().ToNullIfEmpty(), reader.ReadString().ToNullIfEmpty());
				}
				for (int i = 0; i < Ranges.Length; i++)
				{
					Ranges[i] = new CityRange(cities[reader.ReadInt32()], reader.ReadString().ToNullIfEmpty(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadInt16(), reader.ReadUInt32(), reader.ReadUInt32());
				}
				for (int i = 0; i < Ranges6.Length; i++)
				{
					Ranges6[i] = new CityRange6(cities[reader.ReadInt32()], reader.ReadString().ToNullIfEmpty(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadInt16(), reader.ReadBytes(16), reader.ReadBytes(16));
				}

				Cities = cities.ToReadOnlyCollection();
			}
		}

		/// <summary>
		/// Performs a GeoIP city lookup on an IPv4 or IPv6 address and returns the <see cref="CityLookupResult" /> object as the result. If the IP address lookup failed, <see langword="null" /> is returned.
		/// </summary>
		/// <param name="ipAddress">An <see cref="IPAddress" /> object to perform the lookup on.</param>
		/// <returns>
		/// The <see cref="CityLookupResult" /> object as the result, or <see langword="null" />, if the IP address lookup failed.
		/// </returns>
		public static CityLookupResult Lookup(IPAddress ipAddress)
		{
			Check.ArgumentNull(ipAddress, nameof(ipAddress));

			if (GeoIPHelper.TryConvertIPv4(ipAddress, out uint address))
			{
				foreach (CityRange range in Ranges)
				{
					if (address >= range.From && address <= range.To)
					{
						return new CityLookupResult(range.City, range.PostalCode, range.Latitude, range.Longitude, range.AccuracyRadius);
					}
				}
			}
			else if (GeoIPHelper.TryConvertIPv6(ipAddress, out byte[] addressBytes))
			{
				foreach (CityRange6 range in Ranges6)
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

					if (found) return new CityLookupResult(range.City, range.PostalCode, range.Latitude, range.Longitude, range.AccuracyRadius);
				}
			}

			return null;
		}
	}
}