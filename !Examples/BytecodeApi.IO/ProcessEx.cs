using BytecodeApi.IO;
using System;
using System.Diagnostics;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Start process and read console output
		Console.WriteLine("Reading console output of inconfig.exe:");
		Console.WriteLine();
		Console.WriteLine(ProcessEx.ReadProcessOutput("ipconfig.exe"));

		// Start process, wait for it to exit and retrieve the exit code
		int exitCode = ProcessEx.Execute("net.exe");
		// exit code is "1", because net.exe expects arguments.

		// Start notepad.exe with low integrity level (sandbox)
		// Notepad cannot save to the disk, because it has no permission.
		Process sandboxedProcess = ProcessEx.StartWithIntegrity(@"notepad.exe", ProcessIntegrityLevel.Low);

		Console.ReadKey();
	}
}