using System.Numerics;
using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.HashAlgorithms;

/// <summary>
/// Computes the <see cref="MD4" /> hash for the input data.
/// </summary>
public sealed class MD4 : HashAlgorithm
{
	private uint CurrentHashA;
	private uint CurrentHashB;
	private uint CurrentHashC;
	private uint CurrentHashD;
	private BigInteger HashedLength;
	private uint[] Buffer = null!;
	private byte[] LastBlock = null!;
	private int LastBlockLength;

	/// <summary>
	/// Initializes a new instance of the <see cref="MD4" /> class.
	/// </summary>
	public MD4()
	{
		HashSizeValue = 128;
		Initialize();
	}
	/// <summary>
	/// Creates an instance of the default implementation of <see cref="MD4" />.
	/// </summary>
	/// <returns>
	/// A new instance of <see cref="MD4" />.
	/// </returns>
	public static new MD4 Create()
	{
		return new();
	}

	/// <summary>
	/// Resets the hash algorithm to its initial state.
	/// </summary>
	public sealed override void Initialize()
	{
		CurrentHashA = 0x67452301;
		CurrentHashB = 0xefcdab89;
		CurrentHashC = 0x98badcfe;
		CurrentHashD = 0x10325476;
		HashedLength = 0;
		Buffer = new uint[16];
		LastBlock = new byte[64];
		LastBlockLength = 0;
	}
	/// <summary>
	/// Routes data written to the object into the hash algorithm for computing the hash.
	/// </summary>
	/// <param name="array">The input to compute the hash code for.</param>
	/// <param name="ibStart">The offset into the <see cref="byte" />[] from which to begin using data.</param>
	/// <param name="cbSize">The number of bytes in the <see cref="byte" />[] to use as data.</param>
	protected sealed override void HashCore(byte[] array, int ibStart, int cbSize)
	{
		HashedLength += cbSize;

		if (LastBlockLength > 0)
		{
			int remaining = 64 - LastBlockLength;
			if (cbSize >= remaining)
			{
				Array.Copy(array, ibStart, LastBlock, LastBlockLength, remaining);

				Process(LastBlock, 0, 64);
				ibStart += remaining;
				cbSize -= remaining;
				LastBlockLength = 0;
			}
		}

		while (cbSize >= 64)
		{
			Process(array, ibStart, 64);
			ibStart += 64;
			cbSize -= 64;
		}

		if (cbSize > 0)
		{
			Array.Copy(array, ibStart, LastBlock, LastBlockLength, cbSize);
			LastBlockLength += cbSize;
		}
	}
	/// <summary>
	/// Finalizes the hash computation after the last data is processed by the cryptographic hash algorithm.
	/// </summary>
	/// <returns>
	/// A <see cref="byte" />[] with the final hash value.
	/// </returns>
	protected sealed override byte[] HashFinal()
	{
		byte[] padding = GetPadding(LastBlock.AsSpan(0, LastBlockLength));

		for (int i = 0; i < padding.Length; i += 64)
		{
			Process(padding, i, 64);
		}

		byte[] hashBuffer = new byte[16];
		System.Buffer.BlockCopy(BitConverter.GetBytes(CurrentHashA), 0, hashBuffer, 0, 4);
		System.Buffer.BlockCopy(BitConverter.GetBytes(CurrentHashB), 0, hashBuffer, 4, 4);
		System.Buffer.BlockCopy(BitConverter.GetBytes(CurrentHashC), 0, hashBuffer, 8, 4);
		System.Buffer.BlockCopy(BitConverter.GetBytes(CurrentHashD), 0, hashBuffer, 12, 4);

		HashValue = hashBuffer;
		return hashBuffer;
	}
	private void Process(byte[] block, int offset, int length)
	{
		System.Buffer.BlockCopy(block, offset, Buffer, 0, length);

		uint a = CurrentHashA;
		uint b = CurrentHashB;
		uint c = CurrentHashC;
		uint d = CurrentHashD;

		for (int i = 0; i < 16; i += 4)
		{
			a = uint.RotateLeft(a + Buffer[i + 0] + F(b, c, d), 3);
			d = uint.RotateLeft(d + Buffer[i + 1] + F(a, b, c), 7);
			c = uint.RotateLeft(c + Buffer[i + 2] + F(d, a, b), 11);
			b = uint.RotateLeft(b + Buffer[i + 3] + F(c, d, a), 19);
		}

		for (int i = 16, j = 0; i < 32; i += 4, j++)
		{
			a = uint.RotateLeft(a + Buffer[j + 00] + 0x5a827999 + G(b, c, d), 3);
			d = uint.RotateLeft(d + Buffer[j + 04] + 0x5a827999 + G(a, b, c), 5);
			c = uint.RotateLeft(c + Buffer[j + 08] + 0x5a827999 + G(d, a, b), 9);
			b = uint.RotateLeft(b + Buffer[j + 12] + 0x5a827999 + G(c, d, a), 13);
		}

		for (int i = 32, j = 0; i < 48; i += 4, j++)
		{
			int index = (j << 1) + -3 * (j >> 1);

			a = uint.RotateLeft(a + Buffer[index + 00] + 0x6ed9eba1 + H(b, c, d), 3);
			d = uint.RotateLeft(d + Buffer[index + 08] + 0x6ed9eba1 + H(a, b, c), 9);
			c = uint.RotateLeft(c + Buffer[index + 04] + 0x6ed9eba1 + H(d, a, b), 11);
			b = uint.RotateLeft(b + Buffer[index + 12] + 0x6ed9eba1 + H(c, d, a), 15);
		}

		CurrentHashA += a;
		CurrentHashB += b;
		CurrentHashC += c;
		CurrentHashD += d;
	}
	private byte[] GetPadding(ReadOnlySpan<byte> lastBlock)
	{
		int paddingBlocks = lastBlock.Length + 8 > 64 ? 2 : 1;
		byte[] padding = new byte[paddingBlocks * 64];

		lastBlock.CopyTo(padding);

		padding[lastBlock.Length] = 0x80;

		byte[] messageLengthInBits = (HashedLength << 3).ToByteArray();
		int endOffset = padding.Length - 8;
		for (int i = 0; i < messageLengthInBits.Length; i++)
		{
			padding[endOffset + i] = messageLengthInBits[i];
		}

		return padding;
	}
	private static uint F(uint x, uint y, uint z)
	{
		return x & y | ~x & z;
	}
	private static uint G(uint x, uint y, uint z)
	{
		return x & y | x & z | y & z;
	}
	private static uint H(uint x, uint y, uint z)
	{
		return x ^ y ^ z;
	}
}