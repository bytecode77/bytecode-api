using System.Windows.Media.Imaging;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="BitmapEncoder" /> objects.
/// </summary>
public static class BitmapEncoderExtensions
{
	extension(BitmapEncoder bitmapEncoder)
	{
		/// <summary>
		/// Encodes a bitmap image and saves the encoded image to a file.
		/// </summary>
		/// <param name="path">A <see cref="string" /> that contains the name of the file to which to save this bitmap image.</param>
		public void Save(string path)
		{
			Check.ArgumentNull(bitmapEncoder);
			Check.ArgumentNull(path);

			using FileStream file = File.Create(path);
			bitmapEncoder.Save(file);
		}
		/// <summary>
		/// Encodes a bitmap image and returns a new <see cref="byte" />[] representing the encoded image.
		/// </summary>
		/// <returns>
		/// A new <see cref="byte" />[] representing the encoded image.
		/// </returns>
		public byte[] ToArray()
		{
			Check.ArgumentNull(bitmapEncoder);

			using MemoryStream memoryStream = new();
			bitmapEncoder.Save(memoryStream);
			return memoryStream.ToArray();
		}
	}
}