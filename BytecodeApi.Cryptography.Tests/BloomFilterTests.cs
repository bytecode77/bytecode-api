using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;

namespace BytecodeApi.Cryptography.Tests;

public class BloomFilterTests
{
	[Theory]
	[InlineData(false, false)]
	[InlineData(true, false)]
	[InlineData(false, true)]
	[InlineData(true, true)]
	public void BloomFilterTests_AddAndContains(bool secondHashFunction, bool thirdHashFunction)
	{
		string[] data = new[] { "hello", "world", "foo", "bar", "bloom", "filters", "are", "great" };

		for (int i = 0; i < 1000; i++)
		{
			BloomFilter<string> bloomFilter = new(MathEx.Random.Next(64, 1024 * 1024 + 1));

			bloomFilter.HashFunctions.Add(str => BitConverter.ToUInt32(Hashes.ComputeBytes(str.ToUTF8Bytes(), HashType.CRC32)));

			if (secondHashFunction)
			{
				bloomFilter.HashFunctions.Add(str => BitConverter.ToUInt32(Hashes.ComputeBytes(str.ToUTF8Bytes(), HashType.Adler32)));
			}

			if (thirdHashFunction)
			{
				bloomFilter.HashFunctions.Add(str => BitConverter.ToUInt32(Hashes.ComputeBytes(str.ToUTF8Bytes(), HashType.SHA1)));
			}

			foreach (string str in data)
			{
				bloomFilter.Add(str);
			}

			Assert.Equal(bloomFilter.ValueCount, data.Length);
			Assert.True(bloomFilter.BitsSet > 0);

			foreach (string str in data)
			{
				Assert.True(bloomFilter.Contains(str));
			}
		}
	}
}