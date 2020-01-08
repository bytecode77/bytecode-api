using BytecodeApi.Text;
using System;
using System.Diagnostics;

namespace BytecodeApi.GeoIP.City
{
	/// <summary>
	/// Represents a city with a name and a <see cref="GeoIP.Country" /> reference.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class City : IEquatable<City>
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<City>("Name = {0}, Subdivision1: {1} ({2})", new QuotedString(Name), new QuotedString(Subdivision1Name), Subdivision1IsoCode);
		/// <summary>
		/// Gets the <see cref="GeoIP.Country" /> reference associated with this <see cref="City" />.
		/// </summary>
		public Country Country { get; private set; }
		/// <summary>
		/// Gets the name of the city.
		/// <para>Example: Frankfurt am Main</para>
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the name of the least specific subdivision. This is typically the state.
		/// <para>Example: Hesse</para>
		/// </summary>
		public string Subdivision1Name { get; private set; }
		/// <summary>
		/// Gets the ISO code of the least specific subdivision. This is typically the state.
		/// <para>Example: HE</para>
		/// </summary>
		public string Subdivision1IsoCode { get; private set; }
		/// <summary>
		/// Gets the name of the most specific subdivision. This is typically the state.
		/// </summary>
		public string Subdivision2Name { get; private set; }
		/// <summary>
		/// Gets the ISO code of the most specific subdivision. This is typically the state.
		/// </summary>
		public string Subdivision2IsoCode { get; private set; }
		/// <summary>
		/// Gets the time zone name the city.
		/// <para>Example: Europe/Berlin</para>
		/// </summary>
		public string TimeZone { get; private set; }

		internal City(Country country, string name, string subdivision1Name, string subdivision1IsoCode, string subdivision2Name, string subdivision2IsoCode, string timeZone)
		{
			Country = country;
			Name = name;
			Subdivision1Name = subdivision1Name;
			Subdivision1IsoCode = subdivision1IsoCode;
			Subdivision2Name = subdivision2Name;
			Subdivision2IsoCode = subdivision2IsoCode;
			TimeZone = timeZone;
		}

		/// <summary>
		/// Returns the name of this <see cref="City" />.
		/// </summary>
		/// <returns>
		/// The name of this <see cref="City" />.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}
		/// <summary>
		/// Determines whether the specified <see cref="object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Equals(object obj)
		{
			return obj is City city && Equals(city);
		}
		/// <summary>
		/// Determines whether this instance is equal to another <see cref="City" />.
		/// </summary>
		/// <param name="other">The <see cref="City" /> to compare to this instance.</param>
		/// <returns>
		/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(City other)
		{
			return other != null && Name == other.Name && Subdivision1Name == other.Subdivision1Name && Subdivision2Name == other.Subdivision2Name && TimeZone == other.TimeZone;
		}
		/// <summary>
		/// Returns a hash code for this <see cref="City" />.
		/// </summary>
		/// <returns>
		/// The hash code for this <see cref="City" /> instance.
		/// </returns>
		public override int GetHashCode()
		{
			return CSharp.GetHashCode(Country, Name, Subdivision1Name, Subdivision2Name, TimeZone);
		}

		/// <summary>
		/// Compares two <see cref="City" /> instances for equality.
		/// </summary>
		/// <param name="a">The first <see cref="City" /> to compare.</param>
		/// <param name="b">The second <see cref="City" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="City" /> are equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator ==(City a, City b)
		{
			return Equals(a, b);
		}
		/// <summary>
		/// Compares two <see cref="City" /> instances for inequality.
		/// </summary>
		/// <param name="a">The first <see cref="City" /> to compare.</param>
		/// <param name="b">The second <see cref="City" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="City" /> are not equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator !=(City a, City b)
		{
			return !(a == b);
		}
	}
}