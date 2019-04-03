namespace BytecodeApi.GeoIP
{
	internal sealed class GeoIPRange
	{
		public readonly GeoIPCountry Country;
		public readonly uint From;
		public readonly uint To;

		public GeoIPRange(GeoIPCountry country, uint from, uint to)
		{
			Country = country;
			From = from;
			To = to;
		}
	}
}