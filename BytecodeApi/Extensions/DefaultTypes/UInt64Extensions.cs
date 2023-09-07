using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="ulong" /> objects.
/// </summary>
public static class UInt64Extensions
{
	/// <summary>
	/// Converts the value of this <see cref="ulong" /> to its equivalent <see cref="string" /> representation using the invariant culture.
	/// </summary>
	/// <param name="value">The <see cref="ulong" /> value to convert.</param>
	/// <returns>
	/// The equivalent <see cref="string" /> representation of this <see cref="ulong" />.
	/// </returns>
	public static string ToStringInvariant(this ulong value)
	{
		return value.ToString(CultureInfo.InvariantCulture);
	}
	/// <summary>
	/// Converts the value of this <see cref="ulong" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
	/// </summary>
	/// <param name="value">The <see cref="ulong" /> value to convert.</param>
	/// <param name="format">A <see cref="string" /> value with the format that is used to convert this <see cref="ulong" />.</param>
	/// <returns>
	/// The equivalent <see cref="string" /> representation of this <see cref="ulong" />.
	/// </returns>
	public static string ToStringInvariant(this ulong value, string format)
	{
		return value.ToString(format, CultureInfo.InvariantCulture);
	}
	/// <summary>
	/// Returns <see langword="null" />, if this <see cref="ulong" /> value is 0, otherwise its original value.
	/// </summary>
	/// <param name="value">The <see cref="ulong" /> value to convert.</param>
	/// <returns>
	/// <see langword="null" />, if this <see cref="ulong" /> value is 0;
	/// otherwise, its original value.
	/// </returns>
	public static ulong? ToNullIfDefault(this ulong value)
	{
		return value == default ? null : value;
	}
}