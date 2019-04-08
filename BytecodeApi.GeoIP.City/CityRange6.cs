namespace BytecodeApi.GeoIP.City
{
	internal sealed class CityRange6
	{
		public City City;
		public byte[] From;
		public byte[] To;

		public CityRange6(City city, byte[] from, byte[] to)
		{
			City = city;
			From = from;
			To = to;
		}
	}
}