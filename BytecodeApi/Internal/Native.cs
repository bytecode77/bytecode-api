using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security;
using System.Security.Permissions;
using System.Text;
using FileTime = System.Runtime.InteropServices.ComTypes.FILETIME;
using StatStg = System.Runtime.InteropServices.ComTypes.STATSTG;

namespace BytecodeApi
{
	internal static class Native
	{
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr GetModuleHandle(string moduleName);
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CloseHandle(IntPtr obj);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern IntPtr LocalFree(IntPtr mem);
		[DllImport("kernel32.dll")]
		public static extern IntPtr OpenProcess(int desiredAccess, bool inheritHandle, int processId);
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr module, string procName);
		[DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
		public static extern IntPtr VirtualAllocEx(IntPtr process, IntPtr dddress, uint size, uint allocationType, uint protect);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool WriteProcessMemory(IntPtr process, IntPtr baseAddress, byte[] buffer, uint size, out UIntPtr bytesWritten);
		[DllImport("kernel32.dll")]
		public static extern IntPtr CreateRemoteThread(IntPtr process, IntPtr threadAttributes, uint stackSize, IntPtr startAddress, IntPtr parameter, uint creationFlags, IntPtr threadId);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern SafeSnapshotHandle CreateToolhelp32Snapshot(uint flags, uint id);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool Process32First(SafeSnapshotHandle snapshot, ref ProcessEntry lppe);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool Process32Next(SafeSnapshotHandle snapshot, ref ProcessEntry lppe);
		[DllImport("kernel32.dll")]
		public static extern int GetFileType(SafeFileHandle file);
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern SafeFileHandle CreateFile(string name, uint access, FileShare share, IntPtr security, FileMode mode, uint flags, IntPtr template);
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BackupRead(SafeFileHandle file, ref Win32StreamId buffer, int bytesToRead, out int bytesRead, [MarshalAs(UnmanagedType.Bool)] bool abort, [MarshalAs(UnmanagedType.Bool)] bool processSecurity, ref IntPtr context);
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BackupRead(SafeFileHandle file, SafeHGlobalHandle buffer, int bytesToRead, out int bytesRead, [MarshalAs(UnmanagedType.Bool)] bool abort, [MarshalAs(UnmanagedType.Bool)] bool processSecurity, ref IntPtr context);
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool BackupSeek(SafeFileHandle file, int bytesToSeekLow, int bytesToSeekHigh, out int bytesSeekedLow, out int bytesSeekedHigh, ref IntPtr context);
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DeleteFile(string name);
		[DllImport("kernel32.dll")]
		public static extern IntPtr BeginUpdateResource(string fileName, [MarshalAs(UnmanagedType.Bool)] bool deleteExistingResources);
		[DllImport("kernel32.dll")]
		public static extern bool EndUpdateResource(IntPtr update, bool discard);
		[DllImport("kernel32.dll")]
		public static extern int UpdateResource(IntPtr update, uint type, uint name, ushort language, byte[] data, uint dateLength);
		[DllImport("kernel32.dll")]
		public static extern IntPtr LoadLibrary(string path);
		[DllImport("kernel32.dll", SetLastError = true)]
		public static extern bool CreateProcess(string applicationName, string commandLine, IntPtr processAttributes, IntPtr threadAttributes, bool inheritHandles, uint creationFlags, IntPtr environment, string currentDirectory, byte[] startupInfo, int[] processInfo);
		[DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);
		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		public static extern IntPtr LoadLibraryEx(string fileName, IntPtr file, uint flags);
		[DllImport("kernel32.dll", SetLastError = true)]
		[SuppressUnmanagedCodeSecurity]
		public static extern bool FreeLibrary(IntPtr module);
		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		public static extern bool EnumResourceNames(IntPtr module, IntPtr type, EnumerateResourceNames enumFunc, IntPtr param);
		[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		public static extern IntPtr FindResource(IntPtr module, IntPtr name, IntPtr type);
		[DllImport("kernel32.dll", SetLastError = true)]
		[SuppressUnmanagedCodeSecurity]
		public static extern IntPtr LoadResource(IntPtr module, IntPtr resInfo);
		[DllImport("kernel32.dll", SetLastError = true)]
		[SuppressUnmanagedCodeSecurity]
		public static extern IntPtr LockResource(IntPtr resData);
		[DllImport("kernel32.dll", SetLastError = true)]
		[SuppressUnmanagedCodeSecurity]
		public static extern uint SizeofResource(IntPtr module, IntPtr resInfo);
		[DllImport("kernel32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static extern bool FindClose(IntPtr handle);
		[DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern SafeFindHandle FindFirstStreamW(string fileName, int infoLevel, [In, Out, MarshalAs(UnmanagedType.LPStruct)] Win32FindStreamData findStreamData, uint flags);
		[DllImport("kernel32.dll", ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool FindNextStreamW(SafeFindHandle findFile, [In, Out, MarshalAs(UnmanagedType.LPStruct)] Win32FindStreamData findStreamData);
		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool GlobalMemoryStatusEx([In, Out] MemoryStatusEx buffer);
		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr handle);
		[DllImport("user32.dll")]
		public static extern void ReleaseDC(IntPtr handle, IntPtr dc);
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string className, string windowName);
		[DllImport("user32.dll")]
		public static extern IntPtr FindWindowEx(IntPtr parent, IntPtr childAfter, string className, string window);
		[DllImport("user32.dll")]
		public static extern bool GetClientRect(IntPtr handle, out Rect rect);
		[DllImport("user32.dll")]
		public static extern IntPtr SendMessage(IntPtr handle, uint msg, int wParam, int lParam);
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern uint RegisterWindowMessage(string str);
		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern bool SendNotifyMessage(IntPtr handle, uint msg, int wParam, int lParam);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool DestroyIcon(IntPtr icon);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SystemParametersInfo(int action, int param, string paramString, int winIni);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetWindowsHookEx(int hookId, HookProc callback, IntPtr module, int threadId);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool UnhookWindowsHookEx(IntPtr hook);
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr CallNextHookEx(IntPtr hook, int code, IntPtr wParam, IntPtr lParam);
		[DllImport("user32.dll")]
		public static extern short GetKeyState(int virtualKey);
		[DllImport("user32.dll")]
		public static extern int GetKeyboardState(byte[] keyState);
		[DllImport("user32.dll")]
		public static extern int ToAscii(int virtualKey, int scanCode, byte[] keyState, byte[] translatedKey, int state);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int LoadString(IntPtr instance, int id, StringBuilder buffer, int bufferLength);
		[DllImport("gdi32.dll")]
		public static extern bool DeleteObject(IntPtr obj);
		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		public static extern bool ShellExecuteEx(ref ShellExecuteInfo execInfo);
		[DllImport("shell32.dll")]
		public static extern IntPtr SHGetFileInfo(string path, uint fileAttributes, ref SHFileInfo fileInfo, int fileInfoSize, uint flags);
		[DllImport("shell32.dll", EntryPoint = "#62", CharSet = CharSet.Unicode, SetLastError = true)]
		[SuppressUnmanagedCodeSecurity]
		public static extern bool SHPickIconDialog(IntPtr handle, StringBuilder fileName, int fileNameLength, out int iconIndex);
		[DllImport("shell32.dll", SetLastError = true)]
		public static extern IntPtr CommandLineToArgvW([MarshalAs(UnmanagedType.LPWStr)] string commandLine, out int argumentCount);
		[DllImport("shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
		public static extern int ExtractIconEx(string fileName, int index, out IntPtr largeVersion, out IntPtr smallVersion, int iconCount);
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern int NtSetInformationProcess(IntPtr processHandle, int processInformationClass, ref int processInformation, uint processInformationLength);
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern uint NtUnmapViewOfSection(IntPtr process, IntPtr lpBaseAddress);
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern int NtWriteVirtualMemory(IntPtr process, IntPtr baseAddress, IntPtr buffer, uint size, IntPtr bytesWritten);
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern int NtGetContextThread(IntPtr thread, IntPtr context);
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern int NtSetContextThread(IntPtr thread, IntPtr context);
		[DllImport("ntdll.dll", SetLastError = true)]
		public static extern uint NtResumeThread(IntPtr thread, IntPtr suspendCount);
		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
		public static extern int StrCmpLogicalW(string strA, string strB);
		[DllImport("shlwapi.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern uint AssocQueryString(int flags, AssocStr str, string assoc, string extra, [Out] StringBuilder result, [In][Out] ref uint resultLength);
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool OpenProcessToken(IntPtr processHandle, uint desiredAccess, out IntPtr tokenHandle);
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern bool GetTokenInformation(IntPtr tokenHandle, int tokenInformationClass, IntPtr tokenInformation, int tokenInformationLength, out int returnLength);
		[DllImport("advapi32.dll", SetLastError = true)]
		public static extern IntPtr GetSidSubAuthority(IntPtr sid, int subAuthority);
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetLengthSid(IntPtr sid);
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetTokenInformation(IntPtr token, int tokenInformationClass, IntPtr tokenInfo, int tokenInfoLength);
		[DllImport("advapi32.dll")]
		public static extern IntPtr FreeSid(IntPtr sid);
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool DuplicateTokenEx(IntPtr existingToken, uint desiredAccess, IntPtr tokenAttributes, int impersonationLevel, int tokenType, out IntPtr newToken);
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool AllocateAndInitializeSid(ref SidIdentifierAuthority identifierAuthority, byte subAuthorityCount, int subAuthority0, int subAuthority1, int subAuthority2, int subAuthority3, int subAuthority4, int subAuthority5, int subAuthority6, int subAuthority7, out IntPtr sid);
		[DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool CreateProcessAsUser(IntPtr token, string applicationName, string commandLine, IntPtr processAttributes, IntPtr threadAttributes, bool inheritHandles, uint creationFlags, IntPtr environment, string currentDirectory, ref StartupInfo startupInfo, out ProcessInformation processInformation);
		[DllImport("ole32.dll", PreserveSig = false)]
		public static extern ILockBytes CreateILockBytesOnHGlobal(IntPtr handle, bool deleteOnRelease);
		[DllImport("ole32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
		public static extern IStorage StgCreateDocfileOnILockBytes(ILockBytes lockBytes, uint mode, uint reserved = 0);
		[DllImport("mpr.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern int WNetGetConnection([MarshalAs(UnmanagedType.LPTStr)] string localName, [MarshalAs(UnmanagedType.LPTStr)] StringBuilder remoteName, ref int length);
		[DllImport("iphlpapi.dll", SetLastError = true)]
		public static extern uint GetExtendedTcpTable(IntPtr tcpTable, ref int bufferLength, bool sort, int ipVersion, int tableClass, uint reserved = 0);
		[DllImport("iphlpapi.dll", SetLastError = true)]
		public static extern uint GetExtendedUdpTable(IntPtr udpTable, ref int bufferLength, bool sort, int ipVersion, int tableClass, uint reserved = 0);

		[StructLayout(LayoutKind.Sequential)]
		public struct ProcessEntry
		{
			public uint Size;
			public uint Usage;
			public uint ProcessId;
			public IntPtr DefaultHeapId;
			public uint ModuleId;
			public uint ThreadCount;
			public uint ParentProcessId;
			public int PriorityClassBase;
			public uint Flags;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string ExeFile;
		};
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct SHFileInfo
		{
			public IntPtr Icon;
			public int Index;
			public uint Attributes;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string DisplayName;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
			public string TypeName;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct SidIdentifierAuthority
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6, ArraySubType = UnmanagedType.I1)]
			public byte[] Value;
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
			public IntPtr Reserved3;
			public IntPtr StdInput;
			public IntPtr StdOutput;
			public IntPtr StdError;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct ProcessInformation
		{
			public IntPtr Process;
			public IntPtr Thread;
			public int ProcessId;
			public int ThreadId;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct Rect
		{
			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct IconDir
		{
			public ushort Reserved;
			public ushort Type;
			public ushort Count;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct IconDirEntry
		{
			public byte Width;
			public byte Height;
			public byte ColorCount;
			public byte Reserved;
			public ushort Planes;
			public ushort BitCount;
			public uint BytesInRes;
			public uint ImageOffset;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct BitmapInfoHeader
		{
			public uint Size;
			public int Width;
			public int Height;
			public ushort Planes;
			public ushort BitCount;
			public uint Compression;
			public uint SizeImage;
			public int PixelsPerMeterX;
			public int PixelsPerMeterY;
			public uint ClrUsed;
			public uint ClrImportant;
		}
		[StructLayout(LayoutKind.Sequential, Pack = 2)]
		public struct GroupIconDirEntry
		{
			public byte Width;
			public byte Height;
			public byte ColorCount;
			public byte Reserved;
			public ushort Planes;
			public ushort BitCount;
			public uint BytesInRes;
			public ushort Id;
		}
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct ShellExecuteInfo
		{
			public int StructSize;
			public uint Mask;
			public IntPtr Handle;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string Verb;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string FileName;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string Parameters;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string Directory;
			public int Show;
			public IntPtr Instance;
			public IntPtr ItemIdList;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string ClassName;
			public IntPtr ClassKey;
			public uint HotKey;
			public IntPtr Icon;
			public IntPtr Process;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct Win32StreamId
		{
			public readonly int StreamId;
			public readonly int StreamAttributes;
			public LargeInteger Size;
			public readonly int StreamNameSize;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct LargeInteger
		{
			public readonly int Low;
			public readonly int High;

			public long ToInt64()
			{
				return High << 32 | Low;
			}
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct TokenMandatoryLabel
		{
			public SidAndAttributes Label;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct SidAndAttributes
		{
			public IntPtr Sid;
			public int Attributes;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct KeyboardInput
		{
			public int KeyCode;
			public int ScanCode;
			public int Flags;
			public int TimeStamp;
			public IntPtr AdditionalInformation;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct TcpTable
		{
			public uint EntryCount;
			[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
			public TcpRow[] Table;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct TcpRow
		{
			public uint State;
			public uint LocalAddress;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] LocalPort;
			public uint RemoteAddress;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] RemotePort;
			public uint OwningProcessId;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct Tcp6Table
		{
			public uint EntryCount;
			[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
			public Tcp6Row[] Table;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct Tcp6Row
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] LocalAddress;
			public uint LocalScopeId;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] LocalPort;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] RemoteAddress;
			public uint RemoteScopeId;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] RemotePort;
			public uint State;
			public uint OwningProcessId;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct UdpTable
		{
			public uint EntryCount;
			[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
			public UdpRow[] Table;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct UdpRow
		{
			public uint LocalAddress;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] LocalPort;
			public uint OwningProcessId;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct Udp6Table
		{
			public uint EntryCount;
			[MarshalAs(UnmanagedType.ByValArray, ArraySubType = UnmanagedType.Struct, SizeConst = 1)]
			public Udp6Row[] Table;
		}
		[StructLayout(LayoutKind.Sequential)]
		public struct Udp6Row
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] LocalAddress;
			public uint LocalScopeId;
			public uint LocalPort;
			public uint OwningProcessId;
		}
		public sealed class SafeHGlobalHandle : SafeHandle
		{
			public int Size { get; }
			public override bool IsInvalid => handle == IntPtr.Zero;

			public SafeHGlobalHandle() : base(IntPtr.Zero, true)
			{
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			private SafeHGlobalHandle(IntPtr hadle, int size) : base(IntPtr.Zero, true)
			{
				Size = size;
				SetHandle(hadle);
			}

			public static SafeHGlobalHandle Allocate(int bytes)
			{
				return new SafeHGlobalHandle(Marshal.AllocHGlobal(bytes), bytes);
			}
			protected override bool ReleaseHandle()
			{
				Marshal.FreeHGlobal(handle);
				return true;
			}
		}
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
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			private SafeFindHandle() : base(true)
			{
			}

			protected override bool ReleaseHandle()
			{
				return FindClose(handle);
			}
		}
		[SuppressUnmanagedCodeSecurity, HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
		public sealed class SafeSnapshotHandle : SafeHandleMinusOneIsInvalid
		{
			public SafeSnapshotHandle() : base(true)
			{
			}
			[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
			public SafeSnapshotHandle(IntPtr handle) : base(true)
			{
				SetHandle(handle);
			}

			protected override bool ReleaseHandle()
			{
				return CloseHandle(handle);
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success), DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
			private static extern bool CloseHandle(IntPtr handle);
		}
		[StructLayout(LayoutKind.Sequential)]
		public sealed class Point
		{
			public int X;
			public int Y;
		}
		[StructLayout(LayoutKind.Sequential)]
		public sealed class Size
		{
			public int Width;
			public int Height;
		}
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public sealed class FileGroupDescriptorA
		{
			public uint ItemCount;
			public FileDescriptorA[] Items;
		}
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
		public sealed class FileDescriptorA
		{
			public uint Flags;
			public Guid ClsId;
			public Size Size;
			public Point Point;
			public uint FileAttributes;
			public FileTime CreationTime;
			public FileTime LastAccessTime;
			public FileTime LastWriteTime;
			public uint FileSizeHigh;
			public uint FileSizeLow;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string FileName;
		}
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public sealed class FileGroupDescriptorW
		{
			public uint ItemCount;
			public FileDescriptorW[] Items;
		}
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public sealed class FileDescriptorW
		{
			public uint Flags;
			public Guid ClsId;
			public Size Size;
			public Point Point;
			public uint FileAttributes;
			public FileTime CreationTime;
			public FileTime LastAccessTime;
			public FileTime LastWriteTime;
			public uint FileSizeHigh;
			public uint FileSizeLow;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string FileName;
		}
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public sealed class Win32FindStreamData
		{
			public long StreamSize;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 296)]
			public string StreamName;
		}
		[ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("0000000B-0000-0000-C000-000000000046")]
		public interface IStorage
		{
			[return: MarshalAs(UnmanagedType.Interface)]
			IStream CreateStream([In, MarshalAs(UnmanagedType.BStr)] string name, [In, MarshalAs(UnmanagedType.U4)] int mode, [In, MarshalAs(UnmanagedType.U4)] int reserved1, [In, MarshalAs(UnmanagedType.U4)] int reserved2);
			[return: MarshalAs(UnmanagedType.Interface)]
			IStream OpenStream([In, MarshalAs(UnmanagedType.BStr)] string name, IntPtr reserved1, [In, MarshalAs(UnmanagedType.U4)] int mode, [In, MarshalAs(UnmanagedType.U4)] int reserved2);
			[return: MarshalAs(UnmanagedType.Interface)]
			IStorage CreateStorage([In, MarshalAs(UnmanagedType.BStr)] string name, [In, MarshalAs(UnmanagedType.U4)] int mode, [In, MarshalAs(UnmanagedType.U4)] int reserved1, [In, MarshalAs(UnmanagedType.U4)] int reserved2);
			[return: MarshalAs(UnmanagedType.Interface)]
			IStorage OpenStorage([In, MarshalAs(UnmanagedType.BStr)] string name, IntPtr priority, [In, MarshalAs(UnmanagedType.U4)] int mode, IntPtr exclude, [In, MarshalAs(UnmanagedType.U4)] int reserved);
			void CopyTo(int excludeCiid, [In, MarshalAs(UnmanagedType.LPArray)] Guid[] excludedGuids, IntPtr exclude, [In, MarshalAs(UnmanagedType.Interface)] IStorage dest);
			void MoveElementTo([In, MarshalAs(UnmanagedType.BStr)] string name, [In, MarshalAs(UnmanagedType.Interface)] IStorage dest, [In, MarshalAs(UnmanagedType.BStr)] string newName, [In, MarshalAs(UnmanagedType.U4)] int flags);
			void Commit(int commitFlags);
			void Revert();
			void EnumElements([In, MarshalAs(UnmanagedType.U4)] int reserved1, IntPtr reserved2, [In, MarshalAs(UnmanagedType.U4)] int reserved3, [MarshalAs(UnmanagedType.Interface)] out object value);
			void DestroyElement([In, MarshalAs(UnmanagedType.BStr)] string name);
			void RenameElement([In, MarshalAs(UnmanagedType.BStr)] string oldName, [In, MarshalAs(UnmanagedType.BStr)] string newName);
			void SetElementTimes([In, MarshalAs(UnmanagedType.BStr)] string name, [In] FileTime pcTime, [In] FileTime paTime, [In] FileTime pmTime);
			void SetClass([In] ref Guid clsId);
			void SetStateBits(int stateBits, int mask);
			void Stat([Out] out StatStg statStg, int statFlag);
		}
		[ComImport, Guid("0000000A-0000-0000-C000-000000000046"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		public interface ILockBytes
		{
			void ReadAt([In, MarshalAs(UnmanagedType.U8)] long offset, [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, [In, MarshalAs(UnmanagedType.U4)] int cb, [Out, MarshalAs(UnmanagedType.LPArray)] int[] read);
			void WriteAt([In, MarshalAs(UnmanagedType.U8)] long offset, IntPtr pv, [In, MarshalAs(UnmanagedType.U4)] int cb, [Out, MarshalAs(UnmanagedType.LPArray)] int[] written);
			void Flush();
			void SetSize([In, MarshalAs(UnmanagedType.U8)] long cb);
			void LockRegion([In, MarshalAs(UnmanagedType.U8)] long offset, [In, MarshalAs(UnmanagedType.U8)] long cb, [In, MarshalAs(UnmanagedType.U4)] int lockType);
			void UnlockRegion([In, MarshalAs(UnmanagedType.U8)] long offset, [In, MarshalAs(UnmanagedType.U8)] long cb, [In, MarshalAs(UnmanagedType.U4)] int lockType);
			void Stat([Out] out StatStg statStg, [In, MarshalAs(UnmanagedType.U4)] int statFlag);
		}
		public enum AssocStr
		{
			Command = 1,
			Executable,
			FriendlyDocName,
			FriendlyAppName,
			NoOpen,
			ShellNewValue,
			DDECommand,
			DDEIfExec,
			DDEApplication,
			DDETopic,
			InfoTip,
			QuickTip,
			TileInfo,
			ContentType,
			DefaultIcon,
			ShellExtension,
			DropTarget,
			DelegateExecute,
			SupportedUriProtocols,
			ProgId,
			AppId,
			AppPublisher,
			AppIconReference,
			Max,
		}
		[UnmanagedFunctionPointer(CallingConvention.Winapi, SetLastError = true, CharSet = CharSet.Unicode)]
		[SuppressUnmanagedCodeSecurity]
		public delegate bool EnumerateResourceNames(IntPtr module, IntPtr lpszType, IntPtr lpszName, IntPtr lParam);
		public delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);
	}
}