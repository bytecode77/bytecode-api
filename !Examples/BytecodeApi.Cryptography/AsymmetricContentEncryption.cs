using BytecodeApi.Cryptography;
using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System;
using System.Security.Cryptography;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// This class encrypts a randomly generated AES key with an RSA key and the data with the AES key.
		// The AsymmetricEncryption class can only encrypt enough data to encrypt the AES key.
		// AsymmetricContentEncryption is using AsymmetricEncryption internally.

		// Data to encrypt
		byte[] data = new byte[100]; // Can be any size
		for (int i = 0; i < data.Length; i++) data[i] = (byte)i;

		Console.WriteLine("byte[] data =");
		Console.WriteLine(Wording.FormatBinary(data));

		// Generate public/private key pair
		AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);

		// Encrypt using the public key
		byte[] encrypted = AsymmetricContentEncryption.Encrypt(data, publicKey);
		Console.WriteLine("byte[] encrypted =");
		Console.WriteLine(Wording.FormatBinary(encrypted));

		// Decrypt using the private key
		byte[] decrypted = AsymmetricContentEncryption.Decrypt(encrypted, privateKey);

		// Compare decrypted data with original data
		if (!data.Compare(decrypted))
		{
			throw new Exception("Decryted data does not match oridinal data!?");
		}

		Console.ReadKey();
	}
}