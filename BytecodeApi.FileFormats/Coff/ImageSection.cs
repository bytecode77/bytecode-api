using System.Diagnostics;

namespace BytecodeApi.FileFormats.Coff
{
	/// <summary>
	/// Represents a section of a PE image file, containing the header and a <see cref="byte" />[] representing the contents of the section.
	/// </summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public sealed class ImageSection
	{
		private string DebuggerDisplay => CSharp.DebuggerDisplay<ImageSection>("Header: {0}, Size: {1}", Header.Name, Data.Length);
		/// <summary>
		/// Gets or sets the section header.
		/// </summary>
		public ImageSectionHeader Header { get; set; }
		/// <summary>
		/// Gets or sets a <see cref="byte" />[] representing the contents of the section.
		/// </summary>
		public byte[] Data { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageSection" /> class.
		/// </summary>
		/// <param name="header">The header of the section.</param>
		/// <param name="data">A <see cref="byte" />[] representing the contents of the section.</param>
		public ImageSection(ImageSectionHeader header, byte[] data)
		{
			Check.ArgumentNull(header, nameof(header));
			Check.ArgumentNull(data, nameof(data));

			Header = header;
			Data = data;
		}
	}
}