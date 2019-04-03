namespace BytecodeApi.GeoIP
{
	internal sealed class GeoIPRange : GeoIPRangeBase
	{
		public readonly uint From;
		public readonly uint To;

		public GeoIPRange(GeoIPCountry country, bool isAnonymousProxy, bool isSatelliteProvider, uint from, uint to) : base(country, isAnonymousProxy, isSatelliteProvider)
		{
			From = from;
			To = to;
		}
	}
}