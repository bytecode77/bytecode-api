using BytecodeApi.IO.SystemInfo;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Read hosts file

		foreach (HostsFileEntry entry in HostsFile.GetEntries())
		{
			Console.WriteLine(entry.IPAddress + " " + entry.HostName);
		}

		Console.ReadKey();
	}
}