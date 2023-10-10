using BytecodeApi.Extensions;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;

namespace BytecodeApi.IO;

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
	[SupportedOSPlatform("windows")]
	public static string GetOriginalPath(string path)
	{
		Check.ArgumentNull(path);

		StringBuilder stringBuilder = new(260);

		Native.GetShortPathName(path, stringBuilder, stringBuilder.Capacity);
		int result = Native.GetLongPathName(stringBuilder.ToString(), stringBuilder, stringBuilder.Capacity);

		if (result > 0 && result < stringBuilder.Capacity)
		{
			path = stringBuilder.ToString(0, result);
			if (path[0] is >= 'a' and <= 'z' && path[1] == ':')
			{
				path = path[0].ToUpper() + path[1..];
			}
		}

		return path;
	}
	/// <summary>
	/// Gets the UNC path of an existing file or directory. If the path cannot be converted to a UNC path, the original path is returned.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a file or directory.</param>
	/// <returns>
	/// The UNC path of an existing file or directory, if conversion is possible;
	/// otherwise, the original path.
	/// </returns>
	[SupportedOSPlatform("windows")]
	public static string GetUncPath(string path)
	{
		Check.ArgumentNull(path);
		Check.FileOrDirectoryNotFound(path);

		if (path.Length > 2 && path[1] == ':' && path[0] is >= 'a' and <= 'z' or >= 'A' and <= 'Z')
		{
			StringBuilder result = new(512);
			int length = result.Capacity;
			if (Native.WNetGetConnection(path[..2], result, ref length) == 0)
			{
				return Path.Combine(result.ToString().TrimEnd(), path[Path.GetPathRoot(path)!.Length..]);
			}
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
		if (relativePath.IsNullOrEmpty() || Path.IsPathRooted(relativePath))
		{
			return relativePath;
		}
		else
		{
			return Path.Combine(baseDirectory, relativePath);
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
			absolutePath = absolutePath.Replace('/', '\\');
			baseDirectory = baseDirectory.Replace('/', '\\');

			if (absolutePath.StartsWith(baseDirectory, StringComparison.OrdinalIgnoreCase))
			{
				return absolutePath[baseDirectory.Length..].TrimStart('\\');
			}
			else
			{
				return absolutePath;
			}
		}
	}
}

[SupportedOSPlatform("windows")]
file static class Native
{
	[DllImport("kernel32.dll")]
	public static extern int GetShortPathName(string path, StringBuilder buffer, int bufferLength);
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	public static extern int GetLongPathName(string path, StringBuilder buffer, int bufferLength);
	[DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern int WNetGetConnection([MarshalAs(UnmanagedType.LPTStr)] string localName, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder remoteName, ref int length);
}