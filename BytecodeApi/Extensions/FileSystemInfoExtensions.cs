using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Runtime.InteropServices;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="FileSystemInfo" /> objects.
	/// </summary>
	public static class FileSystemInfoExtensions
	{
		/// <summary>
		/// Shows the properties dialog for this file or directory. The dialog closes, when this process exits.
		/// </summary>
		/// <param name="fileOrDirectory">The <see cref="FileSystemInfo" /> representing a file or directory to show the properties dialog from.</param>
		public static void ShowPropertiesDialog(this FileSystemInfo fileOrDirectory)
		{
			Check.ArgumentNull(fileOrDirectory, nameof(fileOrDirectory));
			Check.FileNotFound(fileOrDirectory.FullName);

			Native.ShellExecuteInfo info = new Native.ShellExecuteInfo();
			info.StructSize = Marshal.SizeOf(info);
			info.Verb = "properties";
			info.FileName = fileOrDirectory.FullName;
			info.Show = 5;
			info.Mask = 0x50c;
			if (!Native.ShellExecuteEx(ref info)) throw Throw.Win32("Could not open properties dialog.");
		}
		/// <summary>
		/// Sends this file or directory to recycle bin.
		/// </summary>
		/// <param name="fileOrDirectory">The <see cref="DirectoryInfo" /> to process.</param>
		public static void SendToRecycleBin(this FileSystemInfo fileOrDirectory)
		{
			if (fileOrDirectory is DirectoryInfo)
			{
				Check.ArgumentNull(fileOrDirectory, nameof(fileOrDirectory));
				Check.DirectoryNotFound(fileOrDirectory.FullName);

				FileSystem.DeleteDirectory(fileOrDirectory.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
			}
			else if (fileOrDirectory is FileInfo)
			{
				Check.ArgumentNull(fileOrDirectory, nameof(fileOrDirectory));
				Check.FileNotFound(fileOrDirectory.FullName);

				FileSystem.DeleteFile(fileOrDirectory.FullName, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
			}
			else
			{
				throw Throw.UnsupportedType(nameof(fileOrDirectory));
			}
		}
	}
}