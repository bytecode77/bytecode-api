namespace BytecodeApi.FileFormats.Coff
{
	/// <summary>
	/// Represents a section of a PE image file, containing the header and a <see cref="byte" />[] representing the contents of the section.
	/// </summary>
	public sealed class ImageSection
	{
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
			Header = header;
			Data = data;
		}

		/// <summary>
		/// Returns a <see cref="string" /> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "[" + Header.Name + ", Size: " + Data.Length + "]";
		}
	}
}