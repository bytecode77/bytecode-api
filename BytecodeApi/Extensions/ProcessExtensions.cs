using BytecodeApi.Interop;
using BytecodeApi.IO;
using Microsoft.Win32.SafeHandles;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Process" /> objects.
/// </summary>
[SupportedOSPlatform("windows")]
public static class ProcessExtensions
{
	/// <summary>
	/// Gets the <see cref="WindowsIdentity" /> object that represents the user running this <see cref="Process" />.
	/// </summary>
	/// <param name="process">The <see cref="Process" /> to be checked.</param>
	/// <returns>
	/// The <see cref="WindowsIdentity" /> object that represents the user running this <see cref="Process" />.
	/// </returns>
	public static WindowsIdentity GetUser(this Process process)
	{
		Check.ArgumentNull(process);

		nint token = 0;
		try
		{
			token = process.OpenToken(8);
			return new(token);
		}
		finally
		{
			if (token != 0) Native.CloseHandle(token);
		}
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents the user running this <see cref="Process" />. This <see cref="string" /> contains the full Windows logon name, including the machine or domain name.
	/// </summary>
	/// <param name="process">The <see cref="Process" /> to be checked.</param>
	/// <returns>
	/// A <see cref="string" /> that represents the user running this <see cref="Process" />.
	/// </returns>
	public static string GetUserName(this Process process)
	{
		Check.ArgumentNull(process);

		using WindowsIdentity windowsIdentity = process.GetUser();
		return windowsIdentity.Name;
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents the user running this <see cref="Process" />. This <see cref="string" /> contains only the user, excluding machine or domain name.
	/// </summary>
	/// <param name="process">The <see cref="Process" /> to be checked.</param>
	/// <returns>
	/// A <see cref="string" /> that represents the user running this <see cref="Process" />.
	/// </returns>
	public static string GetUserNameShort(this Process process)
	{
		return process.GetUserName().SubstringFromLast('\\');
	}
	/// <summary>
	/// Gets the parent <see cref="Process" /> of this <see cref="Process" /> or <see langword="null" />, if the process does not have a parent or this method failed.
	/// </summary>
	/// <param name="process">The <see cref="Process" /> to be checked.</param>
	/// <returns>
	/// The parent <see cref="Process" /> of this <see cref="Process" /> or <see langword="null" />, if the process does not have a parent or this method failed.
	/// </returns>
	public static Process? GetParentProcess(this Process process)
	{
		Check.ArgumentNull(process);

		try
		{
			Native.ProcessEntry processEntry = new()
			{
				Size = (uint)Marshal.SizeOf<Native.ProcessEntry>()
			};

			using Native.SafeSnapshotHandle snapshot = Native.CreateToolhelp32Snapshot(2, (uint)process.Id);
			int lastError = Marshal.GetLastWin32Error();

			if (snapshot.IsInvalid || !Native.Process32First(snapshot, ref processEntry) && lastError == 18)
			{
				return null;
			}
			else
			{
				do
				{
					if (processEntry.ProcessId == (uint)process.Id)
					{
						return Process.GetProcessById((int)processEntry.ParentProcessId);
					}
				}
				while (Native.Process32Next(snapshot, ref processEntry));
			}

			return null;
		}
		catch
		{
			return null;
		}
	}
	/// <summary>
	/// Gets the commandline <see cref="string" /> of this <see cref="Process" /> that was passed during process creation.
	/// </summary>
	/// <param name="process">The <see cref="Process" /> to be checked.</param>
	/// <returns>
	/// The commandline <see cref="string" /> of this <see cref="Process" /> that was passed during process creation.
	/// </returns>
	public static string GetCommandLine(this Process process)
	{
		Check.ArgumentNull(process);

		nint processHandle = Native.OpenProcess(0x410, false, process.Id);

		if (processHandle != 0)
		{
			try
			{
				using HGlobal basicInformationPtr = new(Marshal.SizeOf<Native.ProcessBasicInformation>());

				if (Native.NtQueryInformationProcess(processHandle, 0, basicInformationPtr.Handle, (uint)basicInformationPtr.Size, out _) == 0)
				{
					Native.ProcessBasicInformation basicInformation = basicInformationPtr.ToStructure<Native.ProcessBasicInformation>();

					if (basicInformation.PebBaseAddress != 0 &&
						ReadStruct(basicInformation.PebBaseAddress, out Native.PebWithProcessParameters peb) &&
						ReadStruct(peb.ProcessParameters, out Native.RtlUserProcessParameters parameters))
					{
						using HGlobal commandLinePtr = new(parameters.CommandLine.MaximumLength);
						if (Native.ReadProcessMemory(processHandle, parameters.CommandLine.Buffer, commandLinePtr.Handle, (uint)commandLinePtr.Size, out _))
						{
							return commandLinePtr.ToStringUnicode() ?? throw Throw.Win32();
						}
					}
				}
			}
			finally
			{
				if (processHandle != 0) Native.CloseHandle(processHandle);
			}
		}

		throw Throw.Win32();

		bool ReadStruct<TStruct>(nint baseAddress, out TStruct result) where TStruct : struct
		{
			using HGlobal buffer = new(Marshal.SizeOf<TStruct>());
			if (Native.ReadProcessMemory(processHandle, baseAddress, buffer.Handle, (uint)buffer.Size, out uint length) && length == buffer.Size)
			{
				result = buffer.ToStructure<TStruct>();
				return true;
			}

			result = default;
			return false;
		}
	}
	/// <summary>
	/// Gets the commandline arguments of this <see cref="Process" /> that were passed during process creation.
	/// </summary>
	/// <param name="process">The <see cref="Process" /> to be checked.</param>
	/// <returns>
	/// A <see cref="string" />[] with the commandline arguments of this <see cref="Process" /> that were passed during process creation.
	/// </returns>
	public static string[] GetCommandLineArgs(this Process process)
	{
		Check.ArgumentNull(process);

		return CommandLine.GetArguments(process.GetCommandLine());
	}
	/// <summary>
	/// Gets the mandatory integrity level of this <see cref="Process" />.
	/// Usually, this method (specifically, OpenToken) will fail on elevated processes if this method is called with medium IL.
	/// </summary>
	/// <param name="process">The <see cref="Process" /> to be checked.</param>
	/// <returns>
	/// The <see cref="ProcessIntegrityLevel" /> of this <see cref="Process" />.
	/// </returns>
	public static ProcessIntegrityLevel GetIntegrityLevel(this Process process)
	{
		Check.ArgumentNull(process);

		nint token = 0;

		try
		{
			token = process.OpenToken(8);
			Native.GetTokenInformation(token, 25, 0, 0, out int integrityLevelTokenSize);

			using HGlobal integrityLevelToken = new(integrityLevelTokenSize);
			if (!Native.GetTokenInformation(token, 25, integrityLevelToken.Handle, integrityLevelTokenSize, out integrityLevelTokenSize))
			{
				throw Throw.Win32();
			}

			return (ProcessIntegrityLevel)Marshal.ReadInt32(Native.GetSidSubAuthority(integrityLevelToken.ToStructure<Native.TokenMandatoryLabel>().Label.Sid, 0));
		}
		finally
		{
			if (token != 0) Native.CloseHandle(token);
		}
	}
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether this <see cref="Process" /> is a 64-bit or a 32-bit process.
	/// </summary>
	/// <param name="process">The <see cref="Process" /> to be checked.</param>
	/// <returns>
	/// <see langword="true" />, if this <see cref="Process" /> is a 64-bit process;
	/// <see langword="false" />, if this <see cref="Process" /> is a 32-bit process;
	/// </returns>
	public static bool Is64Bit(this Process process)
	{
		Check.ArgumentNull(process);

		if (Environment.Is64BitOperatingSystem)
		{
			return Native.IsWow64Process(process.Handle, out bool result) ? !result : throw Throw.Win32();
		}
		else
		{
			return false;
		}
	}
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether this <see cref="Process" /> is a .NET process.
	/// To identify a .NET process, the presence of either the mscorlib.dll, mscorlib.ni.dll or System.Runtime.dll module is checked.
	/// </summary>
	/// <param name="process">The <see cref="Process" /> to be checked.</param>
	/// <returns>
	/// <see langword="true" />, if this <see cref="Process" /> is a .NET process;
	/// <see langword="false" />, if this <see cref="Process" /> is not a .NET process;
	/// </returns>
	public static bool IsDotNet(this Process process)
	{
		Check.ArgumentNull(process);

		return process.Modules
			.Cast<ProcessModule>()
			.Select(module => Path.GetFileName(module.FileName).ToLower())
			.Any(module => module is "mscorlib.dll" or "mscorlib.ni.dll" or "system.runtime.dll");
	}
	internal static nint OpenToken(this Process process, uint desiredAccess)
	{
		return Native.OpenProcessToken(process.Handle, desiredAccess, out nint token) ? token : throw Throw.Win32();
	}
}

[SupportedOSPlatform("windows")]
file static class Native
{
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool CloseHandle(nint obj);
	[DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsWow64Process([In] nint process, [Out] out bool wow64Process);
	[DllImport("kernel32.dll")]
	public static extern nint OpenProcess(int desiredAccess, bool inheritHandle, int processId);
	[DllImport("kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool ReadProcessMemory(nint process, nint baseAddress, nint buffer, uint size, out uint numberOfBytesRead);
	[DllImport("advapi32.dll", SetLastError = true)]
	public static extern bool OpenProcessToken(nint processHandle, uint desiredAccess, out nint tokenHandle);
	[DllImport("advapi32.dll", SetLastError = true)]
	public static extern bool GetTokenInformation(nint tokenHandle, int tokenInformationClass, nint tokenInformation, int tokenInformationLength, out int returnLength);
	[DllImport("advapi32.dll", SetLastError = true)]
	public static extern nint GetSidSubAuthority(nint sid, int subAuthority);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern SafeSnapshotHandle CreateToolhelp32Snapshot(uint flags, uint id);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool Process32First(SafeSnapshotHandle snapshot, ref ProcessEntry lppe);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool Process32Next(SafeSnapshotHandle snapshot, ref ProcessEntry lppe);
	[DllImport("ntdll.dll")]
	public static extern uint NtQueryInformationProcess(nint Process, uint processInformationClass, nint processInformation, uint processInformationLength, out uint returnLength);

	[StructLayout(LayoutKind.Sequential)]
	public struct ProcessEntry
	{
		public uint Size;
		public uint Usage;
		public uint ProcessId;
		public nint DefaultHeapId;
		public uint ModuleId;
		public uint ThreadCount;
		public uint ParentProcessId;
		public int PriorityClassBase;
		public uint Flags;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string ExeFile;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct UnicodeString
	{
		public ushort Length;
		public ushort MaximumLength;
		public nint Buffer;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct ProcessBasicInformation
	{
		public nint Reserved1;
		public nint PebBaseAddress;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public nint[] Reserved2;
		public nint UniqueProcessId;
		public nint Reserved3;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct PebWithProcessParameters
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public nint[] Reserved;
		public nint ProcessParameters;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct RtlUserProcessParameters
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public byte[] Reserved1;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
		public nint[] Reserved2;
		public UnicodeString ImagePathName;
		public UnicodeString CommandLine;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct TokenMandatoryLabel
	{
		public SidAndAttributes Label;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct SidAndAttributes
	{
		public nint Sid;
		public int Attributes;
	}

	public sealed class SafeSnapshotHandle : SafeHandleMinusOneIsInvalid
	{
		public SafeSnapshotHandle() : base(true)
		{
		}
		public SafeSnapshotHandle(nint handle) : base(true)
		{
			SetHandle(handle);
		}

		protected override bool ReleaseHandle()
		{
			return CloseHandle(handle);
		}
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
		private static extern bool CloseHandle(nint handle);
	}
}