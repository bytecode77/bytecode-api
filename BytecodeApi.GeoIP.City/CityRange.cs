namespace BytecodeApi.GeoIP.City
{
	internal sealed class CityRange
	{
		public City City;
		public uint From;
		public uint To;

		public CityRange(City city, uint from, uint to)
		{
			City = city;
			From = from;
			To = to;
		}
	}
}