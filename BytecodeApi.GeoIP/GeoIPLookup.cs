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
	public static class GeoIPLookup
	{
		private static readonly GeoIPRange[] Ranges;
		private static readonly GeoIPRange6[] Ranges6;
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
					byte country = reader.ReadByte();
					byte flags = reader.ReadByte();
					uint from = reader.ReadUInt32();
					uint to = reader.ReadUInt32();

					Ranges[i] = new GeoIPRange(countries[country], (flags & 1) == 1, (flags & 2) == 2, from, to);
				}
				for (int i = 0; i < Ranges6.Length; i++)
				{
					byte country = reader.ReadByte();
					byte flags = reader.ReadByte();
					byte[] from = reader.ReadBytes(16);
					byte[] to = reader.ReadBytes(16);

					Ranges6[i] = new GeoIPRange6(countries[country], (flags & 1) == 1, (flags & 2) == 2, from, to);
				}

				Countries = countries.ToReadOnlyCollection();

				string ReadTwoCharacterCode() => new[] { (char)reader.ReadByte(), (char)reader.ReadByte() }.AsString();
			}
		}

		public static bool Lookup(IPAddress ipAddress, out GeoIPCountry country, out bool isAnonymousProxy, out bool isSatelliteProvider)
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
							country = range.Country;
							isAnonymousProxy = range.IsAnonymousProxy;
							isSatelliteProvider = range.IsSatelliteProvider;
							return true;
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
							country = range.Country;
							isAnonymousProxy = range.IsAnonymousProxy;
							isSatelliteProvider = range.IsSatelliteProvider;
							return true;
						}
					}
				}
			}

			isAnonymousProxy = false;
			isSatelliteProvider = false;
			country = null;
			return false;
		}
	}
}