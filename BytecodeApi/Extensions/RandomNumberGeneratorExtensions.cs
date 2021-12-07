using BytecodeApi.Mathematics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="RandomNumberGenerator" /> objects.
	/// </summary>
	public static class RandomNumberGeneratorExtensions
	{
		/// <summary>
		/// Creates a <see cref="byte" />[] with a specified length and fills all elements with a cryptographically strong random sequence of bytes.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <param name="count">The size of the returned <see cref="byte" />[].</param>
		/// <returns>
		/// A new <see cref="byte" />[] with a specified length, filled with a cryptographically strong random sequence of bytes.
		/// </returns>
		public static byte[] GetBytes(this RandomNumberGenerator randomNumberGenerator, int count)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));

			byte[] data = new byte[count];
			randomNumberGenerator.GetBytes(data);
			return data;
		}
		/// <summary>
		/// Creates a <see cref="byte" />[] with a specified length and fills all elements with a cryptographically strong random sequence of nonzero bytes.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <param name="count">The size of the returned <see cref="byte" />[].</param>
		/// <returns>
		/// A new <see cref="byte" />[] with a specified length, filled with a cryptographically strong random sequence of nonzero bytes.
		/// </returns>
		public static byte[] GetNonZeroBytes(this RandomNumberGenerator randomNumberGenerator, int count)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));

			byte[] data = new byte[count];
			randomNumberGenerator.GetNonZeroBytes(data);
			return data;
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="bool" /> value that is either <see langword="false" /> or <see langword="true" />.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="bool" /> value that is either <see langword="false" /> or <see langword="true" />.
		/// </returns>
		public static bool GetBoolean(this RandomNumberGenerator randomNumberGenerator)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));

			return (randomNumberGenerator.GetByte() & 1) == 1;
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="byte" /> value that is greater than or equal to 0, and less than or equal to <see cref="byte.MaxValue" />.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="byte" /> value that is greater than or equal to 0, and less than or equal to <see cref="byte.MaxValue" />.
		/// </returns>
		public static byte GetByte(this RandomNumberGenerator randomNumberGenerator)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));

			return randomNumberGenerator.GetBytes(1)[0];
		}
		/// <summary>
		/// Generates a cryptographically strong random nonzero <see cref="byte" /> value that is less than or equal to <see cref="byte.MaxValue" />.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <returns>
		/// A cryptographically strong random nonzero <see cref="byte" /> value that is less than or equal to <see cref="byte.MaxValue" />.
		/// </returns>
		public static byte GetNonZeroByte(this RandomNumberGenerator randomNumberGenerator)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));

			return randomNumberGenerator.GetNonZeroBytes(1)[0];
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="sbyte" /> value that is greater than or equal to <see cref="sbyte.MinValue" />, and less than or equal to <see cref="sbyte.MaxValue" />.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="sbyte" /> value that is greater than or equal to <see cref="sbyte.MinValue" />, and less than or equal to <see cref="sbyte.MaxValue" />.
		/// </returns>
		public static sbyte GetSByte(this RandomNumberGenerator randomNumberGenerator)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));

			return (sbyte)(randomNumberGenerator.GetByte() - 128);
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="char" /> value that is greater than or equal to '\0', and less than or equal to <see cref="char.MaxValue" />.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="char" /> value that is greater than or equal to '\0', and less than or equal to <see cref="char.MaxValue" />.
		/// </returns>
		public static char GetChar(this RandomNumberGenerator randomNumberGenerator)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));

			return BitConverter.ToChar(randomNumberGenerator.GetBytes(2), 0);
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="int" /> value that is greater than or equal to <see cref="int.MinValue" />, and less than or equal to <see cref="int.MaxValue" />.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="int" /> value that is greater than or equal to <see cref="int.MinValue" />, and less than or equal to <see cref="int.MaxValue" />.
		/// </returns>
		public static int GetInt32(this RandomNumberGenerator randomNumberGenerator)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));

			return BitConverter.ToInt32(randomNumberGenerator.GetBytes(4), 0);
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="int" /> value that is less than the specified maximum.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <param name="maxValue">The exclusive upper bound of the random number to be generated. <paramref name="maxValue" /> must be greater than or equal to 0.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="int" /> value that is less than the specified maximum.
		/// </returns>
		public static int GetInt32(this RandomNumberGenerator randomNumberGenerator, int maxValue)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(maxValue, nameof(maxValue));

			byte[] bytes = randomNumberGenerator.GetBytes(4);
			BitCalculator.SetBit(bytes, 31, false);

			return (int)((long)BitConverter.ToInt32(bytes, 0) * maxValue / int.MaxValue);
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="int" /> value that is less than the specified maximum.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <param name="minValue">The inclusive lower bound of the random number returned.</param>
		/// <param name="maxValue">The exclusive upper bound of the random number to be generated. <paramref name="maxValue" /> must be greater than or equal to 0.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="int" /> value that is less than the specified maximum.
		/// </returns>
		public static int GetInt32(this RandomNumberGenerator randomNumberGenerator, int minValue, int maxValue)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(maxValue, nameof(maxValue));
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(maxValue, minValue, nameof(maxValue), nameof(minValue));

			return minValue + randomNumberGenerator.GetInt32(maxValue - minValue);
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="uint" /> value that is greater than or equal to 0, and less than or equal to <see cref="uint.MaxValue" />.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="uint" /> value that is greater than or equal to 0, and less than or equal to <see cref="uint.MaxValue" />.
		/// </returns>
		public static uint GetUInt32(this RandomNumberGenerator randomNumberGenerator)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));

			return BitConverter.ToUInt32(randomNumberGenerator.GetBytes(4), 0);
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="long" /> value that is greater than or equal to <see cref="long.MinValue" />, and less than or equal to <see cref="long.MaxValue" />.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="long" /> value that is greater than or equal to <see cref="long.MinValue" />, and less than or equal to <see cref="long.MaxValue" />.
		/// </returns>
		public static long GetInt64(this RandomNumberGenerator randomNumberGenerator)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));

			return BitConverter.ToInt64(randomNumberGenerator.GetBytes(8), 0);
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="ulong" /> value that is greater than or equal to 0UL, and less than or equal to <see cref="ulong.MaxValue" />.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="ulong" /> value that is greater than or equal to 0UL, and less than or equal to <see cref="ulong.MaxValue" />.
		/// </returns>
		public static ulong GetUInt64(this RandomNumberGenerator randomNumberGenerator)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));

			return BitConverter.ToUInt64(randomNumberGenerator.GetBytes(8), 0);
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="short" /> value that is greater than or equal to <see cref="short.MinValue" />, and less than or equal to <see cref="short.MaxValue" />.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="short" /> value that is greater than or equal to <see cref="short.MinValue" />, and less than or equal to <see cref="short.MaxValue" />.
		/// </returns>
		public static short GetInt16(this RandomNumberGenerator randomNumberGenerator)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));

			return BitConverter.ToInt16(randomNumberGenerator.GetBytes(2), 0);
		}
		/// <summary>
		/// Generates a cryptographically strong random <see cref="ushort" /> value that is greater than or equal to 0, and less than or equal to <see cref="ushort.MaxValue" />.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <returns>
		/// A cryptographically strong random <see cref="ushort" /> value that is greater than or equal to 0, and less than or equal to <see cref="ushort.MaxValue" />.
		/// </returns>
		public static ushort GetUInt16(this RandomNumberGenerator randomNumberGenerator)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));

			return BitConverter.ToUInt16(randomNumberGenerator.GetBytes(2), 0);
		}
		/// <summary>
		/// Creates a <see cref="BitArray" /> with a specified length and fills all elements with a cryptographically strong random sequence of <see cref="bool" /> values.
		/// </summary>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <param name="count">The size of the returned <see cref="BitArray" />.</param>
		/// <returns>
		/// A new <see cref="BitArray" /> with a specified length, filled with a cryptographically strong random sequence of <see cref="bool" /> values.
		/// </returns>
		public static BitArray GetBits(this RandomNumberGenerator randomNumberGenerator, int count)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));

			BitArray bits = new BitArray(count);
			byte[] buffer = new byte[16];
			int bufferPosition = int.MaxValue;

			for (int i = 0; i < count; i++)
			{
				if (bufferPosition >= buffer.Length << 3)
				{
					randomNumberGenerator.GetBytes(buffer);
					bufferPosition = 0;
				}

				bits[i] = (buffer[bufferPosition >> 3] & 1 << (bufferPosition & 7)) > 0;
				bufferPosition++;
			}

			return bits;
		}
		/// <summary>
		/// Returns a random <see cref="object" /> of the specified type from <paramref name="list" />, selected based on a cryptographically strong random index.
		/// </summary>
		/// <typeparam name="T">The element type of <paramref name="list" />.</typeparam>
		/// <param name="randomNumberGenerator">The <see cref="RandomNumberGenerator" /> object to be used for random number generation.</param>
		/// <param name="list">A <see cref="IList{T}" /> of the specified type.</param>
		/// <returns>
		/// A random <see cref="object" /> of the specified type from <paramref name="list" />.
		/// </returns>
		public static T GetObject<T>(this RandomNumberGenerator randomNumberGenerator, IList<T> list)
		{
			Check.ArgumentNull(randomNumberGenerator, nameof(randomNumberGenerator));
			Check.ArgumentNull(list, nameof(list));
			Check.ArgumentEx.ArrayElementsRequired(list, nameof(list));

			return list[randomNumberGenerator.GetInt32(list.Count)];
		}
	}
}