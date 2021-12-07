using BytecodeApi.Cryptography;
using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Data to encrypt
		byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
		Console.WriteLine("byte[] data =");
		Console.WriteLine(Wording.FormatBinary(data));

		// IV is a random value
		byte[] iv = Encryption.GenerateIV();
		Console.WriteLine("byte[] iv =");
		Console.WriteLine(Wording.FormatBinary(iv));

		// Encryption key is derived from a password, hashed 1000 times
		byte[] key = Hashes.ComputeBytes("password", HashType.SHA256, 1000);
		Console.WriteLine("byte[] key =");
		Console.WriteLine(Wording.FormatBinary(key));

		// Encrypt a byte[] using a specified key and IV
		byte[] encrypted = Encryption.Encrypt(data, iv, key);
		Console.WriteLine("byte[] encrypted =");
		Console.WriteLine(Wording.FormatBinary(encrypted));

		// Decrypt data using the same key and IV
		byte[] decrypted = Encryption.Decrypt(encrypted, iv, key);

		// Compare decrypted data with original data
		if (!data.Compare(decrypted))
		{
			throw new Exception("Decryted data does not match oridinal data!?");
		}

		Console.ReadKey();
	}
}