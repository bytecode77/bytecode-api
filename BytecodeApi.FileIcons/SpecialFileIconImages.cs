using System.Windows.Media.Imaging;

namespace BytecodeApi.FileIcons
{
	/// <summary>
	/// Represents a <see langword="static" /> set of <see cref="BitmapSource" /> objects (from <see cref="SpecialFileIcons" />) that do not represent specific file extensions. These properties are typically used in XAML bindings.
	/// </summary>
	public static class SpecialFileIconImages
	{
		/// <summary>
		/// Gets the <see cref="BitmapSource" /> for an unknown file extension in 16x16 resolution.
		/// </summary>
		public static BitmapSource Unknown16 => SpecialFileIcons.Unknown.Icon16ImageSource;
		/// <summary>
		/// Gets the <see cref="BitmapSource" /> for an unknown file extension in 32x32 resolution.
		/// </summary>
		public static BitmapSource Unknown32 => SpecialFileIcons.Unknown.Icon32ImageSource;
		/// <summary>
		/// Gets the <see cref="BitmapSource" /> for an unknown file extension in 48x48 resolution.
		/// </summary>
		public static BitmapSource Unknown48 => SpecialFileIcons.Unknown.Icon48ImageSource;
		/// <summary>
		/// Gets the <see cref="BitmapSource" /> for a directory in 16x16 resolution.
		/// </summary>
		public static BitmapSource Directory16 => SpecialFileIcons.Directory.Icon16ImageSource;
		/// <summary>
		/// Gets the <see cref="BitmapSource" /> for a directory in 32x32 resolution.
		/// </summary>
		public static BitmapSource Directory32 => SpecialFileIcons.Directory.Icon32ImageSource;
		/// <summary>
		/// Gets the <see cref="BitmapSource" /> for a directory in 48x48 resolution.
		/// </summary>
		public static BitmapSource Directory48 => SpecialFileIcons.Directory.Icon48ImageSource;
	}
}