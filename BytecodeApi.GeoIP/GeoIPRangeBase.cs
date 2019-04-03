namespace BytecodeApi.GeoIP
{
	internal abstract class GeoIPRangeBase
	{
		public readonly GeoIPCountry Country;
		public readonly bool IsAnonymousProxy;
		public readonly bool IsSatelliteProvider;

		public GeoIPRangeBase(GeoIPCountry country, bool isAnonymousProxy, bool isSatelliteProvider)
		{
			Country = country;
			IsAnonymousProxy = isAnonymousProxy;
			IsSatelliteProvider = isSatelliteProvider;
		}
	}
}