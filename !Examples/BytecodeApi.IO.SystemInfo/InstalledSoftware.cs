using BytecodeApi.IO.SystemInfo;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Get list of installed software

		foreach (InstalledSoftwareInfo software in InstalledSoftware.GetEntries())
		{
			Console.WriteLine(software.Name);
			Console.WriteLine("  Version:           " + software.Version);
			Console.WriteLine("  Publisher:         " + software.Publisher);
			Console.WriteLine("  Installed on:      " + software.InstallDate);
			Console.WriteLine("  Installation path: " + software.InstallPath);
			Console.WriteLine(new string('-', 50));
		}

		Console.ReadKey();
	}
}