using BytecodeApi;
using BytecodeApi.Cryptography;
using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Cryptography;
using UnitTest.Properties;

namespace UnitTest
{
	[TestClass]
	public class CryptographyTests
	{
		private const string TestString = "The quick brown fox jumps over the lazy dog";

		[TestMethod]
		public void BytecodeApi_Cryptography_AsymmetricContentEncryption()
		{
			byte[] data = MathEx.RandomNumberGenerator.GetBytes(10000);
			AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);

			Assert.IsTrue(AsymmetricContentEncryption.Decrypt(AsymmetricContentEncryption.Encrypt(data, Resources.RSAPublicKey), Resources.RSAPrivateKey).Compare(data));
			Assert.IsTrue(AsymmetricContentEncryption.Decrypt(AsymmetricContentEncryption.Encrypt(data, publicKey), privateKey).Compare(data));
		}
		[TestMethod]
		public void BytecodeApi_Cryptography_AsymmetricEncryption()
		{
			byte[] data = MathEx.RandomNumberGenerator.GetBytes(32);
			AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);

			Assert.IsTrue(AsymmetricEncryption.Decrypt(AsymmetricEncryption.Encrypt(data, Resources.RSAPublicKey), Resources.RSAPrivateKey).Compare(data));
			Assert.IsTrue(AsymmetricEncryption.Decrypt(AsymmetricEncryption.Encrypt(data, publicKey), privateKey).Compare(data));
		}
		[TestMethod]
		public void BytecodeApi_Cryptography_ContentEncryption()
		{
			byte[] data = MathEx.RandomNumberGenerator.GetBytes(10000);
			string password = Create.AlphaNumericString(20);

			Assert.IsTrue(ContentEncryption.Decrypt(ContentEncryption.Encrypt(data, password, 1000), password).Compare(data));
		}
		[TestMethod]
		public void BytecodeApi_Cryptography_Encryption()
		{
			byte[] iv = Encryption.GenerateIV();
			byte[] key = Encryption.GenerateKey(false);
			byte[] data = MathEx.RandomNumberGenerator.GetBytes(10000);

			Assert.IsTrue(Encryption.Decrypt(Encryption.Encrypt(data, iv, key), iv, key).Compare(data));
		}
		[TestMethod]
		public void BytecodeApi_Cryptography_Hashes()
		{
			Assert.AreEqual("9e107d9d372bb6826bd81d3542a419d6", Hashes.Compute(TestString, HashType.MD5));
			Assert.AreEqual("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12", Hashes.Compute(TestString, HashType.SHA1));
			Assert.AreEqual("d7a8fbb307d7809469ca9abcb0082e4f8d5651e46d3cdb762d02d0bf37c9e592", Hashes.Compute(TestString, HashType.SHA256));

			// Custom implementations
			Assert.AreEqual("5bdc0fda", Hashes.Compute(TestString, HashType.Adler32));
			Assert.AreEqual("414fa339", Hashes.Compute(TestString, HashType.CRC32));
			Assert.AreEqual("bcd8bb366d256116", Hashes.Compute(TestString, HashType.CRC64));
		}
	}
}