using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;

namespace BytecodeApi.Cryptography.Tests;

public class EncryptionTests
{
	[Fact]
	public void Encryption_EncryptAndDecrypt()
	{
		for (int i = 1; i <= 1000; i++)
		{
			byte[] data = MathEx.Random.NextBytes(i);
			byte[] iv = Encryption.GenerateIV();
			byte[] key = Encryption.GenerateKey(true);

			byte[] encrypted = Encryption.Encrypt(data, iv, key);
			byte[] decrypted = Encryption.Decrypt(encrypted, iv, key);

			Assert.Equal(data, decrypted);
		}
	}
}