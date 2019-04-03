using BytecodeApi.Extensions;
using BytecodeApi.GeoIP.Properties;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BytecodeApi.GeoIP
{
	public static class GeoIPLookup
	{
		private static bool IsInitialized;
		private static GeoIPCountry[] Countries;
		private static GeoIPRange[] Ranges;
		private static GeoIPRange6[] Ranges6;

		public static GeoIPCountry Lookup(IPAddress ipAddress)
		{
			Initialize();

			if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
			{
				byte[] bytes = CSharp.Try(() => ipAddress.MapToIPv4().GetAddressBytes());

				if (bytes?.Length == 4)
				{
					uint address = (uint)bytes[0] << 24 | (uint)bytes[1] << 16 | (uint)bytes[2] << 8 | bytes[3];
					return Ranges.FirstOrDefault(range => address >= range.From && address <= range.To)?.Country;
				}
			}
			else if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
			{
				byte[] bytes = CSharp.Try(() => ipAddress.GetAddressBytes());

				if (bytes?.Length == 16)
				{
					//CURRENT: Performance
					return Ranges6.FirstOrDefault(range => Enumerable.Range(0, 16).None(i => bytes[i] < range.From[i] || bytes[i] > range.To[i]))?.Country;
				}
			}

			return null;
		}
		private static void Initialize()
		{
			if (!IsInitialized)
			{
				using (BinaryReader file = new BinaryReader(new MemoryStream(Resources.GeoIP), Encoding.UTF8))
				{
					Countries = new GeoIPCountry[file.ReadByte()];
					Ranges = new GeoIPRange[file.ReadInt32()];
					Ranges6 = new GeoIPRange6[file.ReadInt32()];

					for (int i = 0; i < Countries.Length; i++)
					{
						Countries[i] = new GeoIPCountry(new string(new[] { (char)file.ReadByte(), (char)file.ReadByte() }), file.ReadString());
					}
					for (int i = 0; i < Ranges.Length; i++)
					{
						Ranges[i] = new GeoIPRange(Countries[file.ReadByte()], file.ReadUInt32(), file.ReadUInt32());
					}
					for (int i = 0; i < Ranges6.Length; i++)
					{
						Ranges6[i] = new GeoIPRange6(Countries[file.ReadByte()], file.ReadBytes(16), file.ReadBytes(16));
					}
				}

				IsInitialized = true;
			}
		}
	}
}