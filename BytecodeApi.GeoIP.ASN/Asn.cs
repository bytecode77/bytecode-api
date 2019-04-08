namespace BytecodeApi.GeoIP.ASN
{
	/// <summary>
	/// Represents an Autonomous System Number with an organization name.
	/// </summary>
	public sealed class Asn
	{
		/// <summary>
		/// Gets the Autonomous System Number.
		/// <para>Example: 46164</para>
		/// </summary>
		public int Number { get; private set; }
		/// <summary>
		/// Gets the name of the Autonomous System Organization.
		/// <para>Example: AT&amp;T Mobility LLC</para>
		/// </summary>
		public string Organization { get; private set; }

		internal Asn(int number, string organization)
		{
			Number = number;
			Organization = organization;
		}
	}
}