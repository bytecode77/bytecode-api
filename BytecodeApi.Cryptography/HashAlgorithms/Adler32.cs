using System.Security.Cryptography;

namespace BytecodeApi.Cryptography.HashAlgorithms;

/// <summary>
/// Computes the <see cref="Adler32" /> checksum for the input data.
/// </summary>
public sealed class Adler32 : HashAlgorithm
{
	private ushort CurrentHashA;
	private ushort CurrentHashB;

	/// <summary>
	/// Initializes a new instance of the <see cref="Adler32" /> class.
	/// </summary>
	public Adler32()
	{
		HashSizeValue = 32;
		Initialize();
	}
	/// <summary>
	/// Creates an instance of the default implementation of <see cref="Adler32" />.
	/// </summary>
	/// <returns>
	/// A new instance of <see cref="Adler32" />.
	/// </returns>
	public static new Adler32 Create()
	{
		return new();
	}

	/// <summary>
	/// Resets the hash algorithm to its initial state.
	/// </summary>
	public sealed override void Initialize()
	{
		CurrentHashA = 1;
		CurrentHashB = 0;
	}
	/// <summary>
	/// Routes data written to the object into the hash algorithm for computing the hash.
	/// </summary>
	/// <param name="array">The input to compute the hash code for.</param>
	/// <param name="ibStart">The offset into the <see cref="byte" />[] from which to begin using data.</param>
	/// <param name="cbSize">The number of bytes in the <see cref="byte" />[] to use as data.</param>
	protected sealed override void HashCore(byte[] array, int ibStart, int cbSize)
	{
		for (int i = ibStart; i < ibStart + cbSize; i++)
		{
			CurrentHashA = (ushort)((CurrentHashA + array[i]) % 0xfff1);
			CurrentHashB = (ushort)((CurrentHashB + CurrentHashA) % 0xfff1);
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
		HashValue =
		[
			(byte)(CurrentHashB >> 8),
			(byte)(CurrentHashB & 0xff),
			(byte)(CurrentHashA >> 8),
			(byte)(CurrentHashA & 0xff)
		];

		return HashValue;
	}
}