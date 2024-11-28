using System.Windows;
using System.Windows.Media.Imaging;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="BitmapSource" /> objects.
/// </summary>
public static class BitmapSourceExtensions
{
	/// <summary>
	/// Converts this <see cref="BitmapSource" /> to a <see cref="System.Drawing.Bitmap" /> object.
	/// </summary>
	/// <param name="bitmapSource">The <see cref="BitmapSource" /> to convert.</param>
	/// <returns>
	/// A new <see cref="System.Drawing.Bitmap" /> object, which is a bitmap copy of the original <see cref="BitmapSource" />.
	/// </returns>
	public static System.Drawing.Bitmap ToBitmap(this BitmapSource bitmapSource)
	{
		System.Drawing.Bitmap bitmap = new(bitmapSource.PixelWidth, bitmapSource.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

		System.Drawing.Imaging.BitmapData data = bitmap.LockBits(
			new System.Drawing.Rectangle(System.Drawing.Point.Empty, bitmap.Size),
			System.Drawing.Imaging.ImageLockMode.WriteOnly,
			System.Drawing.Imaging.PixelFormat.Format32bppPArgb);

		bitmapSource.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
		bitmap.UnlockBits(data);

		return bitmap;
	}
}