using System.Numerics;
using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.HashAlgorithms;

/// <summary>
/// Computes the <see cref="RIPEMD128" /> hash for the input data.
/// </summary>
public sealed class RIPEMD128 : HashAlgorithm
{
	private readonly uint[] CurrentHash;
	private readonly uint[] CurrentChunk;
	private readonly byte[] UnhashedBuffer;
	private long HashedLength;
	private int UnhashedBufferLength;

	/// <summary>
	/// Initializes a new instance of the <see cref="RIPEMD128" /> class.
	/// </summary>
	public RIPEMD128()
	{
		HashSizeValue = 128;

		CurrentHash = new uint[4];
		CurrentChunk = new uint[16];
		UnhashedBuffer = new byte[64];

		Initialize();
	}
	/// <summary>
	/// Creates an instance of the default implementation of <see cref="RIPEMD128" />.
	/// </summary>
	/// <returns>
	/// A new instance of <see cref="RIPEMD128" />.
	/// </returns>
	public static new RIPEMD128 Create()
	{
		return new();
	}

	/// <summary>
	/// Resets the hash algorithm to its initial state.
	/// </summary>
	public sealed override void Initialize()
	{
		CurrentHash[0] = 0x67452301;
		CurrentHash[1] = 0xefcdab89;
		CurrentHash[2] = 0x98badcfe;
		CurrentHash[3] = 0x10325476;
		Array.Clear(CurrentChunk);
		Array.Clear(UnhashedBuffer);
		HashedLength = 0;
		UnhashedBufferLength = 0;
	}
	/// <summary>
	/// Routes data written to the object into the hash algorithm for computing the hash.
	/// </summary>
	/// <param name="array">The input to compute the hash code for.</param>
	/// <param name="ibStart">The offset into the <see cref="byte" />[] from which to begin using data.</param>
	/// <param name="cbSize">The number of bytes in the <see cref="byte" />[] to use as data.</param>
	protected sealed override void HashCore(byte[] array, int ibStart, int cbSize)
	{
		for (int i = 0; i < cbSize;)
		{
			int bytesRemaining = cbSize - i;

			if (UnhashedBufferLength > 0)
			{
				if (bytesRemaining + UnhashedBufferLength >= UnhashedBuffer.Length)
				{
					Array.Copy(array, ibStart + i, UnhashedBuffer, UnhashedBufferLength, UnhashedBuffer.Length - UnhashedBufferLength);
					i += UnhashedBuffer.Length - UnhashedBufferLength;
					UnhashedBufferLength = UnhashedBuffer.Length;

					for (int j = 0; j < 16; j++)
					{
						CurrentChunk[j] = BitConverter.ToUInt32(UnhashedBuffer, j * 4);
					}

					Process(CurrentChunk);
					UnhashedBufferLength = 0;
				}
				else
				{
					Array.Copy(array, ibStart + i, UnhashedBuffer, UnhashedBufferLength, bytesRemaining);
					UnhashedBufferLength += bytesRemaining;
					i += bytesRemaining;
				}
			}
			else
			{
				if (bytesRemaining >= UnhashedBuffer.Length)
				{
					for (int j = 0; j < 16; j++)
					{
						CurrentChunk[j] = BitConverter.ToUInt32(array, i + j * 4);
					}

					i += UnhashedBuffer.Length;
					Process(CurrentChunk);
				}
				else
				{
					Array.Copy(array, ibStart + i, UnhashedBuffer, 0, bytesRemaining);
					UnhashedBufferLength = bytesRemaining;
					i += bytesRemaining;
				}
			}
		}

		HashedLength += cbSize;
	}
	/// <summary>
	/// Finalizes the hash computation after the last data is processed by the cryptographic hash algorithm.
	/// </summary>
	/// <returns>
	/// A <see cref="byte" />[] with the final hash value.
	/// </returns>
	protected sealed override byte[] HashFinal()
	{
		uint hashedLength = (uint)HashedLength;
		uint[] chunk = new uint[16];

		for (int i = 0; i < (hashedLength & 63); i++)
		{
			chunk[i >> 2] ^= (uint)UnhashedBuffer[i] << (8 * (i & 3));
		}

		chunk[(hashedLength >> 2) & 15] ^= 1u << (int)(8 * (hashedLength & 3) + 7);

		if ((hashedLength & 63) > 55)
		{
			Process(chunk);
			chunk = new uint[16];
		}

		chunk[14] = hashedLength << 3;
		chunk[15] = hashedLength >> 29;
		Process(chunk);

		byte[] hashBuffer = new byte[16];
		for (int i = 0; i < 4; i++)
		{
			Buffer.BlockCopy(BitConverter.GetBytes(CurrentHash[i]), 0, hashBuffer, i * 4, 4);
		}

		HashValue = hashBuffer;
		return HashValue;
	}

	private void Process(uint[] chunk)
	{
		uint a = CurrentHash[0];
		uint b = CurrentHash[1];
		uint c = CurrentHash[2];
		uint d = CurrentHash[3];
		uint aa = CurrentHash[0];
		uint bb = CurrentHash[1];
		uint cc = CurrentHash[2];
		uint dd = CurrentHash[3];

		FF(ref a, b, c, d, chunk[0], 11);
		FF(ref d, a, b, c, chunk[1], 14);
		FF(ref c, d, a, b, chunk[2], 15);
		FF(ref b, c, d, a, chunk[3], 12);
		FF(ref a, b, c, d, chunk[4], 5);
		FF(ref d, a, b, c, chunk[5], 8);
		FF(ref c, d, a, b, chunk[6], 7);
		FF(ref b, c, d, a, chunk[7], 9);
		FF(ref a, b, c, d, chunk[8], 11);
		FF(ref d, a, b, c, chunk[9], 13);
		FF(ref c, d, a, b, chunk[10], 14);
		FF(ref b, c, d, a, chunk[11], 15);
		FF(ref a, b, c, d, chunk[12], 6);
		FF(ref d, a, b, c, chunk[13], 7);
		FF(ref c, d, a, b, chunk[14], 9);
		FF(ref b, c, d, a, chunk[15], 8);

		GG(ref a, b, c, d, chunk[7], 7);
		GG(ref d, a, b, c, chunk[4], 6);
		GG(ref c, d, a, b, chunk[13], 8);
		GG(ref b, c, d, a, chunk[1], 13);
		GG(ref a, b, c, d, chunk[10], 11);
		GG(ref d, a, b, c, chunk[6], 9);
		GG(ref c, d, a, b, chunk[15], 7);
		GG(ref b, c, d, a, chunk[3], 15);
		GG(ref a, b, c, d, chunk[12], 7);
		GG(ref d, a, b, c, chunk[0], 12);
		GG(ref c, d, a, b, chunk[9], 15);
		GG(ref b, c, d, a, chunk[5], 9);
		GG(ref a, b, c, d, chunk[2], 11);
		GG(ref d, a, b, c, chunk[14], 7);
		GG(ref c, d, a, b, chunk[11], 13);
		GG(ref b, c, d, a, chunk[8], 12);

		HH(ref a, b, c, d, chunk[3], 11);
		HH(ref d, a, b, c, chunk[10], 13);
		HH(ref c, d, a, b, chunk[14], 6);
		HH(ref b, c, d, a, chunk[4], 7);
		HH(ref a, b, c, d, chunk[9], 14);
		HH(ref d, a, b, c, chunk[15], 9);
		HH(ref c, d, a, b, chunk[8], 13);
		HH(ref b, c, d, a, chunk[1], 15);
		HH(ref a, b, c, d, chunk[2], 14);
		HH(ref d, a, b, c, chunk[7], 8);
		HH(ref c, d, a, b, chunk[0], 13);
		HH(ref b, c, d, a, chunk[6], 6);
		HH(ref a, b, c, d, chunk[13], 5);
		HH(ref d, a, b, c, chunk[11], 12);
		HH(ref c, d, a, b, chunk[5], 7);
		HH(ref b, c, d, a, chunk[12], 5);

		II(ref a, b, c, d, chunk[1], 11);
		II(ref d, a, b, c, chunk[9], 12);
		II(ref c, d, a, b, chunk[11], 14);
		II(ref b, c, d, a, chunk[10], 15);
		II(ref a, b, c, d, chunk[0], 14);
		II(ref d, a, b, c, chunk[8], 15);
		II(ref c, d, a, b, chunk[12], 9);
		II(ref b, c, d, a, chunk[4], 8);
		II(ref a, b, c, d, chunk[13], 9);
		II(ref d, a, b, c, chunk[3], 14);
		II(ref c, d, a, b, chunk[7], 5);
		II(ref b, c, d, a, chunk[15], 6);
		II(ref a, b, c, d, chunk[14], 8);
		II(ref d, a, b, c, chunk[5], 6);
		II(ref c, d, a, b, chunk[6], 5);
		II(ref b, c, d, a, chunk[2], 12);

		FFF(ref aa, bb, cc, dd, chunk[5], 8);
		FFF(ref dd, aa, bb, cc, chunk[14], 9);
		FFF(ref cc, dd, aa, bb, chunk[7], 9);
		FFF(ref bb, cc, dd, aa, chunk[0], 11);
		FFF(ref aa, bb, cc, dd, chunk[9], 13);
		FFF(ref dd, aa, bb, cc, chunk[2], 15);
		FFF(ref cc, dd, aa, bb, chunk[11], 15);
		FFF(ref bb, cc, dd, aa, chunk[4], 5);
		FFF(ref aa, bb, cc, dd, chunk[13], 7);
		FFF(ref dd, aa, bb, cc, chunk[6], 7);
		FFF(ref cc, dd, aa, bb, chunk[15], 8);
		FFF(ref bb, cc, dd, aa, chunk[8], 11);
		FFF(ref aa, bb, cc, dd, chunk[1], 14);
		FFF(ref dd, aa, bb, cc, chunk[10], 14);
		FFF(ref cc, dd, aa, bb, chunk[3], 12);
		FFF(ref bb, cc, dd, aa, chunk[12], 6);

		GGG(ref aa, bb, cc, dd, chunk[6], 9);
		GGG(ref dd, aa, bb, cc, chunk[11], 13);
		GGG(ref cc, dd, aa, bb, chunk[3], 15);
		GGG(ref bb, cc, dd, aa, chunk[7], 7);
		GGG(ref aa, bb, cc, dd, chunk[0], 12);
		GGG(ref dd, aa, bb, cc, chunk[13], 8);
		GGG(ref cc, dd, aa, bb, chunk[5], 9);
		GGG(ref bb, cc, dd, aa, chunk[10], 11);
		GGG(ref aa, bb, cc, dd, chunk[14], 7);
		GGG(ref dd, aa, bb, cc, chunk[15], 7);
		GGG(ref cc, dd, aa, bb, chunk[8], 12);
		GGG(ref bb, cc, dd, aa, chunk[12], 7);
		GGG(ref aa, bb, cc, dd, chunk[4], 6);
		GGG(ref dd, aa, bb, cc, chunk[9], 15);
		GGG(ref cc, dd, aa, bb, chunk[1], 13);
		GGG(ref bb, cc, dd, aa, chunk[2], 11);

		HHH(ref aa, bb, cc, dd, chunk[15], 9);
		HHH(ref dd, aa, bb, cc, chunk[5], 7);
		HHH(ref cc, dd, aa, bb, chunk[1], 15);
		HHH(ref bb, cc, dd, aa, chunk[3], 11);
		HHH(ref aa, bb, cc, dd, chunk[7], 8);
		HHH(ref dd, aa, bb, cc, chunk[14], 6);
		HHH(ref cc, dd, aa, bb, chunk[6], 6);
		HHH(ref bb, cc, dd, aa, chunk[9], 14);
		HHH(ref aa, bb, cc, dd, chunk[11], 12);
		HHH(ref dd, aa, bb, cc, chunk[8], 13);
		HHH(ref cc, dd, aa, bb, chunk[12], 5);
		HHH(ref bb, cc, dd, aa, chunk[2], 14);
		HHH(ref aa, bb, cc, dd, chunk[10], 13);
		HHH(ref dd, aa, bb, cc, chunk[0], 13);
		HHH(ref cc, dd, aa, bb, chunk[4], 7);
		HHH(ref bb, cc, dd, aa, chunk[13], 5);

		III(ref aa, bb, cc, dd, chunk[8], 15);
		III(ref dd, aa, bb, cc, chunk[6], 5);
		III(ref cc, dd, aa, bb, chunk[4], 8);
		III(ref bb, cc, dd, aa, chunk[1], 11);
		III(ref aa, bb, cc, dd, chunk[3], 14);
		III(ref dd, aa, bb, cc, chunk[11], 14);
		III(ref cc, dd, aa, bb, chunk[15], 6);
		III(ref bb, cc, dd, aa, chunk[0], 14);
		III(ref aa, bb, cc, dd, chunk[5], 6);
		III(ref dd, aa, bb, cc, chunk[12], 9);
		III(ref cc, dd, aa, bb, chunk[2], 12);
		III(ref bb, cc, dd, aa, chunk[13], 9);
		III(ref aa, bb, cc, dd, chunk[9], 12);
		III(ref dd, aa, bb, cc, chunk[7], 5);
		III(ref cc, dd, aa, bb, chunk[10], 15);
		III(ref bb, cc, dd, aa, chunk[14], 8);

		dd += CurrentHash[1] + c;
		CurrentHash[1] = CurrentHash[2] + d + aa;
		CurrentHash[2] = CurrentHash[3] + a + bb;
		CurrentHash[3] = CurrentHash[0] + b + cc;
		CurrentHash[0] = dd;
	}
	private static uint F1(uint x, uint y, uint z)
	{
		return x ^ y ^ z;
	}
	private static uint F2(uint x, uint y, uint z)
	{
		return x & y | ~x & z;
	}
	private static uint F3(uint x, uint y, uint z)
	{
		return (x | ~y) ^ z;
	}
	private static uint F4(uint x, uint y, uint z)
	{
		return x & z | y & ~z;
	}
	private static void FF(ref uint a, uint b, uint c, uint d, uint x, int s)
	{
		a = BitOperations.RotateLeft(a + F1(b, c, d) + x, s);
	}
	private static void GG(ref uint a, uint b, uint c, uint d, uint x, int s)
	{
		a = BitOperations.RotateLeft(a + F2(b, c, d) + x + 0x5a827999, s);
	}
	private static void HH(ref uint a, uint b, uint c, uint d, uint x, int s)
	{
		a = BitOperations.RotateLeft(a + F3(b, c, d) + x + 0x6ed9eba1, s);
	}
	private static void II(ref uint a, uint b, uint c, uint d, uint x, int s)
	{
		a = BitOperations.RotateLeft(a + F4(b, c, d) + x + 0x8f1bbcdc, s);
	}
	private static void FFF(ref uint a, uint b, uint c, uint d, uint x, int s)
	{
		a = BitOperations.RotateLeft(a + F4(b, c, d) + x + 0x50a28be6, s);
	}
	private static void GGG(ref uint a, uint b, uint c, uint d, uint x, int s)
	{
		a = BitOperations.RotateLeft(a + F3(b, c, d) + x + 0x5c4dd124, s);
	}
	private static void HHH(ref uint a, uint b, uint c, uint d, uint x, int s)
	{
		a = BitOperations.RotateLeft(a + F2(b, c, d) + x + 0x6d703ef3, s);
	}
	private static void III(ref uint a, uint b, uint c, uint d, uint x, int s)
	{
		a = BitOperations.RotateLeft(a + F1(b, c, d) + x, s);
	}
}