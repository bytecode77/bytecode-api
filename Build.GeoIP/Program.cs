using Build.GeoIP.Data;
using BytecodeApi.Extensions;
using BytecodeApi.FileFormats.Csv;
using System;
using System.IO;
using System.Linq;
using System.Net;

namespace Build.GeoIP
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			Country[] countries = CsvFile
				.EnumerateFile(@"..\..\..\!Docs\GeoIP\GeoLite2-Country-Locations-en.csv", ",", true)
				.Where(row => row[4].Value != "" && row[5].Value != "")
				.Select(row =>
				{
					return new Country
					{
						ID = Convert.ToInt32(row[0].Value),
						Flag = row[4].Value.ToUpper(),
						Name = row[5].Value
					};
				})
				.OrderBy(country => country.Name, StringComparer.OrdinalIgnoreCase)
				.ToArray();

			if (countries.Length > 256) throw new OverflowException("The limit of 256 countries has been exceeded.");

			IPRange[] ipRanges = CsvFile.EnumerateFile(@"..\..\..\!Docs\GeoIP\GeoLite2-Country-Blocks-IPv4.csv", ",", true)
				.Where(row => row[2].Value != "")
				.Select(row =>
				{
					int countryID = Convert.ToInt32(row[2].Value);
					ConvertCidrToRange(row[0].Value, out uint from, out uint to);

					return new IPRange
					{
						Country = GetCountryByID(countryID),
						From = from,
						To = to
					};
				})
				.ToArray();

			IPRange6[] ipRanges6 = CsvFile.EnumerateFile(@"..\..\..\!Docs\GeoIP\GeoLite2-Country-Blocks-IPv6.csv", ",", true)
				.Where(row => row[2].Value != "")
				.Select(row =>
				{
					int countryID = Convert.ToInt32(row[2].Value);
					ConvertCidr6ToRange(row[0].Value, out byte[] from, out byte[] to);

					return new IPRange6
					{
						Country = GetCountryByID(countryID),
						From = from,
						To = to
					};
				})
				.ToArray();

			using (BinaryWriter file = new BinaryWriter(File.Create(@"..\..\..\!Docs\GeoIP\GeoIP.db")))
			{
				file.Write((byte)countries.Length);
				file.Write(ipRanges.Length);
				file.Write(ipRanges6.Length);

				foreach (Country country in countries)
				{
					if (country.Flag.Length != 2) throw new FormatException("Country flag must have a length of two characters.");

					file.Write((byte)country.Flag[0]);
					file.Write((byte)country.Flag[1]);
					file.Write(country.Name);
				}
				foreach (IPRange range in ipRanges)
				{
					file.Write(range.Country);
					file.Write(range.From);
					file.Write(range.To);
				}
				foreach (IPRange6 range in ipRanges6)
				{
					//IMPORTANT: Optimize byte[]'s with length prefix
					file.Write(range.Country);
					file.Write(range.From);
					file.Write(range.To);
				}
			}

			byte GetCountryByID(int id) => (byte)countries.IndexOf(country => country.ID == id);
		}

		private static void ConvertCidrToRange(string cidr, out uint from, out uint to)
		{
			uint[] parts = cidr.Split('.', '/').Select(part => Convert.ToUInt32(part)).ToArray();
			uint ipnum = parts[0] << 24 | parts[1] << 16 | parts[2] << 8 | parts[3];
			uint mask = uint.MaxValue << (32 - (int)parts[4]);

			from = ipnum & mask;
			to = ipnum | ~mask;
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