using BytecodeApi.Extensions;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts objects of various types to their equivalent <see cref="BitmapSource" /> representation. Accepted value types are: <see cref="Image" /> and <see cref="Icon" />.
	/// </summary>
	public sealed class ObjectToImageSourceConverter : ConverterBase<object, BitmapSource>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectToImageSourceConverter" /> class.
		/// </summary>
		public ObjectToImageSourceConverter()
		{
		}

		/// <summary>
		/// Creates an <see cref="ImageSource" /> from a variety of types.
		/// </summary>
		/// <param name="value">An <see cref="object" /> to convert. Allowed value types are: <see cref="Image" /> and <see cref="Icon" />.</param>
		/// <returns>
		/// The <see cref="ImageSource" /> this method creates.
		/// </returns>
		public override BitmapSource Convert(object value)
		{
			if (value == null) return null;
			else if (value is Image imageValue) return ((Bitmap)imageValue).ToBitmapSource();
			else if (value is Icon iconValue) return iconValue.ToBitmapSource();
			else throw Throw.UnsupportedType(nameof(value));
		}
	}
}