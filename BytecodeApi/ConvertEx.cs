using BytecodeApi.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace BytecodeApi
{
	/// <summary>
	/// Provides <see langword="static" /> methods that extend the <see cref="Convert" /> class.
	/// </summary>
	public static class ConvertEx
	{
		private const string Base32RFC4648 = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";
		private const string Base32Crockford = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
		private static readonly Regex RomanNumberRegex = new Regex("^M{0,3}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})$", RegexOptions.IgnoreCase);

		/// <summary>
		/// Converts the specified <see cref="byte" />[] into into its hexadecimal <see cref="string" /> representation.
		/// </summary>
		/// <param name="array">The <see cref="byte" />[] containing the sequence of bytes to convert.</param>
		/// <returns>
		/// An equivalent hexadecimal <see cref="string" /> representation of <paramref name="array" />.
		/// </returns>
		public static string ToHexadecimalString(byte[] array)
		{
			return array.ToHexadecimalString(false);
		}
		/// <summary>
		/// Converts the specified <see cref="byte" />[] into its hexadecimal <see cref="string" /> representation.
		/// </summary>
		/// <param name="array">The <see cref="byte" />[] containing the sequence of bytes to convert.</param>
		/// <param name="upperCase"><see langword="true" /> to use uppercase letters (a-f); <see langword="false" /> to use lowercase letters (A-F).</param>
		/// <returns>
		/// An equivalent hexadecimal <see cref="string" /> representation of <paramref name="array" />.
		/// </returns>
		public static string ToHexadecimalString(byte[] array, bool upperCase)
		{
			Check.ArgumentNull(array, nameof(array));

			char firstLetter = upperCase ? 'A' : 'a';
			char[] str = new char[array.Length * 2];

			for (int i = 0; i < array.Length; i++)
			{
				int digit1 = array[i] >> 4;
				int digit2 = array[i] & 15;

				str[i * 2] = digit1 < 10 ? (char)('0' + digit1) : (char)(firstLetter + digit1 - 10);
				str[i * 2 + 1] = digit2 < 10 ? (char)('0' + digit2) : (char)(firstLetter + digit2 - 10);
			}

			return str.AsString();
		}
		/// <summary>
		/// Converts a hexadecimal <see cref="string" /> to its equivalent <see cref="byte" />[] representation. Only hexadecimal digits (upper and lowercase) are allowed. <paramref name="str" /> must have an even number of digits.
		/// </summary>
		/// <param name="str">A hexadecimal <see cref="string" /> to convert to a <see cref="byte" />[].</param>
		/// <returns>
		/// The equivalent <see cref="byte" />[] representation of <paramref name="str" />.
		/// </returns>
		public static byte[] FromHexadecimalString(string str)
		{
			Check.ArgumentNull(str, nameof(str));
			Check.Argument((str.Length & 1) == 0, nameof(str), "The hexadecimal string must have an even number of digits.");

			byte[] result = new byte[str.Length / 2];

			for (int i = 0, index = 0; i < str.Length / 2; i++, index += 2)
			{
				if (!str[index].IsHexadecimal() || !str[index + 1].IsHexadecimal()) throw Throw.Argument(nameof(str), "String is not a valid hexadecimal string.");
				result[i] = Convert.ToByte(str.Substring(index, 2), 16);
			}

			return result;
		}
		/// <summary>
		/// Converts a <see cref="byte" />[] to its equivalent Base32 representation using the RFC4648 charset (ABCDEFGHIJKLMNOPQRSTUVWXYZ234567).
		/// </summary>
		/// <param name="data">The <see cref="byte" />[] to convert.</param>
		/// <returns>
		/// An equivalent <see cref="string" /> representation, in Base32, of <paramref name="data" />.
		/// </returns>
		public static string ToBase32String(byte[] data)
		{
			return ToBase32String(data, false);
		}
		/// <summary>
		/// Converts a <see cref="byte" />[] to its equivalent Base32 representation.
		/// </summary>
		/// <param name="data">The <see cref="byte" />[] to convert.</param>
		/// <param name="crockford"><see langword="true" /> to use the crockford charset (0123456789ABCDEFGHJKMNPQRSTVWXYZ); <see langword="false" /> to use the RFC4648 charset (ABCDEFGHIJKLMNOPQRSTUVWXYZ234567).</param>
		/// <returns>
		/// An equivalent <see cref="string" /> representation, in Base32, of <paramref name="data" />.
		/// </returns>
		public static string ToBase32String(byte[] data, bool crockford)
		{
			Check.ArgumentNull(data, nameof(data));

			if (data.Length == 0)
			{
				return "";
			}
			else
			{
				string charset = crockford ? Base32Crockford : Base32RFC4648;
				StringBuilder result = new StringBuilder((data.Length * 8 + 5 - 1) / 5);

				int offset = 0;
				int last = offset + data.Length;
				int buffer = data[offset++];
				int bitsLeft = 8;

				while (bitsLeft > 0 || offset < last)
				{
					if (bitsLeft < 5)
					{
						if (offset < last)
						{
							buffer <<= 8;
							buffer |= data[offset++];
							bitsLeft += 8;
						}
						else
						{
							int pading = 5 - bitsLeft;
							buffer <<= pading;
							bitsLeft += pading;
						}
					}

					int index = 31 & (buffer >> bitsLeft - 5);
					bitsLeft -= 5;
					result.Append(charset[index]);
				}

				int padding = 8 - result.Length % 8;
				if (padding > 0 && padding < 8) result.Append('=', padding);

				return result.ToString();
			}
		}
		/// <summary>
		/// Converts an encoded Base32 <see cref="string" /> back to its original <see cref="byte" />[] using the RFC4648 charset (ABCDEFGHIJKLMNOPQRSTUVWXYZ234567).
		/// </summary>
		/// <param name="str">A <see cref="string" /> representing the Base32 encoded data.</param>
		/// <returns>
		/// The original <see cref="byte" />[], representing the Base32 <see cref="string" /> in <paramref name="str" />.
		/// </returns>
		public static byte[] FromBase32String(string str)
		{
			return FromBase32String(str, false);
		}
		/// <summary>
		/// Converts an encoded Base32 <see cref="string" /> back to its original <see cref="byte" />[].
		/// </summary>
		/// <param name="str">A <see cref="string" /> representing the Base32 encoded data.</param>
		/// <param name="crockford"><see langword="true" /> to use the crockford charset (0123456789ABCDEFGHJKMNPQRSTVWXYZ); <see langword="false" /> to use the RFC4648 charset (ABCDEFGHIJKLMNOPQRSTUVWXYZ234567).</param>
		/// <returns>
		/// The original <see cref="byte" />[], representing the Base32 <see cref="string" /> in <paramref name="str" />.
		/// </returns>
		public static byte[] FromBase32String(string str, bool crockford)
		{
			Check.ArgumentNull(str, nameof(str));

			str = str.Trim().TrimEnd('=').ToUpper();

			if (str.Length == 0)
			{
				return new byte[0];
			}
			else
			{
				string charset = crockford ? Base32Crockford : Base32RFC4648;
				byte[] result = new byte[str.Length * 5 / 8];

				int buffer = 0;
				int index = 0;
				int bitsLeft = 0;

				foreach (char c in str)
				{
					int charValue = -1;

					for (int i = 0; i < 32; i++)
					{
						if (charset[i] == c)
						{
							charValue = i;
							break;
						}
					}

					if (charValue == -1) Throw.Argument(nameof(str), "Illegal character in Base32 string.");

					buffer <<= 5;
					buffer |= charValue & 31;
					bitsLeft += 5;

					if (bitsLeft >= 8)
					{
						result[index++] = (byte)(buffer >> (bitsLeft - 8));
						bitsLeft -= 8;
					}
				}

				return result;
			}
		}
		/// <summary>
		/// Converts a zero-based Excel column index to a string (like A, B, ... AA, AB, ...).
		/// </summary>
		/// <param name="index">A <see cref="int" /> representing an Excel column index.</param>
		/// <returns>
		/// A <see cref="string" /> value with the Excel column string, converted from the zero-based column index.
		/// </returns>
		public static string ToExcelColumnString(int index)
		{
			return ToExcelColumnString(index, false);
		}
		/// <summary>
		/// Converts a zero-based or one-based Excel column index to a string (like A, B, ... AA, AB, ...).
		/// </summary>
		/// <param name="index">A <see cref="int" /> representing an Excel column index.</param>
		/// <param name="oneBased"><see langword="true" /> to treat <paramref name="index" /> as a one-based index; otherwise, <see langword="false" />.</param>
		/// <returns>
		/// A <see cref="string" /> value with the Excel column string, converted from the zero-based or one-based column index.
		/// </returns>
		public static string ToExcelColumnString(int index, bool oneBased)
		{
			if (oneBased) Check.ArgumentOutOfRangeEx.Greater0(index, nameof(index));
			else Check.ArgumentOutOfRangeEx.GreaterEqual0(index, nameof(index));

			if (oneBased) index--;
			string str = "";

			while (index >= 0)
			{
				str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[index % 26] + str;
				index = index / 26 - 1;
			}

			return str;
		}
		/// <summary>
		/// Converts an Excel column name (like A, B, ... AA, AB, ...) to a zero-based index.
		/// </summary>
		/// <param name="str">A <see cref="string" /> representing an Excel column name.</param>
		/// <returns>
		/// A <see cref="int" /> value with the zero-based index, converted from the Excel column name.
		/// </returns>
		public static int FromExcelColumnString(string str)
		{
			return FromExcelColumnString(str, false);
		}
		/// <summary>
		/// Converts an Excel column name (like A, B, ... AA, AB, ...) to a zero-based or one-based index.
		/// </summary>
		/// <param name="str">A <see cref="string" /> representing an Excel column name.</param>
		/// <param name="oneBased"><see langword="true" /> to return a one-based index; otherwise, <see langword="false" />.</param>
		/// <returns>
		/// A <see cref="int" /> value with the zero-based or one-based index, converted from the Excel column name.
		/// </returns>
		public static int FromExcelColumnString(string str, bool oneBased)
		{
			Check.ArgumentNull(str, nameof(str));

			int sum = 0;
			foreach (char columnChar in str.ToUpper())
			{
				if (columnChar < 'A' || columnChar > 'Z') throw Throw.Argument(nameof(str), "String must contain only letters from A-Z.");

				sum *= 26;
				sum += columnChar - 'A' + 1;
			}

			return sum - (oneBased ? 0 : 1);
		}
		/// <summary>
		/// Converts a 32-bit integer into a compressed format.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value to convert.</param>
		/// <returns>
		/// A new <see cref="byte" />[] that represents the specified 32-bit integer in compressed format.
		/// </returns>
		public static byte[] To7BitEncodedInt(int value)
		{
			List<byte> bytes = new List<byte>();
			uint remaining = (uint)value;

			while (remaining > 127)
			{
				bytes.Add((byte)(remaining | 128));
				remaining >>= 7;
			}

			bytes.Add((byte)remaining);
			return bytes.ToArray();
		}
		/// <summary>
		/// Converts a compressed 32-bit integer into a <see cref="int" /> value.
		/// </summary>
		/// <param name="value">The <see cref="byte" />[] value to convert with up to 4 bytes capacity.</param>
		/// <returns>
		/// A <see cref="int" /> value that was converted from the specified 32-bit integer in binary format.
		/// </returns>
		public static int From7BitEncodedInt(byte[] value)
		{
			Check.ArgumentNull(value, nameof(value));

			int returnValue = 0;
			int bitIndex = 0;

			foreach (byte by in value)
			{
				returnValue |= (by & 127) << bitIndex;
				bitIndex += 7;

				if ((by & 128) == 0) return returnValue;
			}

			throw Throw.Format("The value format is invalid.");
		}
		/// <summary>
		/// Converts an integer to a roman numeral. If <paramref name="value" /> is not between 1 and 3999, an exception is thrown.
		/// <para>Examples: I, II, III, IV, V, VI, VII, VIII, IX, X</para>
		/// </summary>
		/// <param name="value">A <see cref="int" /> value to convert to a roman numeral.</param>
		/// <returns>
		/// A roman number <see cref="string" /> that was converted from the specified integer.
		/// </returns>
		public static string ToRomanNumber(int value)
		{
			Check.ArgumentOutOfRange(value >= 1 && value <= 3999, nameof(value), "Number must be in range of 1...3999.");

			// 1	I
			// 5	V
			// 10	X
			// 50	L
			// 100	C
			// 500	D
			// 1000	M

			string result = null;

			while (value > 0)
			{
				if (value >= 1000)
				{
					result += "M";
					value -= 1000;
				}
				else if (value >= 900)
				{
					result += "CM";
					value -= 900;
				}
				else if (value >= 500)
				{
					result += "D";
					value -= 500;
				}
				else if (value >= 400)
				{
					result += "CD";
					value -= 400;
				}
				else if (value >= 100)
				{
					result += "C";
					value -= 100;
				}
				else if (value >= 90)
				{
					result += "XC";
					value -= 90;
				}
				else if (value >= 50)
				{
					result += "L";
					value -= 50;
				}
				else if (value >= 40)
				{
					result += "XL";
					value -= 40;
				}
				else if (value >= 10)
				{
					result += "X";
					value -= 10;
				}
				else if (value >= 9)
				{
					result += "IX";
					value -= 9;
				}
				else if (value >= 5)
				{
					result += "V";
					value -= 5;
				}
				else if (value >= 4)
				{
					result += "IV";
					value -= 4;
				}
				else if (value >= 1)
				{
					result += "I";
					value--;
				}
			}

			return result;
		}
		/// <summary>
		/// Converts a roman number <see cref="string" /> to an integer.
		/// <para>Examples: I, II, III, IV, V, VI, VII, VIII, IX, X</para>
		/// </summary>
		/// <param name="value">A roman number <see cref="string" />.</param>
		/// <returns>
		/// A <see cref="int" /> value that was converted from the roman number <see cref="string" />.
		/// </returns>
		public static int FromRomanNumber(string value)
		{
			Check.ArgumentNull(value, nameof(value));
			Check.Format(RomanNumberRegex.IsMatch(value), "String is not a valid roman number.");

			// 1	I
			// 5	V
			// 10	X
			// 50	L
			// 100	C
			// 500	D
			// 1000	M

			int result = 0;
			value = value.ToUpper();

			while (value.Length > 0)
			{
				if (value.StartsWith("M"))
				{
					result += 1000;
					value = value.Substring(1);
				}
				else if (value.StartsWith("CM"))
				{
					result += 900;
					value = value.Substring(2);
				}
				else if (value.StartsWith("D"))
				{
					result += 500;
					value = value.Substring(1);
				}
				else if (value.StartsWith("CD"))
				{
					result += 400;
					value = value.Substring(2);
				}
				else if (value.StartsWith("C"))
				{
					result += 100;
					value = value.Substring(1);
				}
				else if (value.StartsWith("XC"))
				{
					result += 90;
					value = value.Substring(2);
				}
				else if (value.StartsWith("L"))
				{
					result += 50;
					value = value.Substring(1);
				}
				else if (value.StartsWith("XL"))
				{
					result += 40;
					value = value.Substring(2);
				}
				else if (value.StartsWith("X"))
				{
					result += 10;
					value = value.Substring(1);
				}
				else if (value.StartsWith("IX"))
				{
					result += 9;
					value = value.Substring(2);
				}
				else if (value.StartsWith("V"))
				{
					result += 5;
					value = value.Substring(1);
				}
				else if (value.StartsWith("IV"))
				{
					result += 4;
					value = value.Substring(2);
				}
				else if (value.StartsWith("I"))
				{
					result++;
					value = value.Substring(1);
				}
				else
				{
					throw Throw.Argument(nameof(value), "String is not a valid roman number.");
				}
			}

			return result;
		}

		/// <summary>
		/// Converts the specified <see cref="byte" />[] to a structure of type <typeparamref name="T" />.
		/// </summary>
		/// <typeparam name="T">The type of the structure to return.</typeparam>
		/// <param name="array">A <see cref="byte" />[] with the binary representation of the structure. This array must have an equivalent size of <see cref="Marshal" />.SizeOf(<see langword="typeof" />(<typeparamref name="T" />)).</param>
		/// <returns>
		/// The structure of type <typeparamref name="T" /> this method creates.
		/// </returns>
		public static T ToStructure<T>(byte[] array) where T : struct
		{
			Check.ArgumentNull(array, nameof(array));
			Check.Argument(array.Length == Marshal.SizeOf<T>(), nameof(array), "Array size must be equivalent to the size of the structure type.");

			IntPtr ptr = IntPtr.Zero;

			try
			{
				ptr = Marshal.AllocHGlobal(array.Length);
				Marshal.Copy(array, 0, ptr, array.Length);
				return Marshal.PtrToStructure<T>(ptr);
			}
			finally
			{
				if (ptr != IntPtr.Zero) Marshal.FreeHGlobal(ptr);
			}
		}
		/// <summary>
		/// Converts the specified structure of type <typeparamref name="T" /> to a <see cref="byte" />[] with the size of <see cref="Marshal" />.SizeOf(<see langword="typeof" />(<typeparamref name="T" />)).
		/// </summary>
		/// <typeparam name="T">The type of the structure to convert.</typeparam>
		/// <param name="structure">The structure to convert to a <see cref="byte" />[].</param>
		/// <returns>
		/// An equivalent <see cref="byte" />[] representation of <paramref name="structure" /> with the size of <see cref="Marshal" />.SizeOf(<see langword="typeof" />(<typeparamref name="T" />)).
		/// </returns>
		public static byte[] FromStructure<T>(T structure) where T : struct
		{
			IntPtr ptr = IntPtr.Zero;

			try
			{
				byte[] buffer = new byte[Marshal.SizeOf<T>()];
				ptr = Marshal.AllocHGlobal(buffer.Length);
				Marshal.StructureToPtr(structure, ptr, true);
				Marshal.Copy(ptr, buffer, 0, buffer.Length);
				return buffer;
			}
			finally
			{
				if (ptr != IntPtr.Zero) Marshal.FreeHGlobal(ptr);
			}
		}
		/// <summary>
		/// Converts a <see cref="byte" />[] to an <see cref="Icon" />. The <paramref name="array" /> object should represent the binary of a valid icon file.
		/// </summary>
		/// <param name="array">A <see cref="byte" />[] that represents a valid icon file.</param>
		/// <returns>
		/// A new <see cref="Icon" /> object, created from the contents of <paramref name="array" />.
		/// </returns>
		public static Icon ToIcon(byte[] array)
		{
			Check.ArgumentNull(array, nameof(array));

			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				return new Icon(memoryStream);
			}
		}
		/// <summary>
		/// Converts a <see cref="byte" />[] to an <see cref="Bitmap" />. The <paramref name="array" /> object should represent the binary of a valid image file.
		/// </summary>
		/// <param name="array">A <see cref="byte" />[] that represents a valid image file.</param>
		/// <returns>
		/// A new <see cref="Bitmap" /> object, created from the contents of <paramref name="array" />.
		/// </returns>
		public static Bitmap ToBitmap(byte[] array)
		{
			Check.ArgumentNull(array, nameof(array));

			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				return new Bitmap(memoryStream);
			}
		}
	}
}