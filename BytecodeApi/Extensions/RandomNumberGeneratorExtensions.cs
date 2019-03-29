using System;
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
	}
}