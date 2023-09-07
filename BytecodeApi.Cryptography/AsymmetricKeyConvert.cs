using System.Security.Cryptography;

namespace BytecodeApi.Cryptography;

/// <summary>
/// Class that converts between different RSA key formats, i.e. DER, PEM and the <see cref="RSAParameters" /> structure. Public keys are handled using the PKCS#8 format and private using the PKCS#1 format.
/// </summary>
public static class AsymmetricKeyConvert
{
	//TODO: PEM & DER export in both X.509 and RSA
	/// <summary>
	/// Converts a public or private RSA key, represented in DER format to its equivalent <see cref="RSAParameters" /> structure.
	/// </summary>
	/// <param name="derKey">A <see cref="byte" />[] with the DER key in its binary representation.</param>
	/// <returns>
	/// A new <see cref="RSAParameters" /> structure with the converted key.
	/// </returns>
	public static RSAParameters ToKey(byte[] derKey)
	{
		Check.ArgumentNull(derKey);

		using RSACryptoServiceProvider rsa = new();

		try
		{
			rsa.ImportRSAPrivateKey(derKey, out _);
			return rsa.ExportParameters(true);
		}
		catch
		{
			try
			{
				rsa.ImportSubjectPublicKeyInfo(derKey, out _);
			}
			catch
			{
				rsa.ImportRSAPublicKey(derKey, out _);
			}

			return rsa.ExportParameters(false);
		}
	}
	/// <summary>
	/// Converts a public or private RSA key, represented in PEM format to its equivalent <see cref="RSAParameters" /> structure.
	/// </summary>
	/// <param name="pemKey">A <see cref="string" /> value with the PEM key.</param>
	/// <returns>
	/// A new <see cref="RSAParameters" /> structure with the converted key.
	/// </returns>
	public static RSAParameters ToKey(string pemKey)
	{
		Check.ArgumentNull(pemKey);

		using RSACryptoServiceProvider rsa = new();
		rsa.ImportFromPem(pemKey);
		return rsa.ExportParameters(!rsa.PublicOnly);
	}
	/// <summary>
	/// Converts a public or private RSA key from a <see cref="RSAParameters" /> structure to its equivalent DER representation.
	/// </summary>
	/// <param name="key">A <see cref="RSAParameters" /> structure with the key.</param>
	/// <returns>
	/// A new <see cref="byte" />[] with the converted key.
	/// </returns>
	public static byte[] ToDer(RSAParameters key)
	{
		using RSACryptoServiceProvider rsa = new();
		rsa.ImportParameters(key);
		return rsa.PublicOnly ? rsa.ExportSubjectPublicKeyInfo() : rsa.ExportRSAPrivateKey();
	}
	/// <summary>
	/// Converts a public or private RSA key, represented in PEM format to its equivalent DER representation.
	/// </summary>
	/// <param name="pemKey">A <see cref="string" /> value with the PEM key.</param>
	/// <returns>
	/// A new <see cref="byte" />[] with the converted key.
	/// </returns>
	public static byte[] ToDer(string pemKey)
	{
		Check.ArgumentNull(pemKey);

		return ToDer(ToKey(pemKey));
	}
	/// <summary>
	/// Converts a public or private RSA key from a <see cref="RSAParameters" /> structure to its equivalent PEM representation.
	/// </summary>
	/// <param name="key">A <see cref="RSAParameters" /> structure with the key.</param>
	/// <returns>
	/// The equivalent PEM key, represented as a <see cref="string" />.
	/// </returns>
	public static string ToPem(RSAParameters key)
	{
		using RSACryptoServiceProvider rsa = new();
		rsa.ImportParameters(key);
		return rsa.PublicOnly ? rsa.ExportSubjectPublicKeyInfoPem() : rsa.ExportRSAPrivateKeyPem();
	}
	/// <summary>
	/// Converts a public or private RSA key, represented in DER format to its equivalent PEM representation.
	/// </summary>
	/// <param name="derKey">A <see cref="byte" />[] with the DER key in its binary representation.</param>
	/// <returns>
	/// The equivalent PEM key, represented as a <see cref="string" />.
	/// </returns>
	public static string ToPem(byte[] derKey)
	{
		Check.ArgumentNull(derKey);

		return ToPem(ToKey(derKey));
	}
}