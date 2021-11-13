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

		// Password
		const string password = "secret";
		Console.WriteLine("string password = " + password);
		Console.WriteLine();

		// Encrypt using the password and hash the password 1000 times
		byte[] encrypted = ContentEncryption.Encrypt(data, password, 1000);
		Console.WriteLine("byte[] encrypted =");
		Console.WriteLine(Wording.FormatBinary(encrypted));

		// Decrypt data using the password
		// The encrypted byte[] contains the IV and information about how many times the password was hashed.
		// Therefore, only the password is needed.
		byte[] decrypted = ContentEncryption.Decrypt(encrypted, password);

		// Compare decrypted data with original data
		if (!data.Compare(decrypted))
		{
			throw new Exception("Decryted data does not match oridinal data!?");
		}

		Console.ReadKey();
	}
}