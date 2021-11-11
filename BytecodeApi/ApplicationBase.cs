using BytecodeApi.Extensions;
using BytecodeApi.IO;
using BytecodeApi.IO.Wmi;
using BytecodeApi.Mathematics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace BytecodeApi
{
	/// <summary>
	/// Provides <see langword="static" /> methods that extend the <see cref="Application" /> and the <see cref="System.Windows.Forms.Application" /> class.
	/// </summary>
	public static class ApplicationBase
	{
		private static Version _Version;
		private static bool? _DebugMode;
		/// <summary>
		/// Gets the path for the executable file that started the application, not including the executable name.
		/// </summary>
		public static string Path => System.Windows.Forms.Application.StartupPath;
		/// <summary>
		/// Gets the path for the executable file that started the application, including the executable name.
		/// </summary>
		public static string FileName => System.Windows.Forms.Application.ExecutablePath;
		/// <summary>
		/// Gets the <see cref="System.Version" /> of the entry assembly.
		/// </summary>
		public static Version Version
		{
			get
			{
				if (_Version == null) _Version = Assembly.GetEntryAssembly().GetName().Version;
				return _Version;
			}
		}
		/// <summary>
		/// Gets a <see cref="bool" /> value indicating whether <see cref="Debugger.IsAttached" /> was <see langword="true" /> the first time this property is retrieved, or if this executable is located in a directory named like "\bin\Debug", "\bin\x86\Debug", or "\bin\x64\Debug".
		/// </summary>
		public static bool DebugMode
		{
			get
			{
				if (_DebugMode == null) _DebugMode = Debugger.IsAttached || new[] { @"\bin\Debug", @"\bin\x86\Debug", @"\bin\x64\Debug" }.Any(path => Path.Contains(path + @"\", SpecialStringComparisons.IgnoreCase) || Path.EndsWith(path, StringComparison.OrdinalIgnoreCase));
				return _DebugMode.Value;
			}
		}

		/// <summary>
		/// Invokes an empty <see cref="Action" /> on the <see cref="Dispatcher" /> of <see cref="Application.Current" /> while <paramref name="condition" /> evaluates to <see langword="true" />, thereby refreshing the UI. This is the WPF equivalent to <see cref="System.Windows.Forms.Application.DoEvents" />.
		/// </summary>
		/// <param name="condition">The <see cref="Func{TResult}" /> to be evaluated.</param>
		public static void DoEventsWhile(Func<bool> condition)
		{
			DoEventsWhile(condition, TimeSpan.FromMilliseconds(1));
		}
		/// <summary>
		/// Invokes an empty <see cref="Action" /> on the <see cref="Dispatcher" /> of <see cref="Application.Current" /> while <paramref name="condition" /> evaluates to <see langword="true" />, thereby refreshing the UI. This is the WPF equivalent to <see cref="System.Windows.Forms.Application.DoEvents" />. The specified delay is waited between each call to <paramref name="condition" />.
		/// </summary>
		/// <param name="condition">The <see cref="Func{TResult}" /> to be evaluated.</param>
		/// <param name="delay">A <see cref="TimeSpan" /> that specifies the delay between each call to <paramref name="condition" />. The default value is 1 milliseconds.</param>
		public static void DoEventsWhile(Func<bool> condition, TimeSpan delay)
		{
			Check.ArgumentNull(condition, nameof(condition));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(delay, nameof(delay));

			do
			{
				Thread.Sleep(delay);
				Application.Current.Dispatcher.Invoke(delegate { }, DispatcherPriority.Background);
			}
			while (condition());
		}
		/// <summary>
		/// Restarts the current <see cref="System.Diagnostics.Process" /> with elevated privileges. Returns <see langword="null" />, if the process is already elevated; <see langword="false" />, if elevation failed; <see langword="true" /> if the restart was successful.
		/// </summary>
		/// <param name="commandLine">A <see cref="string" /> specifying the commandline for the new <see cref="System.Diagnostics.Process" />.</param>
		/// <param name="shutdownCallback">A callback that is invoked after the new <see cref="System.Diagnostics.Process" /> was successfully started with elevated privileges. Depending on application type, this is typically <see cref="Environment.Exit(int)" /> or <see cref="Application.Shutdown()" />.</param>
		/// <returns>
		/// <see langword="null" />, if the process is already elevated;
		/// <see langword="false" />, if elevation failed;
		/// <see langword="true" />, if the restart was successful.
		/// </returns>
		public static bool? RestartElevated(string commandLine, Action shutdownCallback)
		{
			if (Process.IsElevated)
			{
				return null;
			}
			else
			{
				try
				{
					System.Diagnostics.Process.Start(new ProcessStartInfo(FileName, commandLine) { Verb = "runas" });
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
			private static Version _FrameworkVersion;
			/// <summary>
			/// Gets the ProcessID of the current <see cref="System.Diagnostics.Process" />.
			/// </summary>
			public static int Id
			{
				get
				{
					if (_Id == null)
					{
						using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
						{
							_Id = process.Id;
						}
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
						using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
						{
							_SessionId = process.SessionId;
						}
					}

					return _SessionId.Value;
				}
			}
			/// <summary>
			/// Gets the mandatory integrity level for the current <see cref="System.Diagnostics.Process" />.
			/// </summary>
			public static ProcessIntegrityLevel IntegrityLevel
			{
				get
				{
					if (_IntegrityLevel == null)
					{
						using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
						{
							_IntegrityLevel = process.GetIntegrityLevel();
						}
					}

					return _IntegrityLevel ?? throw Throw.Win32();
				}
			}
			/// <summary>
			/// Gets a <see cref="bool" /> value indicating whether the current <see cref="System.Diagnostics.Process" /> is elevated or not.
			/// </summary>
			public static bool IsElevated
			{
				get
				{
					if (_IsElevated == null)
					{
						using (WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent())
						{
							_IsElevated = new WindowsPrincipal(windowsIdentity).IsInRole(WindowsBuiltInRole.Administrator);
						}
					}

					return _IsElevated.Value;
				}
			}
			/// <summary>
			/// Gets the <see cref="ElevationType" /> for the current <see cref="System.Diagnostics.Process" />.
			/// </summary>
			public static ElevationType ElevationType
			{
				get
				{
					if (_ElevationType == null)
					{
						IntPtr token;
						using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
						{
							token = process.OpenToken(8);
						}

						if (token != IntPtr.Zero)
						{
							try
							{
								IntPtr elevationTypePtr = Marshal.AllocHGlobal(4);

								try
								{
									if (Native.GetTokenInformation(token, 18, elevationTypePtr, 4, out int returnLength) && returnLength == 4)
									{
										_ElevationType = (ElevationType)Marshal.ReadInt32(elevationTypePtr);
									}
								}
								finally
								{
									Marshal.FreeHGlobal(elevationTypePtr);
								}
							}
							finally
							{
								Native.CloseHandle(token);
							}
						}
					}

					return _ElevationType ?? throw Throw.Win32();
				}
			}
			/// <summary>
			/// Gets the amount of private memory, in bytes, allocated for the current <see cref="System.Diagnostics.Process" />.
			/// </summary>
			public static long Memory
			{
				get
				{
					using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
					{
						return process.PrivateMemorySize64;
					}
				}
			}
			/// <summary>
			/// Gets the <see cref="System.Version" /> of the .NET Framework that the current <see cref="System.Diagnostics.Process" /> is running with.
			/// </summary>
			public static Version FrameworkVersion
			{
				get
				{
					if (_FrameworkVersion == null) _FrameworkVersion = new Version(typeof(object).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version);
					return _FrameworkVersion;
				}
			}
		}
		/// <summary>
		/// Provides information about the current logon session and related information.
		/// </summary>
		public static class Session
		{
			private static string _CurrentUser;
			private static string _DomainName;
			private static string _Workgroup;
			private static bool? _IsRdp;
			/// <summary>
			/// Gets the name of the current <see cref="WindowsIdentity" />, including the domain or workstation name.
			/// </summary>
			public static string CurrentUser
			{
				get
				{
					if (_CurrentUser == null) _CurrentUser = WindowsIdentity.GetCurrent().Name;
					return _CurrentUser;
				}
			}
			/// <summary>
			/// Gets the name of the current <see cref="WindowsIdentity" />, not including the domain or workstation name.
			/// </summary>
			public static string CurrentUserShort => CurrentUser.SubstringFrom(@"\", true);
			/// <summary>
			/// Gets the domain in which the local computer is registered, or <see langword="null" />, if the user is not member of a domain.
			/// </summary>
			public static string DomainName
			{
				get
				{
					if (_DomainName == null) _DomainName = IPGlobalProperties.GetIPGlobalProperties().DomainName.ToNullIfEmpty();
					return _DomainName;
				}
			}
			/// <summary>
			/// Gets the workgroup in which the local computer is registered, or <see langword="null" />, if the user is not member of a workgroup.
			/// </summary>
			public static string Workgroup
			{
				get
				{
					if (_Workgroup == null)
					{
						IntPtr domainStr = IntPtr.Zero;

						try
						{
							if (Native.NetGetJoinInformation(null, out domainStr, out int status) == 0 && status == 2)
							{
								_Workgroup = Marshal.PtrToStringAuto(domainStr);
							}
						}
						finally
						{
							if (domainStr != IntPtr.Zero) Native.NetApiBufferFree(domainStr);
						}
					}

					return _Workgroup;
				}
			}
			/// <summary>
			/// Gets the screen DPI. A value of 96 corresponds to 100% font scaling.
			/// </summary>
			public static System.Drawing.Size Dpi
			{
				get
				{
					using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
					{
						IntPtr desktop = graphics.GetHdc();
						return new System.Drawing.Size(Native.GetDeviceCaps(desktop, 88), Native.GetDeviceCaps(desktop, 90));
					}
				}
			}
			/// <summary>
			/// Gets a <see cref="bool" /> value indicating whether the current session is an RDP session.
			/// </summary>
			public static bool IsRdp
			{
				get
				{
					if (_IsRdp == null) _IsRdp = Environment.GetEnvironmentVariable("SESSIONNAME").StartsWith("RDP-", StringComparison.OrdinalIgnoreCase);
					return _IsRdp.Value;
				}
			}
		}
		/// <summary>
		/// Provides information about the installed operating system.
		/// </summary>
		public static class OperatingSystem
		{
			private static string _Name;
			private static DateTime? _InstallDate;
			private static string[] _InstalledAntiVirusSoftware;
			private static int? _FrameworkVersionNumber;
			/// <summary>
			/// Gets the name of the operating system.
			/// <para>Examples: "Windows 7 Professional", "Windows 10 Pro"</para>
			/// </summary>
			public static string Name
			{
				get
				{
					if (_Name == null)
					{
						using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
						using (RegistryKey key = baseKey.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion"))
						{
							_Name = key.GetStringValue("ProductName");
						}
					}

					return _Name;
				}
			}
			/// <summary>
			/// Gets the installation date of the operating system, or <see langword="null" />, if it could not be determined.
			/// </summary>
			public static DateTime? InstallDate
			{
				get
				{
					if (_InstallDate == null)
					{
						using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
						using (RegistryKey key = baseKey.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion"))
						{
							int installDate = key.GetInt32Value("InstallDate", 0);
							_InstallDate = installDate == 0 ? (DateTime?)null : DateTimeEx.ConvertUnixTimeStamp((uint)installDate);
						}
					}

					return _InstallDate;
				}
			}
			/// <summary>
			/// Gets an array containing a list of installed antivirus software.
			/// </summary>
			public static string[] InstalledAntiVirusSoftware
			{
				get
				{
					if (_InstalledAntiVirusSoftware == null)
					{
						_InstalledAntiVirusSoftware = new WmiNamespace("SecurityCenter2", false, false)
							.GetClass("AntiVirusProduct", false)
							.GetObjects("displayName")
							.Select(obj => obj.Properties["displayName"].GetValue<string>())
							.Where(item => !item.IsNullOrEmpty())
							.ToArray();
					}

					return _InstalledAntiVirusSoftware;
				}
			}
			/// <summary>
			/// Gets the default browser of the current user, or <see langword="null" />, if it could not be determined.
			/// </summary>
			public static KnownBrowser? DefaultBrowser
			{
				get
				{
					using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
					{
						if (key?.GetStringValue("Progid") is string program)
						{
							if (program.Equals("IE.HTTP", StringComparison.OrdinalIgnoreCase)) return KnownBrowser.InternetExplorer;
							else if (program == "AppXq0fevzme2pys62n3e0fbqa7peapykr8v") return KnownBrowser.Edge;
							else if (program.Equals("ChromeHTML", StringComparison.OrdinalIgnoreCase)) return KnownBrowser.Chrome;
							else if (program.StartsWith("FirefoxURL", StringComparison.OrdinalIgnoreCase)) return KnownBrowser.Firefox;
							else if (program.StartsWith("Opera", StringComparison.OrdinalIgnoreCase) || program.StartsWith("OperaStable", StringComparison.OrdinalIgnoreCase)) return KnownBrowser.Opera;
							else if (program.StartsWith("Safari", StringComparison.OrdinalIgnoreCase) || program.StartsWith("SafariHTML", StringComparison.OrdinalIgnoreCase)) return KnownBrowser.Safari;
						}
					}

					using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command"))
					{
						string browser = key
							?.GetStringValue(null)
							.Replace("\"", null)
							.SubstringUntil(".exe", false, false, true)
							.SubstringFrom(@"\", true)
							.Trim()
							.ToLower();

						if (browser != null)
						{
							switch (browser)
							{
								case "iexplore": return KnownBrowser.InternetExplorer;
								case "chrome": return KnownBrowser.Chrome;
								case "firefox": return KnownBrowser.Firefox;
								case "launcher": return KnownBrowser.Opera;
								default: return null;
							}
						}
					}

					return null;
				}
			}
			/// <summary>
			/// Gets the currently installed version of the .NET Framework and returns the version number. Works for .NET 4.5+.
			/// <para>Examples: 528049, 528040, 461814</para>
			/// </summary>
			public static int? FrameworkVersionNumber
			{
				get
				{
					if (_FrameworkVersionNumber == null)
					{
						using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full"))
						{
							_FrameworkVersionNumber = key.GetInt32Value("Release");
						}
					}

					return _FrameworkVersionNumber;
				}
			}
			/// <summary>
			/// Gets the currently installed version of the .NET Framework, deduced from the <see cref="FrameworkVersionNumber" /> property and <see langword="null" />, if the version name could not be determined.
			/// Works for version numbers from NET 4.5 to .NET 4.8.
			/// <para>Examples: 4.5, 4.6, 4.6.1, ..., 4.8</para>
			/// </summary>
			public static string FrameworkVersionName
			{
				get
				{
					// Version numbers: https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed#minimum-version

					if (FrameworkVersionNumber >= 528040) return "4.8";
					else if (FrameworkVersionNumber >= 461808) return "4.7.2";
					else if (FrameworkVersionNumber >= 461308) return "4.7.1";
					else if (FrameworkVersionNumber >= 460798) return "4.7";
					else if (FrameworkVersionNumber >= 394802) return "4.6.2";
					else if (FrameworkVersionNumber >= 394254) return "4.6.1";
					else if (FrameworkVersionNumber >= 393295) return "4.6";
					else if (FrameworkVersionNumber >= 379893) return "4.5.2";
					else if (FrameworkVersionNumber >= 378675) return "4.5.1";
					else if (FrameworkVersionNumber >= 378389) return "4.5";
					else return null;
				}
			}
		}
		/// <summary>
		/// Provides information about installed hardware.
		/// </summary>
		public static class Hardware
		{
			private static string[] _ProcessorNames;
			private static long? _Memory;
			private static string _VideoControllerName;
			/// <summary>
			/// Gets the names of all installed processors.
			/// </summary>
			public static string[] ProcessorNames
			{
				get
				{
					if (_ProcessorNames == null)
					{
						List<string> names = new List<string>();

						using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor"))
						{
							foreach (string subKeyName in key.GetSubKeyNames())
							{
								using (RegistryKey subKey = key.OpenSubKey(subKeyName))
								{
									if (subKey.GetStringValue("ProcessorNameString").ToNullIfEmpty() is string name)
									{
										names.Add(name);
									}
								}
							}
						}

						_ProcessorNames = names.ToArray();
					}

					return _ProcessorNames;
				}
			}
			/// <summary>
			/// Gets the total amount of installed physical memory, or <see langword="null" />, if it could not be determined.
			/// </summary>
			public static long? Memory
			{
				get
				{
					if (_Memory == null)
					{
						Native.MemoryStatusEx memoryStatus = new Native.MemoryStatusEx();
						_Memory = Native.GlobalMemoryStatusEx(memoryStatus) ? (long)memoryStatus.TotalPhys : (long?)null;
					}

					return _Memory;
				}
			}
			/// <summary>
			/// Gets the name of the video controller. If multiple video controllers are installed, the name of the first video controller is returned.
			/// </summary>
			public static string VideoControllerName
			{
				get
				{
					if (_VideoControllerName == null)
					{
						_VideoControllerName = new WmiNamespace("CIMV2", false, false)
							.GetClass("Win32_VideoController", false)
							.GetObjects("Name")
							.First()
							.Properties["Name"]
							.GetValue<string>()
							.Trim();
					}

					return _VideoControllerName;
				}
			}
		}
	}
}