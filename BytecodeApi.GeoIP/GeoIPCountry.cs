using System;

namespace BytecodeApi.GeoIP
{
	public class GeoIPCountry : IEquatable<GeoIPCountry>
	{
		public string Name { get; private set; }
		public string Continent { get; private set; }
		public string ContinentCode { get; private set; }
		public string IsoCode { get; private set; }
		public bool EuropeanUnion { get; private set; }

		public GeoIPCountry(string name, string continent, string continentCode, string isoCode, bool europeanUnion)
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