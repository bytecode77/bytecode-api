using System.Numerics;
using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.HashAlgorithms;

/// <summary>
/// Computes the <see cref="RIPEMD160" /> hash for the input data.
/// </summary>
public sealed class RIPEMD160 : HashAlgorithm
{
	private uint[] CurrentHash = null!;
	private uint[] CurrentChunk = null!;
	private byte[] UnhashedBuffer = null!;
	private long HashedLength;
	private int UnhashedBufferLength;

	/// <summary>
	/// Initializes a new instance of the <see cref="RIPEMD160" /> class.
	/// </summary>
	public RIPEMD160()
	{
		HashSizeValue = 160;
		Initialize();
	}
	/// <summary>
	/// Creates an instance of the default implementation of <see cref="RIPEMD160" />.
	/// </summary>
	/// <returns>
	/// A new instance of <see cref="RIPEMD160" />.
	/// </returns>
	public static new RIPEMD160 Create()
	{
		return new();
	}

	/// <summary>
	/// Resets the hash algorithm to its initial state.
	/// </summary>
	public sealed override void Initialize()
	{
		CurrentHash = new uint[]
		{
			0x67452301,
			0xefcdab89,
			0x98badcfe,
			0x10325476,
			0xc3d2e1f0
		};
		CurrentChunk = new uint[16];
		UnhashedBuffer = new byte[64];
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

		byte[] hashBuffer = new byte[20];
		for (int i = 0; i < 5; i++)
		{
			Buffer.BlockCopy(BitConverter.GetBytes(CurrentHash[i]), 0, hashBuffer, i * 4, 4);
		}

		HashValue = hashBuffer;
		return HashValue;
	}

	private void Process(uint[] chunk)
	{
		uint aa = CurrentHash[0];
		uint bb = CurrentHash[1];
		uint cc = CurrentHash[2];
		uint dd = CurrentHash[3];
		uint ee = CurrentHash[4];
		uint aaa = CurrentHash[0];
		uint bbb = CurrentHash[1];
		uint ccc = CurrentHash[2];
		uint ddd = CurrentHash[3];
		uint eee = CurrentHash[4];

		FF(ref aa, bb, ref cc, dd, ee, chunk[0], 11);
		FF(ref ee, aa, ref bb, cc, dd, chunk[1], 14);
		FF(ref dd, ee, ref aa, bb, cc, chunk[2], 15);
		FF(ref cc, dd, ref ee, aa, bb, chunk[3], 12);
		FF(ref bb, cc, ref dd, ee, aa, chunk[4], 5);
		FF(ref aa, bb, ref cc, dd, ee, chunk[5], 8);
		FF(ref ee, aa, ref bb, cc, dd, chunk[6], 7);
		FF(ref dd, ee, ref aa, bb, cc, chunk[7], 9);
		FF(ref cc, dd, ref ee, aa, bb, chunk[8], 11);
		FF(ref bb, cc, ref dd, ee, aa, chunk[9], 13);
		FF(ref aa, bb, ref cc, dd, ee, chunk[10], 14);
		FF(ref ee, aa, ref bb, cc, dd, chunk[11], 15);
		FF(ref dd, ee, ref aa, bb, cc, chunk[12], 6);
		FF(ref cc, dd, ref ee, aa, bb, chunk[13], 7);
		FF(ref bb, cc, ref dd, ee, aa, chunk[14], 9);
		FF(ref aa, bb, ref cc, dd, ee, chunk[15], 8);

		GG(ref ee, aa, ref bb, cc, dd, chunk[7], 7);
		GG(ref dd, ee, ref aa, bb, cc, chunk[4], 6);
		GG(ref cc, dd, ref ee, aa, bb, chunk[13], 8);
		GG(ref bb, cc, ref dd, ee, aa, chunk[1], 13);
		GG(ref aa, bb, ref cc, dd, ee, chunk[10], 11);
		GG(ref ee, aa, ref bb, cc, dd, chunk[6], 9);
		GG(ref dd, ee, ref aa, bb, cc, chunk[15], 7);
		GG(ref cc, dd, ref ee, aa, bb, chunk[3], 15);
		GG(ref bb, cc, ref dd, ee, aa, chunk[12], 7);
		GG(ref aa, bb, ref cc, dd, ee, chunk[0], 12);
		GG(ref ee, aa, ref bb, cc, dd, chunk[9], 15);
		GG(ref dd, ee, ref aa, bb, cc, chunk[5], 9);
		GG(ref cc, dd, ref ee, aa, bb, chunk[2], 11);
		GG(ref bb, cc, ref dd, ee, aa, chunk[14], 7);
		GG(ref aa, bb, ref cc, dd, ee, chunk[11], 13);
		GG(ref ee, aa, ref bb, cc, dd, chunk[8], 12);

		HH(ref dd, ee, ref aa, bb, cc, chunk[3], 11);
		HH(ref cc, dd, ref ee, aa, bb, chunk[10], 13);
		HH(ref bb, cc, ref dd, ee, aa, chunk[14], 6);
		HH(ref aa, bb, ref cc, dd, ee, chunk[4], 7);
		HH(ref ee, aa, ref bb, cc, dd, chunk[9], 14);
		HH(ref dd, ee, ref aa, bb, cc, chunk[15], 9);
		HH(ref cc, dd, ref ee, aa, bb, chunk[8], 13);
		HH(ref bb, cc, ref dd, ee, aa, chunk[1], 15);
		HH(ref aa, bb, ref cc, dd, ee, chunk[2], 14);
		HH(ref ee, aa, ref bb, cc, dd, chunk[7], 8);
		HH(ref dd, ee, ref aa, bb, cc, chunk[0], 13);
		HH(ref cc, dd, ref ee, aa, bb, chunk[6], 6);
		HH(ref bb, cc, ref dd, ee, aa, chunk[13], 5);
		HH(ref aa, bb, ref cc, dd, ee, chunk[11], 12);
		HH(ref ee, aa, ref bb, cc, dd, chunk[5], 7);
		HH(ref dd, ee, ref aa, bb, cc, chunk[12], 5);

		II(ref cc, dd, ref ee, aa, bb, chunk[1], 11);
		II(ref bb, cc, ref dd, ee, aa, chunk[9], 12);
		II(ref aa, bb, ref cc, dd, ee, chunk[11], 14);
		II(ref ee, aa, ref bb, cc, dd, chunk[10], 15);
		II(ref dd, ee, ref aa, bb, cc, chunk[0], 14);
		II(ref cc, dd, ref ee, aa, bb, chunk[8], 15);
		II(ref bb, cc, ref dd, ee, aa, chunk[12], 9);
		II(ref aa, bb, ref cc, dd, ee, chunk[4], 8);
		II(ref ee, aa, ref bb, cc, dd, chunk[13], 9);
		II(ref dd, ee, ref aa, bb, cc, chunk[3], 14);
		II(ref cc, dd, ref ee, aa, bb, chunk[7], 5);
		II(ref bb, cc, ref dd, ee, aa, chunk[15], 6);
		II(ref aa, bb, ref cc, dd, ee, chunk[14], 8);
		II(ref ee, aa, ref bb, cc, dd, chunk[5], 6);
		II(ref dd, ee, ref aa, bb, cc, chunk[6], 5);
		II(ref cc, dd, ref ee, aa, bb, chunk[2], 12);

		JJ(ref bb, cc, ref dd, ee, aa, chunk[4], 9);
		JJ(ref aa, bb, ref cc, dd, ee, chunk[0], 15);
		JJ(ref ee, aa, ref bb, cc, dd, chunk[5], 5);
		JJ(ref dd, ee, ref aa, bb, cc, chunk[9], 11);
		JJ(ref cc, dd, ref ee, aa, bb, chunk[7], 6);
		JJ(ref bb, cc, ref dd, ee, aa, chunk[12], 8);
		JJ(ref aa, bb, ref cc, dd, ee, chunk[2], 13);
		JJ(ref ee, aa, ref bb, cc, dd, chunk[10], 12);
		JJ(ref dd, ee, ref aa, bb, cc, chunk[14], 5);
		JJ(ref cc, dd, ref ee, aa, bb, chunk[1], 12);
		JJ(ref bb, cc, ref dd, ee, aa, chunk[3], 13);
		JJ(ref aa, bb, ref cc, dd, ee, chunk[8], 14);
		JJ(ref ee, aa, ref bb, cc, dd, chunk[11], 11);
		JJ(ref dd, ee, ref aa, bb, cc, chunk[6], 8);
		JJ(ref cc, dd, ref ee, aa, bb, chunk[15], 5);
		JJ(ref bb, cc, ref dd, ee, aa, chunk[13], 6);

		JJJ(ref aaa, bbb, ref ccc, ddd, eee, chunk[5], 8);
		JJJ(ref eee, aaa, ref bbb, ccc, ddd, chunk[14], 9);
		JJJ(ref ddd, eee, ref aaa, bbb, ccc, chunk[7], 9);
		JJJ(ref ccc, ddd, ref eee, aaa, bbb, chunk[0], 11);
		JJJ(ref bbb, ccc, ref ddd, eee, aaa, chunk[9], 13);
		JJJ(ref aaa, bbb, ref ccc, ddd, eee, chunk[2], 15);
		JJJ(ref eee, aaa, ref bbb, ccc, ddd, chunk[11], 15);
		JJJ(ref ddd, eee, ref aaa, bbb, ccc, chunk[4], 5);
		JJJ(ref ccc, ddd, ref eee, aaa, bbb, chunk[13], 7);
		JJJ(ref bbb, ccc, ref ddd, eee, aaa, chunk[6], 7);
		JJJ(ref aaa, bbb, ref ccc, ddd, eee, chunk[15], 8);
		JJJ(ref eee, aaa, ref bbb, ccc, ddd, chunk[8], 11);
		JJJ(ref ddd, eee, ref aaa, bbb, ccc, chunk[1], 14);
		JJJ(ref ccc, ddd, ref eee, aaa, bbb, chunk[10], 14);
		JJJ(ref bbb, ccc, ref ddd, eee, aaa, chunk[3], 12);
		JJJ(ref aaa, bbb, ref ccc, ddd, eee, chunk[12], 6);

		III(ref eee, aaa, ref bbb, ccc, ddd, chunk[6], 9);
		III(ref ddd, eee, ref aaa, bbb, ccc, chunk[11], 13);
		III(ref ccc, ddd, ref eee, aaa, bbb, chunk[3], 15);
		III(ref bbb, ccc, ref ddd, eee, aaa, chunk[7], 7);
		III(ref aaa, bbb, ref ccc, ddd, eee, chunk[0], 12);
		III(ref eee, aaa, ref bbb, ccc, ddd, chunk[13], 8);
		III(ref ddd, eee, ref aaa, bbb, ccc, chunk[5], 9);
		III(ref ccc, ddd, ref eee, aaa, bbb, chunk[10], 11);
		III(ref bbb, ccc, ref ddd, eee, aaa, chunk[14], 7);
		III(ref aaa, bbb, ref ccc, ddd, eee, chunk[15], 7);
		III(ref eee, aaa, ref bbb, ccc, ddd, chunk[8], 12);
		III(ref ddd, eee, ref aaa, bbb, ccc, chunk[12], 7);
		III(ref ccc, ddd, ref eee, aaa, bbb, chunk[4], 6);
		III(ref bbb, ccc, ref ddd, eee, aaa, chunk[9], 15);
		III(ref aaa, bbb, ref ccc, ddd, eee, chunk[1], 13);
		III(ref eee, aaa, ref bbb, ccc, ddd, chunk[2], 11);

		HHH(ref ddd, eee, ref aaa, bbb, ccc, chunk[15], 9);
		HHH(ref ccc, ddd, ref eee, aaa, bbb, chunk[5], 7);
		HHH(ref bbb, ccc, ref ddd, eee, aaa, chunk[1], 15);
		HHH(ref aaa, bbb, ref ccc, ddd, eee, chunk[3], 11);
		HHH(ref eee, aaa, ref bbb, ccc, ddd, chunk[7], 8);
		HHH(ref ddd, eee, ref aaa, bbb, ccc, chunk[14], 6);
		HHH(ref ccc, ddd, ref eee, aaa, bbb, chunk[6], 6);
		HHH(ref bbb, ccc, ref ddd, eee, aaa, chunk[9], 14);
		HHH(ref aaa, bbb, ref ccc, ddd, eee, chunk[11], 12);
		HHH(ref eee, aaa, ref bbb, ccc, ddd, chunk[8], 13);
		HHH(ref ddd, eee, ref aaa, bbb, ccc, chunk[12], 5);
		HHH(ref ccc, ddd, ref eee, aaa, bbb, chunk[2], 14);
		HHH(ref bbb, ccc, ref ddd, eee, aaa, chunk[10], 13);
		HHH(ref aaa, bbb, ref ccc, ddd, eee, chunk[0], 13);
		HHH(ref eee, aaa, ref bbb, ccc, ddd, chunk[4], 7);
		HHH(ref ddd, eee, ref aaa, bbb, ccc, chunk[13], 5);

		GGG(ref ccc, ddd, ref eee, aaa, bbb, chunk[8], 15);
		GGG(ref bbb, ccc, ref ddd, eee, aaa, chunk[6], 5);
		GGG(ref aaa, bbb, ref ccc, ddd, eee, chunk[4], 8);
		GGG(ref eee, aaa, ref bbb, ccc, ddd, chunk[1], 11);
		GGG(ref ddd, eee, ref aaa, bbb, ccc, chunk[3], 14);
		GGG(ref ccc, ddd, ref eee, aaa, bbb, chunk[11], 14);
		GGG(ref bbb, ccc, ref ddd, eee, aaa, chunk[15], 6);
		GGG(ref aaa, bbb, ref ccc, ddd, eee, chunk[0], 14);
		GGG(ref eee, aaa, ref bbb, ccc, ddd, chunk[5], 6);
		GGG(ref ddd, eee, ref aaa, bbb, ccc, chunk[12], 9);
		GGG(ref ccc, ddd, ref eee, aaa, bbb, chunk[2], 12);
		GGG(ref bbb, ccc, ref ddd, eee, aaa, chunk[13], 9);
		GGG(ref aaa, bbb, ref ccc, ddd, eee, chunk[9], 12);
		GGG(ref eee, aaa, ref bbb, ccc, ddd, chunk[7], 5);
		GGG(ref ddd, eee, ref aaa, bbb, ccc, chunk[10], 15);
		GGG(ref ccc, ddd, ref eee, aaa, bbb, chunk[14], 8);

		FFF(ref bbb, ccc, ref ddd, eee, aaa, chunk[12], 8);
		FFF(ref aaa, bbb, ref ccc, ddd, eee, chunk[15], 5);
		FFF(ref eee, aaa, ref bbb, ccc, ddd, chunk[10], 12);
		FFF(ref ddd, eee, ref aaa, bbb, ccc, chunk[4], 9);
		FFF(ref ccc, ddd, ref eee, aaa, bbb, chunk[1], 12);
		FFF(ref bbb, ccc, ref ddd, eee, aaa, chunk[5], 5);
		FFF(ref aaa, bbb, ref ccc, ddd, eee, chunk[8], 14);
		FFF(ref eee, aaa, ref bbb, ccc, ddd, chunk[7], 6);
		FFF(ref ddd, eee, ref aaa, bbb, ccc, chunk[6], 8);
		FFF(ref ccc, ddd, ref eee, aaa, bbb, chunk[2], 13);
		FFF(ref bbb, ccc, ref ddd, eee, aaa, chunk[13], 6);
		FFF(ref aaa, bbb, ref ccc, ddd, eee, chunk[14], 5);
		FFF(ref eee, aaa, ref bbb, ccc, ddd, chunk[0], 15);
		FFF(ref ddd, eee, ref aaa, bbb, ccc, chunk[3], 13);
		FFF(ref ccc, ddd, ref eee, aaa, bbb, chunk[9], 11);
		FFF(ref bbb, ccc, ref ddd, eee, aaa, chunk[11], 11);

		ddd += cc + CurrentHash[1];
		CurrentHash[1] = CurrentHash[2] + dd + eee;
		CurrentHash[2] = CurrentHash[3] + ee + aaa;
		CurrentHash[3] = CurrentHash[4] + aa + bbb;
		CurrentHash[4] = CurrentHash[0] + bb + ccc;
		CurrentHash[0] = ddd;
	}
	private static uint F(uint x, uint y, uint z)
	{
		return x ^ y ^ z;
	}
	private static uint G(uint x, uint y, uint z)
	{
		return x & y | ~x & z;
	}
	private static uint H(uint x, uint y, uint z)
	{
		return (x | ~y) ^ z;
	}
	private static uint I(uint x, uint y, uint z)
	{
		return x & z | y & ~z;
	}
	private static uint J(uint x, uint y, uint z)
	{
		return x ^ (y | ~z);
	}
	private static void FF(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
	{
		a += F(b, c, d) + x;
		a = BitOperations.RotateLeft(a, s) + e;
		c = BitOperations.RotateLeft(c, 10);
	}
	private static void GG(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
	{
		a += G(b, c, d) + x + 0x5a827999;
		a = BitOperations.RotateLeft(a, s) + e;
		c = BitOperations.RotateLeft(c, 10);
	}
	private static void HH(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
	{
		a += H(b, c, d) + x + 0x6ed9eba1;
		a = BitOperations.RotateLeft(a, s) + e;
		c = BitOperations.RotateLeft(c, 10);
	}
	private static void II(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
	{
		a += I(b, c, d) + x + 0x8f1bbcdc;
		a = BitOperations.RotateLeft(a, s) + e;
		c = BitOperations.RotateLeft(c, 10);
	}
	private static void JJ(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
	{
		a += J(b, c, d) + x + 0xa953fd4e;
		a = BitOperations.RotateLeft(a, s) + e;
		c = BitOperations.RotateLeft(c, 10);
	}
	private static void FFF(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
	{
		a += F(b, c, d) + x;
		a = BitOperations.RotateLeft(a, s) + e;
		c = BitOperations.RotateLeft(c, 10);
	}
	private static void GGG(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
	{
		a += G(b, c, d) + x + 0x7a6d76e9;
		a = BitOperations.RotateLeft(a, s) + e;
		c = BitOperations.RotateLeft(c, 10);
	}
	private static void HHH(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
	{
		a += H(b, c, d) + x + 0x6d703ef3;
		a = BitOperations.RotateLeft(a, s) + e;
		c = BitOperations.RotateLeft(c, 10);
	}
	private static void III(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
	{
		a += I(b, c, d) + x + 0x5c4dd124;
		a = BitOperations.RotateLeft(a, s) + e;
		c = BitOperations.RotateLeft(c, 10);
	}
	private static void JJJ(ref uint a, uint b, ref uint c, uint d, uint e, uint x, int s)
	{
		a += J(b, c, d) + x + 0x50a28be6;
		a = BitOperations.RotateLeft(a, s) + e;
		c = BitOperations.RotateLeft(c, 10);
	}
}