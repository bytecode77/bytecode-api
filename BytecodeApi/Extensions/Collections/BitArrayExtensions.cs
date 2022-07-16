using BytecodeApi.Mathematics;
using System.Collections;
using System.Linq;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="BitArray" /> objects.
	/// </summary>
	public static class BitArrayExtensions
	{
		/// <summary>
		/// Converts the bits of this <see cref="BitArray" /> to an equivalent <see cref="bool" />[].
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to convert.</param>
		/// <returns>
		/// An equivalent <see cref="bool" />[] representing this <see cref="BitArray" />.
		/// </returns>
		public static bool[] ToBooleanArray(this BitArray array)
		{
			Check.ArgumentNull(array, nameof(array));

			bool[] values = new bool[array.Length];
			for (int i = 0; i < values.Length; i++) values[i] = array[i];
			return values;
		}
		/// <summary>
		/// Converts the bits of this <see cref="BitArray" /> to an equivalent <see cref="byte" />[]. If there is a padding of less than 8 bits, the last byte contains the remaining bits.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to convert.</param>
		/// <returns>
		/// An equivalent <see cref="byte" />[] representing this <see cref="BitArray" />.
		/// </returns>
		public static byte[] ToByteArray(this BitArray array)
		{
			Check.ArgumentNull(array, nameof(array));

			byte[] bytes = new byte[(array.Length + 7) / 8];

			for (int i = 0, position = 0; i < bytes.Length; i++)
			{
				for (int j = 0; j < 8 && position < array.Length; j++, position++)
				{
					if (array[position]) bytes[i] = (byte)(bytes[i] | 1 << j);
				}
			}

			return bytes;
		}
		/// <summary>
		/// Converts the bits of this <see cref="BitArray" /> to a <see cref="string" /> containing a sequence of '0' or '1' characters.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to convert.</param>
		/// <returns>
		/// An equivalent sequence of '0' or '1' characters representing this <see cref="BitArray" />.
		/// </returns>
		public static string ToBitString(this BitArray array)
		{
			Check.ArgumentNull(array, nameof(array));

			char[] str = new char[array.Length];
			for (int i = 0; i < str.Length; i++) str[i] = array[i] ? '1' : '0';

			return str.AsString();
		}

		/// <summary>
		/// Returns the number of values in this <see cref="BitArray" /> whose value is <see langword="true" />.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to check.</param>
		/// <returns>
		/// The number of values whose value is <see langword="true" />.
		/// </returns>
		public static int CountTrue(this BitArray array)
		{
			Check.ArgumentNull(array, nameof(array));

			int count = 0;
			foreach (bool value in array)
			{
				if (value) count++;
			}

			return count;
		}
		/// <summary>
		/// Returns the number of values in this <see cref="BitArray" /> whose value is <see langword="false" />.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to check.</param>
		/// <returns>
		/// The number of values whose value is <see langword="false" />.
		/// </returns>
		public static int CountFalse(this BitArray array)
		{
			Check.ArgumentNull(array, nameof(array));

			return array.Length - array.CountTrue();
		}
		/// <summary>
		/// Returns a value indicating whether all elements of this <see cref="BitArray" /> are <see langword="true" />.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to check.</param>
		/// <returns>
		/// A value indicating whether all elements of this <see cref="BitArray" /> are <see langword="true" />.
		/// </returns>
		public static bool AllTrue(this BitArray array)
		{
			Check.ArgumentNull(array, nameof(array));

			foreach (bool value in array)
			{
				if (!value) return false;
			}

			return true;
		}
		/// <summary>
		/// Returns a value indicating whether all elements of this <see cref="BitArray" /> are <see langword="false" />.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to check.</param>
		/// <returns>
		/// A value indicating whether all elements of this <see cref="BitArray" /> are <see langword="false" />.
		/// </returns>
		public static bool AllFalse(this BitArray array)
		{
			Check.ArgumentNull(array, nameof(array));

			foreach (bool value in array)
			{
				if (value) return false;
			}

			return true;
		}
		/// <summary>
		/// Compares the content of this <see cref="BitArray" /> agains another <see cref="BitArray" />. Returns <see langword="true" />, if both arrays contain the exact same set of data. If <paramref name="array" /> and <paramref name="otherArray" /> are both <see langword="null" />, <see langword="true" /> is returned.
		/// </summary>
		/// <param name="array">A <see cref="BitArray" /> to compare to <paramref name="otherArray" />.</param>
		/// <param name="otherArray">A <see cref="BitArray" /> to compare to <paramref name="array" />.</param>
		/// <returns>
		/// <see langword="true" />, if both arrays contain the exact same set of data or if <paramref name="array" /> and <paramref name="otherArray" /> are both <see langword="null" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool Compare(this BitArray array, BitArray otherArray)
		{
			if (array == otherArray)
			{
				return true;
			}
			else if (array?.Length == otherArray?.Length)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != otherArray[i]) return false;
				}
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Copies a specified number of bits from this <see cref="BitArray" /> and returns a new <see cref="BitArray" /> representing a fraction of the original <see cref="BitArray" />.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to take the subset of bits from.</param>
		/// <param name="index">A <see cref="int" /> value specifying the offset from which to start copying bits.</param>
		/// <param name="count">A <see cref="int" /> value specifying the number of bits to copy.</param>
		/// <returns>
		/// A new <see cref="BitArray" /> representing a fraction of the original <see cref="BitArray" />.
		/// </returns>
		public static BitArray GetBits(this BitArray array, int index, int count)
		{
			Check.ArgumentNull(array, nameof(array));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(index, nameof(index));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));
			Check.ArgumentEx.OffsetAndLengthOutOfBounds(index, count, array.Length);

			BitArray copy = new BitArray(count);
			for (int i = 0; i < count; i++) copy[i] = array[index + i];
			return copy;
		}

		/// <summary>
		/// Sets all bits in the specified range in this <see cref="BitArray" /> to the specified value.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to write the new value to.</param>
		/// <param name="offset">The zero-based index in this <see cref="BitArray" /> at which to begin writing values.</param>
		/// <param name="count">The number of values to write.</param>
		/// <param name="value">The <see cref="bool" /> value to assign to all bits in the specified range.</param>
		public static void SetAll(this BitArray array, int offset, int count, bool value)
		{
			Check.ArgumentNull(array, nameof(array));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(offset, nameof(offset));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));
			Check.ArgumentEx.OffsetAndLengthOutOfBounds(offset, count, array.Length);

			for (int i = offset; i < offset + count; i++)
			{
				array[i] = value;
			}
		}
		/// <summary>
		/// Sets the values of this <see cref="BitArray" /> to random values.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to randomize.</param>
		public static void SetRandomValues(this BitArray array)
		{
			array.SetRandomValues(0, array.Length);
		}
		/// <summary>
		/// Sets the values of this <see cref="BitArray" /> to random values.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to randomize.</param>
		/// <param name="cryptographic"><see langword="true" /> to generate cryptographic random values.</param>
		public static void SetRandomValues(this BitArray array, bool cryptographic)
		{
			array.SetRandomValues(0, array.Length, cryptographic);
		}
		/// <summary>
		/// Sets the values of this <see cref="BitArray" /> to random values.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to randomize.</param>
		/// <param name="offset">The zero-based index in this <see cref="BitArray" /> at which to begin writing values.</param>
		/// <param name="count">The number of values to write.</param>
		public static void SetRandomValues(this BitArray array, int offset, int count)
		{
			array.SetRandomValues(offset, count, false);
		}
		/// <summary>
		/// Sets the values of this <see cref="BitArray" /> to random values.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to randomize.</param>
		/// <param name="offset">The zero-based index in this <see cref="BitArray" /> at which to begin writing values.</param>
		/// <param name="count">The number of values to write.</param>
		/// <param name="cryptographic"><see langword="true" /> to generate cryptographic random values.</param>
		public static void SetRandomValues(this BitArray array, int offset, int count, bool cryptographic)
		{
			Check.ArgumentNull(array, nameof(array));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(offset, nameof(offset));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));
			Check.ArgumentEx.OffsetAndLengthOutOfBounds(offset, count, array.Length);

			byte[] buffer = new byte[16];
			int bufferPosition = int.MaxValue;

			if (cryptographic)
			{
				for (int i = offset; i < offset + count; i++)
				{
					if (bufferPosition >= buffer.Length << 3)
					{
						MathEx.RandomNumberGenerator.GetBytes(buffer);
						bufferPosition = 0;
					}

					array[i] = (buffer[bufferPosition >> 3] & 1 << (bufferPosition & 7)) > 0;
					bufferPosition++;
				}
			}
			else
			{
				for (int i = offset; i < offset + count; i++)
				{
					if (bufferPosition >= buffer.Length << 3)
					{
						MathEx.Random.NextBytes(buffer);
						bufferPosition = 0;
					}

					array[i] = (buffer[bufferPosition >> 3] & 1 << (bufferPosition & 7)) > 0;
					bufferPosition++;
				}
			}
		}

		/// <summary>
		/// Copies a specified number of elements from this <see cref="BitArray" /> to <paramref name="dest" />, beginning at the specified source offset, written to the specified destination offset.
		/// </summary>
		/// <param name="array">The <see cref="BitArray" /> to be written to <paramref name="dest" />.</param>
		/// <param name="sourceOffset">The offset, at which to start reading from this <see cref="BitArray" />.</param>
		/// <param name="dest">The <see cref="BitArray" /> that is written to.</param>
		/// <param name="destOffset">The offset, at which to start writing to <paramref name="dest" />.</param>
		/// <param name="count">The number of <see cref="bool" /> values to copy.</param>
		public static void CopyTo(this BitArray array, int sourceOffset, BitArray dest, int destOffset, int count)
		{
			Check.ArgumentNull(array, nameof(array));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(sourceOffset, nameof(sourceOffset));
			Check.ArgumentNull(dest, nameof(dest));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(destOffset, nameof(destOffset));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));
			Check.ArgumentEx.OffsetAndLengthOutOfBounds(sourceOffset, count, array.Length);
			Check.ArgumentEx.OffsetAndLengthOutOfBounds(destOffset, count, dest.Length);

			for (int i = 0; i < count; i++)
			{
				dest[i + destOffset] = array[i + sourceOffset];
			}
		}
		/// <summary>
		/// Merges all <see cref="BitArray" /> objects and returns a new <see cref="BitArray" />, where <paramref name="otherArrays" /> are concatenated after this array.
		/// </summary>
		/// <param name="array">The first <see cref="BitArray" /> object.</param>
		/// <param name="otherArrays">An array of <see cref="BitArray" /> objects to append.</param>
		/// <returns>
		/// A new <see cref="BitArray" /> starting with <paramref name="array" />, followed by all elements from <paramref name="otherArrays" />.
		/// </returns>
		public static BitArray Concat(this BitArray array, params BitArray[] otherArrays)
		{
			Check.ArgumentNull(array, nameof(array));
			Check.ArgumentNull(otherArrays, nameof(otherArrays));
			Check.ArgumentEx.ArrayValuesNotNull(otherArrays, nameof(otherArrays));

			BitArray result = new BitArray(array.Length + otherArrays.Sum(other => other.Length));
			array.CopyTo(0, result, 0, array.Length);

			int offset = array.Length;
			foreach (BitArray other in otherArrays)
			{
				other.CopyTo(0, result, offset, other.Length);
				offset += other.Length;
			}

			return result;
		}
	}
}