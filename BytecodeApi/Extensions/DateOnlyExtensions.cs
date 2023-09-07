using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="DateOnly" /> objects.
/// </summary>
public static class DateOnlyExtensions
{
	/// <summary>
	/// Converts the value of this <see cref="DateOnly" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
	/// </summary>
	/// <param name="dateOnly">The <see cref="DateOnly" /> value to convert.</param>
	/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="DateOnly" />.</param>
	/// <returns>
	/// The equivalent <see cref="string" /> representation of this <see cref="DateOnly" />.
	/// </returns>
	public static string ToStringInvariant(this DateOnly dateOnly, string format)
	{
		return dateOnly.ToString(format, CultureInfo.InvariantCulture);
	}
	/// <summary>
	/// Returns <see langword="null" />, if this <see cref="DateOnly" /> object is <see langword="default" />(<see cref="DateOnly" />), otherwise its original value.
	/// </summary>
	/// <param name="dateOnly">The <see cref="DateOnly" /> value to convert.</param>
	/// <returns>
	/// <see langword="null" />, if this <see cref="DateOnly" /> object is <see langword="default" />(<see cref="DateOnly" />);
	/// otherwise, its original value.
	/// </returns>
	public static DateOnly? ToNullIfDefault(this DateOnly dateOnly)
	{
		return dateOnly == default ? null : dateOnly;
	}
	/// <summary>
	/// Returns a <see cref="DateTime" /> from this <see cref="DateOnly" /> value.
	/// </summary>
	/// <param name="dateOnly">The <see cref="DateOnly" /> value to convert.</param>
	/// <returns>
	/// The converted <see cref="DateOnly" /> value.
	/// </returns>
	public static DateTime ToDateTime(this DateOnly dateOnly)
	{
		return dateOnly.ToDateTime(TimeOnly.MinValue);
	}
}