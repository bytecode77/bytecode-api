using BytecodeApi.Extensions;
using BytecodeApi.IO.Http;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Build.GeoIP
{
	public class CsvData
	{
		public byte[] Country;
		public byte[] Range;
		public byte[] Range6;
		public byte[] Asn;
		public byte[] Asn6;
		public byte[] City;
		public byte[] CityRange;
		public byte[] CityRange6;

		public static CsvData ImportFromCloud()
		{
			CsvData csvData = new CsvData();

			using (MemoryStream memoryStream = new MemoryStream(HttpClient.Default.GetBytes("https://geolite.maxmind.com/download/geoip/database/GeoLite2-Country-CSV.zip")))
			using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
			{
				csvData.Country = archive.Entries.First(entry => entry.Name == "GeoLite2-Country-Locations-en.csv").GetContent();
				csvData.Range = archive.Entries.First(entry => entry.Name == "GeoLite2-Country-Blocks-IPv4.csv").GetContent();
				csvData.Range6 = archive.Entries.First(entry => entry.Name == "GeoLite2-Country-Blocks-IPv6.csv").GetContent();
			}

			using (MemoryStream memoryStream = new MemoryStream(HttpClient.Default.GetBytes("https://geolite.maxmind.com/download/geoip/database/GeoLite2-ASN-CSV.zip")))
			using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
			{
				csvData.Asn = archive.Entries.First(entry => entry.Name == "GeoLite2-ASN-Blocks-IPv4.csv").GetContent();
				csvData.Asn6 = archive.Entries.First(entry => entry.Name == "GeoLite2-ASN-Blocks-IPv6.csv").GetContent();
			}

			using (MemoryStream memoryStream = new MemoryStream(HttpClient.Default.GetBytes("https://geolite.maxmind.com/download/geoip/database/GeoLite2-City-CSV.zip")))
			using (ZipArchive archive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
			{
				csvData.City = archive.Entries.First(entry => entry.Name == "GeoLite2-City-Locations-en.csv").GetContent();
				csvData.CityRange = archive.Entries.First(entry => entry.Name == "GeoLite2-City-Blocks-IPv4.csv").GetContent();
				csvData.CityRange6 = archive.Entries.First(entry => entry.Name == "GeoLite2-City-Blocks-IPv6.csv").GetContent();
			}

			return csvData;
		}
		public static CsvData ImportFromFiles()
		{
			CsvData csvData = new CsvData();

			csvData.Country = File.ReadAllBytes(@"A:\Downloads\GeoLite2-Country-CSV\GeoLite2-Country-Locations-en.csv");
			csvData.Range = File.ReadAllBytes(@"A:\Downloads\GeoLite2-Country-CSV\GeoLite2-Country-Blocks-IPv4.csv");
			csvData.Range6 = File.ReadAllBytes(@"A:\Downloads\GeoLite2-Country-CSV\GeoLite2-Country-Blocks-IPv6.csv");
			csvData.Asn = File.ReadAllBytes(@"A:\Downloads\GeoLite2-ASN-CSV\GeoLite2-ASN-Blocks-IPv4.csv");
			csvData.Asn6 = File.ReadAllBytes(@"A:\Downloads\GeoLite2-ASN-CSV\GeoLite2-ASN-Blocks-IPv6.csv");
			csvData.City = File.ReadAllBytes(@"A:\Downloads\GeoLite2-City-CSV\GeoLite2-City-Locations-en.csv");
			csvData.CityRange = File.ReadAllBytes(@"A:\Downloads\GeoLite2-City-CSV\GeoLite2-City-Blocks-IPv4.csv");
			csvData.CityRange6 = File.ReadAllBytes(@"A:\Downloads\GeoLite2-City-CSV\GeoLite2-City-Blocks-IPv6.csv");

			return csvData;
		}
	}
}