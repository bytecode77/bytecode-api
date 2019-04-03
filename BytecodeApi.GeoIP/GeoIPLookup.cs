using BytecodeApi.Extensions;
using BytecodeApi.GeoIP.Properties;
using BytecodeApi.IO;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BytecodeApi.GeoIP
{
	/// <summary>
	/// Class for GeoIP lookup of IPv4 and IPv6 addresses. Lookup operations are performed on a local database and do not require an online API. The database is part of BytecodeApi.GeoIP.dll.
	/// </summary>
	public static class GeoIPLookup
	{
		private static readonly GeoIPRange[] Ranges;
		private static readonly GeoIPRange6[] Ranges6;
		/// <summary>
		/// Gets a collection of all <see cref="GeoIPCountry" /> objects.
		/// </summary>
		public static ReadOnlyCollection<GeoIPCountry> Countries { get; private set; }

		static GeoIPLookup()
		{
			using (BinaryReader reader = new BinaryReader(new MemoryStream(Compression.Decompress(Resources.GeoIP)), Encoding.UTF8))
			{
				GeoIPCountry[] countries = new GeoIPCountry[reader.ReadByte()];
				Ranges = new GeoIPRange[reader.ReadInt32()];
				Ranges6 = new GeoIPRange6[reader.ReadInt32()];

				for (int i = 0; i < countries.Length; i++)
				{
					countries[i] = new GeoIPCountry(reader.ReadString(), reader.ReadString(), ReadTwoCharacterCode(), ReadTwoCharacterCode(), reader.ReadBoolean());
				}
				for (int i = 0; i < Ranges.Length; i++)
				{
					Ranges[i] = new GeoIPRange(countries[reader.ReadByte()], reader.ReadUInt32(), reader.ReadUInt32());
				}
				for (int i = 0; i < Ranges6.Length; i++)
				{
					Ranges6[i] = new GeoIPRange6(countries[reader.ReadByte()], reader.ReadBytes(16), reader.ReadBytes(16));
				}

				Countries = countries.ToReadOnlyCollection();

				string ReadTwoCharacterCode() => new[] { (char)reader.ReadByte(), (char)reader.ReadByte() }.AsString();
			}
		}

		/// <summary>
		/// Performs a GeoIP lookup on an IPv4 or IPv6 address and returns the <see cref="GeoIPCountry" /> object as the result. If the IP address lookup failed, <see langword="null" /> is returned.
		/// </summary>
		/// <param name="ipAddress">An <see cref="IPAddress" /> object to perform the lookup on.</param>
		/// <returns>
		/// The <see cref="GeoIPCountry" /> object as the result, or <see langword="null" />, if the IP address lookup failed.
		/// </returns>
		public static GeoIPCountry Lookup(IPAddress ipAddress)
		{
			if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
			{
				byte[] bytes = CSharp.Try(() => ipAddress.MapToIPv4().GetAddressBytes());

				if (bytes?.Length == 4)
				{
					uint address = BitConverter.ToUInt32(bytes, 0);

					foreach (GeoIPRange range in Ranges)
					{
						if (address >= range.From && address <= range.To)
						{
							return range.Country;
						}
					}
				}
			}
			else if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
			{
				byte[] bytes = CSharp.Try(() => ipAddress.GetAddressBytes());

				if (bytes?.Length == 16)
				{
					foreach (GeoIPRange6 range in Ranges6)
					{
						bool found = true;

						for (int i = 0; i < 16; i++)
						{
							if (bytes[i] < range.From[i] || bytes[i] > range.To[i])
							{
								found = false;
								break;
							};
						}

						if (found)
						{
							return range.Country;
						}
					}
				}
			}

			return null;
		}
	}
}