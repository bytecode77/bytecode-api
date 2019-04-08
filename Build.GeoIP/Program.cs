using Build.GeoIP.Data;
using BytecodeApi.Extensions;
using BytecodeApi.FileFormats.Csv;
using BytecodeApi.IO;
using BytecodeApi.IO.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;

namespace Build.GeoIP
{
	public static class Program
	{
		public static void Main()
		{
			#region Retrieve CSV data
			Console.WriteLine("Importing CSV data");

			byte[] countryCsv;
			byte[] rangeCsv;
			byte[] range6Csv;
			byte[] asnCsv;
			byte[] asn6Csv;

			using (MemoryStream memoryStream = new MemoryStream(HttpClient.Default.GetBytes("https://geolite.maxmind.com/download/geoip/database/GeoLite2-Country-CSV.zip")))
			using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
			{
				countryCsv = archive.Entries.First(entry => entry.Name == "GeoLite2-Country-Locations-en.csv").GetContent();
				rangeCsv = archive.Entries.First(entry => entry.Name == "GeoLite2-Country-Blocks-IPv4.csv").GetContent();
				range6Csv = archive.Entries.First(entry => entry.Name == "GeoLite2-Country-Blocks-IPv6.csv").GetContent();
			}

			using (MemoryStream memoryStream = new MemoryStream(HttpClient.Default.GetBytes("https://geolite.maxmind.com/download/geoip/database/GeoLite2-ASN-CSV.zip")))
			using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
			{
				asnCsv = archive.Entries.First(entry => entry.Name == "GeoLite2-ASN-Blocks-IPv4.csv").GetContent();
				asn6Csv = archive.Entries.First(entry => entry.Name == "GeoLite2-ASN-Blocks-IPv6.csv").GetContent();
			}

			//countryCsv = File.ReadAllBytes(@"A:\Downloads\GeoLite2-Country-CSV\GeoLite2-Country-Locations-en.csv");
			//rangeCsv = File.ReadAllBytes(@"A:\Downloads\GeoLite2-Country-CSV\GeoLite2-Country-Blocks-IPv4.csv");
			//range6Csv = File.ReadAllBytes(@"A:\Downloads\GeoLite2-Country-CSV\GeoLite2-Country-Blocks-IPv6.csv");
			//asnCsv = File.ReadAllBytes(@"A:\Downloads\GeoLite2-ASN-CSV\GeoLite2-ASN-Blocks-IPv4.csv");
			//asn6Csv = File.ReadAllBytes(@"A:\Downloads\GeoLite2-ASN-CSV\GeoLite2-ASN-Blocks-IPv6.csv");
			#endregion

			#region GeoIP.db
			Console.WriteLine("Creating GeoIP.db");

			Country[] countries = CsvFile
				.EnumerateBinary(countryCsv, ",", true, false, Encoding.UTF8)
				.Where(row => row[5].Value != "")
				.Select(row => new Country
				{
					ID = Convert.ToInt32(row[0].Value),
					Name = row[5].Value,
					Continent = row[3].Value,
					ContinentCode = row[2].Value.ToUpper(),
					IsoCode = row[4].Value.ToUpper(),
					EuropeanUnion = row[6].Int32Value.Value == 1
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
						ConvertCidr(row[0].Value, out uint from, out uint to);

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
						ConvertCidr(row[0].Value, out byte[] from, out byte[] to);

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

			Console.WriteLine("Writing GeoIP.db");

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
			#endregion

			#region GeoIP.ASN.db
			Console.WriteLine("Creating GeoIP.ASN.db");

			List<Asn> asns = new List<Asn>(ushort.MaxValue);
			List<int> asnNumbers = new List<int>(ushort.MaxValue);

			AsnRange[] asnRanges = CsvFile
				.EnumerateBinary(asnCsv, ",", true, false, Encoding.UTF8)
				.Select(row =>
				{
					int number = row[1].Int32Value.Value;

					AddAsn(number, row[2].Value);
					ConvertCidr(row[0].Value, out uint from, out uint to);

					return new AsnRange
					{
						Asn = number,
						From = from,
						To = to
					};
				})
				.ToArray();

			AsnRange6[] asnRanges6 = CsvFile
				.EnumerateBinary(asn6Csv, ",", true, false, Encoding.UTF8)
				.Select(row =>
				{
					int number = row[1].Int32Value.Value;

					AddAsn(number, row[2].Value);
					ConvertCidr(row[0].Value, out byte[] from, out byte[] to);

					return new AsnRange6
					{
						Asn = number,
						From = from,
						To = to
					};
				})
				.ToArray();

			if (asns.Count > 65536) throw new OverflowException("The limit of 65536 ASN's has been exceeded.");
			asns = asns.OrderBy(asn => asn.Organization, StringComparer.OrdinalIgnoreCase).ToList();
			asnNumbers = asns.Select(asn => asn.Number).ToList();

			Console.WriteLine("Writing GeoIP.ASN.db");

			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(memoryStream, Encoding.UTF8))
				{
					writer.Write((ushort)asns.Count);
					writer.Write(asnRanges.Length);
					writer.Write(asnRanges6.Length);

					foreach (Asn asn in asns)
					{
						writer.Write(asn.Number);
						writer.Write(asn.Organization);
					}
					foreach (AsnRange range in asnRanges)
					{
						writer.Write(GetAsnByNumber(range.Asn));
						writer.Write(range.From);
						writer.Write(range.To);
					}
					foreach (AsnRange6 range in asnRanges6)
					{
						writer.Write(GetAsnByNumber(range.Asn));
						writer.Write(range.From);
						writer.Write(range.To);
					}
				}

				File.WriteAllBytes(@"..\..\..\BytecodeApi.GeoIP.ASN\Resources\GeoIP.ASN.db", Compression.Compress(memoryStream.ToArray()));
			}

			void AddAsn(int number, string organization)
			{
				if (!asnNumbers.Contains(number))
				{
					asnNumbers.Add(number);
					asns.Add(new Asn
					{
						Number = number,
						Organization = organization
					});
				}
			}
			ushort GetAsnByNumber(int number)
			{
				int index = asnNumbers.IndexOf(number);
				if (index == -1)
				{
					throw new FormatException("ASN ID '" + number + "' not found.");
				}
				else
				{
					return (ushort)index;
				}
			}
			#endregion
		}

		private static void ConvertCidr(string cidr, out uint from, out uint to)
		{
			uint[] parts = cidr.Split('.', '/').Select(part => Convert.ToUInt32(part)).ToArray();
			uint ip = parts[0] << 24 | parts[1] << 16 | parts[2] << 8 | parts[3];
			uint mask = uint.MaxValue << (32 - (int)parts[4]);

			from = ip & mask;
			to = ip | ~mask;
		}
		private static void ConvertCidr(string cidr, out byte[] from, out byte[] to)
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