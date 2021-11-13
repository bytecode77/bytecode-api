using BytecodeApi.Cryptography;
using BytecodeApi.Text;
using System;
using System.Security.Cryptography;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Generate public/private key pair
		AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);

		// Convert to DER key
		byte[] der = AsymmetricKeyConvert.ToDer(publicKey, AsymmetricKeyType.Public);
		Console.WriteLine("byte[] public_der =");
		Console.WriteLine(Wording.FormatBinary(der));

		// Convert to PEM
		string pem = AsymmetricKeyConvert.ToPem(privateKey, AsymmetricKeyType.Private);
		Console.WriteLine("string private_pem =");
		Console.WriteLine(pem);

		Console.WriteLine("and so on...");

		// This class converts between PEM and DER formats, and RSAParameters objects.

		Console.ReadKey();
	}
}