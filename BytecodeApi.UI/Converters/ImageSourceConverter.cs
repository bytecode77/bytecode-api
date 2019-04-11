using BytecodeApi.Extensions;
using System.Drawing;
using System.Windows.Media.Imaging;

namespace BytecodeApi.UI.Converters
{
	public sealed class ImageSourceConverter : ConverterBase<object, BitmapSource>
	{
		public ImageSourceConverter()
		{
		}

		public override BitmapSource Convert(object value)
		{
			if (value == null) return null;
			else if (value is Image imageValue) return ((Bitmap)imageValue).ToBitmapSource();
			else if (value is Icon iconValue) return iconValue.ToBitmapSource();
			else throw Throw.UnsupportedType(nameof(value));
		}
	}
}