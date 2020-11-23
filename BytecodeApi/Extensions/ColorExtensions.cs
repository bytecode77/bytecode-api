using System;
using System.Drawing;
using MediaColor = System.Windows.Media.Color;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="MediaColor" /> and <see cref="Color" /> objects.
	/// </summary>
	public static class ColorExtensions
	{
		/// <summary>
		/// Converts this <see cref="Color" /> object to an equivalent <see cref="MediaColor" /> object.
		/// </summary>
		/// <param name="color">The <see cref="Color" /> object to convert.</param>
		/// <returns>
		/// An equivalent <see cref="MediaColor" /> representation of this <see cref="Color" />.
		/// </returns>
		public static MediaColor Convert(this Color color)
		{
			return MediaColor.FromArgb(color.A, color.R, color.G, color.B);
		}
		/// <summary>
		/// Converts this <see cref="MediaColor" /> object to an equivalent <see cref="Color" /> object.
		/// </summary>
		/// <param name="color">The <see cref="MediaColor" /> object to convert.</param>
		/// <returns>
		/// An equivalent <see cref="Color" /> representation of this <see cref="MediaColor" />.
		/// </returns>
		public static Color Convert(this MediaColor color)
		{
			return Color.FromArgb(color.A, color.R, color.G, color.B);
		}
		/// <summary>
		/// Converts this <see cref="MediaColor" /> object to a HTML color <see cref="string" />.
		/// <para>Example: #ff7f00</para>
		/// </summary>
		/// <param name="color">The <see cref="MediaColor" /> object to convert.</param>
		/// <returns>
		/// An equivalent HTML color <see cref="string" /> representation of this <see cref="MediaColor" />.
		/// </returns>
		public static string ToHtmlString(this MediaColor color)
		{
			return color.ToHtmlString(false);
		}
		/// <summary>
		/// Converts this <see cref="MediaColor" /> object to a HTML color <see cref="string" />.
		/// <para>Example: #ff7f00 (when <paramref name="rgba" /> = <see langword="false" />)</para>
		/// <para>Example: rgba(255, 127, 0, 0.5) (when <paramref name="rgba" /> = <see langword="true" />)</para>
		/// </summary>
		/// <param name="color">The <see cref="MediaColor" /> object to convert.</param>
		/// <param name="rgba"><see langword="true" /> to convert to an rgba representation; <see langword="false" /> to convert to a hexadecimal representation.</param>
		/// <returns>
		/// An equivalent HTML color <see cref="string" /> representation of this <see cref="MediaColor" />.
		/// </returns>
		public static string ToHtmlString(this MediaColor color, bool rgba)
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
			if (rgba) return "rgba(" + r + ", " + g + ", " + b + ", " + Math.Round(a / 255.0, 2).ToStringInvariant() + ")";
			else return "#" + r.ToString("x2") + g.ToString("x2") + b.ToString("x2");
		}
	}
}