using BytecodeApi.Extensions;
using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.HashAlgorithms;

/// <summary>
/// Computes the <see cref="MD2" /> hash for the input data.
/// </summary>
public sealed class MD2 : HashAlgorithm
{
	private static readonly int[] MD2Table;
	private byte[] CurrentHash = null!;
	private long HashedLength;
	private int[] CheckSum = null!;
	private int[] Buffer = null!;
	private byte[] LastBlock = null!;
	private int LastBlockLength;

	static MD2()
	{
		MD2Table = new int[]
		{
			0x29, 0x2e, 0x43, 0xc9, 0xa2, 0xd8, 0x7c, 0x01, 0x3d, 0x36, 0x54, 0xa1, 0xec, 0xf0, 0x06, 0x13,
			0x62, 0xa7, 0x05, 0xf3, 0xc0, 0xc7, 0x73, 0x8c, 0x98, 0x93, 0x2b, 0xd9, 0xbc, 0x4c, 0x82, 0xca,
			0x1e, 0x9b, 0x57, 0x3c, 0xfd, 0xd4, 0xe0, 0x16, 0x67, 0x42, 0x6f, 0x18, 0x8a, 0x17, 0xe5, 0x12,
			0xbe, 0x4e, 0xc4, 0xd6, 0xda, 0x9e, 0xde, 0x49, 0xa0, 0xfb, 0xf5, 0x8e, 0xbb, 0x2f, 0xee, 0x7a,
			0xa9, 0x68, 0x79, 0x91, 0x15, 0xb2, 0x07, 0x3f, 0x94, 0xc2, 0x10, 0x89, 0x0b, 0x22, 0x5f, 0x21,
			0x80, 0x7f, 0x5d, 0x9a, 0x5a, 0x90, 0x32, 0x27, 0x35, 0x3e, 0xcc, 0xe7, 0xbf, 0xf7, 0x97, 0x03,
			0xff, 0x19, 0x30, 0xb3, 0x48, 0xa5, 0xb5, 0xd1, 0xd7, 0x5e, 0x92, 0x2a, 0xac, 0x56, 0xaa, 0xc6,
			0x4f, 0xb8, 0x38, 0xd2, 0x96, 0xa4, 0x7d, 0xb6, 0x76, 0xfc, 0x6b, 0xe2, 0x9c, 0x74, 0x04, 0xf1,
			0x45, 0x9d, 0x70, 0x59, 0x64, 0x71, 0x87, 0x20, 0x86, 0x5b, 0xcf, 0x65, 0xe6, 0x2d, 0xa8, 0x02,
			0x1b, 0x60, 0x25, 0xad, 0xae, 0xb0, 0xb9, 0xf6, 0x1c, 0x46, 0x61, 0x69, 0x34, 0x40, 0x7e, 0x0f,
			0x55, 0x47, 0xa3, 0x23, 0xdd, 0x51, 0xaf, 0x3a, 0xc3, 0x5c, 0xf9, 0xce, 0xba, 0xc5, 0xea, 0x26,
			0x2c, 0x53, 0x0d, 0x6e, 0x85, 0x28, 0x84, 0x09, 0xd3, 0xdf, 0xcd, 0xf4, 0x41, 0x81, 0x4d, 0x52,
			0x6a, 0xdc, 0x37, 0xc8, 0x6c, 0xc1, 0xab, 0xfa, 0x24, 0xe1, 0x7b, 0x08, 0x0c, 0xbd, 0xb1, 0x4a,
			0x78, 0x88, 0x95, 0x8b, 0xe3, 0x63, 0xe8, 0x6d, 0xe9, 0xcb, 0xd5, 0xfe, 0x3b, 0x00, 0x1d, 0x39,
			0xf2, 0xef, 0xb7, 0x0e, 0x66, 0x58, 0xd0, 0xe4, 0xa6, 0x77, 0x72, 0xf8, 0xeb, 0x75, 0x4b, 0x0a,
			0x31, 0x44, 0x50, 0xb4, 0x8f, 0xed, 0x1f, 0x1a, 0xdb, 0x99, 0x8d, 0x33, 0x9f, 0x11, 0x83, 0x14
		};
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="MD2" /> class.
	/// </summary>
	public MD2()
	{
		HashSizeValue = 128;
		Initialize();
	}
	/// <summary>
	/// Creates an instance of the default implementation of <see cref="MD2" />.
	/// </summary>
	/// <returns>
	/// A new instance of <see cref="MD2" />.
	/// </returns>
	public static new MD2 Create()
	{
		return new();
	}

	/// <summary>
	/// Resets the hash algorithm to its initial state.
	/// </summary>
	public sealed override void Initialize()
	{
		CurrentHash = new byte[16];
		HashedLength = 0;
		CheckSum = new int[16];
		Buffer = new int[48];
		LastBlock = new byte[16];
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
			int remaining = 16 - LastBlockLength;
			if (cbSize >= remaining)
			{
				Array.Copy(array, ibStart, LastBlock, LastBlockLength, remaining);

				Process(LastBlock);
				ibStart += remaining;
				cbSize -= remaining;
				LastBlockLength = 0;
			}
		}

		while (cbSize >= 16)
		{
			Process(array.AsSpan(ibStart, 16));
			ibStart += 16;
			cbSize -= 16;
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

		for (int i = 0; i < padding.Length; i += 16)
		{
			Process(padding.AsSpan(i, 16));
		}

		byte[] finalBlock = new byte[16];
		for (int i = 0; i < 16; i++)
		{
			finalBlock[i] = (byte)CheckSum[i];
		}

		ProcessBlockInternal(finalBlock);

		HashValue = CurrentHash.ToArray();
		return HashValue;
	}
	private void Process(ReadOnlySpan<byte> block)
	{
		ProcessBlockInternal(block);

		for (int i = 0, j = CheckSum[15]; i < 16; i++)
		{
			j = block[i] ^ j;
			j = CheckSum[i] ^= MD2Table[j];
		}
	}
	private void ProcessBlockInternal(ReadOnlySpan<byte> block)
	{
		for (int i = 0; i < 16; i++)
		{
			Buffer[i] = CurrentHash[i];
		}

		for (int i = 16, j = 0; i < 32; i++, j++)
		{
			Buffer[i] = block[j];
		}

		for (int i = 32, j = 0; i < 48; i++, j++)
		{
			Buffer[i] = Buffer[j] ^ block[j];
		}

		for (int i = 0, j = 0; i < 18; i++)
		{
			for (int k = 0; k < 48; k++)
			{
				j = Buffer[k] ^= MD2Table[j];
			}

			j = (j + i) & 0xff;
		}

		for (int i = 0; i < 16; i++)
		{
			CurrentHash[i] = (byte)Buffer[i];
		}
	}
	private byte[] GetPadding(ReadOnlySpan<byte> lastBlock)
	{
		byte[] padding = new byte[16];

		lastBlock.CopyTo(padding);

		byte paddingByte = (byte)(16 - (HashedLength & 0xf));
		for (int i = lastBlock.Length; i < 16; i++)
		{
			padding[i] = paddingByte;
		}

		return padding;
	}
}