using BytecodeApi.Cryptography.Tests.Comparers;
using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.Tests;

public class AsymmetricKeyConvertTests
{
	[Fact]
	public void AsymmetricKeyConvert_ToKey()
	{
		AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);
		byte[] publicDer = AsymmetricKeyConvert.ToDer(publicKey);
		byte[] privateDer = AsymmetricKeyConvert.ToDer(privateKey);
		string publicPem = AsymmetricKeyConvert.ToPem(publicKey);
		string privatePem = AsymmetricKeyConvert.ToPem(privateKey);

		Assert.Equal(publicKey, AsymmetricKeyConvert.ToKey(publicDer), new RSAParametersEqualityComparer());
		Assert.Equal(privateKey, AsymmetricKeyConvert.ToKey(privateDer), new RSAParametersEqualityComparer());
		Assert.Equal(publicKey, AsymmetricKeyConvert.ToKey(publicPem), new RSAParametersEqualityComparer());
		Assert.Equal(privateKey, AsymmetricKeyConvert.ToKey(privatePem), new RSAParametersEqualityComparer());
	}
	[Fact]
	public void AsymmetricKeyConvert_ToDer()
	{
		AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);
		string publicPem = AsymmetricKeyConvert.ToPem(publicKey);
		string privatePem = AsymmetricKeyConvert.ToPem(privateKey);

		Assert.Equal(publicKey, AsymmetricKeyConvert.ToKey(AsymmetricKeyConvert.ToDer(publicKey)), new RSAParametersEqualityComparer());
		Assert.Equal(privateKey, AsymmetricKeyConvert.ToKey(AsymmetricKeyConvert.ToDer(privateKey)), new RSAParametersEqualityComparer());
		Assert.Equal(publicKey, AsymmetricKeyConvert.ToKey(AsymmetricKeyConvert.ToDer(publicPem)), new RSAParametersEqualityComparer());
		Assert.Equal(privateKey, AsymmetricKeyConvert.ToKey(AsymmetricKeyConvert.ToDer(privatePem)), new RSAParametersEqualityComparer());
	}
	[Fact]
	public void AsymmetricKeyConvert_ToPem()
	{
		AsymmetricEncryption.GenerateKeyPair(out RSAParameters publicKey, out RSAParameters privateKey);
		byte[] publicDer = AsymmetricKeyConvert.ToDer(publicKey);
		byte[] privateDer = AsymmetricKeyConvert.ToDer(privateKey);

		Assert.Equal(publicKey, AsymmetricKeyConvert.ToKey(AsymmetricKeyConvert.ToPem(publicKey)), new RSAParametersEqualityComparer());
		Assert.Equal(privateKey, AsymmetricKeyConvert.ToKey(AsymmetricKeyConvert.ToPem(privateKey)), new RSAParametersEqualityComparer());
		Assert.Equal(publicKey, AsymmetricKeyConvert.ToKey(AsymmetricKeyConvert.ToPem(publicDer)), new RSAParametersEqualityComparer());
		Assert.Equal(privateKey, AsymmetricKeyConvert.ToKey(AsymmetricKeyConvert.ToPem(privateDer)), new RSAParametersEqualityComparer());
	}
}