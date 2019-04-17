using System;

namespace BytecodeApi.GeoIP
{
	/// <summary>
	/// Represents a country with a name, an ISO code and related properties.
	/// </summary>
	public sealed class Country : IEquatable<Country>
	{
		/// <summary>
		/// Gets the name of the country.
		/// <para>Example: United States</para>
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the ISO code of the country. This <see cref="string" /> is composed of two uppercase characters.
		/// <para>Example: US</para>
		/// </summary>
		public string IsoCode { get; private set; }
		/// <summary>
		/// Gets the continent name of the country.
		/// <para>Example: North America</para>
		/// </summary>
		public string Continent { get; private set; }
		/// <summary>
		/// Gets the continent code of the country. This <see cref="string" /> is composed of two uppercase characters.
		/// <para>Example: EU</para>
		/// </summary>
		public string ContinentIsoCode { get; private set; }
		/// <summary>
		/// Gets a value indicating whether the country is in the european union.
		/// </summary>
		public bool EuropeanUnion { get; private set; }

		internal Country(string name, string isoCode, string continent, string continentIsoCode, bool europeanUnion)
		{
			Name = name;
			IsoCode = isoCode;
			Continent = continent;
			ContinentIsoCode = continentIsoCode;
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
			return obj is Country country && Equals(country);
		}
		/// <summary>
		/// Determines whether this instance is equal to another <see cref="Country" />.
		/// </summary>
		/// <param name="other">The <see cref="Country" /> to compare to this instance.</param>
		/// <returns>
		/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(Country other)
		{
			return other != null && Name == other.Name;
		}
		/// <summary>
		/// Returns a hash code for this <see cref="Country" />.
		/// </summary>
		/// <returns>
		/// The hash code for this <see cref="Country" /> instance.
		/// </returns>
		public override int GetHashCode()
		{
			return Name.GetHashCode();
		}

		/// <summary>
		/// Compares two <see cref="Country" /> instances for equality.
		/// </summary>
		/// <param name="a">The first <see cref="Country" /> to compare.</param>
		/// <param name="b">The second <see cref="Country" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="Country" /> are equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator ==(Country a, Country b)
		{
			return Equals(a, b);
		}
		/// <summary>
		/// Compares two <see cref="Country" /> instances for inequality.
		/// </summary>
		/// <param name="a">The first <see cref="Country" /> to compare.</param>
		/// <param name="b">The second <see cref="Country" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="Country" /> are not equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator !=(Country a, Country b)
		{
			return !(a == b);
		}
	}
}