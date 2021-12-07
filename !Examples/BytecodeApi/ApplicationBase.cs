using BytecodeApi;
using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using System;
using System.Linq;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		Console.WriteLine("ApplicationBase.Path = " + ApplicationBase.Path);
		Console.WriteLine("ApplicationBase.FileName = " + ApplicationBase.FileName);
		Console.WriteLine("ApplicationBase.Version = " + ApplicationBase.Version);
		Console.WriteLine("ApplicationBase.DebugMode = " + ApplicationBase.DebugMode);
		Console.WriteLine();

		Console.WriteLine("ApplicationBase.Process.Id = " + ApplicationBase.Process.Id);
		Console.WriteLine("ApplicationBase.Process.SessionId = " + ApplicationBase.Process.SessionId);
		Console.WriteLine("ApplicationBase.Process.IntegrityLevel = " + ApplicationBase.Process.IntegrityLevel);
		Console.WriteLine("ApplicationBase.Process.IsElevated = " + ApplicationBase.Process.IsElevated);
		Console.WriteLine("ApplicationBase.Process.ElevationType = " + ApplicationBase.Process.ElevationType);
		Console.WriteLine("ApplicationBase.Process.Memory = " + new ByteSize(ApplicationBase.Process.Memory).Format());
		Console.WriteLine("ApplicationBase.Process.FrameworkVersion = " + ApplicationBase.Process.FrameworkVersion);
		Console.WriteLine();

		Console.WriteLine("ApplicationBase.Session.CurrentUser = " + ApplicationBase.Session.CurrentUser);
		Console.WriteLine("ApplicationBase.Session.CurrentUserShort = " + ApplicationBase.Session.CurrentUserShort);
		Console.WriteLine("ApplicationBase.Session.DomainName = " + ApplicationBase.Session.DomainName);
		Console.WriteLine("ApplicationBase.Session.Workgroup = " + ApplicationBase.Session.Workgroup);
		Console.WriteLine("ApplicationBase.Session.Dpi = " + ApplicationBase.Session.Dpi.Width + "x" + ApplicationBase.Session.Dpi.Height);
		Console.WriteLine("ApplicationBase.Session.IsRdp = " + ApplicationBase.Session.IsRdp);
		Console.WriteLine();

		Console.WriteLine("ApplicationBase.OperatingSystem.Name = " + ApplicationBase.OperatingSystem.Name);
		Console.WriteLine("ApplicationBase.OperatingSystem.InstallDate = " + ApplicationBase.OperatingSystem.InstallDate);
		Console.WriteLine("ApplicationBase.OperatingSystem.InstalledAntiVirusSoftware = " + ApplicationBase.OperatingSystem.InstalledAntiVirusSoftware.AsString(", "));
		Console.WriteLine("ApplicationBase.OperatingSystem.DefaultBrowser = " + ApplicationBase.OperatingSystem.DefaultBrowser);
		Console.WriteLine("ApplicationBase.OperatingSystem.FrameworkVersionNumber = " + ApplicationBase.OperatingSystem.FrameworkVersionNumber);
		Console.WriteLine("ApplicationBase.OperatingSystem.FrameworkVersionName = " + ApplicationBase.OperatingSystem.FrameworkVersionName);
		Console.WriteLine();

		Console.WriteLine("ApplicationBase.Hardware.ProcessorNames = " + ApplicationBase.Hardware.ProcessorNames.Distinct().AsString(", "));
		Console.WriteLine("ApplicationBase.Hardware.Memory = " + new ByteSize(ApplicationBase.Hardware.Memory ?? 0).Format());
		Console.WriteLine("ApplicationBase.Hardware.VideoControllerName = " + ApplicationBase.Hardware.VideoControllerName);

		Console.ReadKey();
	}
}