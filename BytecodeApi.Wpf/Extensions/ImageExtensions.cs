using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Image" /> objects.
/// </summary>
public static class ImageExtensions
{
	/// <summary>
	/// Returns a managed <see cref="BitmapSource" />, based on the provided <see cref="Bitmap" />.
	/// </summary>
	/// <param name="bitmap">The <see cref="Bitmap" /> to convert.</param>
	/// <returns>
	/// The created <see cref="BitmapSource" />.
	/// </returns>
	public static BitmapSource ToBitmapSource(this Bitmap bitmap)
	{
		Check.ArgumentNull(bitmap);

		nint hBitmap = bitmap.GetHbitmap();

		try
		{
			return Imaging.CreateBitmapSourceFromHBitmap(hBitmap, 0, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
		}
		finally
		{
			Native.DeleteObject(hBitmap);
		}
	}
}

file static class Native
{
	[DllImport("gdi32.dll")]
	public static extern bool DeleteObject(nint obj);
}