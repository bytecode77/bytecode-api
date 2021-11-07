using System.Globalization;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="double" /> objects.
	/// </summary>
	public static class DoubleExtensions
	{
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number evaluates to negative or positive infinity.
		/// </summary>
		/// <param name="value">The <see cref="double" /> value to test.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> evaluates to <see cref="double.PositiveInfinity" /> or <see cref="double.NegativeInfinity" />.
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsInfinity(this double value)
		{
			return double.IsInfinity(value);
		}
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number is not a number (<see cref="double.NaN" />).
		/// </summary>
		/// <param name="value">The <see cref="double" /> value to test.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> evaluates to <see cref="double.NaN" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsNaN(this double value)
		{
			return double.IsNaN(value);
		}
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number evaluates to negative infinity.
		/// </summary>
		/// <param name="value">The <see cref="double" /> value to test.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> evaluates to <see cref="double.NegativeInfinity" />.
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsNegativeInfinity(this double value)
		{
			return double.IsNegativeInfinity(value);
		}
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number evaluates to positive infinity.
		/// </summary>
		/// <param name="value">The <see cref="double" /> value to test.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="value" /> evaluates to <see cref="double.PositiveInfinity" />.
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsPositiveInfinity(this double value)
		{
			return double.IsPositiveInfinity(value);
		}
		/// <summary>
		/// Converts the value of this <see cref="double" /> to its equivalent <see cref="string" /> representation using the invariant culture.
		/// </summary>
		/// <param name="value">The <see cref="double" /> value to convert.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="double" />.
		/// </returns>
		public static string ToStringInvariant(this double value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Converts the value of this <see cref="double" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="value">The <see cref="double" /> value to convert.</param>
		/// <param name="format">A <see cref="string" /> value with the format that is used to convert this <see cref="double" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="double" />.
		/// </returns>
		public static string ToStringInvariant(this double value, string format)
		{
			return value.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="double" /> value is 0.0, otherwise its original value.
		/// </summary>
		/// <param name="value">The <see cref="double" /> value to convert.</param>
		/// <returns>
		/// <see langword="null" />, if this <see cref="double" /> value is 0.0;
		/// otherwise, its original value.
		/// </returns>
		public static double? ToNullIfDefault(this double value)
		{
			return value == default ? null : (double?)value;
		}
	}
}