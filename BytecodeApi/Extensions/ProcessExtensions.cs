using BytecodeApi.Interop;
using BytecodeApi.IO;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Text;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Process" /> objects.
/// </summary>
[SupportedOSPlatform("windows")]
public static class ProcessExtensions
{
	extension(Process)
	{
		/// <summary>
		/// Gets a value indicating whether the current <see cref="Process" /> has a console window.
		/// </summary>
		public static bool HasConsole => Native.GetConsoleWindow() != 0;

		/// <summary>
		/// Determines whether a <see cref="Process" /> with the specified process ID is running.
		/// </summary>
		/// <param name="processId">The process ID to check.</param>
		/// <returns>
		/// <see langword="true" />, if the process is running;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsRunning(int processId)
		{
			try
			{
				using Process process = Process.GetProcessById(processId);
				return true;
			}
			catch
			{
				return false;
			}
		}
		/// <summary>
		/// Creates a new <see cref="Process" /> component for each process resource with the SessionId of the current <see cref="Process" /> on the local computer.
		/// </summary>
		/// <returns>
		/// An <see cref="Process" />[] that represents all the process resources with the SessionId of the current <see cref="Process" /> running on the local computer.
		/// </returns>
		public static Process[] GetSessionProcesses()
		{
			return Process
				.GetProcesses()
				.Where(process => process.SessionId == ApplicationBase.Process.SessionId)
				.ToArray();
		}
		/// <summary>
		/// Creates an array of new <see cref="Process" /> components and associates them with all the process resources on the local computer that share the specified process name and the SessionId of the current <see cref="Process" />.
		/// </summary>
		/// <param name="processName">A <see cref="string" /> specifying the friendly name of the process.</param>
		/// <returns>
		/// A <see cref="Process" />[] that represents the process resources running the specified application or file with the SessionId of the current <see cref="Process" />.
		/// </returns>
		public static Process[] GetSessionProcessesByName(string processName)
		{
			Check.ArgumentNull(processName);

			return Process
				.GetProcessesByName(processName)
				.Where(process => process.SessionId == ApplicationBase.Process.SessionId)
				.ToArray();
		}
		/// <summary>
		/// Creates a console window for the current <see cref="Process" />.
		/// </summary>
		/// <param name="alwaysCreateNewConsole"><see langword="true" /> to always create a new console window; <see langword="false" /> to attach to an existing console window, if one already exists.</param>
		/// <param name="setInStream"><see langword="true" /> to set the input stream.</param>
		public static void CreateConsole(bool alwaysCreateNewConsole, bool setInStream)
		{
			bool consoleAttached = true;
			if (alwaysCreateNewConsole || Native.AttachConsole(0xffffffff) == 0 && Marshal.GetLastWin32Error() != 5)
			{
				consoleAttached = Native.AllocConsole() != 0;
			}

			if (consoleAttached)
			{
				StreamWriter streamWriter = new(CreateFileStream("CONOUT$", 0x40000000, FileShare.Write, FileAccess.Write))
				{
					AutoFlush = true
				};

				Console.SetOut(streamWriter);
				Console.SetError(streamWriter);
				Console.OutputEncoding = Encoding.UTF8;
				Console.InputEncoding = Encoding.UTF8;

				if (setInStream)
				{
					Console.SetIn(new StreamReader(CreateFileStream("CONIN$", 0x80000000, FileShare.Read, FileAccess.Read)));
				}
			}

			static FileStream CreateFileStream(string name, uint access, FileShare fileShare, FileAccess fileAccess)
			{
				SafeFileHandle file = Native.CreateFile(name, access, fileShare, 0, FileMode.Open, 0x80, 0);
				return !file.IsInvalid ? new(file, fileAccess) : throw Throw.Win32();
			}
		}
		/// <summary>
		/// Creates a <see cref="Process" /> with the specified commandline and the specified <see cref="ProcessIntegrityLevel" />. If process creation fails, a <see cref="Win32Exception" /> is thrown. This is typically used to create processes with lower integrity.
		/// </summary>
		/// <param name="commandLine">A <see cref="string" /> specifying the commandline to create the <see cref="Process" /> with.</param>
		/// <param name="integrityLevel">The <see cref="ProcessIntegrityLevel" /> to create the <see cref="Process" /> with. This is usually lower than the <see cref="ProcessIntegrityLevel" /> of the current <see cref="Process" />.</param>
		/// <returns>
		/// The <see cref="Process" /> this method creates.
		/// </returns>
		public static Process StartWithIntegrity(string commandLine, ProcessIntegrityLevel integrityLevel)
		{
			Check.ArgumentNull(commandLine);

			nint token = 0;
			nint newToken = 0;
			nint integritySid = 0;

			Native.StartupInfo startupInfo = new()
			{
				StructSize = Marshal.SizeOf<Native.StartupInfo>()
			};
			Native.ProcessInformation processInformation = new();

			try
			{
				using (Process process = Process.GetCurrentProcess())
				{
					token = process.OpenToken(0x8b);
				}

				if (token != 0 && Native.DuplicateTokenEx(token, 0, 0, 2, 1, out newToken))
				{
					Native.SidIdentifierAuthority securityMandatoryLabelAuthority = new() { Value = [0, 0, 0, 0, 0, 16] };
					if (Native.AllocateAndInitializeSid(ref securityMandatoryLabelAuthority, 1, (int)integrityLevel, 0, 0, 0, 0, 0, 0, 0, out integritySid))
					{
						Native.TokenMandatoryLabel mandatoryTokenLabel;
						mandatoryTokenLabel.Label.Attributes = 0x20;
						mandatoryTokenLabel.Label.Sid = integritySid;

						using HGlobal tokenInfoPtr = HGlobal.FromStructure(mandatoryTokenLabel);

						if (Native.SetTokenInformation(newToken, 25, tokenInfoPtr.Handle, tokenInfoPtr.Size + Native.GetLengthSid(integritySid)) &&
							Native.CreateProcessAsUser(newToken, null, commandLine, 0, 0, false, 0, 0, null, ref startupInfo, out processInformation))
						{
							return Process.GetProcessById(processInformation.ProcessId);
						}
					}
				}
			}
			finally
			{
				if (token != 0) Native.CloseHandle(token);
				if (newToken != 0) Native.CloseHandle(newToken);
				if (integritySid != 0) Native.FreeSid(integritySid);
				if (processInformation.Process != 0) Native.CloseHandle(processInformation.Process);
				if (processInformation.Thread != 0) Native.CloseHandle(processInformation.Thread);
			}

			throw Throw.Win32("Process could not be created.");
		}
	}

	extension(Process process)
	{
		/// <summary>
		/// Gets a <see cref="string" /> that represents the user running this <see cref="Process" />. This <see cref="string" /> contains the full Windows logon name, including the machine or domain name.
		/// </summary>
		public string UserName
		{
			get
			{
				Check.ArgumentNull(process);

				using WindowsIdentity windowsIdentity = process.GetUser();
				return windowsIdentity.Name;
			}
		}
		/// <summary>
		/// Gets a <see cref="string" /> that represents the user running this <see cref="Process" />. This <see cref="string" /> contains only the user, excluding machine or domain name.
		/// </summary>
		public string UserNameShort
		{
			get
			{
				Check.ArgumentNull(process);

				return process.UserName.SubstringFromLast('\\');
			}
		}
		/// <summary>
		/// Gets the commandline <see cref="string" /> of this <see cref="Process" /> that was passed during process creation.
		/// </summary>
		public string CommandLine
		{
			get
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
		}
		/// <summary>
		/// Gets the commandline arguments of this <see cref="Process" /> that were passed during process creation.
		/// </summary>
		public string[] CommandLineArgs
		{
			get
			{
				Check.ArgumentNull(process);

				return CommandLine.GetArguments(process.CommandLine);
			}
		}
		/// <summary>
		/// Gets the mandatory integrity level of this <see cref="Process" />.
		/// Usually, this method (specifically, OpenToken) will fail on elevated processes if this method is called with medium IL.
		/// </summary>
		public ProcessIntegrityLevel IntegrityLevel
		{
			get
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
		}
		/// <summary>
		/// Gets a <see cref="bool" /> value indicating whether this <see cref="Process" /> is a 64-bit or a 32-bit process.
		/// </summary>
		public bool Is64Bit
		{
			get
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
		}
		/// <summary>
		/// Gets a <see cref="bool" /> value indicating whether this <see cref="Process" /> is a .NET process.
		/// To identify a .NET process, the presence of either the mscorlib.dll, mscorlib.ni.dll or System.Runtime.dll module is checked.
		/// </summary>
		public bool IsDotNet
		{
			get
			{
				Check.ArgumentNull(process);

				return process.Modules
					.Cast<ProcessModule>()
					.Select(module => Path.GetFileName(module.FileName).ToLower())
					.Any(module => module is "mscorlib.dll" or "mscorlib.ni.dll" or "system.runtime.dll");
			}
		}

		/// <summary>
		/// Gets the <see cref="WindowsIdentity" /> object that represents the user running this <see cref="Process" />.
		/// </summary>
		/// <returns>
		/// The <see cref="WindowsIdentity" /> object that represents the user running this <see cref="Process" />.
		/// </returns>
		public WindowsIdentity GetUser()
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
		/// Gets the parent <see cref="Process" /> of this <see cref="Process" /> or <see langword="null" />, if the process does not have a parent or this method failed.
		/// </summary>
		/// <returns>
		/// The parent <see cref="Process" /> of this <see cref="Process" /> or <see langword="null" />, if the process does not have a parent or this method failed.
		/// </returns>
		public Process? GetParentProcess()
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
		internal nint OpenToken(uint desiredAccess)
		{
			return Native.OpenProcessToken(process.Handle, desiredAccess, out nint token) ? token : throw Throw.Win32();
		}
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
	[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DuplicateTokenEx(nint existingToken, uint desiredAccess, nint tokenAttributes, int impersonationLevel, int tokenType, out nint newToken);
	[DllImport("advapi32.dll", SetLastError = true)]
	public static extern bool GetTokenInformation(nint tokenHandle, int tokenInformationClass, nint tokenInformation, int tokenInformationLength, out int returnLength);
	[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SetTokenInformation(nint token, int tokenInformationClass, nint tokenInfo, int tokenInfoLength);
	[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool CreateProcessAsUser(nint token, string? applicationName, string? commandLine, nint processAttributes, nint threadAttributes, bool inheritHandles, uint creationFlags, nint environment, string? currentDirectory, ref StartupInfo startupInfo, out ProcessInformation processInformation);
	[DllImport("advapi32.dll", SetLastError = true)]
	public static extern nint GetSidSubAuthority(nint sid, int subAuthority);
	[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool AllocateAndInitializeSid(ref SidIdentifierAuthority identifierAuthority, byte subAuthorityCount, int subAuthority0, int subAuthority1, int subAuthority2, int subAuthority3, int subAuthority4, int subAuthority5, int subAuthority6, int subAuthority7, out nint sid);
	[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern int GetLengthSid(nint sid);
	[DllImport("advapi32.dll")]
	public static extern nint FreeSid(nint sid);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern SafeSnapshotHandle CreateToolhelp32Snapshot(uint flags, uint id);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool Process32First(SafeSnapshotHandle snapshot, ref ProcessEntry lppe);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool Process32Next(SafeSnapshotHandle snapshot, ref ProcessEntry lppe);
	[DllImport("ntdll.dll")]
	public static extern uint NtQueryInformationProcess(nint Process, uint processInformationClass, nint processInformation, uint processInformationLength, out uint returnLength);
	[DllImport("kernel32.dll")]
	public static extern nint GetConsoleWindow();
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	public static extern int AllocConsole();
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	public static extern uint AttachConsole(uint processId);
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern SafeFileHandle CreateFile(string name, uint access, FileShare share, nint security, FileMode mode, uint flags, nint template);

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
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct StartupInfo
	{
		public int StructSize;
		public string Reserved1;
		public string Desktop;
		public string Title;
		public int X;
		public int Y;
		public int Width;
		public int Height;
		public int CharacterWidth;
		public int CharacterHeight;
		public int FillAttribute;
		public int Flags;
		public short ShowWindow;
		public short Reserved2;
		public nint Reserved3;
		public nint StdInput;
		public nint StdOutput;
		public nint StdError;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct ProcessInformation
	{
		public nint Process;
		public nint Thread;
		public int ProcessId;
		public int ThreadId;
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
	[StructLayout(LayoutKind.Sequential)]
	public struct SidIdentifierAuthority
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
		public byte[] Value;
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