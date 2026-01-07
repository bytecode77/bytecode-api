using SharpHash.Base;
using SharpHash.Interfaces;
using System.ComponentModel;
using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.Tests;

public class HashesTests
{
	[Theory]
	[InlineData(HashType.Adler32, "", "00000001")]
	[InlineData(HashType.Adler32, "Hello, world!", "205e048a")]
	[InlineData(HashType.Adler32, "The quick brown fox jumps over the lazy dog", "5bdc0fda")]
	[InlineData(HashType.CRC32, "", "00000000")]
	[InlineData(HashType.CRC32, "Hello, world!", "ebe6c6e6")]
	[InlineData(HashType.CRC32, "The quick brown fox jumps over the lazy dog", "414fa339")]
	[InlineData(HashType.CRC64, "", "0000000000000000")]
	[InlineData(HashType.CRC64, "Hello, world!", "1f73c2abcf65431e")]
	[InlineData(HashType.CRC64, "The quick brown fox jumps over the lazy dog", "41e05242ffa9883b")]
	[InlineData(HashType.MD2, "", "8350e5a3e24c153df2275c9f80692773")]
	[InlineData(HashType.MD2, "Hello, world!", "8cca0e965edd0e223b744f9cedf8e141")]
	[InlineData(HashType.MD2, "The quick brown fox jumps over the lazy dog", "03d85a0d629d2c442e987525319fc471")]
	[InlineData(HashType.MD4, "", "31d6cfe0d16ae931b73c59d7e0c089c0")]
	[InlineData(HashType.MD4, "Hello, world!", "0abe9ee1f376caa1bcecad9042f16e73")]
	[InlineData(HashType.MD4, "The quick brown fox jumps over the lazy dog", "1bee69a46ba811185c194762abaeae90")]
	[InlineData(HashType.MD5, "", "d41d8cd98f00b204e9800998ecf8427e")]
	[InlineData(HashType.MD5, "Hello, world!", "6cd3556deb0da54bca060b4c39479839")]
	[InlineData(HashType.MD5, "The quick brown fox jumps over the lazy dog", "9e107d9d372bb6826bd81d3542a419d6")]
	[InlineData(HashType.RIPEMD128, "", "cdf26213a150dc3ecb610f18f6b38b46")]
	[InlineData(HashType.RIPEMD128, "Hello, world!", "3cbb446fc20277b2a4e4b8b8b40aa962")]
	[InlineData(HashType.RIPEMD128, "The quick brown fox jumps over the lazy dog", "3fa9b57f053c053fbe2735b2380db596")]
	[InlineData(HashType.RIPEMD160, "", "9c1185a5c5e9fc54612808977ee8f548b2258d31")]
	[InlineData(HashType.RIPEMD160, "Hello, world!", "58262d1fbdbe4530d8865d3518c6d6e41002610f")]
	[InlineData(HashType.RIPEMD160, "The quick brown fox jumps over the lazy dog", "37f332f68db77bd9d7edd4969571ad671cf9dd3b")]
	[InlineData(HashType.SHA1, "", "da39a3ee5e6b4b0d3255bfef95601890afd80709")]
	[InlineData(HashType.SHA1, "Hello, world!", "943a702d06f34599aee1f8da8ef9f7296031d699")]
	[InlineData(HashType.SHA1, "The quick brown fox jumps over the lazy dog", "2fd4e1c67a2d28fced849ee1bb76e7391b93eb12")]
	[InlineData(HashType.SHA224, "", "d14a028c2a3a2bc9476102bb288234c415a2b01f828ea62ac5b3e42f")]
	[InlineData(HashType.SHA224, "Hello, world!", "8552d8b7a7dc5476cb9e25dee69a8091290764b7f2a64fe6e78e9568")]
	[InlineData(HashType.SHA224, "The quick brown fox  over the lazy dog", "e67581164cd0e855e8175d5d0920edb907a9980d686ed0ef19b37fb8")]
	[InlineData(HashType.SHA256, "", "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")]
	[InlineData(HashType.SHA256, "Hello, world!", "315f5bdb76d078c43b8ac0064e4a0164612b1fce77c869345bfc94c75894edd3")]
	[InlineData(HashType.SHA256, "The quick brown fox jumps over the lazy dog", "d7a8fbb307d7809469ca9abcb0082e4f8d5651e46d3cdb762d02d0bf37c9e592")]
	[InlineData(HashType.SHA384, "", "38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b")]
	[InlineData(HashType.SHA384, "Hello, world!", "55bc556b0d2fe0fce582ba5fe07baafff035653638c7ac0d5494c2a64c0bea1cc57331c7c12a45cdbca7f4c34a089eeb")]
	[InlineData(HashType.SHA384, "The quick brown fox jumps over the lazy dog", "ca737f1014a48f4c0b6dd43cb177b0afd9e5169367544c494011e3317dbf9a509cb1e5dc1e85a941bbee3d7f2afbc9b1")]
	[InlineData(HashType.SHA512, "", "cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e")]
	[InlineData(HashType.SHA512, "Hello, world!", "c1527cd893c124773d811911970c8fe6e857d6df5dc9226bd8a160614c0cd963a4ddea2b94bb7d36021ef9d865d5cea294a82dd49a0bb269f51f6e7a57f79421")]
	[InlineData(HashType.SHA512, "The quick brown fox jumps over the lazy dog", "07e547d9586f6a73f73fbac0435ed76951218fb7d0c8d788a309d785436bbb642e93a252a954f23912547d1e8a3b5ed6e1bfd7097821233fa0538f3db854fee6")]
	[InlineData(HashType.Tiger, "", "3293ac630c13f0245f92bbb1766e16167a4e58492dde73f3")]
	[InlineData(HashType.Tiger, "Hello, world!", "b5e5dd73a5894236937084131bb845189cdc5477579b9f36")]
	[InlineData(HashType.Tiger, "The quick brown fox jumps over the lazy dog", "6d12a41e72e644f017b6f0e2f7b44c6285f06dd5d2c5b075")]
	[InlineData(HashType.Tiger2, "", "4441be75f6018773c206c22745374b924aa8313fef919f41")]
	[InlineData(HashType.Tiger2, "Hello, world!", "5d76a0e497c8cb50616ce102d7c0d9d4c5e6260b1e8bac4e")]
	[InlineData(HashType.Tiger2, "The quick brown fox jumps over the lazy dog", "976abff8062a2e9dcea3a1ace966ed9c19cb85558b4976d8")]
	[InlineData(HashType.Whirlpool, "", "19fa61d75522a4669b44e39c1d2e1726c530232130d407f89afee0964997f7a73e83be698b288febcf88e3e03c4f0757ea8964e59b63d93708b138cc42a66eb3")]
	[InlineData(HashType.Whirlpool, "Hello, world!", "a1a8703be5312b139b42eb331aa800ccaca0c34d58c6988e44f45489cfb16beb4b6bf0ce20be1db22a10b0e4bb680480a3d2429e6c483085453c098b65852495")]
	[InlineData(HashType.Whirlpool, "The quick brown fox jumps over the lazy dog", "b97de512e91e3828b40d2b0fdce9ceb3c4a71f9bea8d88e75c4fa854df36725fd2b52eb6544edcacd6f8beddfea403cb55ae31f03ad62a5ef54e42ee82c3fb35")]
	public void Hashes_Compute(HashType hashType, string str, string expected)
	{
		Assert.Equal(expected, Hashes.Compute(str, hashType));
	}
	[Theory]
	[InlineData(HashType.SHA256, "", 1, "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855")]
	[InlineData(HashType.SHA256, "", 2, "5df6e0e2761359d30a8275058e299fcc0381534545f55cf43e41983f5d4c9456")]
	[InlineData(HashType.SHA256, "", 3, "aa6ac2d4961882f42a345c7615f4133dde8e6d6e7c1b6b40ae4ff6ee52c393d0")]
	public void Hashes_Compute_WithPasses(HashType hashType, string str, int passes, string expected)
	{
		Assert.Equal(expected, Hashes.Compute(str, hashType, passes));
	}

	[Theory]
	[InlineData(HashType.Adler32)]
	[InlineData(HashType.CRC32)]
	[InlineData(HashType.CRC64)]
	[InlineData(HashType.MD2)]
	[InlineData(HashType.MD4)]
	[InlineData(HashType.RIPEMD128)]
	[InlineData(HashType.RIPEMD160)]
	[InlineData(HashType.SHA224)]
	[InlineData(HashType.Tiger)]
	[InlineData(HashType.Tiger2)]
	[InlineData(HashType.Whirlpool)]
	public void CrossCompare(HashType hashType)
	{
		// Use SharpHash to cross-test hashing algorithms.
		IHash hash = hashType switch
		{
			HashType.Adler32 => HashFactory.Checksum.CreateAdler32(),
			HashType.CRC32 => HashFactory.Checksum.CreateCRC32_PKZIP(),
			HashType.CRC64 => HashFactory.Checksum.CreateCRC64_ECMA_182(),
			HashType.MD2 => HashFactory.Crypto.CreateMD2(),
			HashType.MD4 => HashFactory.Crypto.CreateMD4(),
			HashType.RIPEMD128 => HashFactory.Crypto.CreateRIPEMD128(),
			HashType.RIPEMD160 => HashFactory.Crypto.CreateRIPEMD160(),
			HashType.SHA224 => HashFactory.Crypto.CreateSHA2_224(),
			HashType.Tiger => HashFactory.Crypto.CreateTiger(24, HashRounds.Rounds3),
			HashType.Tiger2 => HashFactory.Crypto.CreateTiger2(24, HashRounds.Rounds3),
			HashType.Whirlpool => HashFactory.Crypto.CreateWhirlPool(),
			_ => throw new InvalidEnumArgumentException()
		};

		for (int i = 1; i < 10000; i++)
		{
			int length = Random.Shared.Next(1, 1000);
			byte[] data = new byte[length];
			RandomNumberGenerator.Fill(data);

			Assert.Equal(hash.ComputeBytes(data).ToString(), Hashes.Compute(data, hashType), true);
		}
	}
}