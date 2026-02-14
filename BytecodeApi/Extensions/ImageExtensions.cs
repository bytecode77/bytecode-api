using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Image" /> objects.
/// </summary>
[SupportedOSPlatform("windows")]
public static class ImageExtensions
{
	extension(Image image)
	{
		/// <summary>
		/// Converts this <see cref="Image" /> in the specified format to its <see cref="byte" />[] representation.
		/// </summary>
		/// <param name="format">An <see cref="ImageFormat" /> that specifies the format of the saved image.</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing this <see cref="Image" />.
		/// </returns>
		public byte[] ToArray(ImageFormat format)
		{
			Check.ArgumentNull(image);
			Check.ArgumentNull(format);

			using MemoryStream memoryStream = new();
			image.Save(memoryStream, format);
			return memoryStream.ToArray();
		}
		/// <summary>
		/// Converts this <see cref="Image" /> with the specified encoder and image encoder parameters to its <see cref="byte" />[] representation.
		/// </summary>
		/// <param name="encoder">The <see cref="ImageCodecInfo" /> for this <see cref="Image" />.</param>
		/// <param name="encoderParams">An <see cref="EncoderParameters" /> that specifies parameters used by the image encoder.</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing this <see cref="Image" />.
		/// </returns>
		public byte[] ToArray(ImageCodecInfo encoder, EncoderParameters? encoderParams)
		{
			Check.ArgumentNull(image);
			Check.ArgumentNull(encoder);

			using MemoryStream memoryStream = new();
			image.Save(memoryStream, encoder, encoderParams);
			return memoryStream.ToArray();
		}

		/// <summary>
		/// Saves this <see cref="Image" /> in JPEG format with 95 % quality to the specified file.
		/// </summary>
		/// <param name="path">A <see cref="string" /> that contains the name of the file to which to save this <see cref="Image" />.</param>
		public void SaveJpeg(string path)
		{
			image.SaveJpeg(path, 95);
		}
		/// <summary>
		/// Saves this <see cref="Image" /> in JPEG format with the specified quality to the specified file.
		/// </summary>
		/// <param name="path">A <see cref="string" /> that contains the name of the file to which to save this <see cref="Image" />.</param>
		/// <param name="quality">The quality to be used for JPEG encoding.</param>
		public void SaveJpeg(string path, int quality)
		{
			Check.ArgumentNull(image);
			Check.ArgumentNull(path);
			Check.ArgumentOutOfRange(quality is >= 0 and <= 100, nameof(quality), "Jpeg quality must be in range of 0...100.");

			using FileStream file = File.Create(path);
			image.SaveJpeg(file, quality);
		}
		/// <summary>
		/// Writes this <see cref="Image" /> in JPEG format with 95 % quality to the specified <see cref="Stream" />.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which to write the image to.</param>
		public void SaveJpeg(Stream stream)
		{
			image.SaveJpeg(stream, 95);
		}
		/// <summary>
		/// Writes this <see cref="Image" /> in JPEG format with the specified quality to the specified <see cref="Stream" />.
		/// </summary>
		/// <param name="stream">The <see cref="Stream" /> to which to write the image to.</param>
		/// <param name="quality">The quality to be used for JPEG encoding.</param>
		public void SaveJpeg(Stream stream, int quality)
		{
			Check.ArgumentNull(image);
			Check.ArgumentNull(stream);
			Check.ArgumentOutOfRange(quality is >= 0 and <= 100, nameof(quality), "Jpeg quality must be in range of 0...100.");

			using EncoderParameters encoderParameters = new(1);
			using EncoderParameter encoderParameter = new(Encoder.Quality, quality);

			encoderParameters.Param[0] = encoderParameter;
			image.Save(stream, ImageCodecInfo.GetImageDecoders().First(codec => codec.FormatID == ImageFormat.Jpeg.Guid), encoderParameters);
		}
		/// <summary>
		/// Converts this <see cref="Image" /> in JPEG format with 95 % quality to its <see cref="byte" />[] representation.
		/// </summary>
		/// <returns>
		/// A new <see cref="byte" />[] representing this <see cref="Image" />, saved in JPEG format with 95 % quality.
		/// </returns>
		public byte[] ToArrayJpeg()
		{
			return image.ToArrayJpeg(95);
		}
		/// <summary>
		/// Converts this <see cref="Image" /> in JPEG format with the specified quality to its <see cref="byte" />[] representation.
		/// </summary>
		/// <param name="quality">The quality to be used for JPEG encoding.</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing this <see cref="Image" />, saved in JPEG format with the specified quality.
		/// </returns>
		public byte[] ToArrayJpeg(int quality)
		{
			Check.ArgumentNull(image);
			Check.ArgumentOutOfRange(quality is >= 0 and <= 100, nameof(quality), "Jpeg quality must be in range of 0...100.");

			using MemoryStream memoryStream = new();
			image.SaveJpeg(memoryStream, quality);
			return memoryStream.ToArray();
		}
	}
}