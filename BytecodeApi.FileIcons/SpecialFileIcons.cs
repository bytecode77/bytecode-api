namespace BytecodeApi.FileIcons
{
	/// <summary>
	/// Represents a <see langword="static" /> set of <see cref="FileIcon" /> objects that do not represent specific file extensions.
	/// </summary>
	public static class SpecialFileIcons
	{
		/// <summary>
		/// Gets the <see cref="FileIcon" /> for an unknown file extension.
		/// </summary>
		public static FileIcon Unknown => FileIcon.GetFileIcon("_unknown");
		/// <summary>
		/// Gets the <see cref="FileIcon" /> for a directory.
		/// </summary>
		public static FileIcon Directory => FileIcon.GetFileIcon("_directory");
	}
}