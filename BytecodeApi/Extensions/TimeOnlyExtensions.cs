using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="TimeOnly" /> objects.
/// </summary>
public static class TimeOnlyExtensions
{
	/// <summary>
	/// Converts the value of this <see cref="TimeOnly" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
	/// </summary>
	/// <param name="timeOnly">The <see cref="TimeOnly" /> value to convert.</param>
	/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="TimeOnly" />.</param>
	/// <returns>
	/// The equivalent <see cref="string" /> representation of this <see cref="TimeOnly" />.
	/// </returns>
	public static string ToStringInvariant(this TimeOnly timeOnly, string format)
	{
		return timeOnly.ToString(format, CultureInfo.InvariantCulture);
	}
	/// <summary>
	/// Returns <see langword="null" />, if this <see cref="TimeOnly" /> object is <see langword="default" />(<see cref="TimeOnly" />), otherwise its original value.
	/// </summary>
	/// <param name="timeOnly">The <see cref="TimeOnly" /> value to convert.</param>
	/// <returns>
	/// <see langword="null" />, if this <see cref="TimeOnly" /> object is <see langword="default" />(<see cref="TimeOnly" />);
	/// otherwise, its original value.
	/// </returns>
	public static TimeOnly? ToNullIfDefault(this TimeOnly timeOnly)
	{
		return timeOnly == default ? null : timeOnly;
	}
}