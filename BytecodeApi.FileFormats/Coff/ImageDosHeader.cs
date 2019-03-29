namespace BytecodeApi.FileFormats.Coff
{
	/// <summary>
	/// Represents the DOS header of a PE image file.
	/// </summary>
	public sealed class ImageDosHeader
	{
		/// <summary>
		/// Specifies the amount of bytes on last page of the file.
		/// </summary>
		public ushort LastPageSize { get; set; }
		/// <summary>
		/// Specifies the amount of pages in the file.
		/// </summary>
		public ushort PageCount { get; set; }
		/// <summary>
		/// Specifies the amount of relocations in the file.
		/// </summary>
		public ushort RelocationCount { get; set; }
		/// <summary>
		/// Specifies the size of header in paragraphs.
		/// </summary>
		public ushort HeaderSize { get; set; }
		/// <summary>
		/// Specifies the minimum extra paragraphs needed.
		/// </summary>
		public ushort MinAlloc { get; set; }
		/// <summary>
		/// Specifies the maximum extra paragraphs needed.
		/// </summary>
		public ushort MaxAlloc { get; set; }
		/// <summary>
		/// Specifies the initial (relative) SS value.
		/// </summary>
		public ushort InitialSS { get; set; }
		/// <summary>
		/// Specifies the initial SP value.
		/// </summary>
		public ushort InitialSP { get; set; }
		/// <summary>
		/// Specifies the file checksum.
		/// </summary>
		public ushort Checksum { get; set; }
		/// <summary>
		/// Specifies the initial IP value.
		/// </summary>
		public ushort InitialIP { get; set; }
		/// <summary>
		/// Specifies the initial (relative) CS value.
		/// </summary>
		public ushort InitialCS { get; set; }
		/// <summary>
		/// Specifies the file address of the relocation table.
		/// </summary>
		public ushort RelocationOffset { get; set; }
		/// <summary>
		/// Specifies the overlay number.
		/// </summary>
		public ushort OverlayNumber { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved1 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved2 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved3 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved4 { get; set; }
		/// <summary>
		/// Specifies the OEM Identifier.
		/// </summary>
		public ushort OemIdentifier { get; set; }
		/// <summary>
		/// Specifies the OEM identifier.
		/// </summary>
		public ushort OemInformation { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved5 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved6 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved7 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved8 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved9 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved10 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved11 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved12 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved13 { get; set; }
		/// <summary>
		/// Reserved.
		/// </summary>
		public ushort Reserved14 { get; set; }
		/// <summary>
		/// Specifies the file address of new EXE header.
		/// </summary>
		public uint PEHeaderOffset { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageDosHeader" /> class.
		/// </summary>
		public ImageDosHeader()
		{
		}
	}
}