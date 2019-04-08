namespace BytecodeApi.GeoIP
{
	internal sealed class IPRange
	{
		public readonly Country Country;
		public readonly uint From;
		public readonly uint To;

		public IPRange(Country country, uint from, uint to)
		{
			Country = country;
			From = from;
			To = to;
		}
	}
}