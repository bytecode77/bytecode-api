using System.Windows;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="bool" /> objects.
/// </summary>
public static class BooleanExtensions
{
	extension(bool value)
	{
		/// <summary>
		/// Converts this <see cref="bool" /> value to <see cref="Visibility.Visible" /> or <see cref="Visibility.Collapsed" />.
		/// </summary>
		/// <returns>
		/// <see cref="Visibility.Visible" />, if this <see cref="bool" /> is <see langword="true" />;
		/// otherwise, <see cref="Visibility.Collapsed" />.
		/// </returns>
		public Visibility ToVisibility()
		{
			return value.ToVisibility(false);
		}
		/// <summary>
		/// Converts this <see cref="bool" /> value to <see cref="Visibility.Visible" />, <see cref="Visibility.Hidden" /> or <see cref="Visibility.Collapsed" /> depending on its value and the <paramref name="preserveSpace" /> parameter.
		/// </summary>
		/// <param name="preserveSpace"><see langword="true" /> to use <see cref="Visibility.Hidden" />; <see langword="false" /> to use <see cref="Visibility.Collapsed" />. Only applies if this <see cref="bool" /> is <see langword="false" />.</param>
		/// <returns>
		/// <see cref="Visibility.Visible" />, if this <see cref="bool" /> is <see langword="true" />;
		/// <see cref="Visibility.Collapsed" />, if this <see cref="bool" /> is <see langword="false" />;
		/// <see cref="Visibility.Hidden" />, if this <see cref="bool" /> is <see langword="false" /> and <paramref name="preserveSpace" /> is <see langword="true" />.
		/// </returns>
		public Visibility ToVisibility(bool preserveSpace)
		{
			return value ? Visibility.Visible : preserveSpace ? Visibility.Hidden : Visibility.Collapsed;
		}
	}
}