using System;

namespace BytecodeApi.IO
{
	/// <summary>
	/// Specifies flags for process analysers, such as sandboxes, virtual environments, or a specific debugger or profiler.
	/// </summary>
	[Flags]
	public enum ProcessAnalysers
	{
		/// <summary>
		/// Specifies Sandboxie
		/// <para>Detection: The module 'SbieDll.dll' is loaded.</para>
		/// </summary>
		Sandboxie = 1,
		/// <summary>
		/// Specifies a generic emulator
		/// <para>Detection: A delay of 500 milliseconds is measured. If it is below 450 milliseconds, an emulator is detected.</para>
		/// </summary>
		Emulator = 2,
		/// <summary>
		/// Specifies that Wireshark is currently running.
		/// <para>Detection: Either 'Wireshark.exe' is running in any active user session, or a window with the title 'The Wireshark Network Analyzer' is opened.</para>
		/// </summary>
		Wireshark = 4,
		/// <summary>
		/// Specifies that Process Monitor is currently running.
		/// <para>Detection: A window with a title that starts with 'Process Monitor -' is opened.</para>
		/// </summary>
		ProcessMonitor = 8
	}
}