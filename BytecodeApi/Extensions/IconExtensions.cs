using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.Versioning;
using System.Text;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Icon" /> objects.
/// </summary>
[SupportedOSPlatform("windows")]
public static class IconExtensions
{
	extension(Icon)
	{
		/// <summary>
		/// Merges multiple <see cref="Image" /> frames into a new <see cref="Icon" />.
		/// </summary>
		/// <param name="images">The <see cref="Image" /> objects to be merged into an <see cref="Icon" />.</param>
		/// <returns>
		/// A new <see cref="Icon" /> with the frames from the <paramref name="images" /> parameter.
		/// </returns>
		public static Icon FormImages(Image[] images)
		{
			Check.ArgumentNull(images);
			Check.ArgumentEx.ArrayElementsRequired(images);
			Check.ArgumentEx.ArrayValuesNotNull(images);
			Check.Argument(images.All(image => image.Width <= 256 && image.Height <= 256), nameof(images), "Images may not exceed a resolution of 256x256 pixels.");

			using MemoryStream memoryStream = new();
			using (BinaryWriter iconWriter = new(memoryStream, Encoding.Default, true))
			{
				iconWriter.Write((short)0);
				iconWriter.Write((short)1);
				iconWriter.Write((short)images.Length);

				byte[][] frameBytes = new byte[images.Length][];

				for (int i = 0, offset = 6 + 16 * images.Length; i < images.Length; i++)
				{
					frameBytes[i] = images[i].ToArray(ImageFormat.Png);

					int bitsPerPixel = frameBytes[i][25] switch
					{
						2 => frameBytes[i][24] * 3,
						6 => frameBytes[i][24] * 4,
						_ => frameBytes[i][24]
					};

					iconWriter.Write((byte)images[i].Width);
					iconWriter.Write((byte)images[i].Height);
					iconWriter.Write((byte)images[i].Palette.Entries.Length);
					iconWriter.Write((byte)0);
					iconWriter.Write((short)0);
					iconWriter.Write((short)bitsPerPixel);
					iconWriter.Write(frameBytes[i].Length);
					iconWriter.Write(offset);

					offset += frameBytes[i].Length;
				}

				foreach (byte[] frame in frameBytes)
				{
					iconWriter.Write(frame);
				}
			}

			memoryStream.Position = 0;
			return new(memoryStream);
		}
	}

	extension(Icon icon)
	{
		/// <summary>
		/// Splits an <see cref="Icon" /> and returns an array with a new <see cref="Icon" /> for each frame.
		/// </summary>
		/// <returns>
		/// An array with a new <see cref="Icon" /> for each frame.
		/// </returns>
		public Icon[] Split()
		{
			Check.ArgumentNull(icon);

			byte[] iconData = icon.ToArray();
			Icon[] icons = new Icon[BitConverter.ToUInt16(iconData, 4)];

			for (int i = 0; i < icons.Length; i++)
			{
				int offset = BitConverter.ToInt32(iconData, i * 16 + 18);
				int length = BitConverter.ToInt32(iconData, i * 16 + 14);

				using BinaryWriter writer = new(new MemoryStream(length + 24));

				writer.Write(iconData, 0, 4);
				writer.Write((short)1);
				writer.Write(iconData, i * 16 + 6, 12);
				writer.Write(22);
				writer.Write(iconData, offset, length);

				writer.BaseStream.Seek(0, SeekOrigin.Begin);
				icons[i] = new(writer.BaseStream);
			}

			return icons.OrderBy(i => i.Width).ToArray();
		}
		/// <summary>
		/// Saves this <see cref="Icon" /> to the specified file.
		/// </summary>
		/// <param name="path">A <see cref="string" /> that contains the name of the file to which to save this <see cref="Icon" />.</param>
		public void Save(string path)
		{
			Check.ArgumentNull(icon);
			Check.ArgumentNull(path);

			using FileStream file = File.Create(path);
			icon.Save(file);
		}
		/// <summary>
		/// Converts this <see cref="Icon" /> to its <see cref="byte" />[] representation.
		/// </summary>
		/// <returns>
		/// A new <see cref="byte" />[] representing this <see cref="Icon" />.
		/// </returns>
		public byte[] ToArray()
		{
			Check.ArgumentNull(icon);

			using MemoryStream memoryStream = new();
			icon.Save(memoryStream);
			return memoryStream.ToArray();
		}
	}
}