using BytecodeApi.Extensions;
using System;
using System.IO;
using System.Text;

namespace BytecodeApi.IO.FileSystem
{
	/// <summary>
	/// Provides <see langword="static" /> methods that extend the <see cref="Path" /> class.
	/// </summary>
	public static class PathEx
	{
		/// <summary>
		/// Returns the path of an existing file or directory in its original representation with fixed character casing. If it does not exist, the original <see cref="string" /> is returned.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a file or directory.</param>
		/// <returns>
		/// The path of an existing file or directory in its original representation with fixed character casing. If it does not exist, the original <see cref="string" /> is returned.
		/// </returns>
		public static string GetOriginalPath(string path)
		{
			Check.ArgumentNull(path, nameof(path));

			StringBuilder stringBuilder = new StringBuilder(260);

			Native.GetShortPathName(path, stringBuilder, stringBuilder.Capacity);
			int result = Native.GetLongPathName(stringBuilder.ToString(), stringBuilder, stringBuilder.Capacity);

			if (result > 0 && result < stringBuilder.Capacity)
			{
				path = stringBuilder.ToString(0, result);
				if (path[0] >= 'a' && path[0] <= 'z' && path[1] == ':') path = path[0].ToUpper() + path.Substring(1);
			}

			return path;
		}
		/// <summary>
		/// Converts a relative path to an absolute path.
		/// </summary>
		/// <param name="baseDirectory">The base directory to use for relative paths.</param>
		/// <param name="relativePath">A relative or absolute path to convert.</param>
		/// <returns>
		/// If <paramref name="relativePath" /> is a relative path, the absolute path starting from <paramref name="baseDirectory" />;
		/// otherwise, the original value of <paramref name="relativePath" />.
		/// </returns>
		public static string RelativeToAbsolutePath(string baseDirectory, string relativePath)
		{
			if (relativePath.IsNullOrEmpty())
			{
				return relativePath;
			}
			else
			{
				return Path.IsPathRooted(relativePath) ? relativePath : Path.Combine(baseDirectory, relativePath);
			}
		}
		/// <summary>
		/// Converts an absolute path to a relative path.
		/// </summary>
		/// <param name="baseDirectory">The base directory to use for relative paths.</param>
		/// <param name="absolutePath">A relative or absolute path to convert.</param>
		/// <returns>
		/// If <paramref name="absolutePath" /> is an absolute path, the original value of <paramref name="absolutePath" />;
		/// otherwise the absolute path starting from <paramref name="baseDirectory" />.
		/// </returns>
		public static string AbsoluteToRelativePath(string baseDirectory, string absolutePath)
		{
			if (absolutePath.IsNullOrEmpty())
			{
				return absolutePath;
			}
			else
			{
				absolutePath = absolutePath.Replace("/", @"\");
				baseDirectory = baseDirectory.Replace("/", @"\");

				if (absolutePath.StartsWith(baseDirectory, StringComparison.OrdinalIgnoreCase)) return absolutePath.Substring(baseDirectory.Length).TrimStart('\\');
				else return absolutePath;
			}
		}
	}
}