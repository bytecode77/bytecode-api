namespace BytecodeApi.GeoIP.City
{
	internal sealed class CityRange6
	{
		public City City;
		public string PostalCode;
		public float Latitude;
		public float Longitude;
		public short AccuracyRadius;
		public byte[] From;
		public byte[] To;

		public CityRange6(City city, string postalCode, float latitude, float longitude, short accuracyRadius, byte[] from, byte[] to)
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