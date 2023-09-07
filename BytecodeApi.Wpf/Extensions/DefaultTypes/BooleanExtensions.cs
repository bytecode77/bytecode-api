using System.Windows;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="bool" /> objects.
/// </summary>
public static class BooleanExtensions
{
	/// <summary>
	/// Converts this <see cref="bool" /> value to <see cref="Visibility.Visible" /> or <see cref="Visibility.Collapsed" />.
	/// </summary>
	/// <param name="value">The <see cref="bool" /> value to be converted.</param>
	/// <returns>
	/// <see cref="Visibility.Visible" />, if <paramref name="value" /> is <see langword="true" />;
	/// otherwise, <see cref="Visibility.Collapsed" />.
	/// </returns>
	public static Visibility ToVisibility(this bool value)
	{
		return value.ToVisibility(false);
	}
	/// <summary>
	/// Converts this <see cref="bool" /> value to <see cref="Visibility.Visible" />, <see cref="Visibility.Hidden" /> or <see cref="Visibility.Collapsed" /> depending on its value and the <paramref name="preserveSpace" /> parameter.
	/// </summary>
	/// <param name="value">The <see cref="bool" /> value to be converted.</param>
	/// <param name="preserveSpace"><see langword="true" /> to use <see cref="Visibility.Hidden" />; <see langword="false" /> to use <see cref="Visibility.Collapsed" />. Only applies if <paramref name="value" /> is <see langword="false" />.</param>
	/// <returns>
	/// <see cref="Visibility.Visible" />, if <paramref name="value" /> is <see langword="true" />;
	/// <see cref="Visibility.Collapsed" />, if <paramref name="value" /> is <see langword="false" />;
	/// <see cref="Visibility.Hidden" />, if <paramref name="value" /> is <see langword="false" /> and <paramref name="preserveSpace" /> is <see langword="true" />.
	/// </returns>
	public static Visibility ToVisibility(this bool value, bool preserveSpace)
	{
		return value ? Visibility.Visible : preserveSpace ? Visibility.Hidden : Visibility.Collapsed;
	}
}