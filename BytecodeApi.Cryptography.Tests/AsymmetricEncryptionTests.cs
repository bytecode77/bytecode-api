using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.Tests;

public class AsymmetricEncryptionTests
{
	[Fact]
	public void AsymmetricEncryption_EncryptAndDecrypt()
	{
		for (int i = 1; i <= 86; i++)
		{
			byte[] data = Random.Shared.GetBytes(i);
			AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);

			byte[] encrypted = AsymmetricEncryption.Encrypt(data, publicKey);
			byte[] decrypted = AsymmetricEncryption.Decrypt(encrypted, privateKey);

			Assert.Equal(data, decrypted);
		}
	}
}