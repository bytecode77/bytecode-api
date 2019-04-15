namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Specifies the method that is used to convert the path in <see cref="string" /> values.
	/// </summary>
	public enum PathConverterMethod
	{
		/// <summary>
		/// Returns the file name and extension of the path <see cref="string" /> value.
		/// </summary>
		FileName,
		/// <summary>
		/// Returns the file name of the of the path <see cref="string" /> value without the extension.
		/// </summary>
		FileNameWithoutExtension,
		/// <summary>
		/// Returns the extension of the path <see cref="string" /> value.
		/// </summary>
		Extension,
		/// <summary>
		/// Returns the extension of the path <see cref="string" /> value without the leading dot.
		/// </summary>
		ExtensionWithoutDot,
		/// <summary>
		/// Returns the directory of the path <see cref="string" /> value.
		/// </summary>
		DirectoryName,
		/// <summary>
		/// Returns the root directory of the path <see cref="string" /> value.
		/// </summary>
		Root,
		/// <summary>
		/// Combines the path <see cref="string" /> value with the path <see cref="string" /> in the parameter.
		/// </summary>
		Combine,
		/// <summary>
		/// Changes the extension of the path <see cref="string" /> value with the extension <see cref="string" /> in the parameter.
		/// </summary>
		ChangeExtension,
		/// <summary>
		/// If the file or directory exists, returns the name of the specified path with character casing according to the original path; otherwise, the original value.
		/// </summary>
		CaseSensitiveName,
		/// <summary>
		/// If the file or directory exists, returns the full path of the specified path with character casing according to the original path; otherwise, the original value.
		/// </summary>
		CaseSensitiveFullName
	}
}