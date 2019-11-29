using BytecodeApi.GeoIP;
using BytecodeApi.GeoIP.ASN;
using BytecodeApi.GeoIP.City;
using BytecodeApi.IO.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace UnitTest
{
	[TestClass]
	public class GeoIPTests
	{
		private static readonly IPAddress IpAddress4 = IPAddress.Parse("95.223.73.131");
		private static readonly IPAddress IpAddress6 = IPAddress.Parse("2a02:908:1d3:c080:94b7:3743:d4c0:1dfa");

		[TestMethod]
		public void BytecodeApi_GeoIP_GeoIPLookup_Lookup()
		{
			Assert.AreEqual("DE", GeoIPLookup.Lookup(IpAddress4)?.IsoCode);
			Assert.AreEqual("DE", GeoIPLookup.Lookup(IpAddress6)?.IsoCode);
		}
		[TestMethod]
		public void BytecodeApi_GeoIP_Asn_GeoIPLookup_Lookup()
		{
			Assert.AreEqual("Liberty Global B.V.", GeoIPAsnLookup.Lookup(IpAddress4)?.Organization);
			Assert.AreEqual("Liberty Global B.V.", GeoIPAsnLookup.Lookup(IpAddress6)?.Organization);
		}
		[TestMethod]
		public void BytecodeApi_GeoIP_City_GeoIPLookup_Lookup()
		{
			Assert.AreEqual("Frankfurt am Main", GeoIPCityLookup.Lookup(IpAddress4)?.City.Name);
			Assert.AreEqual("Frankfurt am Main", GeoIPCityLookup.Lookup(IpAddress6)?.City.Name);
		}
		[TestMethod]
		public void BytecodeApi_GeoIP_DatabaseUpToDate()
		{
			// GeoLite2 database is updated on a monthly basis. If this test fails, Build.GeoIP needs to be run.
			if (HttpClient.Default.CreateGetRequest("https://geolite.maxmind.com/download/geoip/database/GeoLite2-Country-CSV.zip.md5").ReadString() != "94119dd84078673777f861bf8ff0b537" ||
				HttpClient.Default.CreateGetRequest("https://geolite.maxmind.com/download/geoip/database/GeoLite2-ASN-CSV.zip.md5").ReadString() != "ec1771a9c15b167e2b4ccaf30df0fe06" ||
				HttpClient.Default.CreateGetRequest("https://geolite.maxmind.com/download/geoip/database/GeoLite2-City-CSV.zip.md5").ReadString() != "7e1174519d1aa1020230947f0dd32d4c")
			{
				Assert.Inconclusive("GeoIP database is not up-to-date");
			}
		}
	}
}