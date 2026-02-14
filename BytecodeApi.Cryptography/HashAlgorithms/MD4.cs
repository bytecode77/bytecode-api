using System.Numerics;
using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.HashAlgorithms;

/// <summary>
/// Computes the <see cref="MD4" /> hash for the input data.
/// </summary>
public sealed class MD4 : HashAlgorithm
{
	private readonly uint[] CurrentState;
	private readonly byte[] Buffer;
	private readonly uint[] Count;
	private readonly uint[] X;
	private readonly byte[] Digest;

	/// <summary>
	/// Initializes a new instance of the <see cref="MD4" /> class.
	/// </summary>
	public MD4()
	{
		HashSizeValue = 128;

		CurrentState = new uint[4];
		Buffer = new byte[64];
		Count = new uint[2];
		X = new uint[16];
		Digest = new byte[16];

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
		CurrentState[0] = 0x67452301;
		CurrentState[1] = 0xefcdab89;
		CurrentState[2] = 0x98badcfe;
		CurrentState[3] = 0x10325476;
		Array.Clear(Buffer);
		Array.Clear(Count);
		Array.Clear(X);
	}
	/// <summary>
	/// Routes data written to the object into the hash algorithm for computing the hash.
	/// </summary>
	/// <param name="array">The input to compute the hash code for.</param>
	/// <param name="ibStart">The offset into the <see cref="byte" />[] from which to begin using data.</param>
	/// <param name="cbSize">The number of bytes in the <see cref="byte" />[] to use as data.</param>
	protected sealed override void HashCore(byte[] array, int ibStart, int cbSize)
	{
		int index = (int)((Count[0] >> 3) & 0x3f);
		Count[0] += (uint)(cbSize << 3);

		if (Count[0] < cbSize << 3)
		{
			Count[1]++;
		}

		Count[1] += (uint)(cbSize >> 29);

		int partLength = 64 - index;
		int i = 0;

		if (cbSize >= partLength)
		{
			System.Buffer.BlockCopy(array, ibStart, Buffer, index, partLength);
			Transform(CurrentState, Buffer, 0);

			for (i = partLength; i + 63 < cbSize; i += 64)
			{
				Transform(CurrentState, array, ibStart + i);
			}

			index = 0;
		}

		System.Buffer.BlockCopy(array, ibStart + i, Buffer, index, cbSize - i);
	}
	/// <summary>
	/// Finalizes the hash computation after the last data is processed by the cryptographic hash algorithm.
	/// </summary>
	/// <returns>
	/// A <see cref="byte" />[] with the final hash value.
	/// </returns>
	protected sealed override byte[] HashFinal()
	{
		byte[] countBits = new byte[8];
		Encode(countBits, Count);

		uint index = (Count[0] >> 3) & 0x3f;
		int paddingLength = (int)((index < 56) ? 56 - index : 120 - index);

		HashCore(GetPadding(paddingLength), 0, paddingLength);
		HashCore(countBits, 0, 8);
		Encode(Digest, CurrentState);

		HashValue = Digest.ToArray();
		return HashValue;
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
	private static void FF(ref uint a, uint b, uint c, uint d, uint x, byte s)
	{
		a = BitOperations.RotateLeft(a + F(b, c, d) + x, s);
	}
	private static void GG(ref uint a, uint b, uint c, uint d, uint x, byte s)
	{
		a = BitOperations.RotateLeft(a + G(b, c, d) + x + 0x5a827999, s);
	}
	private static void HH(ref uint a, uint b, uint c, uint d, uint x, byte s)
	{
		a = BitOperations.RotateLeft(a + H(b, c, d) + x + 0x6ed9eba1, s);
	}
	private static void Encode(byte[] output, uint[] input)
	{
		for (int i = 0, j = 0; j < output.Length; i++, j += 4)
		{
			output[j] = (byte)input[i];
			output[j + 1] = (byte)(input[i] >> 8);
			output[j + 2] = (byte)(input[i] >> 16);
			output[j + 3] = (byte)(input[i] >> 24);
		}
	}
	private static void Decode(uint[] output, byte[] input, int index)
	{
		for (int i = 0, j = index; i < output.Length; i++, j += 4)
		{
			output[i] = (uint)(input[j] | input[j + 1] << 8 | input[j + 2] << 16 | input[j + 3] << 24);
		}
	}
	private void Transform(uint[] state, byte[] block, int index)
	{
		uint a = state[0];
		uint b = state[1];
		uint c = state[2];
		uint d = state[3];

		Decode(X, block, index);

		FF(ref a, b, c, d, X[0], 3);
		FF(ref d, a, b, c, X[1], 7);
		FF(ref c, d, a, b, X[2], 11);
		FF(ref b, c, d, a, X[3], 19);
		FF(ref a, b, c, d, X[4], 3);
		FF(ref d, a, b, c, X[5], 7);
		FF(ref c, d, a, b, X[6], 11);
		FF(ref b, c, d, a, X[7], 19);
		FF(ref a, b, c, d, X[8], 3);
		FF(ref d, a, b, c, X[9], 7);
		FF(ref c, d, a, b, X[10], 11);
		FF(ref b, c, d, a, X[11], 19);
		FF(ref a, b, c, d, X[12], 3);
		FF(ref d, a, b, c, X[13], 7);
		FF(ref c, d, a, b, X[14], 11);
		FF(ref b, c, d, a, X[15], 19);

		GG(ref a, b, c, d, X[0], 3);
		GG(ref d, a, b, c, X[4], 5);
		GG(ref c, d, a, b, X[8], 9);
		GG(ref b, c, d, a, X[12], 13);
		GG(ref a, b, c, d, X[1], 3);
		GG(ref d, a, b, c, X[5], 5);
		GG(ref c, d, a, b, X[9], 9);
		GG(ref b, c, d, a, X[13], 13);
		GG(ref a, b, c, d, X[2], 3);
		GG(ref d, a, b, c, X[6], 5);
		GG(ref c, d, a, b, X[10], 9);
		GG(ref b, c, d, a, X[14], 13);
		GG(ref a, b, c, d, X[3], 3);
		GG(ref d, a, b, c, X[7], 5);
		GG(ref c, d, a, b, X[11], 9);
		GG(ref b, c, d, a, X[15], 13);

		HH(ref a, b, c, d, X[0], 3);
		HH(ref d, a, b, c, X[8], 9);
		HH(ref c, d, a, b, X[4], 11);
		HH(ref b, c, d, a, X[12], 15);
		HH(ref a, b, c, d, X[2], 3);
		HH(ref d, a, b, c, X[10], 9);
		HH(ref c, d, a, b, X[6], 11);
		HH(ref b, c, d, a, X[14], 15);
		HH(ref a, b, c, d, X[1], 3);
		HH(ref d, a, b, c, X[9], 9);
		HH(ref c, d, a, b, X[5], 11);
		HH(ref b, c, d, a, X[13], 15);
		HH(ref a, b, c, d, X[3], 3);
		HH(ref d, a, b, c, X[11], 9);
		HH(ref c, d, a, b, X[7], 11);
		HH(ref b, c, d, a, X[15], 15);

		state[0] += a;
		state[1] += b;
		state[2] += c;
		state[3] += d;
	}
	private static byte[] GetPadding(int length)
	{
		if (length > 0)
		{
			byte[] padding = new byte[length];
			padding[0] = 0x80;
			return padding;
		}
		else
		{
			return Array.Empty<byte>();
		}
	}
}