using BytecodeApi.Mathematics;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// The ByteSize struct represent a size, in bytes.
		// There are also WPF converters to display ByteSize values in the UI.

		ByteSize a = 10000000; // 10 million bytes

		// Format automatically
		Console.WriteLine(a.Format());

		// Format with specific unit
		Console.WriteLine(a.Format(ByteSizeUnit.KiloByte));

		// Format with 3 decimals
		Console.WriteLine(a.Format(ByteSizeUnit.KiloByte, 3));

		// Use thousands separator, because the unit (KiloByte) results in a number larger than 1000
		Console.WriteLine(a.Format(ByteSizeUnit.KiloByte, 3, false, true));

		// Get megabytes as double value
		double megaBytes = a.MegaBytes;

		// Cast back to long value
		long bytes = (long)a;
		// or
		bytes = a.Bytes;

		Console.ReadKey();
	}
}