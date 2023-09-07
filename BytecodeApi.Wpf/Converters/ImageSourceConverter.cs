using BytecodeApi.Wpf.Extensions;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Represents the converter that converts <see cref="Image" /> or <see cref="Icon" /> values to an image source. The <see cref="Convert(object)" /> method returns an <see cref="BitmapSource" /> created from the specified value. The value must be an <see cref="Image" /> or an <see cref="Icon" />.
/// </summary>
public sealed class ImageSourceConverter : ConverterBase<object?>
{
	/// <summary>
	/// Initializes a new instance of the <see cref="ImageSourceConverter" /> class.
	/// </summary>
	public ImageSourceConverter()
	{
	}

	/// <summary>
	/// Converts the <see cref="Image" /> or <see cref="Icon" /> value to an image source.
	/// </summary>
	/// <param name="value">The <see cref="Image" /> or <see cref="Icon" /> value to convert.</param>
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
				_ => throw Throw.UnsupportedType(nameof(value))
			};
		}
	}
}