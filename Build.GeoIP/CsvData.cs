using BytecodeApi.Data;
using BytecodeApi.IO;
using BytecodeApi.IO.Http;
using System.IO;
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

			BlobTree countryBlob = ZipCompression.Decompress(HttpClient.Default.GetBytes("https://geolite.maxmind.com/download/geoip/database/GeoLite2-Country-CSV.zip"));
			BlobTree asnBlob = ZipCompression.Decompress(HttpClient.Default.GetBytes("https://geolite.maxmind.com/download/geoip/database/GeoLite2-ASN-CSV.zip"));
			BlobTree cityBlob = ZipCompression.Decompress(HttpClient.Default.GetBytes("https://geolite.maxmind.com/download/geoip/database/GeoLite2-City-CSV.zip"));

			csvData.Country = FindBlob(countryBlob, "GeoLite2-Country-Locations-en.csv");
			csvData.Range = FindBlob(countryBlob, "GeoLite2-Country-Blocks-IPv4.csv");
			csvData.Range6 = FindBlob(countryBlob, "GeoLite2-Country-Blocks-IPv6.csv");
			csvData.Asn = FindBlob(asnBlob, "GeoLite2-ASN-Blocks-IPv4.csv");
			csvData.Asn6 = FindBlob(asnBlob, "GeoLite2-ASN-Blocks-IPv6.csv");
			csvData.City = FindBlob(cityBlob, "GeoLite2-City-Locations-en.csv");
			csvData.CityRange = FindBlob(cityBlob, "GeoLite2-City-Blocks-IPv4.csv");
			csvData.CityRange6 = FindBlob(cityBlob, "GeoLite2-City-Blocks-IPv6.csv");

			return csvData;

			byte[] FindBlob(BlobTree blobs, string name)
			{
				return blobs.Root.Nodes.First().Blobs[name].Content;
			}
		}
		public static CsvData ImportFromFiles()
		{
			const string path = @"A:\Downloads";
			CsvData csvData = new CsvData();

			csvData.Country = File.ReadAllBytes(Path.Combine(path, @"GeoLite2-Country-CSV\GeoLite2-Country-Locations-en.csv"));
			csvData.Range = File.ReadAllBytes(Path.Combine(path, @"GeoLite2-Country-CSV\GeoLite2-Country-Blocks-IPv4.csv"));
			csvData.Range6 = File.ReadAllBytes(Path.Combine(path, @"GeoLite2-Country-CSV\GeoLite2-Country-Blocks-IPv6.csv"));
			csvData.Asn = File.ReadAllBytes(Path.Combine(path, @"GeoLite2-ASN-CSV\GeoLite2-ASN-Blocks-IPv4.csv"));
			csvData.Asn6 = File.ReadAllBytes(Path.Combine(path, @"GeoLite2-ASN-CSV\GeoLite2-ASN-Blocks-IPv6.csv"));
			csvData.City = File.ReadAllBytes(Path.Combine(path, @"GeoLite2-City-CSV\GeoLite2-City-Locations-en.csv"));
			csvData.CityRange = File.ReadAllBytes(Path.Combine(path, @"GeoLite2-City-CSV\GeoLite2-City-Blocks-IPv4.csv"));
			csvData.CityRange6 = File.ReadAllBytes(Path.Combine(path, @"GeoLite2-City-CSV\GeoLite2-City-Blocks-IPv6.csv"));

			return csvData;
		}
	}
}