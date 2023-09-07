using Microsoft.VisualBasic.FileIO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="FileSystemInfo" /> objects.
/// </summary>
public static class FileSystemInfoExtensions
{
	/// <summary>
	/// Shows the properties dialog for this file or directory. The dialog closes, when this process exits.
	/// </summary>
	/// <param name="fileOrDirectory">The <see cref="FileSystemInfo" /> representing a file or directory to show the properties dialog from.</param>
	[SupportedOSPlatform("windows")]
	public static void ShowPropertiesDialog(this FileSystemInfo fileOrDirectory)
	{
		Check.ArgumentNull(fileOrDirectory);
		Check.FileOrDirectoryNotFound(fileOrDirectory.FullName);

		Native.ShellExecuteInfo info = new()
		{
			StructSize = Marshal.SizeOf<Native.ShellExecuteInfo>(),
			Verb = "properties",
			FileName = fileOrDirectory.FullName,
			Show = 5,
			Mask = 0x50c
		};

		if (!Native.ShellExecuteEx(ref info))
		{
			throw Throw.Win32("Could not open properties dialog.");
		}
	}
	/// <summary>
	/// Sends this file or directory to recycle bin.
	/// </summary>
	/// <param name="fileOrDirectory">The <see cref="DirectoryInfo" /> to process.</param>
	[SupportedOSPlatform("windows")]
	public static void SendToRecycleBin(this FileSystemInfo fileOrDirectory)
	{
		Check.ArgumentNull(fileOrDirectory);
		Check.FileOrDirectoryNotFound(fileOrDirectory.FullName);

		if (fileOrDirectory is DirectoryInfo && fileOrDirectory.Exists)
		{
			FileSystem.DeleteDirectory(fileOrDirectory.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
		}
		else if (fileOrDirectory is FileInfo && fileOrDirectory.Exists)
		{
			FileSystem.DeleteFile(fileOrDirectory.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
		}
		else
		{
			throw Throw.UnsupportedType(nameof(fileOrDirectory));
		}
	}
}

[SupportedOSPlatform("windows")]
file static class Native
{
	[DllImport("shell32.dll", CharSet = CharSet.Auto)]
	public static extern bool ShellExecuteEx(ref ShellExecuteInfo execInfo);

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public struct ShellExecuteInfo
	{
		public int StructSize;
		public uint Mask;
		public nint Handle;
		[MarshalAs(UnmanagedType.LPTStr)]
		public string Verb;
		[MarshalAs(UnmanagedType.LPTStr)]
		public string FileName;
		[MarshalAs(UnmanagedType.LPTStr)]
		public string Parameters;
		[MarshalAs(UnmanagedType.LPTStr)]
		public string Directory;
		public int Show;
		public nint Instance;
		public nint ItemIdList;
		[MarshalAs(UnmanagedType.LPTStr)]
		public string ClassName;
		public nint ClassKey;
		public uint HotKey;
		public nint Icon;
		public nint Process;
	}
}