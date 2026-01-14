using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="double" /> objects.
/// </summary>
public static class DoubleExtensions
{
	extension(double value)
	{
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number evaluates to negative or positive infinity.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="double" /> evaluates to <see cref="double.PositiveInfinity" /> or <see cref="double.NegativeInfinity" />.
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsInfinity()
		{
			return double.IsInfinity(value);
		}
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number is not a number (<see cref="double.NaN" />).
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="double" /> evaluates to <see cref="double.NaN" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsNaN()
		{
			return double.IsNaN(value);
		}
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number evaluates to negative infinity.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="double" /> evaluates to <see cref="double.NegativeInfinity" />.
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsNegativeInfinity()
		{
			return double.IsNegativeInfinity(value);
		}
		/// <summary>
		/// Returns a <see cref="bool" /> value indicating whether this number evaluates to positive infinity.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if this <see cref="double" /> evaluates to <see cref="double.PositiveInfinity" />.
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsPositiveInfinity()
		{
			return double.IsPositiveInfinity(value);
		}
		/// <summary>
		/// Converts the value of this <see cref="double" /> to its equivalent <see cref="string" /> representation using the invariant culture.
		/// </summary>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="double" />.
		/// </returns>
		public string ToStringInvariant()
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Converts the value of this <see cref="double" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value with the format that is used to convert this <see cref="double" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="double" />.
		/// </returns>
		public string ToStringInvariant(string format)
		{
			return value.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="double" /> value is 0.0, otherwise its original value.
		/// </summary>
		/// <returns>
		/// <see langword="null" />, if this <see cref="double" /> value is 0.0;
		/// otherwise, its original value.
		/// </returns>
		public double? ToNullIfDefault()
		{
			return value == default ? null : value;
		}
	}
}