using BytecodeApi.Extensions;
using BytecodeApi.Wmi;
using Microsoft.Win32;
using System.Runtime.InteropServices;

namespace BytecodeApi.Win32.SystemInfo;

/// <summary>
/// Provides information about installed hardware.
/// </summary>
public static class HardwareInfo
{
	private static string[]? _ProcessorNames;
	private static string[]? _VideoControllerNames;
	private static long? _TotalMemory;
	/// <summary>
	/// Gets the names of all installed processors.
	/// </summary>
	public static string[] ProcessorNames
	{
		get
		{
			if (_ProcessorNames == null)
			{
				List<string> names = [];

				using RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor") ?? throw Throw.Win32();
				foreach (string subKeyName in key.GetSubKeyNames())
				{
					using RegistryKey subKey = key.OpenSubKey(subKeyName) ?? throw Throw.Win32();
					if (subKey.GetStringValue("ProcessorNameString")?.Trim().ToNullIfEmpty() is string name)
					{
						names.Add(name);
					}
				}

				_ProcessorNames = names.ToArray();
			}

			return _ProcessorNames;
		}
	}
	/// <summary>
	/// Gets the names of all installed video controllers.
	/// </summary>
	public static string[] VideoControllerNames => _VideoControllerNames ??= WmiContext.Root
		.GetNamespace("CIMV2")
		.GetClass("Win32_VideoController")
		.Select("Name")
		.ToArray()
		.Select(obj => obj.Properties["Name"].GetValue<string>()?.Trim().ToNullIfEmpty())
		.ExceptNull()
		.ToArray();
	/// <summary>
	/// Gets the total amount of installed physical memory.
	/// </summary>
	public static long TotalMemory
	{
		get
		{
			if (_TotalMemory == null)
			{
				Native.MemoryStatusEx memoryStatus = new();
				_TotalMemory = Native.GlobalMemoryStatusEx(memoryStatus) ? (long)memoryStatus.TotalPhys : throw Throw.Win32();
			}

			return _TotalMemory.Value;
		}
	}
	/// <summary>
	/// Gets the total amount of available physical memory.
	/// </summary>
	public static long AvailableMemory
	{
		get
		{
			Native.MemoryStatusEx memoryStatus = new();
			return Native.GlobalMemoryStatusEx(memoryStatus) ? (long)memoryStatus.AvailPhys : throw Throw.Win32();
		}
	}
}

file static class Native
{
	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern bool GlobalMemoryStatusEx([In, Out] MemoryStatusEx buffer);

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	public sealed class MemoryStatusEx
	{
		public uint Length;
		public uint MemoryLoad;
		public ulong TotalPhys;
		public ulong AvailPhys;
		public ulong TotalPageFile;
		public ulong AvailPageFile;
		public ulong TotalVirtual;
		public ulong AvailVirtual;
		public ulong AvailExtendedVirtual;

		public MemoryStatusEx()
		{
			Length = (uint)Marshal.SizeOf<MemoryStatusEx>();
		}
	}
}