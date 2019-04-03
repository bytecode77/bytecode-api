namespace BytecodeApi.GeoIP
{
	internal class GeoIPRange6
	{
		public readonly GeoIPCountry Country;
		public readonly byte[] From;
		public readonly byte[] To;

		public GeoIPRange6(GeoIPCountry country, byte[] from, byte[] to)
		{
			Country = country;
			From = from;
			To = to;
		}
	}
}