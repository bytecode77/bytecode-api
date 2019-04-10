using System.Globalization;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="float" /> objects.
	/// </summary>
	public static class SingleExtensions
	{
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number evaluates to negative or positive infinity.
		/// </summary>
		/// <param name="value">The <see cref="float" /> value to test.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> evaluates to <see cref="float.PositiveInfinity" /> or <see cref="float.NegativeInfinity" />.
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsInfinity(this float value)
		{
			return float.IsInfinity(value);
		}
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number is not a number (<see cref="float.NaN" />).
		/// </summary>
		/// <param name="value">The <see cref="float" /> value to test.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> evaluates to <see cref="float.NaN" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsNaN(this float value)
		{
			return float.IsNaN(value);
		}
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number evaluates to negative infinity.
		/// </summary>
		/// <param name="value">The <see cref="float" /> value to test.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> evaluates to <see cref="float.NegativeInfinity" />.
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsNegativeInfinity(this float value)
		{
			return float.IsNegativeInfinity(value);
		}
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number evaluates to positive infinity.
		/// </summary>
		/// <param name="value">The <see cref="float" /> value to test.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> evaluates to <see cref="float.PositiveInfinity" />.
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsPositiveInfinity(this float value)
		{
			return float.IsPositiveInfinity(value);
		}
		/// <summary>
		/// Converts the value of this <see cref="float" /> to its equivalent <see cref="string" /> representation using the invariant culture.
		/// </summary>
		/// <param name="value">The <see cref="float" /> value to convert.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="float" />.
		/// </returns>
		public static string ToStringInvariant(this float value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Converts the value of this <see cref="float" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="value">The <see cref="float" /> value to convert.</param>
		/// <param name="format">A <see cref="string" /> value with the format that is used to convert this <see cref="float" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="float" />.
		/// </returns>
		public static string ToStringInvariant(this float value, string format)
		{
			return value.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="float" /> value is 0.0f, otherwise its original value.
		/// </summary>
		/// <param name="value">The <see cref="float" /> value to convert.</param>
		/// <returns>
		/// <see langword="null" />, if this <see cref="float" /> value is 0.0f;
		/// otherwise, its original value.
		/// </returns>
		public static float? ToNullIfDefault(this float value)
		{
			return value == default ? (float?)null : value;
		}
	}
}