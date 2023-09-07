using System.Windows;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Visibility" /> objects.
/// </summary>
public static class VisibilityExtensions
{
	/// <summary>
	/// Converts this <see cref="Visibility" /> value to its equivalent <see cref="bool" /> representation. <see cref="Visibility.Visible" /> is converted to <see langword="true" />, <see cref="Visibility.Collapsed" /> is converted to <see langword="false" /> and <see cref="Visibility.Hidden" /> is converted to <see langword="null" />.
	/// </summary>
	/// <param name="visibility">The <see cref="Visibility" /> value to convert.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="visibility" /> is equal to <see cref="Visibility.Visible" />;
	/// <see langword="false" />, if <paramref name="visibility" /> is equal to <see cref="Visibility.Collapsed" />;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static bool? ToBoolean(this Visibility visibility)
	{
		return visibility switch
		{
			Visibility.Visible => true,
			Visibility.Collapsed => false,
			_ => null
		};
	}
}