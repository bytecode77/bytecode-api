namespace BytecodeApi.FileFormats.Coff
{
	/// <summary>
	/// Represents the base class for PE image optional headers. This is an abstract class.
	/// </summary>
	public abstract class ImageOptionalHeader
	{
		/// <summary>
		/// Gets or sets the linker major version number.
		/// </summary>
		public byte MajorLinkerVersion { get; set; }
		/// <summary>
		/// Gets or sets the linker minor version number.
		/// </summary>
		public byte MinorLinkerVersion { get; set; }
		/// <summary>
		/// Gets or sets the size of the code (text) section, or the sum of all code sections if there are multiple sections.
		/// </summary>
		public uint SizeOfCode { get; set; }
		/// <summary>
		/// Gets or sets the size of the initialized data section, or the sum of all such sections if there are multiple data sections.
		/// </summary>
		public uint SizeOfInitializedData { get; set; }
		/// <summary>
		/// Gets or sets the size of the uninitialized data section (BSS), or the sum of all such sections if there are multiple BSS sections.
		/// </summary>
		public uint SizeOfUninitializedData { get; set; }
		/// <summary>
		/// Gets or sets the address of the entry point relative to the image base when the executable file is loaded into memory. For program images, this is the starting address. For device drivers, this is the address of the initialization function. An entry point is optional for DLLs. When no entry point is present, this property must be zero.
		/// </summary>
		public uint AddressOfEntryPoint { get; set; }
		/// <summary>
		/// Gets or sets the address that is relative to the image base of the beginning-of-code section when it is loaded into memory.
		/// </summary>
		public uint BaseOfCode { get; set; }
		/// <summary>
		/// Gets or sets the alignment (in bytes) of sections when they are loaded into memory. It must be greater than or equal to <see cref="FileAlignment" />. The default is the page size for the architecture.
		/// </summary>
		public uint SectionAlignment { get; set; }
		/// <summary>
		/// Gets or sets the alignment factor (in bytes) that is used to align the raw data of sections in the image file. The value should be a power of 2 between 512 and 64 K, inclusive. The default is 512. If the <see cref="SectionAlignment" /> is less than the architecture's page size, then <see cref="FileAlignment" /> must match <see cref="SectionAlignment" />.
		/// </summary>
		public uint FileAlignment { get; set; }
		/// <summary>
		/// Gets or sets the major version number of the required operating system.
		/// </summary>
		public ushort MajorOperatingSystemVersion { get; set; }
		/// <summary>
		/// Gets or sets the minor version number of the required operating system.
		/// </summary>
		public ushort MinorOperatingSystemVersion { get; set; }
		/// <summary>
		/// Gets or sets the major version number of the image.
		/// </summary>
		public ushort MajorImageVersion { get; set; }
		/// <summary>
		/// Gets or sets the minor version number of the image.
		/// </summary>
		public ushort MinorImageVersion { get; set; }
		/// <summary>
		/// Gets or sets the major version number of the subsystem.
		/// </summary>
		public ushort MajorSubsystemVersion { get; set; }
		/// <summary>
		/// Gets or sets the minor version number of the subsystem.
		/// </summary>
		public ushort MinorSubsystemVersion { get; set; }
		/// <summary>
		/// Reserved, must be zero.
		/// </summary>
		public uint Win32VersionValue { get; set; }
		/// <summary>
		/// Gets or sets the size (in bytes) of the image, including all headers, as the image is loaded in memory. It must be a multiple of <see cref="SectionAlignment" />.
		/// </summary>
		public uint SizeOfImage { get; set; }
		/// <summary>
		/// Gets or sets the combined size of an MS-DOS stub, PE header, and section headers rounded up to a multiple of <see cref="FileAlignment" />.
		/// </summary>
		public uint SizeOfHeaders { get; set; }
		/// <summary>
		/// Gets or sets the image file checksum. The algorithm for computing the checksum is incorporated into IMAGHELP.DLL. The following are checked for validation at load time: all drivers, any DLL loaded at boot time, and any DLL that is loaded into a critical Windows process.
		/// </summary>
		public uint Checksum { get; set; }
		/// <summary>
		/// Gets or sets the subsystem that is required to run this image.
		/// </summary>
		public ImageSubsystem Subsystem { get; set; }
		/// <summary>
		/// Gets or sets the "DllCharacteristics" attribute of the PE image optional header.
		/// </summary>
		public ImageDllCharacteristics DllCharacteristics { get; set; }
		/// <summary>
		/// Reserved, must be zero.
		/// </summary>
		public uint LoaderFlags { get; set; }
		/// <summary>
		/// Gets or sets the number of data-directory entries in the remainder of the optional header. Each describes a location and size.
		/// </summary>
		public uint NumberOfRvaAndSizes { get; set; }
		/// <summary>
		/// Gets or sets the collection data directories of the PE image optional header.
		/// </summary>
		public ImageDataDirectory[] DataDirectories { get; set; }

		private protected ImageOptionalHeader()
		{
		}

		/// <summary>
		/// Returns the <see cref="ImageOptionalHeader32.ImageBase" /> or <see cref="ImageOptionalHeader64.ImageBase" /> property of this instance.
		/// </summary>
		/// <returns>
		/// The <see cref="ImageOptionalHeader32.ImageBase" /> or <see cref="ImageOptionalHeader64.ImageBase" /> property of this instance.
		/// </returns>
		public ulong GetImageBase()
		{
			if (this is ImageOptionalHeader32 optionalHeader32) return optionalHeader32.ImageBase;
			else if (this is ImageOptionalHeader64 optionalHeader64) return optionalHeader64.ImageBase;
			else throw Throw.UnsupportedType(nameof(optionalHeader32.ImageBase));
		}
	}
}