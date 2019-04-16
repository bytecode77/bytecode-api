using BytecodeApi.GeoIP.City;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace UnitTest
{
	[TestClass]
	public class GeoIPCityTests
	{
		[TestMethod]
		public void BytecodeApi_GeoIP_City_GeoIPCityLookup_Lookup_IPv4()
		{
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("123.45.67.89"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("56.78.90.123"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("78.30.58.111"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("23.9.50.250"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("12.31.222.56"))?.City.Name);
			Assert.AreEqual("Mianyang", GeoIPCityLookup.Lookup(IPAddress.Parse("139.205.190.243"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("170.23.68.67"))?.City.Name);
			Assert.AreEqual("Seattle", GeoIPCityLookup.Lookup(IPAddress.Parse("17.25.79.120"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("60.89.183.10"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("190.97.231.17"))?.City.Name);
		}
		[TestMethod]
		public void BytecodeApi_GeoIP_City_GeoIPCityLookup_Lookup_IPv6()
		{
			Assert.AreEqual("Frankfurt am Main", GeoIPCityLookup.Lookup(IPAddress.Parse("2a00:1450:4001:816::200e"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("2804:21d4:4001:816::200e"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("2a00:c920:8000:816::200e"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("2a01:73a0:4001:816::200e"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("2a03:b600:241:0:20:1337::"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("2a03:b600:357:1337::20"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("2a05:d016:1337:1337::"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("2a0b:1306:2:1337:1337:1337::"))?.City.Name);
			Assert.AreEqual("Frankfurt am Main", GeoIPCityLookup.Lookup(IPAddress.Parse("2a00:1450:4001:816:1337::200e"))?.City.Name);
			Assert.IsNull(GeoIPCityLookup.Lookup(IPAddress.Parse("2a0c:7782:4000:1337::"))?.City.Name);
		}
	}
}