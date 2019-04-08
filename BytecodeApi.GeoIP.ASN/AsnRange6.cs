namespace BytecodeApi.GeoIP.ASN
{
	internal sealed class AsnRange6
	{
		public readonly Asn Asn;
		public readonly byte[] From;
		public readonly byte[] To;

		public AsnRange6(Asn asn, byte[] from, byte[] to)
		{
			Asn = asn;
			From = from;
			To = to;
		}
	}
}