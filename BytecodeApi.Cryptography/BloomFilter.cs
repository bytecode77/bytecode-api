using BytecodeApi.Extensions;
using System.Collections;
using System.Numerics;

namespace BytecodeApi.Cryptography;

/// <summary>
/// Represents a bloom filter.
/// </summary>
/// <typeparam name="T">The type of data to be added to the bloom filter.</typeparam>
public sealed class BloomFilter<T>
{
	private readonly BitArray Bits;
	private readonly int HashMask;
	/// <summary>
	/// Gets the size, in bits, of this <see cref="BloomFilter{T}" />.
	/// </summary>
	public int Size => Bits.Length;
	/// <summary>
	/// Gets the number of values that have been added.
	/// </summary>
	public int ValueCount { get; private set; }
	/// <summary>
	/// Gets the number of bits that are set to 1.
	/// </summary>
	public int BitsSet { get; private set; }
	/// <summary>
	/// Gets a collection of hash functions to be used for comparison. At least one hash function is required.
	/// </summary>
	public List<BloomFilterHashFunction<T>> HashFunctions { get; private init; }

	/// <summary>
	/// Initializes a new instance of the <see cref="BloomFilter{T}" /> class.
	/// </summary>
	/// <param name="capacity">The size, in bits, of the bloom filter. This value is rounded up to the next power of 2 value.</param>
	public BloomFilter(int capacity)
	{
		Check.ArgumentOutOfRangeEx.Greater0(capacity);
		Check.ArgumentOutOfRange(capacity <= 1024 * 1024 * 1024, nameof(capacity), "Maximum capacity is 1073741824.");

		Bits = new((int)BitOperations.RoundUpToPowerOf2((uint)capacity));
		HashMask = Bits.Length - 1;
		HashFunctions = [];
	}

	/// <summary>
	/// Adds a value to the bloom filter by hashing it using all of the specified hash functions.
	/// </summary>
	/// <param name="value">The value to be hashed and added to the bloom filter.</param>
	public void Add(T value)
	{
		Check.ArgumentNull(value);
		Check.ArgumentEx.ArrayElementsRequired(HashFunctions);
		Check.ArgumentEx.ArrayValuesNotNull(HashFunctions);

		foreach (BloomFilterHashFunction<T> hashFunction in HashFunctions)
		{
			int index = (int)hashFunction(value) & HashMask;

			if (!Bits[index])
			{
				Bits[index] = true;
				BitsSet++;
			}
		}

		ValueCount++;
	}
	/// <summary>
	/// Checks, if the specified value is found in the bloom filter by hashing it using all of the specified hash functions.
	/// </summary>
	/// <param name="value">The value to be searched in the bloom filter.</param>
	/// <returns>
	/// <see langword="true" />, if all generated hashes were found in the bloom filter. The value is likely in the bloom filter;
	/// <see langword="false" />, if at least one of the generated hashes was not found in the bloom filter. The value is definitely not in the bloom filter.
	/// </returns>
	public bool Contains(T value)
	{
		Check.ArgumentNull(value);
		Check.ArgumentEx.ArrayElementsRequired(HashFunctions);
		Check.ArgumentEx.ArrayValuesNotNull(HashFunctions);

		foreach (BloomFilterHashFunction<T> hashFunction in HashFunctions)
		{
			int index = (int)hashFunction(value) & HashMask;

			if (!Bits[index])
			{
				return false;
			}
		}

		return true;
	}
	/// <summary>
	/// Exports this <see cref="BloomFilter{T}" /> to a <see cref="byte" />[].
	/// </summary>
	/// <returns>
	/// A new <see cref="byte" />[] with the bits of this <see cref="BloomFilter{T}" />.
	/// </returns>
	public byte[] ToArray()
	{
		return Bits.ToByteArray();
	}
}