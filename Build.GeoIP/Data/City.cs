namespace Build.GeoIP.Data
{
	public sealed class City
	{
		public int ID;
		public byte CountryIndex;
		public string Name;
		public string Subdivision1Name;
		public string Subdivision1IsoCode;
		public string Subdivision2Name;
		public string Subdivision2IsoCode;
		public string TimeZone;
	}
}