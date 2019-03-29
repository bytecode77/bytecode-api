using System.IO;

namespace BytecodeApi.IO.FileSystem
{
	/// <summary>
	/// Represents the method that is called when a <see cref="CacheFile" /> instance is invalid and requires to be updated.
	/// </summary>
	/// <returns>
	/// The <see cref="Stream" /> to read from that is written to the cache file at the specified location.
	/// </returns>
	public delegate Stream CacheFileUpdateCallback();
}