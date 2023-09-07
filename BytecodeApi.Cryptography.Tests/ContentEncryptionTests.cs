using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;

namespace BytecodeApi.Cryptography.Tests;

public class ContentEncryptionTests
{
	[Theory]
	[InlineData(1)]
	[InlineData(10)]
	[InlineData(100)]
	public void ContentEncryption_EncryptAndDecrypt(int passwordHashPasses)
	{
		for (int i = 1; i <= 1000; i++)
		{
			byte[] data = MathEx.Random.NextBytes(i);
			string password = Create.AlphaNumericString(1 + i % 10);

			byte[] encrypted = ContentEncryption.Encrypt(data, password, passwordHashPasses);
			byte[] decrypted = ContentEncryption.Decrypt(encrypted, password);

			Assert.Equal(data, decrypted);
		}
	}
}