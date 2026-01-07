using BytecodeApi.Extensions;
using System.Drawing;
using WpfColor = System.Windows.Media.Color;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="WpfColor" /> and <see cref="Color" /> objects.
/// </summary>
public static class ColorExtensions
{
	extension(Color color)
	{
		/// <summary>
		/// Converts this <see cref="Color" /> object to an equivalent <see cref="WpfColor" /> object.
		/// </summary>
		/// <returns>
		/// An equivalent <see cref="WpfColor" /> representation of this <see cref="Color" />.
		/// </returns>
		public WpfColor Convert()
		{
			return WpfColor.FromArgb(color.A, color.R, color.G, color.B);
		}
		/// <summary>
		/// Converts this <see cref="Color" /> object to a HTML color <see cref="string" />.
		/// <para>Example: #ff7f00</para>
		/// </summary>
		/// <returns>
		/// An equivalent HTML color <see cref="string" /> representation of this <see cref="Color" />.
		/// </returns>
		public string ToHtmlString()
		{
			return color.ToHtmlString(false);
		}
		/// <summary>
		/// Converts this <see cref="Color" /> object to a HTML color <see cref="string" />.
		/// <para>Example: #ff7f00 (when <paramref name="rgba" /> = <see langword="false" />)</para>
		/// <para>Example: rgba(255, 127, 0, 0.5) (when <paramref name="rgba" /> = <see langword="true" />)</para>
		/// </summary>
		/// <param name="rgba"><see langword="true" /> to convert to an rgba representation; <see langword="false" /> to convert to a hexadecimal representation.</param>
		/// <returns>
		/// An equivalent HTML color <see cref="string" /> representation of this <see cref="Color" />.
		/// </returns>
		public string ToHtmlString(bool rgba)
		{
			return ToHtmlString(color.R, color.G, color.B, color.A, rgba);
		}
	}

	extension(WpfColor color)
	{
		/// <summary>
		/// Converts this <see cref="WpfColor" /> object to an equivalent <see cref="Color" /> object.
		/// </summary>
		/// <returns>
		/// An equivalent <see cref="Color" /> representation of this <see cref="WpfColor" />.
		/// </returns>
		public Color Convert()
		{
			return Color.FromArgb(color.A, color.R, color.G, color.B);
		}
		/// <summary>
		/// Converts this <see cref="WpfColor" /> object to a HTML color <see cref="string" />.
		/// <para>Example: #ff7f00</para>
		/// </summary>
		/// <returns>
		/// An equivalent HTML color <see cref="string" /> representation of this <see cref="WpfColor" />.
		/// </returns>
		public string ToHtmlString()
		{
			return color.ToHtmlString(false);
		}
		/// <summary>
		/// Converts this <see cref="WpfColor" /> object to a HTML color <see cref="string" />.
		/// <para>Example: #ff7f00 (when <paramref name="rgba" /> = <see langword="false" />)</para>
		/// <para>Example: rgba(255, 127, 0, 0.5) (when <paramref name="rgba" /> = <see langword="true" />)</para>
		/// </summary>
		/// <param name="rgba"><see langword="true" /> to convert to an rgba representation; <see langword="false" /> to convert to a hexadecimal representation.</param>
		/// <returns>
		/// An equivalent HTML color <see cref="string" /> representation of this <see cref="WpfColor" />.
		/// </returns>
		public string ToHtmlString(bool rgba)
		{
			return ToHtmlString(color.R, color.G, color.B, color.A, rgba);
		}
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