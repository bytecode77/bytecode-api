using System.Drawing;
using System.Windows.Media.Imaging;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Icon" /> objects.
/// </summary>
public static class IconExtensions
{
	/// <summary>
	/// Returns a managed <see cref="BitmapSource" />, based on the provided <see cref="Icon" />.
	/// </summary>
	/// <param name="icon">The <see cref="Icon" /> to convert.</param>
	/// <returns>
	/// The created <see cref="BitmapSource" />.
	/// </returns>
	public static BitmapSource ToBitmapSource(this Icon icon)
	{
		Check.ArgumentNull(icon);

		return icon.ToBitmap().ToBitmapSource();
	}
}