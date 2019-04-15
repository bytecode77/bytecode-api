using System.IO;
using System.Windows.Media.Imaging;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="BitmapEncoder" /> objects.
	/// </summary>
	public static class BitmapEncoderExtensions
	{
		/// <summary>
		/// Encodes a bitmap image and saves the encoded image to a file.
		/// </summary>
		/// <param name="bitmapEncoder">The <see cref="BitmapEncoder" /> to be used.</param>
		/// <param name="path">A <see cref="string" /> that contains the name of the file to which to save this bitmap image.</param>
		public static void Save(this BitmapEncoder bitmapEncoder, string path)
		{
			Check.ArgumentNull(bitmapEncoder, nameof(bitmapEncoder));
			Check.ArgumentNull(path, nameof(path));

			using (FileStream file = File.Create(path))
			{
				bitmapEncoder.Save(file);
			}
		}
		/// <summary>
		/// Encodes a bitmap image and returns a new <see cref="byte" />[] representing the encoded image.
		/// </summary>
		/// <param name="bitmapEncoder">The <see cref="BitmapEncoder" /> to be used.</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing the encoded image.
		/// </returns>
		public static byte[] ToArray(this BitmapEncoder bitmapEncoder)
		{
			Check.ArgumentNull(bitmapEncoder, nameof(bitmapEncoder));

			using (MemoryStream memoryStream = new MemoryStream())
			{
				bitmapEncoder.Save(memoryStream);
				return memoryStream.ToArray();
			}
		}
	}
}