using BytecodeApi.GeoIP;
using BytecodeApi.IO.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace UnitTest
{
	[TestClass]
	public class GeoIPTests
	{
		[TestMethod]
		public void BytecodeApi_GeoIP_GeoIPLookup_Lookup_IPv4()
		{
			Assert.AreEqual("PL", GeoIPLookup.Lookup(IPAddress.Parse("123.45.67.89"))?.IsoCode);
			Assert.AreEqual("CN", GeoIPLookup.Lookup(IPAddress.Parse("56.78.90.123"))?.IsoCode);
			Assert.AreEqual("CN", GeoIPLookup.Lookup(IPAddress.Parse("78.30.58.111"))?.IsoCode);
			Assert.IsNull(GeoIPLookup.Lookup(IPAddress.Parse("23.9.50.250"))?.IsoCode);
			Assert.AreEqual("US", GeoIPLookup.Lookup(IPAddress.Parse("10.31.222.56"))?.IsoCode);
			Assert.IsNull(GeoIPLookup.Lookup(IPAddress.Parse("240.205.190.243"))?.IsoCode);
			Assert.AreEqual("CA", GeoIPLookup.Lookup(IPAddress.Parse("170.23.68.67"))?.IsoCode);
			Assert.AreEqual("CN", GeoIPLookup.Lookup(IPAddress.Parse("17.25.79.120"))?.IsoCode);
			Assert.IsNull(GeoIPLookup.Lookup(IPAddress.Parse("60.89.183.10"))?.IsoCode);
			Assert.AreEqual("US", GeoIPLookup.Lookup(IPAddress.Parse("190.97.231.17"))?.IsoCode);
		}
		[TestMethod]
		public void BytecodeApi_GeoIP_GeoIPLookup_Lookup_IPv6()
		{
			Assert.AreEqual("DE", GeoIPLookup.Lookup(IPAddress.Parse("2a00:1450:4001:816::200e"))?.IsoCode);
			Assert.AreEqual("BR", GeoIPLookup.Lookup(IPAddress.Parse("2804:21d4:4001:816::200e"))?.IsoCode);
			Assert.AreEqual("BE", GeoIPLookup.Lookup(IPAddress.Parse("2a00:c920:8000:816::200e"))?.IsoCode);
			Assert.AreEqual("FR", GeoIPLookup.Lookup(IPAddress.Parse("2a01:73a0:4001:816::200e"))?.IsoCode);
			Assert.AreEqual("US", GeoIPLookup.Lookup(IPAddress.Parse("2a03:b600:241:0:20:1337::"))?.IsoCode);
			Assert.AreEqual("US", GeoIPLookup.Lookup(IPAddress.Parse("2a03:b600:357:1337::20"))?.IsoCode);
			Assert.AreEqual("IE", GeoIPLookup.Lookup(IPAddress.Parse("2a05:d016:1337:1337::"))?.IsoCode);
			Assert.AreEqual("US", GeoIPLookup.Lookup(IPAddress.Parse("2a0b:1306:2:1337:1337:1337::"))?.IsoCode);
			Assert.AreEqual("DE", GeoIPLookup.Lookup(IPAddress.Parse("2a00:1450:4001:816:1337::200e"))?.IsoCode);
			Assert.AreEqual("GB", GeoIPLookup.Lookup(IPAddress.Parse("2a0c:7782:4000:1337::"))?.IsoCode);
		}
		[TestMethod]
		public void BytecodeApi_GeoIP_DatabaseUpToDate()
		{
			// GeoLite2 database is updated on a monthly basis. If this test fails, Build.GeoIP needs to be run.
			Assert.AreEqual("d1b05e69850da8c39d8287551f29755f", HttpClient.Default.GetString("https://geolite.maxmind.com/download/geoip/database/GeoLite2-Country-CSV.zip.md5"));
		}
	}
}