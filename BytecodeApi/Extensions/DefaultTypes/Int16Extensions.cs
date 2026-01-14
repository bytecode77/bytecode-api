using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="short" /> objects.
/// </summary>
public static class Int16Extensions
{
	extension(short value)
	{
		/// <summary>
		/// Converts the value of this <see cref="short" /> to its equivalent <see cref="string" /> representation using the invariant culture.
		/// </summary>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="short" />.
		/// </returns>
		public string ToStringInvariant()
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Converts the value of this <see cref="short" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value with the format that is used to convert this <see cref="short" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="short" />.
		/// </returns>
		public string ToStringInvariant(string format)
		{
			return value.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="short" /> value is 0, otherwise its original value.
		/// </summary>
		/// <returns>
		/// <see langword="null" />, if this <see cref="short" /> value is 0;
		/// otherwise, its original value.
		/// </returns>
		public short? ToNullIfDefault()
		{
			return value == default(short) ? null : value;
		}
	}
}