using BytecodeApi.Extensions;
using BytecodeApi.Interop;
using BytecodeApi.IO;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Text;

namespace BytecodeApi;

/// <summary>
/// Provides <see langword="static" /> methods to manage an application.
/// </summary>
public static class ApplicationBase
{
	private static string? _FileName;
	private static Version? _Version;
	private static bool? _DebugMode;
	/// <summary>
	/// Gets the path for the executable file that started the application, not including the executable name.
	/// </summary>
	public static string Path => AppDomain.CurrentDomain.BaseDirectory;
	/// <summary>
	/// Gets the path for the executable file that started the application, including the executable name.
	/// </summary>
	[SupportedOSPlatform("windows")]
	public static string FileName
	{
		get
		{
			if (_FileName == null)
			{
				StringBuilder fileName = new(260);
				_FileName = Native.GetModuleFileName(0, fileName, fileName.Capacity) != 0 ? fileName.ToString() : throw Throw.Win32();
			}

			return _FileName;
		}
	}
	/// <summary>
	/// Gets the <see cref="System.Version" /> of the entry assembly.
	/// </summary>
	public static Version Version => _Version ??= Assembly.GetEntryAssembly()?.GetName().Version ?? throw Throw.Win32();
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether <see cref="Debugger.IsAttached" /> was <see langword="true" /> the first time this property is retrieved, or if this executable is located in a directory named like "\bin\Debug", "\bin\x86\Debug", or "\bin\x64\Debug".
	/// </summary>
	public static bool DebugMode => _DebugMode ??= Debugger.IsAttached || new[] { @"\bin\Debug", @"\bin\x86\Debug", @"\bin\x64\Debug", @"\$Build" }.Any(path => Path.Replace('/', '\\').Contains($@"{path}\", StringComparison.OrdinalIgnoreCase) || Path.Replace('/', '\\').EndsWith(path, StringComparison.OrdinalIgnoreCase));

	/// <summary>
	/// Restarts the current <see cref="System.Diagnostics.Process" /> with elevated privileges. Returns <see langword="null" />, if the process is already elevated; <see langword="false" />, if elevation failed; <see langword="true" /> if the restart was successful.
	/// </summary>
	/// <param name="commandLine">A <see cref="string" /> specifying the commandline for the new <see cref="System.Diagnostics.Process" />.</param>
	/// <param name="shutdownCallback">A callback that is invoked after the new <see cref="System.Diagnostics.Process" /> was successfully started with elevated privileges. Depending on application type, this is typically <see cref="Environment.Exit(int)" /> or Application.Shutdown() in a GUI app.</param>
	/// <returns>
	/// <see langword="null" />, if the process is already elevated;
	/// <see langword="false" />, if elevation failed;
	/// <see langword="true" />, if the restart was successful.
	/// </returns>
	[SupportedOSPlatform("windows")]
	public static bool? RestartElevated(string? commandLine, Action? shutdownCallback)
	{
		if (Process.IsElevated)
		{
			return null;
		}
		else
		{
			try
			{
				using System.Diagnostics.Process? process = System.Diagnostics.Process.Start(new ProcessStartInfo(FileName, commandLine ?? "")
				{
					UseShellExecute = true,
					Verb = "runas"
				});

				shutdownCallback?.Invoke();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}

	/// <summary>
	/// Provides information about the current process.
	/// </summary>
	public static class Process
	{
		private static int? _Id;
		private static int? _SessionId;
		private static ProcessIntegrityLevel? _IntegrityLevel;
		private static bool? _IsElevated;
		private static ElevationType? _ElevationType;
		private static Version? _FrameworkVersion;
		/// <summary>
		/// Gets the ProcessID of the current <see cref="System.Diagnostics.Process" />.
		/// </summary>
		public static int Id
		{
			get
			{
				if (_Id == null)
				{
					using System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
					_Id = process.Id;
				}

				return _Id.Value;
			}
		}
		/// <summary>
		/// Gets the SessionID of the current <see cref="System.Diagnostics.Process" />.
		/// </summary>
		public static int SessionId
		{
			get
			{
				if (_SessionId == null)
				{
					using System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
					_SessionId = process.SessionId;
				}

				return _SessionId.Value;
			}
		}
		/// <summary>
		/// Gets the mandatory integrity level for the current <see cref="System.Diagnostics.Process" />.
		/// </summary>
		[SupportedOSPlatform("windows")]
		public static ProcessIntegrityLevel IntegrityLevel
		{
			get
			{
				if (_IntegrityLevel == null)
				{
					using System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
					_IntegrityLevel = process.GetIntegrityLevel();
				}

				return _IntegrityLevel.Value;
			}
		}
		/// <summary>
		/// Gets a <see cref="bool" /> value indicating whether the current <see cref="System.Diagnostics.Process" /> is elevated or not.
		/// </summary>
		[SupportedOSPlatform("windows")]
		public static bool IsElevated
		{
			get
			{
				if (_IsElevated == null)
				{
					using WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
					_IsElevated = new WindowsPrincipal(windowsIdentity).IsInRole(WindowsBuiltInRole.Administrator);
				}

				return _IsElevated.Value;
			}
		}
		/// <summary>
		/// Gets the <see cref="ElevationType" /> for the current <see cref="System.Diagnostics.Process" />.
		/// </summary>
		[SupportedOSPlatform("windows")]
		public static ElevationType ElevationType
		{
			get
			{
				if (_ElevationType == null)
				{
					nint token;
					using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
					{
						token = process.OpenToken(8);
					}

					try
					{
						using HGlobal elevationTypePtr = new(4);

						if (Native.GetTokenInformation(token, 18, elevationTypePtr.Handle, 4, out int returnLength) && returnLength == 4)
						{
							_ElevationType = (ElevationType)Marshal.ReadInt32(elevationTypePtr.Handle);
						}
						else
						{
							throw Throw.Win32();
						}
					}
					finally
					{
						if (token != 0) Native.CloseHandle(token);
					}
				}

				return _ElevationType.Value;
			}
		}
		/// <summary>
		/// Gets the amount of private memory, in bytes, allocated for the current <see cref="System.Diagnostics.Process" />.
		/// </summary>
		public static long Memory
		{
			get
			{
				using System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess();
				return process.PrivateMemorySize64;
			}
		}
		/// <summary>
		/// Gets the <see cref="System.Version" /> of the .NET Framework that the current <see cref="System.Diagnostics.Process" /> is running with.
		/// </summary>
		public static Version FrameworkVersion => _FrameworkVersion ??= new(typeof(object).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion.SubstringUntil('+') ?? throw Throw.Win32());
	}
	/// <summary>
	/// Provides information about the current logon session and related information.
	/// </summary>
	[SupportedOSPlatform("windows")]
	public static class Session
	{
		private static string? _CurrentUser;
		private static string? _DomainName;
		/// <summary>
		/// Gets the name of the current <see cref="WindowsIdentity" />, including the domain or workstation name.
		/// </summary>
		public static string CurrentUser => _CurrentUser ??= WindowsIdentity.GetCurrent().Name;
		/// <summary>
		/// Gets the name of the current <see cref="WindowsIdentity" />, not including the domain or workstation name.
		/// </summary>
		public static string CurrentUserShort => CurrentUser.SubstringFromLast('\\');
		/// <summary>
		/// Gets the domain in which the local computer is registered, or <see langword="null" />, if the user is not member of a domain.
		/// </summary>
		public static string? DomainName
		{
			get
			{
				if (_DomainName == null)
				{
					_DomainName = IPGlobalProperties.GetIPGlobalProperties().DomainName;
				}

				return _DomainName.ToNullIfEmpty();
			}
		}
		/// <summary>
		/// Gets a <see cref="bool" /> value indicating whether the current session is an RDP session.
		/// </summary>
		public static bool IsRdp => Environment.GetEnvironmentVariable("SESSIONNAME")?.StartsWith("RDP-", StringComparison.OrdinalIgnoreCase) == true;
	}
}

[SupportedOSPlatform("windows")]
file static class Native
{
	[DllImport("kernel32.dll", SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool CloseHandle(nint obj);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern uint GetModuleFileName([In] nint module, [Out] StringBuilder fileName, [In][MarshalAs(UnmanagedType.U4)] int size);
	[DllImport("advapi32.dll", SetLastError = true)]
	public static extern bool GetTokenInformation(nint tokenHandle, int tokenInformationClass, nint tokenInformation, int tokenInformationLength, out int returnLength);
}