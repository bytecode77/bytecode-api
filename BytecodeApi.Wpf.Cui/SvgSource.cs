using BytecodeApi.Wpf.Extensions;
using SharpVectors.Converters;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace BytecodeApi.Wpf.Cui;

/// <summary>
/// Class to convert SVG resources to WPF image sources.
/// </summary>
public static class SvgSource
{
	private static readonly Dictionary<string, DrawingImage> ImageSourceCache = [];
	private static readonly Dictionary<(string, Color), DrawingImage> ImageSourceColoredCache = [];
	private static readonly Dictionary<(string, int), System.Drawing.Bitmap> BitmapCache = [];
	private static readonly Dictionary<(string, int, Color), System.Drawing.Bitmap> BitmapColoredCache = [];
	private static readonly Dictionary<(string, int), BitmapSource> BitmapSourceCache = [];
	private static readonly Dictionary<(string, int, Color), BitmapSource> BitmapSourceColoredCache = [];

	/// <summary>
	/// Loads an SVG resource from the specified URI and converts it to a <see cref="DrawingImage" />.
	/// </summary>
	/// <param name="uri">The URI of the SVG resource.</param>
	/// <returns>
	/// A new <see cref="DrawingImage" /> if the resource was found;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static DrawingImage? ImageSource(string uri)
	{
		Check.ArgumentNull(uri);
		Check.ArgumentEx.StringNotEmpty(uri);

		if (ImageSourceCache.TryGetValue(uri, out DrawingImage? existingImage))
		{
			return existingImage;
		}
		else if (GetSvgResource(uri) is DrawingImage image)
		{
			if (image.CanFreeze) image.Freeze();
			ImageSourceCache[uri] = image;
			return image;
		}
		else
		{
			return null;
		}
	}
	/// <summary>
	/// Loads an SVG resource from the specified URI, converts it to a <see cref="DrawingImage" />, and changes its color.
	/// </summary>
	/// <param name="uri">The URI of the SVG resource.</param>
	/// <param name="color">A <see cref="Color" /> to apply to all SVG elements.</param>
	/// <returns>
	/// A new <see cref="DrawingImage" /> if the resource was found;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static DrawingImage? ImageSource(string uri, Color color)
	{
		Check.ArgumentNull(uri);
		Check.ArgumentEx.StringNotEmpty(uri);

		if (ImageSourceColoredCache.TryGetValue((uri, color), out DrawingImage? existingImage))
		{
			return existingImage;
		}
		else if (GetSvgResource(uri) is DrawingImage image)
		{
			ChangeColor(image, color);
			if (image.CanFreeze) image.Freeze();

			ImageSourceColoredCache[(uri, color)] = image;
			return image;
		}
		else
		{
			return null;
		}
	}
	/// <summary>
	/// Loads an SVG resource from the specified URI and converts it to a <see cref="System.Drawing.Bitmap" /> of the specified size.
	/// </summary>
	/// <param name="uri">The URI of the SVG resource.</param>
	/// <param name="size">The width and height, in pixels, of the resulting <see cref="System.Drawing.Bitmap" />.</param>
	/// <returns>
	/// A new <see cref="System.Drawing.Bitmap" /> if the resource was found;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static System.Drawing.Bitmap? Bitmap(string uri, int size)
	{
		Check.ArgumentNull(uri);
		Check.ArgumentEx.StringNotEmpty(uri);
		Check.ArgumentOutOfRangeEx.Greater0(size);

		if (BitmapCache.TryGetValue((uri, size), out System.Drawing.Bitmap? existingImage))
		{
			return existingImage;
		}
		else if (BitmapSource(uri, size) is BitmapSource bitmapSource)
		{
			System.Drawing.Bitmap bitmap = bitmapSource.ToBitmap();
			BitmapCache[(uri, size)] = bitmap;
			return bitmap;
		}
		else
		{
			return null;
		}
	}
	/// <summary>
	/// Loads an SVG resource from the specified URI, converts it to a <see cref="System.Drawing.Bitmap" /> of the specified size, and changes its color.
	/// </summary>
	/// <param name="uri">The URI of the SVG resource.</param>
	/// <param name="size">The width and height, in pixels, of the resulting <see cref="System.Drawing.Bitmap" />.</param>
	/// <param name="color">A <see cref="Color" /> to apply to all SVG elements.</param>
	/// <returns>
	/// A new <see cref="System.Drawing.Bitmap" /> if the resource was found;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static System.Drawing.Bitmap? Bitmap(string uri, int size, Color color)
	{
		Check.ArgumentNull(uri);
		Check.ArgumentEx.StringNotEmpty(uri);
		Check.ArgumentOutOfRangeEx.Greater0(size);

		if (BitmapColoredCache.TryGetValue((uri, size, color), out System.Drawing.Bitmap? existingImage))
		{
			return existingImage;
		}
		else if (BitmapSource(uri, size, color) is BitmapSource bitmapSource)
		{
			System.Drawing.Bitmap bitmap = bitmapSource.ToBitmap();
			BitmapColoredCache[(uri, size, color)] = bitmap;
			return bitmap;
		}
		else
		{
			return null;
		}
	}
	/// <summary>
	/// Loads an SVG resource from the specified URI, converts it to a <see cref="System.Drawing.Bitmap" /> of the specified size, and returns an equivalent <see cref="System.Windows.Media.Imaging.BitmapSource" />.
	/// </summary>
	/// <param name="uri">The URI of the SVG resource.</param>
	/// <param name="size">The width and height, in pixels, of the <see cref="System.Drawing.Bitmap" />.</param>
	/// <returns>
	/// A new <see cref="System.Windows.Media.Imaging.BitmapSource" /> if the resource was found;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static BitmapSource? BitmapSource(string uri, int size)
	{
		Check.ArgumentNull(uri);
		Check.ArgumentEx.StringNotEmpty(uri);
		Check.ArgumentOutOfRangeEx.Greater0(size);

		if (BitmapSourceCache.TryGetValue((uri, size), out BitmapSource? existingImage))
		{
			return existingImage;
		}
		else if (ImageSource(uri) is DrawingImage image)
		{
			BitmapSource bitmap = ConvertDrawingImageToBitmap(image, size);
			BitmapSourceCache[(uri, size)] = bitmap;
			return bitmap;
		}
		else
		{
			return null;
		}
	}
	/// <summary>
	/// Loads an SVG resource from the specified URI, converts it to a <see cref="System.Drawing.Bitmap" /> of the specified size, changes its color, and returns an equivalent <see cref="System.Windows.Media.Imaging.BitmapSource" />.
	/// </summary>
	/// <param name="uri">The URI of the SVG resource.</param>
	/// <param name="size">The width and height, in pixels, of the <see cref="System.Drawing.Bitmap" />.</param>
	/// <param name="color">A <see cref="Color" /> to apply to all SVG elements.</param>
	/// <returns>
	/// A new <see cref="System.Windows.Media.Imaging.BitmapSource" /> if the resource was found;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static BitmapSource? BitmapSource(string uri, int size, Color color)
	{
		Check.ArgumentNull(uri);
		Check.ArgumentEx.StringNotEmpty(uri);
		Check.ArgumentOutOfRangeEx.Greater0(size);

		if (BitmapSourceColoredCache.TryGetValue((uri, size, color), out BitmapSource? existingImage))
		{
			return existingImage;
		}
		else if (ImageSource(uri, color) is DrawingImage image)
		{
			BitmapSource bitmap = ConvertDrawingImageToBitmap(image, size);
			BitmapSourceColoredCache[(uri, size, color)] = bitmap;
			return bitmap;
		}
		else
		{
			return null;
		}
	}

	private static byte[] GetResource(string uri)
	{
		StreamResourceInfo resourceInfo = Application
			.GetResourceStream(new(uri))
			?? throw new FileNotFoundException($"Resource '{uri}' not found.");

		using MemoryStream memoryStream = new();
		resourceInfo.Stream.CopyTo(memoryStream);

		return memoryStream.ToArray();
	}
	private static DrawingImage GetSvgResource(string uri)
	{
		using FileSvgReader reader = new(new());
		using MemoryStream memoryStream = new(GetResource(uri.StartsWith("pack://", StringComparison.OrdinalIgnoreCase) ? uri : $"{Packs.Application}{uri}"));

		return new(reader.Read(memoryStream));
	}
	private static void ChangeColor(DrawingImage image, Color color)
	{
		SetColor(image.Drawing);

		void SetColor(Drawing drawing)
		{
			if (drawing is DrawingGroup group)
			{
				foreach (Drawing child in group.Children)
				{
					SetColor(child);
				}
			}
			else if (drawing is GeometryDrawing geometry)
			{
				if (geometry.Pen?.Brush is SolidColorBrush solidColorBrush1)
				{
					solidColorBrush1.Color = color;
				}
				if (geometry.Brush is SolidColorBrush solidColorBrush2)
				{
					solidColorBrush2.Color = color;
				}
			}
		}
	}
	private static BitmapSource ConvertDrawingImageToBitmap(DrawingImage image, int size)
	{
		Image wpfImage = new() { Source = image };
		wpfImage.Measure(new(size, size));
		wpfImage.Arrange(new(0, 0, size, size));

		RenderTargetBitmap renderTarget = new(size, size, 96, 96, PixelFormats.Pbgra32);
		renderTarget.Render(wpfImage);

		return renderTarget;
	}
}