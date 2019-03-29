using BytecodeApi.IO;
using BytecodeApi.IO.Wmi;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Process" /> objects.
	/// </summary>
	public static class ProcessExtensions
	{
		private static readonly Regex MscorlibModuleRegex = new Regex("^mscorlib.(ni.)?dll$", RegexOptions.IgnoreCase);

		/// <summary>
		/// Gets the <see cref="WindowsIdentity" /> object that represents the user running this <see cref="Process" />.
		/// </summary>
		/// <param name="process">The <see cref="Process" /> to be checked.</param>
		/// <returns>
		/// The <see cref="WindowsIdentity" /> object that represents the user running this <see cref="Process" />.
		/// </returns>
		public static WindowsIdentity GetUser(this Process process)
		{
			Check.ArgumentNull(process, nameof(process));

			IntPtr token = IntPtr.Zero;
			try
			{
				token = process.OpenToken(8);
				return new WindowsIdentity(token);
			}
			catch
			{
				return null;
			}
			finally
			{
				if (token != IntPtr.Zero) Native.CloseHandle(token);
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
			Check.ArgumentNull(process, nameof(process));

			using (WindowsIdentity windowsIdentity = process.GetUser())
			{
				return windowsIdentity?.Name;
			}
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
			return process.GetUserName()?.SubstringFrom(@"\", true);
		}
		/// <summary>
		/// Gets the parent <see cref="Process" /> of this <see cref="Process" /> or <see langword="null" />, if this method failed.
		/// </summary>
		/// <param name="process">The <see cref="Process" /> to be checked.</param>
		/// <returns>
		/// The parent <see cref="Process" /> of this <see cref="Process" /> or <see langword="null" />, if this method failed.
		/// </returns>
		public static Process GetParentProcess(this Process process)
		{
			Check.ArgumentNull(process, nameof(process));

			try
			{
				Native.ProcessEntry processEntry = new Native.ProcessEntry
				{
					Size = (uint)Marshal.SizeOf(typeof(Native.ProcessEntry))
				};

				using (Native.SafeSnapshotHandle snapshot = Native.CreateToolhelp32Snapshot(2, (uint)process.Id))
				{
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
			Check.ArgumentNull(process, nameof(process));

			return new WmiNamespace("CIMV2", false, false)
				.GetClass("Win32_Process", false)
				.GetObjects(Singleton.Array("CommandLine"), "ProcessId = " + process.Id)
				.First()
				.Properties["CommandLine"]
				.GetValue<string>();
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
			Check.ArgumentNull(process, nameof(process));

			return ApplicationBase.ParseCommandLineArgs(process.GetCommandLine());
		}
		/// <summary>
		/// Gets the mandatory integrity level of this <see cref="Process" /> or <see langword="null" />, if this method failed.
		/// Usually, this method (specifically, OpenToken) will fail on elevated processes if this method is called with medium IL.
		/// </summary>
		/// <param name="process">The <see cref="Process" /> to be checked.</param>
		/// <returns>
		/// The <see cref="ProcessIntegrityLevel" /> of this <see cref="Process" /> or <see langword="null" />, if this method failed.
		/// </returns>
		public static ProcessIntegrityLevel? GetIntegrityLevel(this Process process)
		{
			Check.ArgumentNull(process, nameof(process));

			IntPtr token = IntPtr.Zero;
			IntPtr integrityLevelToken = IntPtr.Zero;

			try
			{
				token = process.OpenToken(8);
				Native.GetTokenInformation(token, 25, IntPtr.Zero, 0, out int integrityLevelTokenLength);

				integrityLevelToken = Marshal.AllocHGlobal(integrityLevelTokenLength);
				Native.GetTokenInformation(token, 25, integrityLevelToken, integrityLevelTokenLength, out integrityLevelTokenLength);
				Native.TokenMandatoryLabel mandatoryLevelToken = Marshal.PtrToStructure<Native.TokenMandatoryLabel>(integrityLevelToken);

				return (ProcessIntegrityLevel)Marshal.ReadInt32(Native.GetSidSubAuthority(mandatoryLevelToken.Label.Sid, 0));
			}
			catch
			{
				return null;
			}
			finally
			{
				if (token != IntPtr.Zero) Native.CloseHandle(token);
				if (integrityLevelToken != IntPtr.Zero) Marshal.FreeHGlobal(integrityLevelToken);
			}
		}
		/// <summary>
		/// Gets a <see cref="bool" /> value indicating whether this <see cref="Process" /> is a 64-bit or a 32-bit process. Returns <see langword="null" />, if this method failed.
		/// </summary>
		/// <param name="process">The <see cref="Process" /> to be checked.</param>
		/// <returns>
		/// <see langword="true" />, if this <see cref="Process" /> is a 64-bit process;
		/// <see langword="false" />, if this <see cref="Process" /> is a 32-bit process;
		/// <see langword="null" />, if this method failed.
		/// </returns>
		public static bool? Is64Bit(this Process process)
		{
			Check.ArgumentNull(process, nameof(process));

			if (Environment.Is64BitOperatingSystem)
			{
				return CSharp.Try<bool?>(() => Native.IsWow64Process(process.Handle, out bool result) && !result);
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// Gets a <see cref="bool" /> value indicating whether this <see cref="Process" /> is a .NET process. Returns <see langword="null" />, if this method failed.
		/// To identify a .NET process, the presence of either the mscorlib.dll or the mscorlib.ni.dll module is checked.
		/// </summary>
		/// <param name="process">The <see cref="Process" /> to be checked.</param>
		/// <returns>
		/// <see langword="true" />, if this <see cref="Process" /> is a .NET process;
		/// <see langword="false" />, if this <see cref="Process" /> is not a .NET process;
		/// <see langword="null" />, if this method failed.
		/// </returns>
		public static bool? IsDotNet(this Process process)
		{
			Check.ArgumentNull(process, nameof(process));

			return CSharp.Try(() => process.Modules.Cast<ProcessModule>().Any(module => MscorlibModuleRegex.IsMatch(module.FileName)));
		}
		/// <summary>
		/// Injects a DLL into this <see cref="Process" /> using the WriteProcessMemory / CreateRemoteThread technique. If <see cref="ProcessLoadLibraryResult.Success" /> is returned, the DLL has been successfully loaded by this <see cref="Process" />.
		/// </summary>
		/// <param name="process">The <see cref="Process" /> to be injected.</param>
		/// <param name="dllPath">A <see cref="string" /> specifying the path of the DLL file to inject into this <see cref="Process" />.</param>
		/// <returns>
		/// <see cref="ProcessLoadLibraryResult.Success" />, if DLL injection succeeded;
		/// otherwise, a <see cref="ProcessLoadLibraryResult" /> value that indicates the error reason.
		/// </returns>
		public static ProcessLoadLibraryResult LoadLibrary(this Process process, string dllPath)
		{
			Check.ArgumentNull(process, nameof(process));
			Check.ArgumentNull(dllPath, nameof(dllPath));
			Check.FileNotFound(dllPath);

			if (CSharp.Try(() => process.Modules.Cast<ProcessModule>().Any(module => module.FileName.CompareTo(dllPath, SpecialStringComparisons.IgnoreCase) == 0)))
			{
				return ProcessLoadLibraryResult.AlreadyLoaded;
			}
			else
			{
				IntPtr processHandle = Native.OpenProcess(1082, false, process.Id);
				if (processHandle == IntPtr.Zero) return ProcessLoadLibraryResult.OpenProcessFailed;
				IntPtr loadLibraryAddress = Native.GetProcAddress(Native.GetModuleHandle("kernel32.dll"), "LoadLibraryW");
				IntPtr allocatedMemoryAddress = Native.VirtualAllocEx(processHandle, IntPtr.Zero, (uint)dllPath.Length * 2 + 1, 0x3000, 4);
				if (allocatedMemoryAddress == IntPtr.Zero) return ProcessLoadLibraryResult.VirtualAllocFailed;
				if (!Native.WriteProcessMemory(processHandle, allocatedMemoryAddress, dllPath.ToUnicodeBytes(), (uint)dllPath.Length * 2 + 1, out _)) return ProcessLoadLibraryResult.WriteProcessMemoryFailed;
				return Native.CreateRemoteThread(processHandle, IntPtr.Zero, 0, loadLibraryAddress, allocatedMemoryAddress, 0, IntPtr.Zero) == IntPtr.Zero ? ProcessLoadLibraryResult.CreateRemoteThreadFailed : ProcessLoadLibraryResult.Success;
			}
		}
		internal static IntPtr OpenToken(this Process process, uint desiredAccess)
		{
			return Native.OpenProcessToken(process.Handle, desiredAccess, out IntPtr token) ? token : IntPtr.Zero;
		}
	}
}