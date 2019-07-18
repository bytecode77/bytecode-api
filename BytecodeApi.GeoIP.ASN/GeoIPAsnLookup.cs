using BytecodeApi.Extensions;
using BytecodeApi.GeoIP.ASN.Properties;
using BytecodeApi.IO;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;

namespace BytecodeApi.GeoIP.ASN
{
	/// <summary>
	/// Class for GeoIP ASN lookup of IPv4 and IPv6 addresses. Lookup operations are performed on a local database and do not require an online API.
	/// The database is part of BytecodeApi.GeoIP.ASN.dll and is a variant of GeoLite2 ASN (https://dev.maxmind.com/geoip/geoip2/geolite2).
	/// </summary>
	public static class GeoIPAsnLookup
	{
		private static readonly AsnRange[] Ranges;
		private static readonly AsnRange6[] Ranges6;
		/// <summary>
		/// Gets a <see cref="DateTime" /> value indicating, when this database was last updated.
		/// </summary>
		public static DateTime DatabaseTimeStamp { get; private set; }
		/// <summary>
		/// Gets a collection of all <see cref="Asn" /> objects.
		/// </summary>
		public static ReadOnlyCollection<Asn> Asns { get; private set; }

		static GeoIPAsnLookup()
		{
			using (BinaryReader reader = new BinaryReader(new MemoryStream(Compression.Decompress(Resources.GeoIP_ASN)), Encoding.UTF8))
			{
				DatabaseTimeStamp = new DateTime(reader.ReadInt64());
				Asn[] asns = new Asn[reader.ReadUInt16()];
				Ranges = new AsnRange[reader.ReadInt32()];
				Ranges6 = new AsnRange6[reader.ReadInt32()];

				for (int i = 0; i < asns.Length; i++)
				{
					asns[i] = new Asn(reader.ReadInt32(), reader.ReadString());
				}
				for (int i = 0; i < Ranges.Length; i++)
				{
					Ranges[i] = new AsnRange(asns[reader.ReadUInt16()], reader.ReadUInt32(), reader.ReadUInt32());
				}
				for (int i = 0; i < Ranges6.Length; i++)
				{
					Ranges6[i] = new AsnRange6(asns[reader.ReadUInt16()], reader.ReadBytes(16), reader.ReadBytes(16));
				}

				Asns = asns.ToReadOnlyCollection();
			}
		}

		/// <summary>
		/// Performs a GeoIP ASN lookup on an IPv4 or IPv6 address and returns the <see cref="Asn" /> object as the result. If the IP address lookup failed, <see langword="null" /> is returned.
		/// </summary>
		/// <param name="ipAddress">An <see cref="IPAddress" /> object to perform the lookup on.</param>
		/// <returns>
		/// The <see cref="Asn" /> object as the result, or <see langword="null" />, if the IP address lookup failed.
		/// </returns>
		public static Asn Lookup(IPAddress ipAddress)
		{
			Check.ArgumentNull(ipAddress, nameof(ipAddress));

			if (GeoIPHelper.TryConvertIPv4(ipAddress, out uint address))
			{
				foreach (AsnRange range in Ranges)
				{
					if (address >= range.From && address <= range.To)
					{
						return range.Asn;
					}
				}
			}
			else if (GeoIPHelper.TryConvertIPv6(ipAddress, out byte[] addressBytes))
			{
				foreach (AsnRange6 range in Ranges6)
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

					if (found) return range.Asn;
				}
			}

			return null;
		}
	}
}