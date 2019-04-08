namespace BytecodeApi.GeoIP.City
{
	internal sealed class CityRange
	{
		public City City;
		public string PostalCode;
		public float Latitude;
		public float Longitude;
		public short AccuracyRadius;
		public uint From;
		public uint To;

		public CityRange(City city, string postalCode, float latitude, float longitude, short accuracyRadius, uint from, uint to)
		{
			City = city;
			PostalCode = postalCode;
			Latitude = latitude;
			Longitude = longitude;
			AccuracyRadius = accuracyRadius;
			From = from;
			To = to;
		}
	}
}