using BytecodeApi.Wpf.Extensions;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="Image" />, <see cref="Icon" /> or <see cref="byte" />[] values to an image source. The <see cref="Convert(object)" /> method returns an <see cref="BitmapSource" /> created from the specified value. The value must be an <see cref="Image" />, an <see cref="Icon" /> or a <see cref="byte" />[].
/// </summary>
public sealed class ImageSourceConverter : ConverterBase<object>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ImageSourceConverter" /> class.
	/// </summary>
	public ImageSourceConverter()
	{
	}

	/// <summary>
	/// Converts the <see cref="Image" />, <see cref="Icon" /> or <see cref="byte" />[] value to an image source.
	/// </summary>
	/// <param name="value">The <see cref="Image" />, <see cref="Icon" /> or <see cref="byte" />[] value to convert.</param>
	/// <returns>
	/// A <see cref="BitmapSource" /> with the result of the conversion.
	/// </returns>
	public override object? Convert(object? value)
	{
		if (value == null)
		{
			return null;
		}
		else
		{
			return value switch
			{
				Image imageValue => ((Bitmap)imageValue).ToBitmapSource(),
				Icon iconValue => iconValue.ToBitmapSource(),
				byte[] byteArrayValue => ((Bitmap)Image.FromStream(new MemoryStream(byteArrayValue))).ToBitmapSource(),
				_ => throw Throw.UnsupportedType(nameof(value))
			};
		}
	}
}