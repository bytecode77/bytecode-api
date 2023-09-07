using BytecodeApi.Extensions;
using System.Drawing;
using WpfColor = System.Windows.Media.Color;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="WpfColor" /> and <see cref="Color" /> objects.
/// </summary>
public static class ColorExtensions
{
	/// <summary>
	/// Converts this <see cref="Color" /> object to an equivalent <see cref="WpfColor" /> object.
	/// </summary>
	/// <param name="color">The <see cref="Color" /> object to convert.</param>
	/// <returns>
	/// An equivalent <see cref="WpfColor" /> representation of this <see cref="Color" />.
	/// </returns>
	public static WpfColor Convert(this Color color)
	{
		return WpfColor.FromArgb(color.A, color.R, color.G, color.B);
	}
	/// <summary>
	/// Converts this <see cref="WpfColor" /> object to an equivalent <see cref="Color" /> object.
	/// </summary>
	/// <param name="color">The <see cref="WpfColor" /> object to convert.</param>
	/// <returns>
	/// An equivalent <see cref="Color" /> representation of this <see cref="WpfColor" />.
	/// </returns>
	public static Color Convert(this WpfColor color)
	{
		return Color.FromArgb(color.A, color.R, color.G, color.B);
	}
	/// <summary>
	/// Converts this <see cref="WpfColor" /> object to a HTML color <see cref="string" />.
	/// <para>Example: #ff7f00</para>
	/// </summary>
	/// <param name="color">The <see cref="WpfColor" /> object to convert.</param>
	/// <returns>
	/// An equivalent HTML color <see cref="string" /> representation of this <see cref="WpfColor" />.
	/// </returns>
	public static string ToHtmlString(this WpfColor color)
	{
		return color.ToHtmlString(false);
	}
	/// <summary>
	/// Converts this <see cref="WpfColor" /> object to a HTML color <see cref="string" />.
	/// <para>Example: #ff7f00 (when <paramref name="rgba" /> = <see langword="false" />)</para>
	/// <para>Example: rgba(255, 127, 0, 0.5) (when <paramref name="rgba" /> = <see langword="true" />)</para>
	/// </summary>
	/// <param name="color">The <see cref="WpfColor" /> object to convert.</param>
	/// <param name="rgba"><see langword="true" /> to convert to an rgba representation; <see langword="false" /> to convert to a hexadecimal representation.</param>
	/// <returns>
	/// An equivalent HTML color <see cref="string" /> representation of this <see cref="WpfColor" />.
	/// </returns>
	public static string ToHtmlString(this WpfColor color, bool rgba)
	{
		return ToHtmlString(color.R, color.G, color.B, color.A, rgba);
	}
	/// <summary>
	/// Converts this <see cref="Color" /> object to a HTML color <see cref="string" />.
	/// <para>Example: #ff7f00</para>
	/// </summary>
	/// <param name="color">The <see cref="Color" /> object to convert.</param>
	/// <returns>
	/// An equivalent HTML color <see cref="string" /> representation of this <see cref="Color" />.
	/// </returns>
	public static string ToHtmlString(this Color color)
	{
		return color.ToHtmlString(false);
	}
	/// <summary>
	/// Converts this <see cref="Color" /> object to a HTML color <see cref="string" />.
	/// <para>Example: #ff7f00 (when <paramref name="rgba" /> = <see langword="false" />)</para>
	/// <para>Example: rgba(255, 127, 0, 0.5) (when <paramref name="rgba" /> = <see langword="true" />)</para>
	/// </summary>
	/// <param name="color">The <see cref="Color" /> object to convert.</param>
	/// <param name="rgba"><see langword="true" /> to convert to an rgba representation; <see langword="false" /> to convert to a hexadecimal representation.</param>
	/// <returns>
	/// An equivalent HTML color <see cref="string" /> representation of this <see cref="Color" />.
	/// </returns>
	public static string ToHtmlString(this Color color, bool rgba)
	{
		return ToHtmlString(color.R, color.G, color.B, color.A, rgba);
	}
	private static string ToHtmlString(byte r, byte g, byte b, byte a, bool rgba)
	{
		if (rgba)
		{
			return $"rgba({r}, {g}, {b}, {Math.Round(a / 255.0, 2).ToStringInvariant()})";
		}
		else
		{
			return $"#{r:x2}{g:x2}{b:x2}";
		}
	}
}