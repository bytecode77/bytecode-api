using BytecodeApi.Cryptography;
using BytecodeApi.Text;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		const string str = "Hello, World!";

		// Convert str to UTF8 bytes, compute hash and return hexadecimal hash string
		Console.WriteLine("Hash of \"" + str + "\" =");
		Console.WriteLine(Hashes.Compute(str, HashType.SHA256));
		Console.WriteLine();

		// Convert str to UTF8 bytes, compute hash and return byte[]
		Console.WriteLine("Hash bytes of \"" + str + "\" =");
		byte[] hash = Hashes.ComputeBytes(str, HashType.SHA256);
		Console.WriteLine(Wording.FormatBinary(hash));

		// Compute hash of a byte[] and return hash as byte[]
		Console.WriteLine("Hash bytes of byte[] { 1, 2, 3 } =");
		hash = Hashes.ComputeBytes(new byte[] { 1, 2, 3 }, HashType.SHA256);
		Console.WriteLine(Wording.FormatBinary(hash));

		Console.ReadKey();
	}
}