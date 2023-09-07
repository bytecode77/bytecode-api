using BytecodeApi.Extensions;
using BytecodeApi.Interop;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace BytecodeApi.IO;

/// <summary>
/// Provides <see langword="static" /> methods that extend the <see cref="Process" /> class.
/// </summary>
[SupportedOSPlatform("windows")]
public static class ProcessEx
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
				Native.SidIdentifierAuthority securityMandatoryLabelAuthority = new() { Value = new byte[] { 0, 0, 0, 0, 0, 16 } };
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

[SupportedOSPlatform("windows")]
file static class Native
{
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool CloseHandle(nint obj);
	[DllImport("kernel32.dll")]
	public static extern nint GetConsoleWindow();
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	public static extern int AllocConsole();
	[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
	public static extern uint AttachConsole(uint processId);
	[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
	public static extern SafeFileHandle CreateFile(string name, uint access, FileShare share, nint security, FileMode mode, uint flags, nint template);
	[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool DuplicateTokenEx(nint existingToken, uint desiredAccess, nint tokenAttributes, int impersonationLevel, int tokenType, out nint newToken);
	[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool AllocateAndInitializeSid(ref SidIdentifierAuthority identifierAuthority, byte subAuthorityCount, int subAuthority0, int subAuthority1, int subAuthority2, int subAuthority3, int subAuthority4, int subAuthority5, int subAuthority6, int subAuthority7, out nint sid);
	[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SetTokenInformation(nint token, int tokenInformationClass, nint tokenInfo, int tokenInfoLength);
	[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern int GetLengthSid(nint sid);
	[DllImport("advapi32.dll")]
	public static extern nint FreeSid(nint sid);
	[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool CreateProcessAsUser(nint token, string? applicationName, string? commandLine, nint processAttributes, nint threadAttributes, bool inheritHandles, uint creationFlags, nint environment, string? currentDirectory, ref StartupInfo startupInfo, out ProcessInformation processInformation);

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
}