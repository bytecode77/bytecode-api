namespace BytecodeApi.GeoIP
{
	internal sealed class GeoIPRange6 : GeoIPRangeBase
	{
		public readonly byte[] From;
		public readonly byte[] To;

		public GeoIPRange6(GeoIPCountry country, bool isAnonymousProxy, bool isSatelliteProvider, byte[] from, byte[] to) : base(country, isAnonymousProxy, isSatelliteProvider)
		{
			From = from;
			To = to;
		}
	}
}