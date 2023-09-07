using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="bool" /> objects.
/// </summary>
public static class BooleanExtensions
{
	/// <summary>
	/// Converts the value of this <see cref="bool" /> to its equivalent <see cref="string" /> representation using the invariant culture.
	/// </summary>
	/// <param name="value">The <see cref="bool" /> value to convert.</param>
	/// <returns>
	/// The equivalent <see cref="string" /> representation of this <see cref="bool" />.
	/// </returns>
	public static string ToStringInvariant(this bool value)
	{
		return value.ToString(CultureInfo.InvariantCulture);
	}
	/// <summary>
	/// Returns <see langword="null" />, if this <see cref="bool" /> value is <see langword="false" />, otherwise its original value.
	/// </summary>
	/// <param name="value">The <see cref="bool" /> value to convert.</param>
	/// <returns>
	/// <see langword="null" />, if this <see cref="bool" /> value is <see langword="false" />;
	/// otherwise, its original value.
	/// </returns>
	public static bool? ToNullIfDefault(this bool value)
	{
		return value != default ? value : null;
	}
}