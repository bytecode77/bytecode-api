using System.ComponentModel;

namespace BytecodeApi.Cryptography;

/// <summary>
/// Specifies a hash algorithm.
/// </summary>
public enum HashType
{
	/// <summary>
	/// The Adler-32 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "00000001".</para>
	/// </summary>
	[Description("Adler-32")]
	Adler32,
	/// <summary>
	/// The CRC32 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "00000000".</para>
	/// </summary>
	[Description("CRC32")]
	CRC32,
	/// <summary>
	/// The CRC64 ECMA 182 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "0000000000000000".</para>
	/// </summary>
	[Description("CRC64")]
	CRC64,
	/// <summary>
	/// The MD2 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "8350e5a3e24c153df2275c9f80692773".</para>
	/// </summary>
	[Description("MD2")]
	MD2,
	/// <summary>
	/// The MD4 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "31d6cfe0d16ae931b73c59d7e0c089c0".</para>
	/// </summary>
	[Description("MD4")]
	MD4,
	/// <summary>
	/// The MD5 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "d41d8cd98f00b204e9800998ecf8427e".</para>
	/// </summary>
	[Description("MD5")]
	MD5,
	/// <summary>
	/// The RIPEMD-128 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "cdf26213a150dc3ecb610f18f6b38b46".</para>
	/// </summary>
	[Description("RIPEMD-128")]
	RIPEMD128,
	/// <summary>
	/// The RIPEMD-160 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "9c1185a5c5e9fc54612808977ee8f548b2258d31".</para>
	/// </summary>
	[Description("RIPEMD-160")]
	RIPEMD160,
	/// <summary>
	/// The SHA-1 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "da39a3ee5e6b4b0d3255bfef95601890afd80709".</para>
	/// </summary>
	[Description("SHA-1")]
	SHA1,
	/// <summary>
	/// The SHA-224 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "d14a028c2a3a2bc9476102bb288234c415a2b01f828ea62ac5b3e42f".</para>
	/// </summary>
	[Description("SHA-224")]
	SHA224,
	/// <summary>
	/// The SHA-256 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855".</para>
	/// </summary>
	[Description("SHA-256")]
	SHA256,
	/// <summary>
	/// The SHA-384 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "38b060a751ac96384cd9327eb1b1e36a21fdb71114be07434c0cc7bf63f6e1da274edebfe76f65fbd51ad2f14898b95b".</para>
	/// </summary>
	[Description("SHA-384")]
	SHA384,
	/// <summary>
	/// The SHA-512 algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "cf83e1357eefb8bdf1542850d66d8007d620e4050b5715dc83f4a921d36ce9ce47d0d13c5d85f2b0ff8318d2877eec2f63b931bd47417a81a538327af927da3e".</para>
	/// </summary>
	[Description("SHA-512")]
	SHA512,
	/// <summary>
	/// The Tiger algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "3293ac630c13f0245f92bbb1766e16167a4e58492dde73f3".</para>
	/// </summary>
	[Description("Tiger")]
	Tiger,
	/// <summary>
	/// The Tiger algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "4441be75f6018773c206c22745374b924aa8313fef919f41".</para>
	/// </summary>
	[Description("Tiger2")]
	Tiger2,
	/// <summary>
	/// The Whirlpool algorithm is used.
	/// <para>A <see cref="string.Empty" /> hashed by this algorithm will return "19fa61d75522a4669b44e39c1d2e1726c530232130d407f89afee0964997f7a73e83be698b288febcf88e3e03c4f0757ea8964e59b63d93708b138cc42a66eb3".</para>
	/// </summary>
	[Description("Whirlpool")]
	Whirlpool
}