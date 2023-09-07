using BytecodeApi.Extensions;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BytecodeApi.Penetration;

/// <summary>
/// Class that injects DLL files into running processes.
/// </summary>
public static class DllInjection
{
	/// <summary>
	/// Injects a DLL into a <see cref="Process" /> using the WriteProcessMemory / CreateRemoteThread technique.
	/// </summary>
	/// <param name="process">The <see cref="Process" /> to be injected.</param>
	/// <param name="dllName">A <see cref="string" /> specifying the path of the DLL file to inject into the specified <see cref="Process" />.</param>
	/// <returns>
	/// <see langword="true" />, if DLL injection succeeded;
	/// <see langword="false" />, if a module with the name is already loaded;
	/// otherwise, an exception is thrown.
	/// </returns>
	public static bool Inject(Process process, string dllName)
	{
		Check.ArgumentNull(process);
		Check.ArgumentNull(dllName);
		Check.FileNotFound(dllName);

		if (CSharp.Try(() => process.Modules.Cast<ProcessModule>().Any(module => module.FileName.Equals(dllName, StringComparison.OrdinalIgnoreCase))))
		{
			return false;
		}
		else
		{
			nint processHandle = Native.OpenProcess(1082, false, process.Id);
			if (processHandle == 0)
			{
				throw Throw.Win32("OpenProcess failed.");
			}

			nint loadLibraryAddress = Native.GetProcAddress(Native.GetModuleHandle("kernel32.dll"), "LoadLibraryW");
			if (loadLibraryAddress == 0)
			{
				throw Throw.Win32("Address of LoadLibraryW could not be determined.");
			}

			nint allocatedMemoryAddress = Native.VirtualAllocEx(processHandle, 0, (uint)(dllName.Length + 1) * 2, 0x3000, 4);
			if (allocatedMemoryAddress == 0)
			{
				throw Throw.Win32("VirtualAllocEx failed.");
			}

			if (!Native.WriteProcessMemory(processHandle, allocatedMemoryAddress, dllName.ToUnicodeBytes(), (uint)dllName.Length * 2 + 1, out _))
			{
				throw Throw.Win32("WriteProcessMemory failed.");
			}

			if (Native.CreateRemoteThread(processHandle, 0, 0, loadLibraryAddress, allocatedMemoryAddress, 0, 0) == 0)
			{
				throw Throw.Win32("CreateRemoteThread failed.");
			}

			return true;
		}
	}
}

file static class Native
{
	[DllImport("kernel32.dll")]
	public static extern nint OpenProcess(int desiredAccess, bool inheritHandle, int processId);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern nint GetModuleHandle(string moduleName);
	[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	public static extern nint GetProcAddress(nint module, string procName);
	[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
	public static extern nint VirtualAllocEx(nint process, nint address, uint size, uint allocationType, uint protect);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool WriteProcessMemory(nint process, nint baseAddress, byte[] buffer, uint size, out nuint bytesWritten);
	[DllImport("kernel32.dll")]
	public static extern nint CreateRemoteThread(nint process, nint threadAttributes, uint stackSize, nint startAddress, nint parameter, uint creationFlags, nint threadId);
}