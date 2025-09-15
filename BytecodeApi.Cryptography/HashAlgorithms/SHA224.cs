using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.HashAlgorithms;

/// <summary>
/// Computes the <see cref="SHA224" /> hash for the input data.
/// </summary>
public sealed class SHA224 : HashAlgorithm
{
	private static readonly uint[] SHA224Table;
	private readonly uint[] CurrentHash;
	private ulong Count;
	private readonly byte[] ProcessingBuffer;
	private int ProcessingBufferCount;
	private readonly uint[] Buffer;

	static SHA224()
	{
		SHA224Table =
		[
			0x428a2f98, 0x71374491, 0xb5c0fbcf, 0xe9b5dba5,
			0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5,
			0xd807aa98, 0x12835b01, 0x243185be, 0x550c7dc3,
			0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174,
			0xe49b69c1, 0xefbe4786, 0x0fc19dc6, 0x240ca1cc,
			0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da,
			0x983e5152, 0xa831c66d, 0xb00327c8, 0xbf597fc7,
			0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967,
			0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13,
			0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85,
			0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3,
			0xd192e819, 0xd6990624, 0xf40e3585, 0x106aa070,
			0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5,
			0x391c0cb3, 0x4ed8aa4a, 0x5b9cca4f, 0x682e6ff3,
			0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208,
			0x90befffa, 0xa4506ceb, 0xbef9a3f7, 0xc67178f2
		];
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="SHA224" /> class.
	/// </summary>
	public SHA224()
	{
		HashSizeValue = 224;

		CurrentHash = new uint[8];
		ProcessingBuffer = new byte[64];
		Buffer = new uint[64];

		Initialize();
	}
	/// <summary>
	/// Creates an instance of the default implementation of <see cref="SHA224" />.
	/// </summary>
	/// <returns>
	/// A new instance of <see cref="SHA224" />.
	/// </returns>
	public static new SHA224 Create()
	{
		return new();
	}

	/// <summary>
	/// Resets the hash algorithm to its initial state.
	/// </summary>
	public sealed override void Initialize()
	{
		CurrentHash[0] = 0xc1059ed8;
		CurrentHash[1] = 0x367cd507;
		CurrentHash[2] = 0x3070dd17;
		CurrentHash[3] = 0xf70e5939;
		CurrentHash[4] = 0xffc00b31;
		CurrentHash[5] = 0x68581511;
		CurrentHash[6] = 0x64f98fa7;
		CurrentHash[7] = 0xbefa4fa4;
		Count = 0;
		ProcessingBufferCount = 0;
	}
	/// <summary>
	/// Routes data written to the object into the hash algorithm for computing the hash.
	/// </summary>
	/// <param name="array">The input to compute the hash code for.</param>
	/// <param name="ibStart">The offset into the <see cref="byte" />[] from which to begin using data.</param>
	/// <param name="cbSize">The number of bytes in the <see cref="byte" />[] to use as data.</param>
	protected sealed override void HashCore(byte[] array, int ibStart, int cbSize)
	{
		int i;

		if (ProcessingBufferCount != 0)
		{
			if (cbSize < (64 - ProcessingBufferCount))
			{
				System.Buffer.BlockCopy(array, ibStart, ProcessingBuffer, ProcessingBufferCount, cbSize);
				ProcessingBufferCount += cbSize;
				return;
			}
			else
			{
				i = (64 - ProcessingBufferCount);
				System.Buffer.BlockCopy(array, ibStart, ProcessingBuffer, ProcessingBufferCount, i);
				ProcessBlock(ProcessingBuffer, 0);
				ProcessingBufferCount = 0;
				ibStart += i;
				cbSize -= i;
			}
		}

		for (i = 0; i < cbSize - cbSize % 64; i += 64)
		{
			ProcessBlock(array, ibStart + i);
		}

		if (cbSize % 64 != 0)
		{
			System.Buffer.BlockCopy(array, cbSize - cbSize % 64 + ibStart, ProcessingBuffer, 0, cbSize % 64);
			ProcessingBufferCount = cbSize % 64;
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
		ProcessFinalBlock(ProcessingBuffer, 0, ProcessingBufferCount);

		byte[] hashBuffer = new byte[28];
		for (int i = 0; i < 7; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				hashBuffer[i * 4 + j] = (byte)(CurrentHash[i] >> (24 - j * 8));
			}
		}

		HashValue = hashBuffer;
		return HashValue;
	}

	private void ProcessBlock(byte[] inputBuffer, int inputOffset)
	{
		Count += 64;

		for (int i = 0; i < 16; i++)
		{
			Buffer[i] = (uint)(
				inputBuffer[inputOffset + 4 * i] << 24 |
				inputBuffer[inputOffset + 4 * i + 1] << 16 |
				inputBuffer[inputOffset + 4 * i + 2] << 8 |
				inputBuffer[inputOffset + 4 * i + 3]);
		}

		for (int i = 16; i < 64; i++)
		{
			uint t1 = Buffer[i - 15];
			t1 = (t1 >> 7 | t1 << 25) ^ (t1 >> 18 | t1 << 14) ^ (t1 >> 3);

			uint t2 = Buffer[i - 2];
			t2 = (t2 >> 17 | t2 << 15) ^ (t2 >> 19 | t2 << 13) ^ (t2 >> 10);
			Buffer[i] = t2 + Buffer[i - 7] + t1 + Buffer[i - 16];
		}

		uint a = CurrentHash[0];
		uint b = CurrentHash[1];
		uint c = CurrentHash[2];
		uint d = CurrentHash[3];
		uint e = CurrentHash[4];
		uint f = CurrentHash[5];
		uint g = CurrentHash[6];
		uint h = CurrentHash[7];

		for (int i = 0; i < 64; i++)
		{
			uint t1 = h + ((e >> 6 | e << 26) ^ (e >> 11 | e << 21) ^ (e >> 25 | e << 7)) + (e & f ^ ~e & g) + SHA224Table[i] + Buffer[i];

			uint t2 = (a >> 2 | a << 30) ^ (a >> 13 | a << 19) ^ (a >> 22 | a << 10);
			t2 = t2 + (a & b ^ a & c ^ b & c);
			h = g;
			g = f;
			f = e;
			e = d + t1;
			d = c;
			c = b;
			b = a;
			a = t1 + t2;
		}

		CurrentHash[0] += a;
		CurrentHash[1] += b;
		CurrentHash[2] += c;
		CurrentHash[3] += d;
		CurrentHash[4] += e;
		CurrentHash[5] += f;
		CurrentHash[6] += g;
		CurrentHash[7] += h;
	}
	private void ProcessFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
	{
		ulong total = Count + (ulong)inputCount;
		int paddingSize = 56 - (int)(total % 64);

		if (paddingSize < 1)
		{
			paddingSize += 64;
		}

		byte[] paddingBuffer = new byte[inputCount + paddingSize + 8];

		for (int i = 0; i < inputCount; i++)
		{
			paddingBuffer[i] = inputBuffer[i + inputOffset];
		}

		paddingBuffer[inputCount] = 0x80;

		for (int i = inputCount + 1; i < inputCount + paddingSize; i++)
		{
			paddingBuffer[i] = 0;
		}

		AddLength(total << 3, paddingBuffer, inputCount + paddingSize);
		ProcessBlock(paddingBuffer, 0);

		if (inputCount + paddingSize + 8 == 128)
		{
			ProcessBlock(paddingBuffer, 64);
		}
	}
	private static void AddLength(ulong length, byte[] buffer, int position)
	{
		buffer[position++] = (byte)(length >> 56);
		buffer[position++] = (byte)(length >> 48);
		buffer[position++] = (byte)(length >> 40);
		buffer[position++] = (byte)(length >> 32);
		buffer[position++] = (byte)(length >> 24);
		buffer[position++] = (byte)(length >> 16);
		buffer[position++] = (byte)(length >> 8);
		buffer[position] = (byte)length;
	}
}