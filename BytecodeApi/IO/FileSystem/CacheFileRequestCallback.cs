using System.IO;

namespace BytecodeApi.IO.FileSystem
{
	/// <summary>
	/// Represents the method that is called when a <see cref="CacheFile" /> instance is checked for validity. A return value of <see langword="false" /> means the <see cref="CacheFile" /> needs to be updated.
	/// </summary>
	/// <param name="file">The <see cref="FileInfo" /> that points to the cached file. This <see cref="object" /> can be used to check timestamp and file properties.</param>
	/// <returns>
	/// <see langword="true" />, if the <see cref="CacheFile" /> does not need to be updated;
	/// <see langword="false" />, if the <see cref="CacheFile" /> is invalid and needs to be updated.
	/// </returns>
	public delegate bool CacheFileRequestCallback(FileInfo file);
}