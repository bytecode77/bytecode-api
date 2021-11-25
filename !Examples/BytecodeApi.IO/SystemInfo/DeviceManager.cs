using BytecodeApi.IO.SystemInfo;
using System;
using System.Collections.Generic;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Enumerate over devices

		foreach (DeviceTypeInfo type in DeviceManager.Create().DeviceTypes)
		{
			Console.WriteLine(type.ClassName);

			foreach (DeviceInfo device in type.Devices)
			{
				Console.WriteLine("  " + device.Name);

				foreach (KeyValuePair<string, object> attribute in device.Attributes)
				{
					Console.WriteLine("    " + attribute.Key + " = " + attribute.Value);
				}
			}
		}

		Console.ReadKey();
	}
}