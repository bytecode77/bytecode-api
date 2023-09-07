using BytecodeApi.Extensions;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace BytecodeApi.IO;

/// <summary>
/// Provides <see langword="static" /> methods that extend the <see cref="Directory" /> class.
/// </summary>
public static class DirectoryEx
{
	/// <summary>
	/// Retrieves the number of free bytes, available on the specified path.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a directory. This can also be a UNC path.</param>
	/// <returns>
	/// The number of free bytes, available on the specified path.
	/// </returns>
	[SupportedOSPlatform("windows")]
	public static long GetFreeDiskSpace(string path)
	{
		Check.ArgumentNull(path);

		if (Native.GetDiskFreeSpaceEx(path, out long free, out _, out _))
		{
			return free;
		}
		else
		{
			throw Throw.Win32("Error retrieving disk space information.");
		}
	}
	/// <summary>
	/// Retrieves the number of total bytes on the specified path.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a directory. This can also be a UNC path.</param>
	/// <returns>
	/// The number of total bytes on the specified path.
	/// </returns>
	[SupportedOSPlatform("windows")]
	public static long GetTotalDiskSpace(string path)
	{
		Check.ArgumentNull(path);

		if (Native.GetDiskFreeSpaceEx(path, out _, out long total, out _))
		{
			return total;
		}
		else
		{
			throw Throw.Win32("Error retrieving disk space information.");
		}
	}
	/// <summary>
	/// Computes the size of this directory including all of its subdirectories.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a directory.</param>
	/// <returns>
	/// The total size in bytes of this directory.
	/// </returns>
	public static long ComputeDirectorySize(string path)
	{
		Check.ArgumentNull(path);
		Check.DirectoryNotFound(path);

		return new DirectoryInfo(path).ComputeDirectorySize();
	}
	/// <summary>
	/// Compares two directories with an <see cref="CompareDirectoryOptions" /> parameter specifying the properties to compare.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a directory.</param>
	/// <param name="otherPath">A <see cref="string" /> representing the path to the other directory to compare.</param>
	/// <param name="options">The <see cref="CompareDirectoryOptions" /> flags specifying what properties to compare.</param>
	/// <returns>
	/// A value indicating whether the directories are equal.
	/// </returns>
	public static bool Compare(string path, string otherPath, CompareDirectoryOptions options)
	{
		Check.ArgumentNull(path);
		Check.DirectoryNotFound(path);
		Check.ArgumentNull(path);
		Check.DirectoryNotFound(otherPath);

		return new DirectoryInfo(path).Compare(new(otherPath), options);
	}
	/// <summary>
	/// Copies this directory to a new location including all files and subdirectories recursively.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a directory.</param>
	/// <param name="destDirectoryName">The path of the new location including the top directory name.</param>
	public static void CopyTo(string path, string destDirectoryName)
	{
		CopyTo(path, destDirectoryName, false);
	}
	/// <summary>
	/// Copies this directory to a new location including all files and subdirectories recursively.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a directory.</param>
	/// <param name="destDirectoryName">The path of the new location including the top directory name.</param>
	/// <param name="overwrite"><see langword="true" /> to overwrite contents. Existing files in the destination directory that do not exist in the source directory are not deleted.</param>
	public static void CopyTo(string path, string destDirectoryName, bool overwrite)
	{
		Check.ArgumentNull(path);
		Check.DirectoryNotFound(path);
		Check.ArgumentNull(destDirectoryName);

		new DirectoryInfo(path).CopyTo(destDirectoryName, overwrite);
	}
	/// <summary>
	/// Deletes all files and directories from this directory, if it exists.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a directory.</param>
	public static void DeleteContents(string path)
	{
		DeleteContents(path, false);
	}
	/// <summary>
	/// Deletes all files and directories from this directory, if it exists. If <paramref name="create" /> is set to <see langword="true" />, an empty directory will be created.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a directory.</param>
	/// <param name="create"><see langword="true" /> to create the directory, if it does not exist.</param>
	public static void DeleteContents(string path, bool create)
	{
		Check.ArgumentNull(path);

		new DirectoryInfo(path).DeleteContents(create);
	}
	/// <summary>
	/// Sends this directory and all of its contents to recycle bin.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a directory.</param>
	[SupportedOSPlatform("windows")]
	public static void SendToRecycleBin(string path)
	{
		Check.ArgumentNull(path);
		Check.DirectoryNotFound(path);

		new DirectoryInfo(path).SendToRecycleBin();
	}
	/// <summary>
	/// Shows the properties dialog for the directory.
	/// </summary>
	/// <param name="path">A <see cref="string" /> representing the path to a directory.</param>
	[SupportedOSPlatform("windows")]
	public static void ShowPropertiesDialog(string path)
	{
		Check.ArgumentNull(path);
		Check.DirectoryNotFound(path);

		new DirectoryInfo(path).ShowPropertiesDialog();
	}
}

[SupportedOSPlatform("windows")]
file static class Native
{
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool GetDiskFreeSpaceEx(string path, out long free, out long totalFree, out long total);
}