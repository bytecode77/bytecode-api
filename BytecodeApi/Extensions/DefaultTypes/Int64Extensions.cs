using System.Globalization;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="long" /> objects.
	/// </summary>
	public static class Int64Extensions
	{
		/// <summary>
		/// Converts the value of this <see cref="long" /> to its equivalent <see cref="string" /> representation using the invariant culture.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to convert.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="long" />.
		/// </returns>
		public static string ToStringInvariant(this long value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Converts the value of this <see cref="long" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to convert.</param>
		/// <param name="format">A <see cref="string" /> value with the format that is used to convert this <see cref="long" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="long" />.
		/// </returns>
		public static string ToStringInvariant(this long value, string format)
		{
			return value.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="long" /> value is 0L, otherwise its original value.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to convert.</param>
		/// <returns>
		/// <see langword="null" />, if this <see cref="long" /> value is 0L;
		/// otherwise, its original value.
		/// </returns>
		public static long? ToNullIfDefault(this long value)
		{
			return value == default ? (long?)null : value;
		}
	}
}