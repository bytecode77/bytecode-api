using System;

namespace BytecodeApi.IO.FileSystem
{
	//FEATURE: IgnoreEmptyDirectories
	/// <summary>
	/// Specifies flags for the comparison of two directories.
	/// </summary>
	[Flags]
	public enum CompareDirectoryOptions
	{
		/// <summary>
		/// Specifies that no comparison flags are defined.
		/// </summary>
		Default = 0,
		/// <summary>
		/// Specifies to compare subdirectories recursively.
		/// </summary>
		Recursive = 1,
		/// <summary>
		/// Specifies that character casing of file and directory names is ignored during comparison.
		/// </summary>
		IgnoreCase = 2,
		/// <summary>
		/// Specifies that files with equal size are considered equal. If no CompareFile* flags are defined, the list of filenames is compared.
		/// </summary>
		CompareFileSize = 4,
		/// <summary>
		/// Specifies that file contents are compared. If no CompareFile* flags are defined, the list of filenames is compared.
		/// </summary>
		CompareFileContents = 8,
		/// <summary>
		/// Specifies that the timestamps (last write time) of files are compared.
		/// </summary>
		CompareDirectoryLastWriteTime = 16,
		/// <summary>
		/// Specifies that the timestamps (last write time) of directories are compared.
		/// </summary>
		CompareFileLastWriteTime = 32,
		/// <summary>
		/// Specifies that files are excluded from comparison.
		/// </summary>
		IgnoreFiles = 64,
		/// <summary>
		/// Specifies that directories are excluded from comparison.
		/// </summary>
		IgnoreDirectories = 128
	}
}