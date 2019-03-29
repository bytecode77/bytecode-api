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
		/// Shows the properties dialog for this file or directory.
		/// </summary>
		/// <param name="file">The <see cref="FileSystemInfo" /> representing a file or directory to show the properties dialog from.</param>
		public static void ShowPropertiesDialog(this FileSystemInfo file)
		{
			Check.ArgumentNull(file, nameof(file));
			Check.FileNotFound(file.FullName);

			//TODO: Bug: Window closes when this process exited, even with SEE_MASK_NOASYNC
			Native.ShellExecuteInfo info = new Native.ShellExecuteInfo();
			info.StructSize = Marshal.SizeOf(info);
			info.Verb = "properties";
			info.FileName = file.FullName;
			info.Show = 5;
			info.Mask = 0x50c;
			if (!Native.ShellExecuteEx(ref info)) throw Throw.Win32("Could not open properties dialog.");
		}
	}
}