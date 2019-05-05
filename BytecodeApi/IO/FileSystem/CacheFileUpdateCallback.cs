using System.IO;

namespace BytecodeApi.IO.FileSystem
{
	/// <summary>
	/// Represents the method that is called when a <see cref="CacheFile" /> instance is invalid and requires to be updated.
	/// </summary>
	/// <param name="stream">The <see cref="FileStream" /> of the cached file. Updated data needs to be written to this stream.</param>
	public delegate void CacheFileUpdateCallback(FileStream stream);
}