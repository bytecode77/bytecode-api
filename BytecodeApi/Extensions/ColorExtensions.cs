using System.Drawing;
using MediaColor = System.Windows.Media.Color;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Color" /> and <see cref="MediaColor" /> objects.
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
	}
}