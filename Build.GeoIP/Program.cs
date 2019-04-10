using Build.GeoIP.Data;
using BytecodeApi;
using BytecodeApi.Extensions;
using BytecodeApi.FileFormats.Csv;
using BytecodeApi.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Build.GeoIP
{
	public static class Program
	{
		private static CsvData Csv;
		private static Country[] Countries;
		private static IPRange[] IPRanges;
		private static IPRange6[] IPRanges6;
		private static List<Asn> Asns;
		private static AsnRange[] AsnRanges;
		private static AsnRange6[] AsnRanges6;
		private static City[] Cities;
		private static CityRange[] CityRanges;
		private static CityRange6[] CityRanges6;

		public static void Main()
		{
			Console.WriteLine("Importing CSV data");
			Csv = CsvData.ImportFromCloud();
			//Csv = CsvData.ImportFromFiles();

			Console.WriteLine("Creating GeoIP.db");
			ParseCountries();
			Console.WriteLine("Writing GeoIP.db");
			WriteCountries();
			Console.WriteLine("Creating GeoIP.ASN.db");
			ParseAsn();
			Console.WriteLine("Writing GeoIP.ASN.db");
			WriteAsn();
			Console.WriteLine("Creating GeoIP.City.db");
			ParseCities();
			Console.WriteLine("Writing GeoIP.City.db");
			WriteCities();

			Console.WriteLine("Done.");
		}

		private static void ParseCountries()
		{
			Countries = CsvFile
				.EnumerateBinary(Csv.Country, ",", true, false, Encoding.UTF8)
				.Where(row => row[5].Value != "")
				.Select(row =>
				{
					string isoCode = row[4].Value.ToUpper();
					string continentIsoCode = row[2].Value.ToUpper();
					if (isoCode.Length != 2 || isoCode[0] > byte.MaxValue || isoCode[1] > byte.MaxValue) throw new FormatException("Country ISO code must be composed of two ANSI characters.");
					if (continentIsoCode.Length != 2 || continentIsoCode[0] > byte.MaxValue || continentIsoCode[1] > byte.MaxValue) throw new FormatException("Country continent code must be composed of two ANSI characters.");

					return new Country
					{
						ID = row[0].Int32Value.Value,
						Name = row[5].Value,
						IsoCode = isoCode,
						Continent = row[3].Value,
						ContinentIsoCode = continentIsoCode,
						EuropeanUnion = row[6].Int32Value.Value == 1
					};
				})
				.OrderBy(country => country.Name, StringComparer.OrdinalIgnoreCase)
				.ToArray();

			if (Countries.Length > 255) throw new OverflowException("The limit of 255 countries has been exceeded.");

			IPRanges = CsvFile
				.EnumerateBinary(Csv.Range, ",", true, false, Encoding.UTF8)
				.Where(row => row[1].Value != "")
				.Select(row => new
				{
					Range = row[0].Value,
					CountryIndex = GetCountryIndex(row[1].Int32Value.Value)
				})
				.Where(range => range.CountryIndex != null)
				.Select(range =>
				{
					ConvertCidr(range.Range, out uint from, out uint to);

					return new IPRange
					{
						CountryIndex = range.CountryIndex.Value,
						From = from,
						To = to
					};
				})
				.ToArray();

			IPRanges6 = CsvFile
				.EnumerateBinary(Csv.Range6, ",", true, false, Encoding.UTF8)
				.Where(row => row[1].Value != "")
				.Select(row => new
				{
					Range = row[0].Value,
					CountryIndex = GetCountryIndex(row[1].Int32Value.Value)
				})
				.Where(range => range.CountryIndex != null)
				.Select(range =>
				{
					ConvertCidr(range.Range, out byte[] from, out byte[] to);

					return new IPRange6
					{
						CountryIndex = range.CountryIndex.Value,
						From = from,
						To = to
					};
				})
				.ToArray();

			byte? GetCountryIndex(int id)
			{
				int index = Countries.IndexOf(country => country.ID == id);
				return index != -1 ? (byte)index : (byte?)null;
			}
		}
		private static void ParseAsn()
		{
			Asns = new List<Asn>(ushort.MaxValue);
			List<int> asnNumbers = new List<int>(ushort.MaxValue);

			AsnRanges = CsvFile
				.EnumerateBinary(Csv.Asn, ",", true, false, Encoding.UTF8)
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

			AsnRanges6 = CsvFile
				.EnumerateBinary(Csv.Asn6, ",", true, false, Encoding.UTF8)
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

			if (Asns.Count > 65535) throw new OverflowException("The limit of 65535 ASN's has been exceeded.");
			Asns = Asns.OrderBy(asn => asn.Organization, StringComparer.OrdinalIgnoreCase).ToList();

			void AddAsn(int number, string organization)
			{
				if (!asnNumbers.Contains(number))
				{
					asnNumbers.Add(number);
					Asns.Add(new Asn
					{
						Number = number,
						Organization = organization
					});
				}
			}
		}
		private static void ParseCities()
		{
			Cities = CsvFile
				.EnumerateBinary(Csv.City, ",", true, false, Encoding.UTF8)
				.Select(row => new City
				{
					ID = row[0].Int32Value.Value,
					CountryIndex = GetCountryIndex(row[4].Value),
					Name = row[10].Value,
					Subdivision1Name = row[7].Value,
					Subdivision1IsoCode = row[6].Value,
					Subdivision2Name = row[9].Value,
					Subdivision2IsoCode = row[8].Value,
					TimeZone = row[12].Value
				})
				.OrderBy(city => city.Name, StringComparer.OrdinalIgnoreCase)
				.ToArray();

			int[] cityIndices = Create.Array(Cities.Max(city => city.ID) + 1, -1);
			for (int i = 0; i < Cities.Length; i++) cityIndices[Cities[i].ID] = i;

			CityRanges = CsvFile
				.EnumerateBinary(Csv.CityRange, ",", true, false, Encoding.UTF8)
				.Where(row => row[1].Value != "")
				.Select(row =>
				{
					ConvertCidr(row[0].Value, out uint from, out uint to);

					return new CityRange
					{
						CityIndex = GetCityIndex(row[1].Int32Value.Value),
						PostalCode = row[6].Value,
						Latitude = row[7].Value.ToSingleOrNull().Value,
						Longitude = row[8].Value.ToSingleOrNull().Value,
						AccuracyRadius = Convert.ToInt16(row[9].Int32Value.Value),
						From = from,
						To = to
					};
				})
				.ToArray();

			CityRanges6 = CsvFile
				.EnumerateBinary(Csv.CityRange6, ",", true, false, Encoding.UTF8)
				.Where(row => row[1].Value != "")
				.Select(row =>
				{
					ConvertCidr(row[0].Value, out byte[] from, out byte[] to);

					return new CityRange6
					{
						CityIndex = GetCityIndex(row[1].Int32Value.Value),
						PostalCode = row[6].Value,
						Latitude = row[7].Value.ToSingleOrNull().Value,
						Longitude = row[8].Value.ToSingleOrNull().Value,
						AccuracyRadius = Convert.ToInt16(row[9].Int32Value.Value),
						From = from,
						To = to
					};
				})
				.ToArray();

			byte GetCountryIndex(string isoCode)
			{
				int index = Countries.IndexOf(country => country.IsoCode == isoCode);
				return index != -1 ? (byte)index : byte.MaxValue;
			}
			int GetCityIndex(int id)
			{
				int index = cityIndices[id];
				return index != -1 ? index : throw new FormatException("City ID '" + id + "' not found.");
			}
		}
		private static void WriteCountries()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(memoryStream, Encoding.UTF8))
				{
					writer.Write((byte)Countries.Length);
					writer.Write(IPRanges.Length);
					writer.Write(IPRanges6.Length);

					foreach (Country country in Countries)
					{
						writer.Write(country.Name);
						writer.Write((byte)country.IsoCode[0]);
						writer.Write((byte)country.IsoCode[1]);
						writer.Write(country.Continent);
						writer.Write((byte)country.ContinentIsoCode[0]);
						writer.Write((byte)country.ContinentIsoCode[1]);
						writer.Write(country.EuropeanUnion);
					}
					foreach (IPRange range in IPRanges)
					{
						writer.Write(range.CountryIndex);
						writer.Write(range.From);
						writer.Write(range.To);
					}
					foreach (IPRange6 range in IPRanges6)
					{
						writer.Write(range.CountryIndex);
						writer.Write(range.From);
						writer.Write(range.To);
					}
				}

				File.WriteAllBytes(@"..\..\..\BytecodeApi.GeoIP\Resources\GeoIP.db", Compression.Compress(memoryStream.ToArray()));
			}
		}
		private static void WriteAsn()
		{
			int[] asnIndices = Create.Array(Asns.Max(asn => asn.Number) + 1, -1);
			for (ushort i = 0; i < Asns.Count; i++) asnIndices[Asns[i].Number] = i;

			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(memoryStream, Encoding.UTF8))
				{
					writer.Write((ushort)Asns.Count);
					writer.Write(AsnRanges.Length);
					writer.Write(AsnRanges6.Length);

					foreach (Asn asn in Asns)
					{
						writer.Write(asn.Number);
						writer.Write(asn.Organization);
					}
					foreach (AsnRange range in AsnRanges)
					{
						writer.Write(GetAsnIndex(range.Asn));
						writer.Write(range.From);
						writer.Write(range.To);
					}
					foreach (AsnRange6 range in AsnRanges6)
					{
						writer.Write(GetAsnIndex(range.Asn));
						writer.Write(range.From);
						writer.Write(range.To);
					}
				}

				File.WriteAllBytes(@"..\..\..\BytecodeApi.GeoIP.ASN\Resources\GeoIP.ASN.db", Compression.Compress(memoryStream.ToArray()));
			}

			ushort GetAsnIndex(int number)
			{
				int index = asnIndices[number];
				return index != -1 ? (ushort)index : throw new FormatException("ASN ID '" + number + "' not found.");
			}
		}
		private static void WriteCities()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter writer = new BinaryWriter(memoryStream, Encoding.UTF8))
				{
					writer.Write(Cities.Length);
					writer.Write(CityRanges.Length);
					writer.Write(CityRanges6.Length);

					foreach (City city in Cities)
					{
						writer.Write(city.CountryIndex);
						writer.Write(city.Name);
						writer.Write(city.Subdivision1Name);
						writer.Write(city.Subdivision1IsoCode);
						writer.Write(city.Subdivision2Name);
						writer.Write(city.Subdivision2IsoCode);
						writer.Write(city.TimeZone);
					}
					foreach (CityRange range in CityRanges)
					{
						writer.Write(range.CityIndex);
						writer.Write(range.PostalCode);
						writer.Write(range.Latitude);
						writer.Write(range.Longitude);
						writer.Write(range.AccuracyRadius);
						writer.Write(range.From);
						writer.Write(range.To);
					}
					foreach (CityRange6 range in CityRanges6)
					{
						writer.Write(range.CityIndex);
						writer.Write(range.PostalCode);
						writer.Write(range.Latitude);
						writer.Write(range.Longitude);
						writer.Write(range.AccuracyRadius);
						writer.Write(range.From);
						writer.Write(range.To);
					}
				}

				File.WriteAllBytes(@"..\..\..\BytecodeApi.GeoIP.City\Resources\GeoIP.City.db", Compression.Compress(memoryStream.ToArray()));
			}
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