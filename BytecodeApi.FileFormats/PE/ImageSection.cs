using System.Diagnostics;

namespace BytecodeApi.FileFormats.PE
{
	/// <summary>
	/// Represents a section of a PE image file, containing the header and a <see cref="byte" />[] representing the contents of the section.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class ImageSection
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<ImageSection>("Header: {0}, Size: {1}", Header.Name, Data.Length);
		/// <summary>
		/// Gets the section header.
		/// </summary>
		public ImageSectionHeader Header { get; private set; }
		/// <summary>
		/// Gets a <see cref="byte" />[] representing the contents of the section.
		/// </summary>
		public byte[] Data { get; private set; }

		internal ImageSection(ImageSectionHeader header, byte[] data)
		{
			Header = header;
			Data = data;
		}
	}
}