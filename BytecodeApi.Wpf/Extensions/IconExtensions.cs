using System.Drawing;
using System.Windows.Media.Imaging;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Icon" /> objects.
/// </summary>
public static class IconExtensions
{
	extension(Icon icon)
	{
		/// <summary>
		/// Returns a managed <see cref="BitmapSource" />, based on the provided <see cref="Icon" />.
		/// </summary>
		/// <returns>
		/// The created <see cref="BitmapSource" />.
		/// </returns>
		public BitmapSource ToBitmapSource()
		{
			Check.ArgumentNull(icon);

			return icon.ToBitmap().ToBitmapSource();
		}
	}
}