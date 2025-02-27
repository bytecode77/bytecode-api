using BytecodeApi.Extensions;
using System.Text;

namespace BytecodeApi.Text;

/// <summary>
/// Class that performs linguistic <see cref="string" /> manipulation on objects.
/// </summary>
public static class Wording
{
	internal const int DefaultFormatTimeSpanMaxElements = 2;
	internal const string DefaultFormatTimeSpanSeparator = ", ";

	/// <summary>
	/// Trims the specified <see cref="string" /> by the specified length. If the <see cref="string" /> is longer than the value of <paramref name="length" />, it will be truncated by a leading "..." to match the <paramref name="length" /> parameter, including the length of the "..." appendix (3 characters).
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be trimmed.</param>
	/// <param name="length">A <see cref="int" /> value specifying the maximum length of the returned <see cref="string" />.</param>
	/// <returns>
	/// The original value of <paramref name="str" />, if the length of the <see cref="string" /> is less than or equal to the <paramref name="length" /> parameter;
	/// otherwise, the truncated <see cref="string" /> and a leading "..." that matches the <paramref name="length" /> parameter, including the length of the "..." appendix (3 characters).
	/// </returns>
	public static string TrimText(string str, int length)
	{
		return TrimText(str, length, "...");
	}
	/// <summary>
	/// Trims the specified <see cref="string" /> by the specified length. If the <see cref="string" /> is longer than the value of <paramref name="length" />, it will be truncated and the value of <paramref name="trailingText" /> will be appended to match the <paramref name="length" /> parameter, including the length of the <paramref name="trailingText" /> paramter.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be trimmed.</param>
	/// <param name="length">A <see cref="int" /> value specifying the maximum length of the returned <see cref="string" />.</param>
	/// <param name="trailingText">The trailing text.</param>
	/// <returns>
	/// The original value of <paramref name="str" />, if the length of the <see cref="string" /> is less than or equal to the <paramref name="length" /> parameter and the length of the <paramref name="trailingText" /> parameter;
	/// otherwise, the truncated <see cref="string" /> and the value of <paramref name="trailingText" /> that matches the <paramref name="length" /> parameter, including the length of the <paramref name="trailingText" /> paramter.
	/// </returns>
	public static string TrimText(string str, int length, string trailingText)
	{
		Check.ArgumentNull(str);
		Check.Argument(length >= trailingText.Length, nameof(length), "Length must be at least the length of the trailing text string.");
		Check.ArgumentNull(trailingText);

		return str.Length > length ? str[..(length - trailingText.Length)] + trailingText : str;
	}
	/// <summary>
	/// Concatenates all values in the specified <see cref="string" /> collection, where <paramref name="lastSeparator" /> is used for the last separator.
	/// <para>Example with 1 value: A</para>
	/// <para>Example with 2 values: A and B</para>
	/// <para>Example with 3 values: A, B and C</para>
	/// <para>Example with 4 values: A, B, C and D</para>
	/// </summary>
	/// <param name="separator">A <see cref="string" /> value specifying the separator between each <see cref="string" /> value.</param>
	/// <param name="lastSeparator">A <see cref="string" /> value specifying the separator between the last two <see cref="string" /> values.</param>
	/// <param name="strings">The <see cref="string" />[] that is concatenated.</param>
	/// <returns>
	/// A <see cref="string" />, where the values of <paramref name="strings" /> is concatenated by the specified separators, or <see cref="string.Empty" />, if the collection is empty.
	/// </returns>
	public static string JoinStrings(string separator, string lastSeparator, params string[] strings)
	{
		Check.ArgumentNull(separator);
		Check.ArgumentNull(lastSeparator);

		return strings.IsNullOrEmpty() ? "" : strings.Take(strings.Length - 1).AsString(separator) + (strings.Length > 1 ? lastSeparator : "") + strings[^1];
	}
	/// <summary>
	/// Concatenates all values in the specified <see cref="string" /> collection, where <paramref name="lastSeparator" /> is used for the last separator.
	/// <para>Example with 1 value: A</para>
	/// <para>Example with 2 values: A and B</para>
	/// <para>Example with 3 values: A, B and C</para>
	/// <para>Example with 4 values: A, B, C and D</para>
	/// </summary>
	/// <param name="separator">A <see cref="string" /> value specifying the separator between each <see cref="string" /> value.</param>
	/// <param name="lastSeparator">A <see cref="string" /> value specifying the separator between the last two <see cref="string" /> values.</param>
	/// <param name="strings">The collection of <see cref="string" /> objects that is concatenated.</param>
	/// <returns>
	/// A <see cref="string" />, where the values of <paramref name="strings" /> is concatenated by the specified separators, or <see cref="string.Empty" />, if the collection is empty.
	/// </returns>
	public static string JoinStrings(string separator, string lastSeparator, IEnumerable<string> strings)
	{
		return JoinStrings(separator, lastSeparator, strings.ToArray());
	}
	/// <summary>
	/// Converts the value of the specified <see cref="TimeSpan" /> to a human readable <see cref="string" /> representation by displaying the two most significant elements of either days, hours, minutes or seconds that are greater than zero, separated by a comma.
	/// <para>Example: "12:00:03" is converted to "12h, 3s"</para>
	/// </summary>
	/// <param name="timeSpan">The <see cref="TimeSpan" /> value to convert.</param>
	/// <returns>
	/// The value of the specified <see cref="TimeSpan" /> as a human readable <see cref="string" /> representation.
	/// </returns>
	public static string FormatTimeSpan(TimeSpan timeSpan)
	{
		return FormatTimeSpan(timeSpan, DefaultFormatTimeSpanMaxElements);
	}
	/// <summary>
	/// Converts the value of the specified <see cref="TimeSpan" /> to a human readable <see cref="string" /> representation by displaying a specified number of most significant elements of either days, hours, minutes or seconds that are greater than zero, separated by a comma.
	/// <para>Example: "5.03:30:15" is converted to "5d, 3h", if <paramref name="maxElements" /> is 2 and "5d, 3h, 30m", if <paramref name="maxElements" /> is 3</para>
	/// </summary>
	/// <param name="timeSpan">The <see cref="TimeSpan" /> value to convert.</param>
	/// <param name="maxElements">A <see cref="int" /> value specifying the number of elements of either days, hours, minutes or seconds to display.</param>
	/// <returns>
	/// The value of the specified <see cref="TimeSpan" /> as a human readable <see cref="string" /> representation.
	/// </returns>
	public static string FormatTimeSpan(TimeSpan timeSpan, int maxElements)
	{
		return FormatTimeSpan(timeSpan, maxElements, DefaultFormatTimeSpanSeparator);
	}
	/// <summary>
	/// Converts the value of the specified <see cref="TimeSpan" /> to a human readable <see cref="string" /> representation by displaying a specified number of most significant elements of either days, hours, minutes or seconds that are greater than zero, separated by the specified separator.
	/// <para>Example: "5.03:30:15" is converted to "5d, 3h", if <paramref name="maxElements" /> is 2 and "5d, 3h, 30m", if <paramref name="maxElements" /> is 3</para>
	/// </summary>
	/// <param name="timeSpan">The <see cref="TimeSpan" /> value to convert.</param>
	/// <param name="maxElements">A <see cref="int" /> value specifying the number of elements of either days, hours, minutes or seconds to display.</param>
	/// <param name="separator">A <see cref="string" /> specifying the separator to use between each element, including whitespaces, if needed.</param>
	/// <returns>
	/// The value of the specified <see cref="TimeSpan" /> as a human readable <see cref="string" /> representation.
	/// </returns>
	public static string FormatTimeSpan(TimeSpan timeSpan, int maxElements, string separator)
	{
		Check.ArgumentOutOfRangeEx.Greater0(maxElements);
		Check.ArgumentNull(separator);

		if (timeSpan == TimeSpan.Zero)
		{
			return "";
		}
		else
		{
			bool sign = timeSpan < TimeSpan.Zero;
			if (sign) timeSpan = -timeSpan;

			List<string> elements = new();
			if (timeSpan.Days > 0) elements.Add(timeSpan.Days + "d");
			if (timeSpan.Hours > 0) elements.Add(timeSpan.Hours + "h");
			if (timeSpan.Minutes > 0) elements.Add(timeSpan.Minutes + "m");
			if (timeSpan.Seconds > 0) elements.Add(timeSpan.Seconds + "s");

			return (sign ? "-" : null) + elements.Take(maxElements).AsString(separator);
		}
	}
	/// <summary>
	/// Wraps the specified text by splitting it up by whitespace characters and wrapping it by a specified maximum column width. This is typically used for console windows or in conjunction with monospace fonts.
	/// </summary>
	/// <param name="text">A <see cref="string" /> specifying the text to wrap.</param>
	/// <param name="width">A <see cref="int" /> value specifying the maximum number of characters per line.</param>
	/// <param name="overflow"><see langword="true" /> to allow words that are longer than <paramref name="width" /> to overflow; <see langword="false" /> to split up long words.</param>
	/// <returns>
	/// A multiline <see cref="string" /> with the wrapped text. Each line does not exceed an amount of characters equal to <paramref name="width" />, unless <paramref name="overflow" /> is set t <see langword="true" />.
	/// </returns>
	public static string WrapText(string text, int width, bool overflow)
	{
		Check.ArgumentNull(text);
		Check.ArgumentOutOfRangeEx.Greater0(width);

		char[] separators = new[] { ' ', '\t', '\r', '\n' };
		StringBuilder stringBuilder = new();

		int index = 0;
		int column = 0;

		while (index < text.Length)
		{
			int spaceIndex = text.IndexOfAny(separators, index);

			if (spaceIndex == -1)
			{
				break;
			}
			else if (spaceIndex == index)
			{
				index++;
			}
			else
			{
				AddWord(text[index..spaceIndex]);
				index = spaceIndex + 1;
			}
		}

		if (index < text.Length)
		{
			AddWord(text[index..]);
		}

		void AddWord(string word)
		{
			if (!overflow && word.Length > width)
			{
				for (int wordIndex = 0; wordIndex < word.Length;)
				{
					string subWord = word.Substring(wordIndex, Math.Min(width, word.Length - wordIndex));
					AddWord(subWord);

					wordIndex += subWord.Length;
				}
			}
			else
			{
				if (column + word.Length >= width)
				{
					if (column > 0)
					{
						stringBuilder.AppendLine();
						column = 0;
					}
				}
				else if (column > 0)
				{
					stringBuilder.Append(' ');
					column++;
				}

				stringBuilder.Append(word);
				column += word.Length;
			}
		}

		return stringBuilder.ToString();
	}
	/// <summary>
	/// Creates a binary view for the specified <see cref="byte" />[].
	/// <para>Example: 00000000h: 41 42 43 00 00 00 00 00 00 00 00 00 00 00 00 00 ; ABC.............</para>
	/// </summary>
	/// <param name="bytes">The <see cref="byte" />[] to create the binary view from.</param>
	/// <returns>
	/// A new <see cref="string" /> representing the specified <see cref="byte" />[] as a binary view.
	/// </returns>
	public static string FormatBinary(byte[] bytes)
	{
		return FormatBinary(bytes, 0, bytes.Length);
	}
	/// <summary>
	/// Creates a binary view for the specified <see cref="byte" />[], starting from the specified offset, including the specified number of bytes.
	/// <para>Example: 00000000h: 41 42 43 00 00 00 00 00 00 00 00 00 00 00 00 00 ; ABC.............</para>
	/// </summary>
	/// <param name="bytes">The <see cref="byte" />[] to create the binary view from.</param>
	/// <param name="offset">The starting point in the buffer at which to begin.</param>
	/// <param name="count">The number of bytes to take.</param>
	/// <returns>
	/// A new <see cref="string" /> representing the specified <see cref="byte" />[] as a binary view.
	/// </returns>
	public static string FormatBinary(byte[] bytes, int offset, int count)
	{
		return FormatBinary(bytes, offset, count, 0);
	}
	/// <summary>
	/// Creates a binary view for the specified <see cref="byte" />[], starting from the specified offset, including the specified number of bytes.
	/// <para>Example: 00000000h: 41 42 43 00 00 00 00 00 00 00 00 00 00 00 00 00 ; ABC.............</para>
	/// </summary>
	/// <param name="bytes">The <see cref="byte" />[] to create the binary view from.</param>
	/// <param name="offset">The starting point in the buffer at which to begin.</param>
	/// <param name="count">The number of bytes to take.</param>
	/// <param name="startPosition">Indicates the starting position that is displayed in the first column of the result <see cref="string" />. This can be any number. The default value is 0.</param>
	/// <returns>
	/// A new <see cref="string" /> representing the specified <see cref="byte" />[] as a binary view.
	/// </returns>
	public static string FormatBinary(byte[] bytes, int offset, int count, int startPosition)
	{
		Check.ArgumentNull(bytes);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(offset);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);
		Check.ArgumentEx.OffsetAndLengthOutOfBounds(offset, count, bytes.Length);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(startPosition);

		StringBuilder stringBuilder = new();

		for (int i = offset; i < offset + count; i += 16, startPosition += 16)
		{
			int length = Math.Min(bytes.Length - i, 16);
			StringBuilder line1 = new(startPosition.ToStringInvariant("x8") + "h: ");
			StringBuilder line2 = new();

			for (int j = 0; j < length; j++)
			{
				line1.Append(bytes[i + j].ToStringInvariant("x2") + " ");
				line2.Append(bytes[i + j] >= 32 && bytes[i + j] <= 126 ? (char)bytes[i + j] : '.');
			}

			if (length < 16)
			{
				line1.Append(' '.Repeat((16 - length) * 3));
				line2.Append(' '.Repeat(16 - length));
			}

			stringBuilder.AppendLine($"{line1}; {line2}");
		}

		return stringBuilder.ToString();
	}
	/// <summary>
	/// Encodes a <see cref="string" /> to its equivalent ROT13 representation. This function either encodes a <see cref="string" /> or decodes an already encoded <see cref="string" />.
	/// <para>Example:"http://example.com/" is encoded to "uggc://rknzcyr.pbz/"</para>
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be encoded or decoded.</param>
	/// <returns>
	/// The encoded or decoded representation of <paramref name="str" />.
	/// </returns>
	public static string Rot13(string str)
	{
		Check.ArgumentNull(str);

		char[] newString = str.ToCharArray();
		for (int i = 0; i < str.Length; i++)
		{
			char c = newString[i];
			if (c is >= 'a' and <= 'z')
			{
				newString[i] = (char)(c + 13 > 'z' ? c - 13 : c + 13);
			}
			else if (c is >= 'A' and <= 'Z')
			{
				newString[i] = (char)(c + 13 > 'Z' ? c - 13 : c + 13);
			}
		}

		return newString.AsString();
	}
	/// <summary>
	/// Escapes a SendKeys sequence. The characters +^%~(){}[] are encapsulated by braces. The returned <see cref="string" /> can be then passed to SendKeys.
	/// </summary>
	/// <param name="str">The <see cref="string" /> value to be escaped.</param>
	/// <returns>
	/// A <see cref="string" /> with the escaped representation of <paramref name="str" />.
	/// </returns>
	public static string EscapeSendKeys(string str)
	{
		Check.ArgumentNull(str);

		return str.Select(key => key is '+' or '^' or '%' or '~' or '(' or ')' or '{' or '}' or '[' or ']' ? "{" + key + "}" : key.ToString()).AsString();
	}
}