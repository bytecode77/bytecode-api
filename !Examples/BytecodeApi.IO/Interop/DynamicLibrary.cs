using BytecodeApi.IO.Interop;
using System;
using System.Runtime.InteropServices;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Dynamically create P/Invoke methods at runtime:

		int tickCount = new DynamicLibrary("kernel32.dll")
			.GetFunction<int>("GetTickCount", CallingConvention.StdCall, CharSet.Ansi)
			.Call();

		Console.WriteLine("GetTickCount() = " + tickCount);
		Console.ReadKey();
	}
}