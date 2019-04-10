using BytecodeApi.GeoIP.ASN;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace UnitTest
{
	[TestClass]
	public class GeoIPAsnTests
	{
		[TestMethod]
		public void BytecodeApi_GeoIP_ASN_GeoIPAsnLookup_Lookup_IPv4()
		{
			Assert.AreEqual("SamsungSDS Inc.", GeoIPAsnLookup.Lookup(IPAddress.Parse("123.45.67.89"))?.Organization);
			Assert.IsNull(GeoIPAsnLookup.Lookup(IPAddress.Parse("56.78.90.123"))?.Organization);
			Assert.AreEqual("Xtra Telecom S.A.", GeoIPAsnLookup.Lookup(IPAddress.Parse("78.30.58.111"))?.Organization);
			Assert.AreEqual("INTERNEXA S.A. E.S.P", GeoIPAsnLookup.Lookup(IPAddress.Parse("23.9.50.250"))?.Organization);
			Assert.AreEqual("AT&T Services, Inc.", GeoIPAsnLookup.Lookup(IPAddress.Parse("12.31.222.56"))?.Organization);
			Assert.AreEqual("No.31,Jin-rong Street", GeoIPAsnLookup.Lookup(IPAddress.Parse("139.205.190.243"))?.Organization);
			Assert.IsNull(GeoIPAsnLookup.Lookup(IPAddress.Parse("170.23.68.67"))?.Organization);
			Assert.AreEqual("Apple Inc.", GeoIPAsnLookup.Lookup(IPAddress.Parse("17.25.79.120"))?.Organization);
			Assert.AreEqual("Softbank BB Corp.", GeoIPAsnLookup.Lookup(IPAddress.Parse("60.89.183.10"))?.Organization);
			Assert.AreEqual("NetLink América C.A.", GeoIPAsnLookup.Lookup(IPAddress.Parse("190.97.231.17"))?.Organization);
		}
		[TestMethod]
		public void BytecodeApi_GeoIP_ASN_GeoIPAsnLookup_Lookup_IPv6()
		{
			Assert.AreEqual("Google LLC", GeoIPAsnLookup.Lookup(IPAddress.Parse("2a00:1450:4001:816::200e"))?.Organization);
			Assert.IsNull(GeoIPAsnLookup.Lookup(IPAddress.Parse("2804:21d4:4001:816::200e"))?.Organization);
			Assert.AreEqual("Crossing Telecom Sarl", GeoIPAsnLookup.Lookup(IPAddress.Parse("2a00:c920:8000:816::200e"))?.Organization);
			Assert.IsNull(GeoIPAsnLookup.Lookup(IPAddress.Parse("2a01:73a0:4001:816::200e"))?.Organization);
			Assert.AreEqual("RESNET", GeoIPAsnLookup.Lookup(IPAddress.Parse("2a03:b600:241:0:20:1337::"))?.Organization);
			Assert.AreEqual("RESNET", GeoIPAsnLookup.Lookup(IPAddress.Parse("2a03:b600:357:1337::20"))?.Organization);
			Assert.IsNull(GeoIPAsnLookup.Lookup(IPAddress.Parse("2a05:d016:1337:1337::"))?.Organization);
			Assert.AreEqual("trivago N.V.", GeoIPAsnLookup.Lookup(IPAddress.Parse("2a0b:1306:2:1337:1337:1337::"))?.Organization);
			Assert.AreEqual("Google LLC", GeoIPAsnLookup.Lookup(IPAddress.Parse("2a00:1450:4001:816:1337::200e"))?.Organization);
			Assert.IsNull(GeoIPAsnLookup.Lookup(IPAddress.Parse("2a0c:7782:4000:1337::"))?.Organization);
		}
	}
}