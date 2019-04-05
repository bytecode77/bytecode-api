using System;

namespace BytecodeApi.GeoIP
{
	/// <summary>
	/// Represents a country with a name, an ISO code and related properties.
	/// </summary>
	public sealed class GeoIPCountry : IEquatable<GeoIPCountry>
	{
		/// <summary>
		/// Gets the name of the country.
		/// <para>Example: United States</para>
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the continent name of the country.
		/// <para>Example: North America</para>
		/// </summary>
		public string Continent { get; private set; }
		/// <summary>
		/// Gets the continent code of the country. This <see cref="string" /> is composed of two upper case characters.
		/// <para>Example: EU</para>
		/// </summary>
		public string ContinentCode { get; private set; }
		/// <summary>
		/// Gets the ISO code of the country. This <see cref="string" /> is composed of two upper case characters.
		/// <para>Example: US</para>
		/// </summary>
		public string IsoCode { get; private set; }
		/// <summary>
		/// Gets a value indicating whether the country is in the european union.
		/// </summary>
		public bool EuropeanUnion { get; private set; }

		internal GeoIPCountry(string name, string continent, string continentCode, string isoCode, bool europeanUnion)
		{
			Name = name;
			Continent = continent;
			ContinentCode = continentCode;
			IsoCode = isoCode;
			EuropeanUnion = europeanUnion;
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Name + ", " + IsoCode + "]";
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
			return obj is GeoIPCountry country && Equals(country);
		}
		/// <summary>
		/// Determines whether this instance is equal to another <see cref="GeoIPCountry" />.
		/// </summary>
		/// <param name="other">The <see cref="GeoIPCountry" /> to compare to this instance.</param>
		/// <returns>
		/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(GeoIPCountry other)
		{
			return Name == other.Name;
		}
		/// <summary>
		/// Returns a hash code for this <see cref="GeoIPCountry" />.
		/// </summary>
		/// <returns>
		/// The hash code for this <see cref="GeoIPCountry" /> instance.
		/// </returns>
		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		/// <summary>
		/// Compares two <see cref="GeoIPCountry" /> instances for equality.
		/// </summary>
		/// <param name="a">The first <see cref="GeoIPCountry" /> to compare.</param>
		/// <param name="b">The second <see cref="GeoIPCountry" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="GeoIPCountry" /> are equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator ==(GeoIPCountry a, GeoIPCountry b)
		{
			return Equals(a, b);
		}
		/// <summary>
		/// Compares two <see cref="GeoIPCountry" /> instances for inequality.
		/// </summary>
		/// <param name="a">The first <see cref="GeoIPCountry" /> to compare.</param>
		/// <param name="b">The second <see cref="GeoIPCountry" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="GeoIPCountry" /> are not equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator !=(GeoIPCountry a, GeoIPCountry b)
		{
			return !(a == b);
		}
	}
}