using System.Security.Cryptography;

namespace BytecodeApi.Cryptography
{
	/// <summary>
	/// Class to encrypt and decrypt data using RSA. The length of the encrypted data is limited. For larger amounts of data, the <see cref="AsymmetricContentEncryption" /> class is more suitable.
	/// </summary>
	public static class AsymmetricEncryption
	{
		/// <summary>
		/// Generates a new public and private key pair.
		/// </summary>
		/// <param name="publicKey">When this method returns, an <see cref="RSAParameters" /> structure with the public key information.</param>
		/// <param name="privateKey">When this method returns, an <see cref="RSAParameters" /> structure with the private key information.</param>
		public static void GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey)
		{
			using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			publicKey = rsa.ExportParameters(false);
			privateKey = rsa.ExportParameters(true);
		}
		/// <summary>
		/// Encrypts the specified <see cref="byte" />[] using RSA and the specified public key.
		/// </summary>
		/// <param name="data">A <see cref="byte" />[] with the data to be encrypted.</param>
		/// <param name="derKey">A <see cref="byte" />[] containing the public key in its DER representation.</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing the encrypted version of <paramref name="data" />.
		/// </returns>
		public static byte[] Encrypt(byte[] data, byte[] derKey)
		{
			Check.ArgumentNull(data, nameof(data));
			Check.ArgumentNull(derKey, nameof(derKey));

			return Encrypt(data, AsymmetricKeyConvert.ToKey(derKey));
		}
		/// <summary>
		/// Encrypts the specified <see cref="byte" />[] using RSA and the specified public key.
		/// </summary>
		/// <param name="data">A <see cref="byte" />[] with the data to be encrypted.</param>
		/// <param name="pemKey">A <see cref="string" /> value with the public key in its PEM representation, starting with "-----BEGIN PUBLIC KEY-----".</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing the encrypted version of <paramref name="data" />.
		/// </returns>
		public static byte[] Encrypt(byte[] data, string pemKey)
		{
			Check.ArgumentNull(data, nameof(data));
			Check.ArgumentNull(pemKey, nameof(pemKey));

			return Encrypt(data, AsymmetricKeyConvert.ToKey(pemKey));
		}
		/// <summary>
		/// Encrypts the specified <see cref="byte" />[] using RSA and the specified public key.
		/// </summary>
		/// <param name="data">A <see cref="byte" />[] with the data to be encrypted.</param>
		/// <param name="key">An <see cref="RSAParameters" /> value containing the public key information.</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing the encrypted version of <paramref name="data" />.
		/// </returns>
		public static byte[] Encrypt(byte[] data, RSAParameters key)
		{
			Check.ArgumentNull(data, nameof(data));

			using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.ImportParameters(key);
			return rsa.Encrypt(data, true);
		}
		/// <summary>
		/// Decrypts the specified <see cref="byte" />[] using RSA and the specified private key.
		/// </summary>
		/// <param name="data">A <see cref="byte" />[] with the data to be decrypted.</param>
		/// <param name="derKey">A <see cref="byte" />[] containing the private key in its DER representation.</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing the decrypted version of <paramref name="data" />.
		/// </returns>
		public static byte[] Decrypt(byte[] data, byte[] derKey)
		{
			Check.ArgumentNull(data, nameof(data));
			Check.ArgumentNull(derKey, nameof(derKey));

			return Decrypt(data, AsymmetricKeyConvert.ToKey(derKey));
		}
		/// <summary>
		/// Decrypts the specified <see cref="byte" />[] using RSA and the specified private key.
		/// </summary>
		/// <param name="data">A <see cref="byte" />[] with the data to be decrypted.</param>
		/// <param name="pemKey">A <see cref="string" /> value with the private key in its PEM representation, starting with "-----BEGIN RSA PRIVATE KEY-----".</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing the decrypted version of <paramref name="data" />.
		/// </returns>
		public static byte[] Decrypt(byte[] data, string pemKey)
		{
			Check.ArgumentNull(data, nameof(data));
			Check.ArgumentNull(pemKey, nameof(pemKey));

			return Decrypt(data, AsymmetricKeyConvert.ToKey(pemKey));
		}
		/// <summary>
		/// Decrypts the specified <see cref="byte" />[] using RSA and the specified private key.
		/// </summary>
		/// <param name="data">A <see cref="byte" />[] with the data to be decrypted.</param>
		/// <param name="key">An <see cref="RSAParameters" /> value containing the private key information.</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing the decrypted version of <paramref name="data" />.
		/// </returns>
		public static byte[] Decrypt(byte[] data, RSAParameters key)
		{
			Check.ArgumentNull(data, nameof(data));

			using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
			rsa.ImportParameters(key);
			return rsa.Decrypt(data, true);
		}
	}
}