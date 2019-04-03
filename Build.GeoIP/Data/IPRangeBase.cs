namespace Build.GeoIP.Data
{
	public abstract class IPRangeBase
	{
		public byte Country;
		public bool IsAnonymousProxy;
		public bool IsSatelliteProvider;
	}
}