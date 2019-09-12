using BytecodeApi.Mathematics;
using System.Collections;

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

			if (cryptographic)
			{
				lock (MathEx._RandomNumberGenerator)
				{
					byte[] bytes = new byte[1];

					for (int i = offset; i < offset + count; i++)
					{
						MathEx._RandomNumberGenerator.GetBytes(bytes);
						array[i] = (bytes[0] & 1) == 1;
					}
				}
			}
			else
			{
				lock (MathEx._Random)
				{
					for (int i = offset; i < offset + count; i++)
					{
						array[i] = (MathEx._Random.Next() & 1) == 1;
					}
				}
			}
		}
	}
}