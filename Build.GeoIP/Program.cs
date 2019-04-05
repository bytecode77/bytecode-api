using Build.GeoIP.Data;
using BytecodeApi.Extensions;
using BytecodeApi.FileFormats.Csv;
using BytecodeApi.IO;
using BytecodeApi.IO.Http;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace Build.GeoIP
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			byte[] countryCsv;
			byte[] rangeCsv;
			byte[] range6Csv;

			using (MemoryStream memoryStream = new MemoryStream(HttpClient.Default.GetBytes("https://geolite.maxmind.com/download/geoip/database/GeoLite2-Country-CSV.zip")))
			using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
			{
				//CURRENT: dynamic version:
				countryCsv = archive.GetEntry(@"GeoLite2-Country-CSV_20190402/GeoLite2-Country-Locations-en.csv").GetContent();
				rangeCsv = archive.GetEntry(@"GeoLite2-Country-CSV_20190402/GeoLite2-Country-Blocks-IPv4.csv").GetContent();
				range6Csv = archive.GetEntry(@"GeoLite2-Country-CSV_20190402/GeoLite2-Country-Blocks-IPv6.csv").GetContent();
			}

			Country[] countries = CsvFile
				.EnumerateBinary(countryCsv, ",", true, false, Encoding.UTF8)
				.Where(row => row[5].Value != "")
				.Select(row =>
				{
					return new Country
					{
						ID = Convert.ToInt32(row[0].Value),
						Name = row[5].Value,
						Continent = row[3].Value,
						ContinentCode = row[2].Value.ToUpper(),
						IsoCode = row[4].Value.ToUpper(),
						EuropeanUnion = row[6].Int32Value.Value == 1
					};
				})
				.OrderBy(country => country.Name, StringComparer.OrdinalIgnoreCase)
				.ToArray();

			if (countries.Length > 256) throw new OverflowException("The limit of 256 countries has been exceeded.");

			IPRange[] ranges = CsvFile
				.EnumerateBinary(rangeCsv, ",", true, false, Encoding.UTF8)
				.Where(row => row[1].Value != "")
				.Select(row =>
				{
					if (GetCountryByID(Convert.ToInt32(row[1].Value), out byte country))
					{
						ConvertCidrToRange(row[0].Value, out uint from, out uint to);

						return new IPRange
						{
							Country = country,
							From = from,
							To = to
						};
					}
					else
					{
						return null;
					}
				})
				.ExceptNull()
				.ToArray();

			IPRange6[] ranges6 = CsvFile
				.EnumerateBinary(range6Csv, ",", true, false, Encoding.UTF8)
				.Where(row => row[1].Value != "")
				.Select(row =>
				{
					if (GetCountryByID(Convert.ToInt32(row[1].Value), out byte country))
					{
						ConvertCidr6ToRange(row[0].Value, out byte[] from, out byte[] to);

						return new IPRange6
						{
							Country = country,
							From = from,
							To = to
						};
					}
					else
					{
						return null;
					}
				})
				.ExceptNull()
				.ToArray();

			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(memoryStream, Encoding.UTF8))
				{
					writer.Write((byte)countries.Length);
					writer.Write(ranges.Length);
					writer.Write(ranges6.Length);

					foreach (Country country in countries)
					{
						if (country.IsoCode.Length != 2 || country.IsoCode[0] > byte.MaxValue || country.IsoCode[1] > byte.MaxValue) throw new FormatException("Country ISO code must be composed of two ANSI characters.");
						if (country.ContinentCode.Length != 2 || country.ContinentCode[0] > byte.MaxValue || country.ContinentCode[1] > byte.MaxValue) throw new FormatException("Country continent code must be composed of two ANSI characters.");

						writer.Write(country.Name);
						writer.Write(country.Continent);
						writer.Write((byte)country.ContinentCode[0]);
						writer.Write((byte)country.ContinentCode[1]);
						writer.Write((byte)country.IsoCode[0]);
						writer.Write((byte)country.IsoCode[1]);
						writer.Write(country.EuropeanUnion);
					}
					foreach (IPRange range in ranges)
					{
						writer.Write(range.Country);
						writer.Write(range.From);
						writer.Write(range.To);
					}
					foreach (IPRange6 range in ranges6)
					{
						writer.Write(range.Country);
						writer.Write(range.From);
						writer.Write(range.To);
					}
				}

				File.WriteAllBytes(@"..\..\..\BytecodeApi.GeoIP\Resources\GeoIP.db", Compression.Compress(memoryStream.ToArray()));
			}

			bool GetCountryByID(int id, out byte countryID)
			{
				int index = countries.IndexOf(country => country.ID == id);
				if (index == -1)
				{
					countryID = 0;
					return false;
				}
				else
				{
					countryID = (byte)index;
					return true;
				}
			}
		}

		private static void ConvertCidrToRange(string cidr, out uint from, out uint to)
		{
			uint[] parts = cidr.Split('.', '/').Select(part => Convert.ToUInt32(part)).ToArray();
			uint ip = parts[0] << 24 | parts[1] << 16 | parts[2] << 8 | parts[3];
			uint mask = uint.MaxValue << (32 - (int)parts[4]);

			from = ip & mask;
			to = ip | ~mask;
		}
		private static void ConvertCidr6ToRange(string cidr, out byte[] from, out byte[] to)
		{
			string[] parts = cidr.Split('/');
			int prefix = Convert.ToInt32(parts[1]);
			from = IPAddress.Parse(parts[0]).GetAddressBytes();
			to = from.ToArray();

			for (int i = 0; i < 16; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (i * 8 + j >= prefix)
					{
						byte mask = (byte)(byte.MaxValue << (8 - j));
						from[i] &= mask;
						to[i] = (byte)(to[i] | ~mask);
					}
				}
			}
		}
	}
}