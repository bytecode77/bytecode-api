using BytecodeApi.Extensions;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace BytecodeApi.IO
{
	/// <summary>
	/// Helper class for <see cref="Icon" /> objects.
	/// </summary>
	public static class IconHelper
	{
		/// <summary>
		/// Splits an <see cref="Icon" /> and returns an array with a new <see cref="Icon" /> for each frame.
		/// </summary>
		/// <param name="icon">The original <see cref="Icon" /> to split.</param>
		/// <returns>
		/// An array with a new <see cref="Icon" /> for each frame.
		/// </returns>
		public static Icon[] Split(this Icon icon)
		{
			Check.ArgumentNull(icon, nameof(icon));

			byte[] iconData = icon.ToArray();
			Icon[] icons = new Icon[BitConverter.ToUInt16(iconData, 4)];

			for (int i = 0; i < icons.Length; i++)
			{
				int offset = BitConverter.ToInt32(iconData, i * 16 + 18);
				int length = BitConverter.ToInt32(iconData, i * 16 + 14);

				using BinaryWriter writer = new BinaryWriter(new MemoryStream(length + 24));

				writer.Write(iconData, 0, 4);
				writer.Write((short)1);
				writer.Write(iconData, i * 16 + 6, 12);
				writer.Write(22);
				writer.Write(iconData, offset, length);

				writer.BaseStream.Seek(0, SeekOrigin.Begin);
				icons[i] = new Icon(writer.BaseStream);
			}

			return icons.OrderBy(i => i.Width).ToArray();
		}
		/// <summary>
		/// Merges multiple <see cref="Image" /> frames into a new <see cref="Icon" />.
		/// </summary>
		/// <param name="images">The <see cref="Image" /> objects to be merged into an <see cref="Icon" />.</param>
		/// <returns>
		/// A new <see cref="Icon" /> with the frames from the <paramref name="images" /> parameter.
		/// </returns>
		public static Icon Merge(Image[] images)
		{
			Check.ArgumentNull(images, nameof(images));
			Check.ArgumentEx.ArrayElementsRequired(images, nameof(images));
			Check.ArgumentEx.ArrayValuesNotNull(images, nameof(images));
			Check.Argument(images.All(image => image.Width < 256 && image.Height < 256), nameof(images), "Images may not exceed a resolution of 256x256 pixels.");

			using MemoryStream memoryStream = new MemoryStream();

			using (BinaryWriter iconWriter = new BinaryWriter(memoryStream, Encoding.Default, true))
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
						2 => bitsPerPixel = 3 * frameBytes[i][24],
						6 => bitsPerPixel = 4 * frameBytes[i][24],
						_ => bitsPerPixel = frameBytes[i][24]
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

				foreach (byte[] frame in frameBytes) iconWriter.Write(frame);
			}

			memoryStream.Position = 0;
			return new Icon(memoryStream);
		}
	}
}