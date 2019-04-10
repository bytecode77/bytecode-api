namespace BytecodeApi.GeoIP.ASN
{
	internal sealed class AsnRange
	{
		public readonly Asn Asn;
		public readonly uint From;
		public readonly uint To;

		public AsnRange(Asn asn, uint from, uint to)
		{
			Asn = asn;
			From = from;
			To = to;
		}
	}
}