using BytecodeApi.Extensions;

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
			byte[] data = Random.Shared.GetBytes(i);
			string password = Create.AlphaNumericString(1 + i % 10);

			byte[] encrypted = ContentEncryption.Encrypt(data, password, passwordHashPasses);
			byte[] decrypted = ContentEncryption.Decrypt(encrypted, password);

			Assert.Equal(data, decrypted);
		}
	}
}