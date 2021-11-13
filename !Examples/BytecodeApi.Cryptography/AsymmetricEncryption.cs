using System;
using System.Security.Cryptography;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// The maximum amount of data that can be encrypted depends on the RSA key size.
		// To encrypt any amount of data, use the AsymmetricContentEncryption class.

		// Data to encrypt
		byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7 };
		Console.WriteLine("byte[] data =");
		Console.WriteLine(Wording.FormatBinary(data));

		// Generate public/private key pair
		AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);

		// Encrypt using the public key
		byte[] encrypted = AsymmetricEncryption.Encrypt(data, publicKey);
		Console.WriteLine("byte[] encrypted =");
		Console.WriteLine(Wording.FormatBinary(encrypted));

		// Decrypt using the private key
		byte[] decrypted = AsymmetricEncryption.Decrypt(encrypted, privateKey);

		// Compare decrypted data with original data
		if (!data.Compare(decrypted))
		{
			throw new Exception("Decryted data does not match oridinal data!?");
		}

		Console.ReadKey();
	}
}