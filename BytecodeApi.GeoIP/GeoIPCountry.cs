namespace BytecodeApi.GeoIP
{
	public class GeoIPCountry
	{
		public string Flag { get; private set; }
		public string Name { get; private set; }

		public GeoIPCountry(string flag, string name)
		{
			Flag = flag;
			Name = name;
		}
	}
}