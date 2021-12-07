using BytecodeApi.Extensions;
using BytecodeApi.IO.Wmi;
using System;
using System.Linq;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// WMI query using the classes in the BytecodeApi.IO.Wmi namespace

		string[] installedAntiVirus = new WmiNamespace("SecurityCenter2")
			.GetClass("AntiVirusProduct")
			.GetObjects("displayName")
			.Select(obj => obj.Properties["displayName"].GetValue<string>())
			.Where(item => !item.IsNullOrEmpty())
			.ToArray();

		// Note, that WMI is typically convenient to use, but lacks the performance that the native API offers.
		// If a task can be achieved without the use of WMI, it is recommended to use the WinAPI instead.
	}
}