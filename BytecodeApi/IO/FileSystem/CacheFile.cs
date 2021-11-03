using BytecodeApi.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BytecodeApi.IO.FileSystem
{
	/// <summary>
	/// Represents a file that is a cached version of another content source. The file is located on the disk and is updated by a specified callback condition or a timeout. Read operations are performed directly from the disk, the file is not cached in memory.
	/// </summary>
	public class CacheFile
	{
		private readonly TimeSpan? Timeout;
		private readonly CacheFileRequestCallback RequestCallback;
		private readonly CacheFileUpdateCallback UpdateCallback;
		/// <summary>
		/// Gets the path to the file of this instance. This file does not need to exist.
		/// </summary>
		public string Path { get; private set; }
		/// <summary>
		/// Gets the amount of time that has passed since the cached file was last written to.
		/// </summary>
		public TimeSpan Age => DateTime.Now - new FileInfo(Path).LastWriteTime;

		private CacheFile(string path, TimeSpan? timeout, CacheFileRequestCallback requestCallback, CacheFileUpdateCallback updateCallback)
		{
			Path = path;
			Timeout = timeout;
			RequestCallback = requestCallback;
			UpdateCallback = updateCallback;
		}
		/// <summary>
		/// Creates a new <see cref="CacheFile" /> instance for the file specified by <paramref name="path" />. If the file does not exist, yet, it is created upon first access.
		/// </summary>
		/// <param name="path">A <see cref="string" /> that contains the name of the file that represents the cached version. This file does not need to exist.</param>
		/// <param name="timeout">A <see cref="TimeSpan" /> value that specifies the timeout period after which the file will be updated. This value is compared to the LastWriteTime property of the file.</param>
		/// <param name="updateCallback">The method that is called when the file needs to be updated.</param>
		/// <returns>
		/// The <see cref="CacheFile" /> this method creates.
		/// </returns>
		public static CacheFile CreateWithTimeout(string path, TimeSpan timeout, CacheFileUpdateCallback updateCallback)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.ArgumentEx.StringNotEmpty(path, nameof(path));
			Check.ArgumentNull(updateCallback, nameof(updateCallback));

			return new CacheFile(path, timeout, null, updateCallback);
		}
		/// <summary>
		/// Creates a new <see cref="CacheFile" /> instance for the file specified by <paramref name="path" />. If the file does not exist, yet, it is created upon first access.
		/// </summary>
		/// <param name="path">A <see cref="string" /> that contains the name of the file that represents the cached version. This file does not need to exist.</param>
		/// <param name="requestCallback">The method that is called when the file is accessed. If the method returns <see langword="false" />, it means the cached file is invalid and <paramref name="updateCallback" /> is called to update the file. If it returns <see langword="true" />, the existing file is read from the disk. This method is only called when the file on the disk exists.</param>
		/// <param name="updateCallback">The method that is called when the file needs to be updated.</param>
		/// <returns>
		/// The <see cref="CacheFile" /> this method creates.
		/// </returns>
		public static CacheFile CreateWithCallback(string path, CacheFileRequestCallback requestCallback, CacheFileUpdateCallback updateCallback)
		{
			Check.ArgumentNull(path, nameof(path));
			Check.ArgumentEx.StringNotEmpty(path, nameof(path));
			Check.ArgumentNull(requestCallback, nameof(requestCallback));
			Check.ArgumentNull(updateCallback, nameof(updateCallback));

			return new CacheFile(path, null, requestCallback, updateCallback);
		}

		/// <summary>
		/// Opens the file for reading.
		/// If the file does not exist or an update is required by the specified conditions (timeout or callback), the file is updated and then opened.
		/// </summary>
		/// <param name="updated">When this method returns, a <see cref="bool" /> value indicating whether the file has been updated before it was opened.</param>
		/// <returns>
		/// A <see cref="FileStream" /> from the file located in <see cref="Path" />.
		/// </returns>
		public FileStream OpenRead(out bool updated)
		{
			if (!File.Exists(Path) || Age > Timeout || RequestCallback?.Invoke(new FileInfo(Path)) == false)
			{
				using FileStream file = File.Create(Path);
				UpdateCallback(file);

				updated = true;
			}
			else
			{
				updated = false;
			}

			return File.OpenRead(Path);
		}
		/// <summary>
		/// Opens the file, reads the contents of the file into a <see cref="byte" />[], and then closes the file.
		/// If the file does not exist or an update is required by the specified conditions (timeout or callback), the file is updated and then opened.
		/// </summary>
		/// <returns>
		/// A new <see cref="byte" />[] containing the contents of the file.
		/// </returns>
		public byte[] ReadAllBytes()
		{
			using FileStream file = OpenRead(out _);
			using MemoryStream memoryStream = new MemoryStream();

			file.CopyTo(memoryStream);
			return memoryStream.ToArray();
		}
		/// <summary>
		/// Opens the file, reads all lines of the file into a <see cref="string" />[], and then closes the file.
		/// If the file does not exist or an update is required by the specified conditions (timeout or callback), the file is updated and then opened.
		/// </summary>
		/// <returns>
		/// A new <see cref="string" />[] containing all lines of the file.
		/// </returns>
		public string[] ReadAllLines()
		{
			return ReadAllLines(Encoding.UTF8);
		}
		/// <summary>
		/// Opens the file, reads all lines of the file with the specified encoding into a <see cref="string" />[], and then closes the file.
		/// If the file does not exist or an update is required by the specified conditions (timeout or callback), the file is updated and then opened.
		/// </summary>
		/// <param name="encoding">The encoding applied to the contents of the file.</param>
		/// <returns>
		/// A new <see cref="string" />[] containing all lines of the file.
		/// </returns>
		public string[] ReadAllLines(Encoding encoding)
		{
			Check.ArgumentNull(encoding, nameof(encoding));

			return ReadLines(encoding).ToArray();
		}
		/// <summary>
		/// Opens a text file, reads all lines of the file into a <see cref="string" />, and then closes the file.
		/// If the file does not exist or an update is required by the specified conditions (timeout or callback), the file is updated and then opened.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> containing all lines of the file.
		/// </returns>
		public string ReadAllText()
		{
			return ReadAllText(Encoding.UTF8);
		}
		/// <summary>
		/// Opens a text file, reads all lines of the file with the specified encoding into a <see cref="string" />, and then closes the file.
		/// If the file does not exist or an update is required by the specified conditions (timeout or callback), the file is updated and then opened.
		/// </summary>
		/// <param name="encoding">The encoding applied to the contents of the file.</param>
		/// <returns>
		/// A <see cref="string" /> containing all lines of the file.
		/// </returns>
		public string ReadAllText(Encoding encoding)
		{
			Check.ArgumentNull(encoding, nameof(encoding));

			using StreamReader stream = new StreamReader(OpenRead(out _), encoding);
			return stream.ReadToEnd();
		}
		/// <summary>
		/// Reads the lines of a file.
		/// If the file does not exist or an update is required by the specified conditions (timeout or callback), the file is updated and then opened.
		/// </summary>
		/// <returns>
		/// All the lines of the file, or the lines that are the result of a query.
		/// </returns>
		public IEnumerable<string> ReadLines()
		{
			return ReadLines(Encoding.UTF8);
		}
		/// <summary>
		/// Reads the lines of a file that has a specified encoding.
		/// If the file does not exist or an update is required by the specified conditions (timeout or callback), the file is updated and then opened.
		/// </summary>
		/// <param name="encoding">The encoding applied to the contents of the file.</param>
		/// <returns>
		/// All the lines of the file, or the lines that are the result of a query.
		/// </returns>
		public IEnumerable<string> ReadLines(Encoding encoding)
		{
			Check.ArgumentNull(encoding, nameof(encoding));

			using StreamReader stream = new StreamReader(OpenRead(out _), encoding);
			string line;

			while ((line = stream.ReadLine()) != null)
			{
				yield return line;
			}
		}
		/// <summary>
		/// Deletes the cached file, if it exists. The next call to <see cref="OpenRead(out bool)" /> will trigger an update as specified in the <see cref="CacheFileUpdateCallback" /> delegate.
		/// </summary>
		public void Delete()
		{
			File.Delete(Path);
		}
	}
}