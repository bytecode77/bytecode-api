using System.ComponentModel;
using System.Reflection;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see langword="enum" /> objects.
/// </summary>
public static class EnumExtensions
{
	/// <summary>
	/// Returns the <see cref="DescriptionAttribute.Description" /> of this <see langword="enum" /> value, or <see langword="null" />, if the attribute was not found.
	/// </summary>
	/// <param name="value">The <see cref="Enum" /> value to be processed.</param>
	/// <returns>
	/// The <see cref="DescriptionAttribute.Description" /> of this <see langword="enum" /> value, or <see langword="null" />, if the attribute was not found.
	/// </returns>
	public static string? GetDescription(this Enum value)
	{
		Check.ArgumentNull(value);

		return value
			.GetType()
			.GetField(value.ToString())
			?.GetCustomAttribute<DescriptionAttribute>()
			?.Description;
	}
}