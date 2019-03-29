using BytecodeApi.Extensions;
using System.Diagnostics;

namespace BytecodeApi.IO
{
	/// <summary>
	/// Specifies the result of a DLL injection attempt, performed by <see cref="ProcessExtensions.LoadLibrary" />.
	/// </summary>
	public enum ProcessLoadLibraryResult
	{
		/// <summary>
		/// The DLL was successfully loaded.
		/// </summary>
		Success,
		/// <summary>
		/// The DLL was not loaded, because a module with the same name was already loaded by the <see cref="Process" />.
		/// </summary>
		AlreadyLoaded,
		/// <summary>
		/// The native method OpenProcess failed.
		/// </summary>
		OpenProcessFailed,
		/// <summary>
		/// The native method VirtualAllocEx failed.
		/// </summary>
		VirtualAllocFailed,
		/// <summary>
		/// The native method WriteProcessMemory failed.
		/// </summary>
		WriteProcessMemoryFailed,
		/// <summary>
		/// The native method CreateRemoteThread failed.
		/// </summary>
		CreateRemoteThreadFailed
	}
}