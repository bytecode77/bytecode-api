using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Icon" /> objects.
	/// </summary>
	public static class IconExtensions
	{
		/// <summary>
		/// Splits this <see cref="Icon" /> and returns an array with a new <see cref="Icon" /> for each frame.
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

				using (BinaryWriter writer = new BinaryWriter(new MemoryStream(length + 24)))
				{
					writer.Write(iconData, 0, 4);
					writer.Write((short)1);
					writer.Write(iconData, i * 16 + 6, 12);
					writer.Write(22);
					writer.Write(iconData, offset, length);

					writer.BaseStream.Seek(0);
					icons[i] = new Icon(writer.BaseStream);
				}
			}

			return icons.OrderBy(i => i.Width).ToArray();
		}

		/// <summary>
		/// Saves this <see cref="Icon" /> to the specified file.
		/// </summary>
		/// <param name="icon">The <see cref="Icon" /> to save.</param>
		/// <param name="path">A <see cref="string" /> that contains the name of the file to which to save this <see cref="Icon" />.</param>
		public static void Save(this Icon icon, string path)
		{
			Check.ArgumentNull(icon, nameof(icon));
			Check.ArgumentNull(path, nameof(path));

			using (FileStream file = File.Create(path))
			{
				icon.Save(file);
			}
		}
		/// <summary>
		/// Converts this <see cref="Icon" /> to its <see cref="byte" />[] representation.
		/// </summary>
		/// <param name="icon">The <see cref="Icon" /> to save.</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing this <see cref="Icon" />.
		/// </returns>
		public static byte[] ToArray(this Icon icon)
		{
			Check.ArgumentNull(icon, nameof(icon));

			using (MemoryStream memoryStream = new MemoryStream())
			{
				icon.Save(memoryStream);
				return memoryStream.ToArray();
			}
		}

		/// <summary>
		/// Returns a managed <see cref="BitmapSource" />, based on the provided <see cref="Icon" />.
		/// </summary>
		/// <param name="icon">The <see cref="Icon" /> to convert.</param>
		/// <returns>
		/// The created <see cref="BitmapSource" />.
		/// </returns>
		public static BitmapSource ToBitmapSource(this Icon icon)
		{
			Check.ArgumentNull(icon, nameof(icon));

			return icon.ToBitmap().ToBitmapSource();
		}
	}
}