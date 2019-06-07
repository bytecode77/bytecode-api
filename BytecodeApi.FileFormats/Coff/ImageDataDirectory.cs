using System.Diagnostics;

namespace BytecodeApi.FileFormats.Coff
{
	/// <summary>
	/// Represents a data directory of a PE image file.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class ImageDataDirectory
	{
		private string DebuggerDisplay => CSharp.DebuggerDisplay<ImageDataDirectory>("Name = {0}", Name);
		/// <summary>
		/// Gets the name of the data directory. This may not be a valid enum value of <see cref="ImageDataDirectoryName" />, if the image has more than 14 data directories.
		/// </summary>
		public ImageDataDirectoryName Name { get; private set; }
		/// <summary>
		/// Gets or sets the address of the first byte of a table or string that Windows uses.
		/// </summary>
		public uint VirtualAddress { get; set; }
		/// <summary>
		/// Gets or sets size of a table or string that Windows uses.
		/// </summary>
		public uint Size { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageDataDirectory" /> class.
		/// </summary>
		public ImageDataDirectory(ImageDataDirectoryName name, uint virtualAddress, uint size)
		{
			Name = name;
			VirtualAddress = virtualAddress;
			Size = size;
		}
	}
}