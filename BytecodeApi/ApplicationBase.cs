using BytecodeApi.Extensions;
using BytecodeApi.IO;
using BytecodeApi.IO.Wmi;
using BytecodeApi.Mathematics;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
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
		private static readonly Dictionary<string, object> Properties = new Dictionary<string, object>();
		/// <summary>
		/// Gets the path for the executable file that started the application, not including the executable name.
		/// </summary>
		public static string Path => GetProperty
		(
			() => Path,
			() => System.Windows.Forms.Application.StartupPath
		);
		/// <summary>
		/// Gets the path for the executable file that started the application, including the executable name.
		/// </summary>
		public static string FileName => GetProperty
		(
			() => FileName,
			() => System.Windows.Forms.Application.ExecutablePath
		);
		/// <summary>
		/// Gets the <see cref="System.Version" /> of the entry assembly.
		/// </summary>
		public static Version Version => GetProperty
		(
			() => Version,
			() => Assembly.GetEntryAssembly().GetName().Version
		);
		/// <summary>
		/// Gets a <see cref="bool" /> value indicating whether <see cref="Debugger.IsAttached" /> was <see langword="true" /> the first time this property is retrieved, or if this executable is located in a directory named like "\bin\Debug", "\bin\x86\Debug", or "\bin\x64\Debug".
		/// </summary>
		public static bool DebugMode => GetProperty
		(
			() => DebugMode,
			() => Debugger.IsAttached || new[] { @"\bin\Debug", @"\bin\x86\Debug", @"\bin\x64\Debug" }.Any(path => Path.Contains(path, SpecialStringComparisons.IgnoreCase))
		);

		/// <summary>
		/// Parses commandline arguments from a <see cref="string" /> and returns the equivalent <see cref="string" />[] with split commandline arguments.
		/// </summary>
		/// <param name="commandLine">A <see cref="string" /> specifying a commandline.</param>
		/// <returns>
		/// The equivalent <see cref="string" />[] with split commandline arguments from the given commandline.
		/// </returns>
		public static string[] ParseCommandLineArgs(string commandLine)
		{
			Check.ArgumentNull(commandLine, nameof(commandLine));

			IntPtr argumentsPtr = Native.CommandLineToArgvW(commandLine, out int count);
			if (argumentsPtr == IntPtr.Zero) throw Throw.Win32();

			try
			{
				return Create.Array(count, i => Marshal.PtrToStringUni(Marshal.ReadIntPtr(argumentsPtr, i * IntPtr.Size)));
			}
			finally
			{
				Native.LocalFree(argumentsPtr);
			}
		}
		/// <summary>
		/// Invokes an empty <see cref="Action" /> on the <see cref="Dispatcher" /> of <see cref="Application.Current" /> while <paramref name="condition" /> evaluates to <see langword="true" />, thereby refreshing the UI. This is the WPF equivalent to <see cref="System.Windows.Forms.Application.DoEvents" />.
		/// </summary>
		/// <param name="condition">The <see cref="Func{TResult}" /> to be evaluated.</param>
		public static void DoEventsWhile(Func<bool> condition)
		{
			DoEventsWhile(condition, TimeSpan.Zero);
		}
		/// <summary>
		/// Invokes an empty <see cref="Action" /> on the <see cref="Dispatcher" /> of <see cref="Application.Current" /> while <paramref name="condition" /> evaluates to <see langword="true" />, thereby refreshing the UI. This is the WPF equivalent to <see cref="System.Windows.Forms.Application.DoEvents" />. The specified delay is waited between each call to <paramref name="condition" />.
		/// </summary>
		/// <param name="condition">The <see cref="Func{TResult}" /> to be evaluated.</param>
		/// <param name="delay">A <see cref="TimeSpan" /> that specifies the delay between each call to <paramref name="condition" />.</param>
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
		/// Restarts the current <see cref="Process" /> with elevated privileges. Returns <see langword="null" />, if the process is already elevated; <see langword="false" />, if elevation failed; <see langword="true" /> if the restart was successful.
		/// </summary>
		/// <param name="commandLine">A <see cref="string" /> specifying the commandline for the new <see cref="Process" />.</param>
		/// <param name="shutdownCallback">A callback that is invoked after the new <see cref="Process" /> was successfully started with elevated privileges. Depending on application type, this is typically <see cref="Environment.Exit(int)" /> or <see cref="Application.Shutdown()" />.</param>
		/// <returns>
		/// <see langword="null" />, if the process is already elevated;
		/// <see langword="false" />, if elevation failed;
		/// <see langword="true" /> if the restart was successful.
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

		private static T GetProperty<T>(Expression<Func<T>> property, Func<T> getter)
		{
			string name = property.GetMemberName();

			lock (Properties)
			{
				if (!Properties.ContainsKey(name)) Properties[name] = CSharp.Try(getter);
				return (T)Properties[name];
			}
		}

		/// <summary>
		/// Provides information about the current process.
		/// </summary>
		public static class Process
		{
			/// <summary>
			/// Gets the ProcessID of the current <see cref="Process" />.
			/// </summary>
			public static int Id => GetProperty
			(
				() => Id,
				() =>
				{
					using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
					{
						return process.Id;
					}
				}
			);
			/// <summary>
			/// Gets the SessionID of the current <see cref="Process" />.
			/// </summary>
			public static int SessionId => GetProperty
			(
				() => SessionId,
				() =>
				{
					using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
					{
						return process.SessionId;
					}
				}
			);
			/// <summary>
			/// Gets the mandatory integrity level for the current <see cref="Process" />, or <see langword="null" />, if it could not be determined.
			/// </summary>
			public static ProcessIntegrityLevel? IntegrityLevel => GetProperty
			(
				() => IntegrityLevel,
				() =>
				{
					using (System.Diagnostics.Process process = System.Diagnostics.Process.GetCurrentProcess())
					{
						return process.GetIntegrityLevel();
					}
				}
			);
			/// <summary>
			/// Gets a <see cref="bool" /> value indicating whether the current <see cref="Process" /> is elevated or not.
			/// </summary>
			public static bool IsElevated => GetProperty
			(
				() => IsElevated,
				() =>
				{
					using (WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent())
					{
						return new WindowsPrincipal(windowsIdentity).IsInRole(WindowsBuiltInRole.Administrator);
					}
				}
			);
			/// <summary>
			/// Gets the <see cref="ElevationType" /> for the current <see cref="Process" />, or <see langword="null" />, if it could not be determined.
			/// </summary>
			public static ElevationType? ElevationType => GetProperty
			(
				() => ElevationType,
				() =>
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
									return (ElevationType)Marshal.ReadInt32(elevationTypePtr);
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

					return null;
				}
			);
			/// <summary>
			/// Gets the amount of private memory, in bytes, allocated for the current <see cref="Process" />.
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
		}
		/// <summary>
		/// Provides information about the current logon session and related information.
		/// </summary>
		public static class Session
		{
			private static bool? _IsWorkstationLocked;
			/// <summary>
			/// Gets the name of the current <see cref="WindowsIdentity" />, including the domain or workstation name.
			/// </summary>
			public static string CurrentUser => GetProperty
			(
				() => CurrentUser,
				() => WindowsIdentity.GetCurrent().Name
			);
			/// <summary>
			/// Gets the name of the current <see cref="WindowsIdentity" />, not including the domain or workstation name.
			/// </summary>
			public static string CurrentUserShort => CurrentUser?.SubstringFrom(@"\", true);
			/// <summary>
			/// Gets the domain in which the local computer is registered, or <see langword="null" />, if the user is not member of a domain.
			/// </summary>
			public static string DomainName => GetProperty
			(
				() => DomainName,
				() => IPGlobalProperties.GetIPGlobalProperties().DomainName.ToNullIfEmpty()
			);
			/// <summary>
			/// Gets the workgroup in which the local computer is registered, or <see langword="null" />, if the user is not member of a workgroup.
			/// </summary>
			public static string Workgroup => GetProperty
			(
				() => Workgroup,
				() => new WmiNamespace("CIMV2", false, false)
					.GetClass("Win32_ComputerSystem", false)
					.GetObjects("Workgroup")
					.First()
					.Properties["Workgroup"]
					.GetValue<string>()
					?.Trim()
					.ToNullIfEmpty()
			);
			/// <summary>
			/// Gets a <see cref="bool" /> value indicating whether the workstation is locked. If the workstation was locked the first time this property was called, <see langword="false" /> is returned.
			/// </summary>
			public static bool IsWorkstationLocked
			{
				get
				{
					if (_IsWorkstationLocked == null)
					{
						_IsWorkstationLocked = false;

						SystemEvents.SessionSwitch += delegate (object sender, SessionSwitchEventArgs e)
						{
							if (e.Reason == SessionSwitchReason.SessionLock) _IsWorkstationLocked = true;
							else if (e.Reason == SessionSwitchReason.SessionUnlock) _IsWorkstationLocked = false;
						};
					}

					return _IsWorkstationLocked == true;
				}
			}
			/// <summary>
			/// Gets the screen DPI. A value of 96.0 corresponds to 100% font scaling.
			/// </summary>
			public static SizeF DesktopDpi => GetProperty
			(
				() => DesktopDpi,
				() =>
				{
					IntPtr desktop = IntPtr.Zero;

					try
					{
						desktop = Native.GetDC(IntPtr.Zero);
						using (Graphics graphics = Graphics.FromHdc(desktop))
						{
							return new SizeF(graphics.DpiX, graphics.DpiY);
						}
					}
					finally
					{
						if (desktop != IntPtr.Zero) Native.ReleaseDC(IntPtr.Zero, desktop);
					}
				}
			);
		}
		/// <summary>
		/// Provides information about the installed operating system.
		/// </summary>
		public static class OperatingSystem
		{
			/// <summary>
			/// Gets the name of the operating system.
			/// <para>Examples: "Windows 7 Professional", "Windows 10 Pro"</para>
			/// </summary>
			public static string Name => GetProperty
			(
				() => Name,
				() => new WmiNamespace("CIMV2", false, false)
					.GetClass("Win32_OperatingSystem", false)
					.GetObjects("Caption")
					.First()
					.Properties["Caption"]
					.GetValue<string>()
					.SubstringFrom("Microsoft ")
					.Trim()
			);
			/// <summary>
			/// Gets the installation date of the operating system, or <see langword="null" />, if it could not be determined.
			/// </summary>
			public static DateTime? InstallDate => GetProperty
			(
				() => InstallDate,
				() =>
				{
					using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion"))
					{
						int installDate = key.GetInt32Value("InstallDate", 0);
						return installDate == 0 ? (DateTime?)null : DateTimeEx.ConvertUnixTimeStamp((uint)installDate);
					}
				}
			);
			/// <summary>
			/// Gets an array containing a list of installed antivirus software.
			/// </summary>
			public static string[] InstalledAntiVirusSoftware => GetProperty
			(
				() => InstalledAntiVirusSoftware,
				() => new WmiNamespace("SecurityCenter2", false, false)
					.GetClass("AntiVirusProduct", false)
					.GetObjects("displayName")
					.Select(obj => obj.Properties["displayName"].GetValue<string>())
					.Where(item => !item.IsNullOrEmpty())
					.ToArray()
			);
			/// <summary>
			/// Gets the default browser of the current user, or <see langword="null" />, if it could not be determined.
			/// </summary>
			public static KnownBrowser? DefaultBrowser
			{
				get
				{
					try
					{
						using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
						{
							if (key?.GetStringValue("Progid") is string program)
							{
								if (program.CompareTo("IE.HTTP", SpecialStringComparisons.IgnoreCase) == 0) return KnownBrowser.InternetExplorer;
								else if (program == "AppXq0fevzme2pys62n3e0fbqa7peapykr8v") return KnownBrowser.Edge;
								else if (program.CompareTo("ChromeHTML", SpecialStringComparisons.IgnoreCase) == 0) return KnownBrowser.Chrome;
								else if (program.StartsWith("FirefoxURL", SpecialStringComparisons.IgnoreCase)) return KnownBrowser.Firefox;
								else if (program.StartsWith("Opera", SpecialStringComparisons.IgnoreCase)) return KnownBrowser.Opera;
								else if (program.StartsWith("Safari", SpecialStringComparisons.IgnoreCase)) return KnownBrowser.Safari;
								else return null;
							}
						}

						using (RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command"))
						{
							string browser = key
								.GetStringValue(null)
								.Replace("\"", "")
								.SubstringUntil(".exe", false, false, true)
								.SubstringFrom(@"\", true)
								.Trim()
								.ToLower();

							switch (browser)
							{
								case "iexplore": return KnownBrowser.InternetExplorer;
								case "chrome": return KnownBrowser.Chrome;
								case "firefox": return KnownBrowser.Firefox;
								case "launcher": return KnownBrowser.Opera;
							}
						}
					}
					catch { }

					return null;
				}
			}
			/// <summary>
			/// Gets the latest version of the .NET Framework and returns the version number as a fallback, if the version could not be looked up. Works only for .NET 4.0+.
			/// <para>Examples: 4.6, 4.7, 4.7.1</para>
			/// <para>Example of a fallback version number: 461310</para>
			/// </summary>
			public static string FrameworkVersion => GetProperty
			(
				() => FrameworkVersion,
				() =>
				{
					using (RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full"))
					{
						int? version = key.GetInt32Value("Release");
						if (version != null)
						{
							return new Dictionary<int, string>
							{
								[378389] = "4.5",
								[378675] = "4.5.1",
								[378758] = "4.5.1",
								[379893] = "4.5.2",
								[393295] = "4.6",
								[393297] = "4.6",
								[394254] = "4.6.1",
								[394271] = "4.6.1",
								[394802] = "4.6.2",
								[394806] = "4.6.2",
								[460798] = "4.7",
								[460805] = "4.7",
								[461308] = "4.7.1",
								[461310] = "4.7.1",
								[461808] = "4.7.2",
								[461814] = "4.7.2"
							}.ValueOrDefault(version.Value, version.ToString());
						}
					}

					return null;
				}
			);
		}
		/// <summary>
		/// Provides information about installed hardware.
		/// </summary>
		public static class Hardware
		{
			/// <summary>
			/// Gets the name of the processor. If multiple processors are installed, the name of the first processor is returned.
			/// </summary>
			public static string Processor => GetProperty
			(
				() => Processor,
				() => new WmiNamespace("CIMV2", false, false)
					.GetClass("Win32_Processor", false)
					.GetObjects("Name")
					.First()
					.Properties["Name"]
					.GetValue<string>()
					.Trim()
			);
			/// <summary>
			/// Gets the total amount of installed physical memory, or <see langword="null" />, if it could not be determined.
			/// </summary>
			public static long? Memory => GetProperty
			(
				() => Memory,
				() =>
				{
					Native.MemoryStatusEx memoryStatus = new Native.MemoryStatusEx();
					return Native.GlobalMemoryStatusEx(memoryStatus) ? (long)memoryStatus.TotalPhys : (long?)null;
				}
			);
			/// <summary>
			/// Gets the name of the video controller. If multiple video controllers are installed, the name of the first video controller is returned.
			/// </summary>
			public static string VideoController => GetProperty
			(
				() => VideoController,
				() => new WmiNamespace("CIMV2", false, false)
					.GetClass("Win32_VideoController", false)
					.GetObjects("Name")
					.First()
					.Properties["Name"]
					.GetValue<string>()
					.Trim()
			);
		}
	}
}