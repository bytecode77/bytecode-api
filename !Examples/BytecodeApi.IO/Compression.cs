using BytecodeApi.Extensions;
using BytecodeApi.IO;
using BytecodeApi.Text;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Data to compress
		byte[] data = new byte[100];
		Console.WriteLine("byte[] data =");
		Console.WriteLine(Wording.FormatBinary(data));

		// Compress data using GZip
		byte[] compressed = Compression.Compress(data);
		Console.WriteLine("byte[] compressed =");
		Console.WriteLine(Wording.FormatBinary(compressed));

		byte[] decompressed = Compression.Decompress(compressed);

		// Compare decompressed data with original data
		if (!data.Compare(decompressed))
		{
			throw new Exception("Decompressed data does not match oridinal data!?");
		}

		Console.ReadKey();
	}
}