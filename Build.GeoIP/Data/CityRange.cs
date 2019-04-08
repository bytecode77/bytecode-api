namespace Build.GeoIP.Data
{
	public sealed class CityRange
	{
		public int CityIndex;
		public string PostalCode;
		public float Latitude;
		public float Longitude;
		public short AccuracyRadius;
		public uint From;
		public uint To;
	}
}