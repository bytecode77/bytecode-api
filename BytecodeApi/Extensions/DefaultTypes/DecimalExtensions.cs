using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="decimal" /> objects.
/// </summary>
public static class DecimalExtensions
{
	extension(decimal value)
	{
		/// <summary>
		/// Converts the value of this <see cref="decimal" /> to its equivalent <see cref="string" /> representation using the invariant culture.
		/// </summary>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="decimal" />.
		/// </returns>
		public string ToStringInvariant()
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Converts the value of this <see cref="decimal" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value with the format that is used to convert this <see cref="decimal" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="decimal" />.
		/// </returns>
		public string ToStringInvariant(string format)
		{
			return value.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="decimal" /> value is 0.0m, otherwise its original value.
		/// </summary>
		/// <returns>
		/// <see langword="null" />, if this <see cref="decimal" /> value is 0.0m;
		/// otherwise, its original value.
		/// </returns>
		public decimal? ToNullIfDefault()
		{
			return value == default ? null : value;
		}
	}
}