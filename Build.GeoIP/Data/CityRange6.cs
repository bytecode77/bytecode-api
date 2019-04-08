namespace Build.GeoIP.Data
{
	public sealed class CityRange6
	{
		public int CityIndex;
		public string PostalCode;
		public float Latitude;
		public float Longitude;
		public short AccuracyRadius;
		public byte[] From;
		public byte[] To;
	}
}