namespace BytecodeApi.IO
{
	/// <summary>
	/// Specifies process analysers, such as sandboxes, virtual environments, or a specific debugger or profiler.
	/// </summary>
	public enum ProcessAnalyser
	{
		/// <summary>
		/// Specifies Sandboxie
		/// <para>Detection: The module 'SbieDll.dll' is loaded.</para>
		/// </summary>
		Sandboxie,
		/// <summary>
		/// Specifies a generic emulator
		/// <para>Detection: A delay of 500 milliseconds is measured. If it is below 450 milliseconds, an emulator is detected.</para>
		/// </summary>
		Emulator,
		/// <summary>
		/// Specifies that Wireshark is currently running.
		/// <para>Detection: Either 'Wireshark.exe' is running in any active user session, or a window with the title 'The Wireshark Network Analyzer' is opened.</para>
		/// </summary>
		Wireshark,
		/// <summary>
		/// Specifies that Process Monitor is currently running.
		/// <para>Detection: A window with a title that starts with 'Process Monitor -' is opened.</para>
		/// </summary>
		ProcessMonitor
	}
}