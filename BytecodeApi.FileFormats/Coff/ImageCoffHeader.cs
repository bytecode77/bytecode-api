using BytecodeApi.Mathematics;
using System;

namespace BytecodeApi.FileFormats.Coff
{
	/// <summary>
	/// Represents the COFF header of a PE image file.
	/// </summary>
	public sealed class ImageCoffHeader
	{
		/// <summary>
		/// Gets or sets the number that identifies the type of target machine.
		/// </summary>
		public ImageMachineType Machine { get; set; }
		/// <summary>
		/// Gets or sets the number of sections. This indicates the size of the section table, which immediately follows the headers.
		/// </summary>
		public ushort NumberOfSections { get; set; }
		/// <summary>
		/// Gets or sets the low 32 bits of the number of seconds since 01.01.1970 00:00:00, that indicates when the file was created.
		/// </summary>
		public uint TimeDateStamp { get; set; }
		/// <summary>
		/// Gets the <see cref="DateTime" /> representation of the <see cref="TimeDateStamp" /> property.
		/// </summary>
		public DateTime TimeDateStampValue => DateTimeEx.ConvertUnixTimeStamp(TimeDateStamp);
		/// <summary>
		/// Gets or sets the file offset of the COFF symbol table, or zero if no COFF symbol table is present. This value should be zero for an image because COFF debugging information is deprecated.
		/// </summary>
		public uint PointerToSymbolTable { get; set; }
		/// <summary>
		/// Gets or sets the number of entries in the symbol table. This data can be used to locate the string table, which immediately follows the symbol table. This value should be zero for an image because COFF debugging information is deprecated.
		/// </summary>
		public uint NumberOfSymbols { get; set; }
		/// <summary>
		/// Gets or sets the size of the optional header, which is required for executable files but not for object files. This value should be zero for an object file.
		/// </summary>
		public ushort SizeOfOptionalHeader { get; set; }
		/// <summary>
		/// Gets or sets the flags that indicate the attributes of the file.
		/// </summary>
		public ImageCharacteristics Characteristics { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageCoffHeader" /> class.
		/// </summary>
		public ImageCoffHeader()
		{
		}
	}
}