namespace BytecodeApi.GeoIP
{
	internal sealed class IPRange6
	{
		public readonly Country Country;
		public readonly byte[] From;
		public readonly byte[] To;

		public IPRange6(Country country, byte[] from, byte[] to)
		{
			Country = country;
			From = from;
			To = to;
		}
	}
}