namespace BytecodeApi.Cryptography;

/// <summary>
/// Represents a custom hash function to be used by a <see cref="BloomFilter{T}" />.
/// </summary>
/// <typeparam name="T">The type of data to be used in the bloom filter.</typeparam>
/// <param name="data">The data to be hashed.</param>
/// <returns>
/// A 32-bit unsigned integer, representing the hash of the value.
/// </returns>
public delegate uint BloomFilterHashFunction<T>(T data);