using BytecodeApi.Extensions;
using BytecodeApi.Interop;
using System.Runtime.InteropServices;

namespace BytecodeApi.Penetration;

/// <summary>
/// Class that handles shellcode (buffers with compiled assembly instructions).
/// </summary>
public static class Shellcode
{
	/// <summary>
	/// Extracts the contents of an executable file's .text section. If the executable contains multiple sections with executable code, the first is returned.
	/// <para>In order for the retrieved shellcode to be executable in-memory, it must be position independent.</para>
	/// </summary>
	/// <param name="file">The executable file to extract the shellcode from.</param>
	/// <returns>
	/// The .text section, excluding padding, of the provided executable file.
	/// </returns>
	public static byte[] ExtractFromExecutable(byte[] file)
	{
		Check.ArgumentNull(file);
		Check.Format(file.Length >= 512 && file[0] == 'M' && file[1] == 'Z', "File is not a valid executable.");

		int ntHeaders = BitConverter.ToInt32(file, 0x3c);
		short numberOfSections = BitConverter.ToInt16(file, ntHeaders + 0x6);
		short sizeOfOptionalHeader = BitConverter.ToInt16(file, ntHeaders + 0x14);

		for (short j = 0; j < numberOfSections; j++)
		{
			byte[] section = file.GetBytes(ntHeaders + 0x18 + sizeOfOptionalHeader + j * 0x28, 0x28);
			uint characteristics = BitConverter.ToUInt32(section, 0x24);

			if ((characteristics & 0x60000020) == 0x60000020) // IMAGE_SCN_CNT_CODE | IMAGE_SCN_MEM_EXECUTE | IMAGE_SCN_MEM_READ
			{
				int pointerToRawData = BitConverter.ToInt32(section, 0x14);
				int virtualSize = BitConverter.ToInt32(section, 0x8);

				return file.GetBytes(pointerToRawData, virtualSize);
			}
		}

		throw Throw.Format("Could not find section with executable code.");
	}
	/// <summary>
	/// Executes position independent shellcode in memory. The shellcode is executed in a new thread and this method returns after this thread terminated.
	/// </summary>
	/// <param name="shellcode">A buffer containing position independent shellcode.</param>
	/// <returns>
	/// The <see cref="int" /> value that the created thread returned.
	/// </returns>
	public static int Execute(byte[] shellcode)
	{
		return Execute(shellcode, 0);
	}
	/// <summary>
	/// Executes position independent shellcode in memory. The shellcode is executed in a new thread and this method returns after this thread terminated.
	/// </summary>
	/// <param name="shellcode">A buffer containing position independent shellcode.</param>
	/// <param name="offset">The offset within the shellcode at which to execute.</param>
	/// <returns>
	/// The <see cref="int" /> value that the created thread returned.
	/// </returns>
	public static int Execute(byte[] shellcode, int offset)
	{
		Check.ArgumentNull(shellcode);
		Check.IndexOutOfRange(offset, shellcode.Length);

		nint buffer = Native.VirtualAlloc(0, shellcode.Length, 0x1000, 0x40);
		Marshal.Copy(shellcode, 0, buffer, shellcode.Length);

		nint thread = Native.CreateThread(0, 0, buffer + offset, 0, 0, out _);
		Native.WaitForSingleObject(thread, 0xffffffff);

		using HGlobal exitCode = new(4);
		Native.GetExitCodeThread(thread, exitCode.Handle);
		return Marshal.ReadInt32(exitCode.Handle);
	}
}

file static class Native
{
	[DllImport("kernel32.dll")]
	public static extern nint VirtualAlloc(nint address, nint size, int allocationType, int protect);
	[DllImport("kernel32.dll")]
	public static extern nint CreateThread(nint threadAttributes, uint stackSize, nint startAddress, nint parameter, uint creationFlags, out uint threadId);
	[DllImport("kernel32.dll")]
	public static extern uint WaitForSingleObject(nint handle, uint milliseconds);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool GetExitCodeThread(nint thread, nint exitCode);
}