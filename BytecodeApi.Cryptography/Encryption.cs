using BytecodeApi.Extensions;
using System.Security.Cryptography;

namespace BytecodeApi.Cryptography;

/// <summary>
/// Class to encrypt and decrypt data using AES-128 or AES-256.
/// </summary>
public static class Encryption
{
	/// <summary>
	/// Generates a 128-bit initialization vector for symmetric algorithms. The result is a <see cref="byte" />[] with a length of 16.
	/// </summary>
	/// <returns>
	/// A <see cref="byte" />[] with a length of 16, representing a 128-bit initialization vector.
	/// </returns>
	public static byte[] GenerateIV()
	{
		using Aes aes = Aes.Create();
		return aes.IV;
	}
	/// <summary>
	/// Generates a 128-bit or 256-bit key for symmetric algorithms. The result is a <see cref="byte" />[] with a length of 16 or 32.
	/// </summary>
	/// <param name="aes256"><see langword="true" /> to return a 256-bit key, <see langword="false" /> to return a 128-bit key.</param>
	/// <returns>
	/// A <see cref="byte" />[] with a length of 16 or 32, representing a 128-bit or 256-bit key.
	/// </returns>
	public static byte[] GenerateKey(bool aes256)
	{
		using Aes aes = Aes.Create();
		return aes.Key.GetBytes(0, aes256 ? 32 : 16);
	}
	/// <summary>
	/// Encrypts the specified <see cref="byte" />[] using the specified IV and key and returns a <see cref="byte" />[] representing the encrypted version of <paramref name="data" />.
	/// </summary>
	/// <param name="data">A <see cref="byte" />[] with the data to be encrypted.</param>
	/// <param name="iv">A <see cref="byte" />[] with the IV that is used to encrypt <paramref name="data" />. This array must have 16 elements.</param>
	/// <param name="key">A <see cref="byte" />[] with the key that is used to encrypt <paramref name="data" />. This array must have 16 or 32 elements.</param>
	/// <returns>
	/// A new <see cref="byte" />[] representing the encrypted version of <paramref name="data" />.
	/// </returns>
	public static byte[] Encrypt(byte[] data, byte[] iv, byte[] key)
	{
		Check.ArgumentNull(data);
		Check.ArgumentNull(iv);
		Check.Argument(iv.Length == 16, nameof(iv), "Array must be a 128-bit sized byte array (16 bytes).");
		Check.ArgumentNull(key);
		Check.Argument(key.Length is 16 or 32, nameof(key), "Array must be a 128-bit or 256-bit sized byte array (16 or 32 bytes).");

		using Aes aes = Aes.Create();
		aes.IV = iv;
		aes.Key = key;

		using ICryptoTransform encryptor = aes.CreateEncryptor();
		using MemoryStream memoryStream = new();

		using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
		{
			cryptoStream.Write(data);
		}

		return memoryStream.ToArray();
	}
	/// <summary>
	/// Decrypts the specified <see cref="byte" />[] using the specified IV and key and returns a <see cref="byte" />[] representing the decrypted version of <paramref name="data" />.
	/// </summary>
	/// <param name="data">A <see cref="byte" />[] with the data to be decrypted.</param>
	/// <param name="iv">A <see cref="byte" />[] with the IV that is used to decrypt <paramref name="data" />. This array must have 16 elements.</param>
	/// <param name="key">A <see cref="byte" />[] with the key that is used to decrypt <paramref name="data" />. This array must have 16 or 32 elements.</param>
	/// <returns>
	/// A new <see cref="byte" />[] representing the decrypted version of <paramref name="data" />.
	/// </returns>
	public static byte[] Decrypt(byte[] data, byte[] iv, byte[] key)
	{
		Check.ArgumentNull(data);
		Check.ArgumentNull(iv);
		Check.Argument(iv.Length == 16, nameof(iv), "Array must be a 128-bit sized byte array (16 bytes).");
		Check.ArgumentNull(key);
		Check.Argument(key.Length is 16 or 32, nameof(key), "Array must be a 128-bit or 256-bit sized byte array (16 or 32 bytes).");

		using Aes aes = Aes.Create();
		aes.IV = iv;
		aes.Key = key;

		using ICryptoTransform decryptor = aes.CreateDecryptor();
		using MemoryStream memoryStream = new();

		using (CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Write))
		{
			cryptoStream.Write(data);
		}

		return memoryStream.ToArray();
	}
}