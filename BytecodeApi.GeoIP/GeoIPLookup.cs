using BytecodeApi.Extensions;
using BytecodeApi.GeoIP.Properties;
using BytecodeApi.IO;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BytecodeApi.GeoIP
{
	public static class GeoIPLookup
	{
		private static readonly GeoIPCountry[] Countries;
		private static readonly GeoIPRange[] Ranges;
		private static readonly GeoIPRange6[] Ranges6;

		static GeoIPLookup()
		{
			using (BinaryReader reader = new BinaryReader(new MemoryStream(Compression.Decompress(Resources.GeoIP)), Encoding.UTF8))
			{
				Countries = new GeoIPCountry[reader.ReadByte()];
				Ranges = new GeoIPRange[reader.ReadInt32()];
				Ranges6 = new GeoIPRange6[reader.ReadInt32()];

				for (int i = 0; i < Countries.Length; i++)
				{
					Countries[i] = new GeoIPCountry(new[] { (char)reader.ReadByte(), (char)reader.ReadByte() }.AsString(), reader.ReadString());
				}
				for (int i = 0; i < Ranges.Length; i++)
				{
					Ranges[i] = new GeoIPRange(Countries[reader.ReadByte()], reader.ReadUInt32(), reader.ReadUInt32());
				}
				for (int i = 0; i < Ranges6.Length; i++)
				{
					Ranges6[i] = new GeoIPRange6(Countries[reader.ReadByte()], reader.ReadBytes(16), reader.ReadBytes(16));
				}
			}
		}
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
						if (address >= range.From && address <= range.To) return range.Country;
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

						if (found) return range.Country;
					}
				}
			}

			return null;
		}
	}
}