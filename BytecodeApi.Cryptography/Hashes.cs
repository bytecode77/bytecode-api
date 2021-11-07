using BytecodeApi.Extensions;
using System;
using System.Security.Cryptography;
using System.Text;

namespace BytecodeApi.Cryptography
{
	/// <summary>
	/// Class to compute hashes of a specific <see cref="HashType" />.
	/// </summary>
	public static class Hashes
	{
		/// <summary>
		/// Computes the hash value for the specified <see cref="string" /> using the specified <see cref="HashType" />. The <see cref="string" /> is converted using the <see cref="Encoding.UTF8" /> encoding.
		/// </summary>
		/// <param name="data">The <see cref="string" /> to be used in the hash computation. The <see cref="string" /> is converted using the <see cref="Encoding.UTF8" /> encoding.</param>
		/// <param name="type">The <see cref="HashType" /> specifying the algorithm that is used.</param>
		/// <returns>
		/// A <see cref="byte" />[] representing the fixed-length binary of the hash of the <paramref name="data" /> parameter.
		/// </returns>
		public static byte[] ComputeBytes(string data, HashType type)
		{
			return ComputeBytes(data, type, 1);
		}
		/// <summary>
		/// Computes the hash value for the specified <see cref="string" /> using the specified <see cref="HashType" /> and repeats computation a specified number of times. The <see cref="string" /> is converted using the <see cref="Encoding.UTF8" /> encoding.
		/// </summary>
		/// <param name="data">The <see cref="string" /> to be used in the hash computation. The <see cref="string" /> is converted using the <see cref="Encoding.UTF8" /> encoding.</param>
		/// <param name="type">The <see cref="HashType" /> specifying the algorithm that is used.</param>
		/// <param name="passes">A <see cref="int" /> value indicating the number of times <paramref name="data" /> should be processed. For successive passes, the binary result of the previous pass is used as input value for the next pass.</param>
		/// <returns>
		/// A <see cref="byte" />[] representing the fixed-length binary of the hash of the <paramref name="data" /> parameter.
		/// </returns>
		public static byte[] ComputeBytes(string data, HashType type, int passes)
		{
			return ComputeBytes(data?.ToUTF8Bytes(), type, passes);
		}
		/// <summary>
		/// Computes the hash value for the specified <see cref="byte" />[] using the specified <see cref="HashType" />.
		/// </summary>
		/// <param name="data">The <see cref="byte" />[] to be used in the hash computation.</param>
		/// <param name="type">The <see cref="HashType" /> specifying the algorithm that is used.</param>
		/// <returns>
		/// A <see cref="byte" />[] representing the fixed-length binary of the hash of the <paramref name="data" /> parameter.
		/// </returns>
		public static byte[] ComputeBytes(byte[] data, HashType type)
		{
			return ComputeBytes(data, type, 1);
		}
		/// <summary>
		/// Computes the hash value for the specified <see cref="byte" />[] using the specified <see cref="HashType" /> and repeats computation a specified number of times.
		/// </summary>
		/// <param name="data">The <see cref="byte" />[] to be used in the hash computation.</param>
		/// <param name="type">The <see cref="HashType" /> specifying the algorithm that is used.</param>
		/// <param name="passes">A <see cref="int" /> value indicating the number of times <paramref name="data" /> should be processed. For successive passes, the binary result of the previous pass is used as input value for the next pass.</param>
		/// <returns>
		/// A <see cref="byte" />[] representing the fixed-length binary of the hash of the <paramref name="data" /> parameter.
		/// </returns>
		public static byte[] ComputeBytes(byte[] data, HashType type, int passes)
		{
			Check.ArgumentNull(data, nameof(data));
			Check.ArgumentOutOfRangeEx.Greater0(passes, nameof(passes));

			if (type == HashType.Adler32)
			{
				Repeat(ref data, HashAlgorithms.Adler32);
			}
			else if (type == HashType.CRC32)
			{
				Repeat(ref data, HashAlgorithms.Crc32);
			}
			else if (type == HashType.CRC64)
			{
				Repeat(ref data, HashAlgorithms.Crc64);
			}
			else
			{
				using (HashAlgorithm hash = HashAlgorithm.Create(type.ToString()))
				{
					if (hash == null) throw Throw.InvalidEnumArgument(nameof(type), type);
					else Repeat(ref data, hash.ComputeHash);
				}
			}

			return data;

			void Repeat(ref byte[] bytes, Func<byte[], byte[]> func)
			{
				for (int i = 0; i < passes; i++) bytes = func(bytes);
			}
		}
		/// <summary>
		/// Computes the hash value for the specified <see cref="string" /> using the specified <see cref="HashType" />. The <see cref="string" /> is converted using the <see cref="Encoding.UTF8" /> encoding. The result is the lowercase hexadecimal hash representation of the <paramref name="data" /> parameter.
		/// </summary>
		/// <param name="data">The <see cref="string" /> to be used in the hash computation. The <see cref="string" /> is converted using the <see cref="Encoding.UTF8" /> encoding.</param>
		/// <param name="type">The <see cref="HashType" /> specifying the algorithm that is used.</param>
		/// <returns>
		/// The lowercase hexadecimal hash representation of the <paramref name="data" /> parameter.
		/// </returns>
		public static string Compute(string data, HashType type)
		{
			return Compute(data, type, 1);
		}
		/// <summary>
		/// Computes the hash value for the specified <see cref="string" /> using the specified <see cref="HashType" /> and repeats computation a specified number of times. The <see cref="string" /> is converted using the <see cref="Encoding.UTF8" /> encoding. The result is the lowercase hexadecimal hash representation of the <paramref name="data" /> parameter.
		/// </summary>
		/// <param name="data">The <see cref="string" /> to be used in the hash computation. The <see cref="string" /> is converted using the <see cref="Encoding.UTF8" /> encoding.</param>
		/// <param name="type">The <see cref="HashType" /> specifying the algorithm that is used.</param>
		/// <param name="passes">A <see cref="int" /> value indicating the number of times <paramref name="data" /> should be processed. For successive passes, the binary result of the previous pass is used as input value for the next pass.</param>
		/// <returns>
		/// The lowercase hexadecimal hash representation of the <paramref name="data" /> parameter.
		/// </returns>
		public static string Compute(string data, HashType type, int passes)
		{
			Check.ArgumentNull(data, nameof(data));
			Check.ArgumentOutOfRangeEx.Greater0(passes, nameof(passes));

			return ComputeBytes(data?.ToUTF8Bytes(), type, passes).ToHexadecimalString();
		}
		/// <summary>
		/// Computes the hash value for the specified <see cref="byte" />[] using the specified <see cref="HashType" />. The result is the lowercase hexadecimal hash representation of the <paramref name="data" /> parameter.
		/// </summary>
		/// <param name="data">The <see cref="byte" />[] to be used in the hash computation.</param>
		/// <param name="type">The <see cref="HashType" /> specifying the algorithm that is used.</param>
		/// <returns>
		/// The lowercase hexadecimal hash representation of the <paramref name="data" /> parameter.
		/// </returns>
		public static string Compute(byte[] data, HashType type)
		{
			return Compute(data, type, 1);
		}
		/// <summary>
		/// Computes the hash value for the specified <see cref="byte" />[] using the specified <see cref="HashType" /> and repeats computation a specified number of times. The result is the lowercase hexadecimal hash representation of the <paramref name="data" /> parameter.
		/// </summary>
		/// <param name="data">The <see cref="byte" />[] to be used in the hash computation.</param>
		/// <param name="type">The <see cref="HashType" /> specifying the algorithm that is used.</param>
		/// <param name="passes">A <see cref="int" /> value indicating the number of times <paramref name="data" /> should be processed. For successive passes, the binary result of the previous pass is used as input value for the next pass.</param>
		/// <returns>
		/// The lowercase hexadecimal hash representation of the <paramref name="data" /> parameter.
		/// </returns>
		public static string Compute(byte[] data, HashType type, int passes)
		{
			Check.ArgumentNull(data, nameof(data));
			Check.ArgumentOutOfRangeEx.Greater0(passes, nameof(passes));

			return ComputeBytes(data, type, passes).ToHexadecimalString();
		}
	}
}