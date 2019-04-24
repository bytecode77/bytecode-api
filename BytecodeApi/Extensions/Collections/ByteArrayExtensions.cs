using System;
using System.Linq;
using System.Text;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="byte" />[] objects.
	/// </summary>
	public static class ByteArrayExtensions
	{
		/// <summary>
		/// Compares the content of this <see cref="byte" />[] agains another <see cref="byte" />[]. Returns <see langword="true" />, if both arrays contain the exact same set of data. If <paramref name="array" /> and <paramref name="otherArray" /> are both <see langword="null" />, <see langword="true" /> is returned.
		/// </summary>
		/// <param name="array">A <see cref="byte" />[] to compare to <paramref name="otherArray" />.</param>
		/// <param name="otherArray">A <see cref="byte" />[] to compare to <paramref name="array" />.</param>
		/// <returns>
		/// <see langword="true" />, if both arrays contain the exact same set of data or if <paramref name="array" /> and <paramref name="otherArray" /> are both <see langword="null" />;
		/// otherwise <see langword="false" />.
		/// </returns>
		public static bool Compare(this byte[] array, byte[] otherArray)
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
		/// Copies a specified number of bytes from this <see cref="byte" />[] and returns a new array representing a fraction of the original <see cref="byte" />[].
		/// </summary>
		/// <param name="array">The <see cref="byte" />[] to take the subset of bytes from.</param>
		/// <param name="index">A <see cref="int" /> value specifying the offset from which to start copying bytes.</param>
		/// <param name="count">A <see cref="int" /> value specifying the number of bytes to copy.</param>
		/// <returns>
		/// A new <see cref="byte" />[] representing a fraction of the original <see cref="byte" />[].
		/// </returns>
		public static byte[] GetBytes(this byte[] array, int index, int count)
		{
			Check.ArgumentNull(array, nameof(array));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(index, nameof(index));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(count, nameof(count));
			Check.ArgumentEx.OffsetAndLengthOutOfBounds(index, count, array.Length);

			byte[] result = new byte[count];
			Buffer.BlockCopy(array, index, result, 0, count);
			return result;
		}
		/// <summary>
		/// Searches this <see cref="byte" />[] for the first occurrence of <paramref name="sequence" />. If not found, returns -1.
		/// </summary>
		/// <param name="array">The <see cref="byte" />[] to be searched.</param>
		/// <param name="sequence">The <see cref="byte" />[] to search for.</param>
		/// <returns>
		/// The index of the first occurrence of <paramref name="sequence" /> and -1, if not found.
		/// </returns>
		public static int FindSequence(this byte[] array, byte[] sequence)
		{
			return array.FindSequence(sequence, 0);
		}
		/// <summary>
		/// Searches this <see cref="byte" />[] for the first occurrence of <paramref name="sequence" /> starting from <paramref name="startIndex" />. If not found, returns -1.
		/// </summary>
		/// <param name="array">The <see cref="byte" />[] to be searched.</param>
		/// <param name="sequence">The <see cref="byte" />[] to search for.</param>
		/// <param name="startIndex">The zero-based starting position to start searching from.</param>
		/// <returns>
		/// The index of the first occurrence of <paramref name="sequence" /> and -1, if not found.
		/// </returns>
		public static int FindSequence(this byte[] array, byte[] sequence, int startIndex)
		{
			Check.ArgumentNull(array, nameof(array));
			Check.ArgumentNull(sequence, nameof(sequence));
			Check.ArgumentEx.ArrayElementsRequired(sequence, nameof(sequence));
			Check.ArgumentEx.OffsetAndLengthOutOfBounds(startIndex, 0, array.Length);

			for (int i = startIndex; i < array.Length - sequence.Length + 1; i++)
			{
				bool found = true;
				for (int j = 0; j < sequence.Length; j++)
				{
					if (array[i + j] != sequence[j])
					{
						found = false;
						break;
					}
				}
				if (found) return i;
			}

			return -1;
		}
		/// <summary>
		/// Decodes all the bytes in this <see cref="byte" />[] into a <see cref="string" /> using the <see cref="Encoding.Default" /> encoding.
		/// </summary>
		/// <param name="array">The <see cref="byte" />[] containing the sequence of bytes to decode.</param>
		/// <returns>
		/// A <see cref="string" /> that contains the results of decoding this sequence of bytes.
		/// </returns>
		public static string ToAnsiString(this byte[] array)
		{
			Check.ArgumentNull(array, nameof(array));

			return Encoding.Default.GetString(array);
		}
		/// <summary>
		/// Decodes all the bytes in this <see cref="byte" />[] into a <see cref="string" /> using the <see cref="Encoding.UTF8" /> encoding.
		/// </summary>
		/// <param name="array">The <see cref="byte" />[] containing the sequence of bytes to decode.</param>
		/// <returns>
		/// A <see cref="string" /> that contains the results of decoding this sequence of bytes.
		/// </returns>
		public static string ToUTF8String(this byte[] array)
		{
			Check.ArgumentNull(array, nameof(array));

			return Encoding.UTF8.GetString(array);
		}
		/// <summary>
		/// Decodes all the bytes in this <see cref="byte" />[] into a <see cref="string" /> using the <see cref="Encoding.Unicode" /> encoding.
		/// </summary>
		/// <param name="array">The <see cref="byte" />[] containing the sequence of bytes to decode.</param>
		/// <returns>
		/// A <see cref="string" /> that contains the results of decoding this sequence of bytes.
		/// </returns>
		public static string ToUnicodeString(this byte[] array)
		{
			Check.ArgumentNull(array, nameof(array));

			return Encoding.Unicode.GetString(array);
		}
		/// <summary>
		/// Converts this <see cref="byte" />[] into into its hexadecimal <see cref="string" /> representation.
		/// </summary>
		/// <param name="array">The <see cref="byte" />[] containing the sequence of bytes to convert.</param>
		/// <returns>
		/// An equivalent hexadecimal <see cref="string" /> representation of this <see cref="byte" />[].
		/// </returns>
		public static string ToHexadecimalString(this byte[] array)
		{
			return array.ToHexadecimalString(false);
		}
		/// <summary>
		/// Converts this <see cref="byte" />[] into its hexadecimal <see cref="string" /> representation.
		/// </summary>
		/// <param name="array">The <see cref="byte" />[] containing the sequence of bytes to convert.</param>
		/// <param name="upperCase"><see langword="true" /> to use uppercase letters (a-f); <see langword="false" /> to use lowercase letters (A-F).</param>
		/// <returns>
		/// An equivalent hexadecimal <see cref="string" /> representation of this <see cref="byte" />[].
		/// </returns>
		public static string ToHexadecimalString(this byte[] array, bool upperCase)
		{
			Check.ArgumentNull(array, nameof(array));

			return ConvertEx.ToHexadecimalString(array, upperCase);
		}
		/// <summary>
		/// Merges all <see cref="byte" />[] objects and returns a new <see cref="byte" />[], where <paramref name="otherArrays" /> are concatenated after this array.
		/// </summary>
		/// <param name="array">The first <see cref="byte" />[] object.</param>
		/// <param name="otherArrays">An array of <see cref="byte" />[] objects to append.</param>
		/// <returns>
		/// A new <see cref="byte" />[] starting with <paramref name="array" />, followed by all elements from <paramref name="otherArrays" />.
		/// </returns>
		public static byte[] Concat(this byte[] array, params byte[][] otherArrays)
		{
			Check.ArgumentNull(array, nameof(array));
			Check.ArgumentNull(otherArrays, nameof(otherArrays));
			Check.ArgumentEx.ArrayValuesNotNull(otherArrays, nameof(otherArrays));

			byte[] result = new byte[array.Length + otherArrays.Sum(other => other.Length)];
			Buffer.BlockCopy(array, 0, result, 0, array.Length);

			int offset = array.Length;
			foreach (byte[] other in otherArrays)
			{
				Buffer.BlockCopy(other, 0, result, offset, other.Length);
				offset += other.Length;
			}

			return result;
		}
	}
}