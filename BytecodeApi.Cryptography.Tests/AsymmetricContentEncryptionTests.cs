using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.Tests;

public class AsymmetricContentEncryptionTests
{
	[Fact]
	public void AsymmetricContentEncryption_EncryptAndDecrypt()
	{
		for (int i = 1; i <= 200; i++)
		{
			byte[] data = Random.Shared.GetBytes(i);
			AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);

			byte[] encrypted = AsymmetricContentEncryption.Encrypt(data, publicKey);
			byte[] decrypted = AsymmetricContentEncryption.Decrypt(encrypted, privateKey);

			Assert.Equal(data, decrypted);
		}
	}
}