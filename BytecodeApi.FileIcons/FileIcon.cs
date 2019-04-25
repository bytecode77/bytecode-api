using BytecodeApi.Extensions;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Media.Imaging;

namespace BytecodeApi.FileIcons
{
	/// <summary>
	/// Class to retrieve file icons from the resources of BytecodeApi.FileIcons.dll. The file icons are pre-defined images and limited to a fixed amount of known file extensions.
	/// </summary>
	public sealed class FileIcon
	{
		private static DictionaryEntry[] _Resources;
		private static DictionaryEntry[] Resources
		{
			get
			{
				if (_Resources == null)
				{
					_Resources = Properties.Resources.ResourceManager
						.GetResourceSet(CultureInfo.InvariantCulture, true, false)
						.ToArray<DictionaryEntry>();
				}

				return _Resources;
			}
		}
		private static FileIcon[] _AllIcons;
		/// <summary>
		/// Gets all <see cref="FileIcon" /> objects from the resources of BytecodeApi.FileIcons.dll.
		/// </summary>
		public static FileIcon[] AllIcons
		{
			get
			{
				if (_AllIcons == null)
				{
					_AllIcons = Resources
						.Where(icon => CSharp.EqualsNone(icon.Key, nameof(Properties.Resources._unknown), nameof(Properties.Resources._directory)))
						.OrderBy(icon => icon.Key as string)
						.Select(icon => GetFileIconFromResources(icon))
						.ToArray();
				}

				return _AllIcons;
			}
		}

		private Bitmap _Icon16;
		private Bitmap _Icon32;
		private Bitmap _Icon48;
		/// <summary>
		/// Gets the extension, without a dot, that identifies this icon.
		/// </summary>
		public string Extension { get; private set; }
		/// <summary>
		/// Gets the combined icon containing all sizes (16, 32 and 48).
		/// </summary>
		public Icon Icon { get; private set; }
		/// <summary>
		/// Gets the 16x16 pixel frame from <see cref="Icon" />.
		/// </summary>
		public Bitmap Icon16
		{
			get
			{
				if (_Icon16 == null) _Icon16 = new Icon(Icon, new Size(16, 16)).ToBitmap();
				return _Icon16;
			}
		}
		/// <summary>
		/// Gets the 32x32 pixel frame from <see cref="Icon" />.
		/// </summary>
		public Bitmap Icon32
		{
			get
			{
				if (_Icon32 == null) _Icon32 = new Icon(Icon, new Size(32, 32)).ToBitmap();
				return _Icon32;
			}
		}
		/// <summary>
		/// Gets the 48x48 pixel frame from <see cref="Icon" />.
		/// </summary>
		public Bitmap Icon48
		{
			get
			{
				if (_Icon48 == null) _Icon48 = new Icon(Icon, new Size(48, 48)).ToBitmap();
				return _Icon48;
			}
		}
		/// <summary>
		/// Gets a managed <see cref="BitmapSource" />, based on the <see cref="Icon16" /> property.
		/// </summary>
		public BitmapSource Icon16ImageSource => Icon16.ToBitmapSource();
		/// <summary>
		/// Gets a managed <see cref="BitmapSource" />, based on the <see cref="Icon32" /> property.
		/// </summary>
		public BitmapSource Icon32ImageSource => Icon32.ToBitmapSource();
		/// <summary>
		/// Gets a managed <see cref="BitmapSource" />, based on the <see cref="Icon48" /> property.
		/// </summary>
		public BitmapSource Icon48ImageSource => Icon48.ToBitmapSource();

		private FileIcon()
		{
		}

		/// <summary>
		/// Gets a <see cref="FileIcon" /> from the resources of BytecodeApi.FileIcons.dll, if the specified extension is known; otherwise, returns <see langword="null" />.
		/// </summary>
		/// <param name="extension">A <see cref="string" /> value specifying the file extension, which is case insensitive and does not require a leading dot character.</param>
		/// <returns>
		/// A <see cref="FileIcon" /> object containing image objects for the specified extension, if found;
		/// otherwise, returns <see langword="null" />.
		/// </returns>
		public static FileIcon FromExtension(string extension)
		{
			Check.ArgumentNull(extension, nameof(extension));

			return AllIcons.FirstOrDefault(icon => icon.Extension.Equals(extension.TrimStart('.'), SpecialStringComparisons.IgnoreCase));
		}
		/// <summary>
		/// Checks whether the specified extension is known and exists in the resources of BytecodeApi.FileIcons.dll.
		/// </summary>
		/// <param name="extension">A <see cref="string" /> value specifying the file extension, which is case insensitive and does not require a leading dot character.</param>
		/// <returns>
		/// A <see cref="bool" /> value indicating whether the specified extension is known and exists in the resources of BytecodeApi.FileIcons.dll.
		/// </returns>
		public static bool Exists(string extension)
		{
			return extension != null && FromExtension(extension) != null;
		}
		/// <summary>
		/// Gets a specific frame from <see cref="Icon" />. The <paramref name="size" /> parameter must be 16, 32 or 48.
		/// </summary>
		/// <param name="size">A <see cref="int" /> value representing the size of the frame to retrieve. The value must be 16, 32 or 48.</param>
		/// <returns>
		/// The frame from <see cref="Icon" />, specified by <paramref name="size" />.
		/// </returns>
		public Bitmap GetIcon(int size)
		{
			if (size == 16) return Icon16;
			else if (size == 32) return Icon32;
			else if (size == 48) return Icon48;
			else throw Throw.Argument(nameof(size), "Icon size must be 16, 32 or 48.");
		}
		/// <summary>
		/// Gets a managed <see cref="BitmapSource" />, based on the specific frame from <see cref="Icon" />. The <paramref name="size" /> parameter must be 16, 32 or 48.
		/// </summary>
		/// <param name="size">A <see cref="int" /> value representing the size of the frame to retrieve. The value must be 16, 32 or 48.</param>
		/// <returns>
		/// A managed <see cref="BitmapSource" />, based on the frame from <see cref="Icon" />, specified by <paramref name="size" />.
		/// </returns>
		public BitmapSource GetIconImageSource(int size)
		{
			return GetIcon(size).ToBitmapSource();
		}

		private static FileIcon GetFileIconFromResources(DictionaryEntry entry)
		{
			return new FileIcon
			{
				Extension = (string)entry.Key,
				Icon = (Icon)entry.Value
			};
		}
		internal static FileIcon GetFileIcon(string name)
		{
			return GetFileIconFromResources(Resources.First(icon => icon.Key as string == name));
		}
	}
}