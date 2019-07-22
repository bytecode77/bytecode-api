using System;

namespace BytecodeApi.Text
{
	/// <summary>
	/// Represents a <see cref="string" /> value that can be converted to a quoted string (a C# string literal or verbatim string literal).
	/// </summary>
	public struct QuotedString : IEquatable<QuotedString>
	{
		/// <summary>
		/// The original <see cref="string" /> value.
		/// </summary>
		public string OriginalString { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="QuotedString" /> structure with a <see cref="string" /> value.
		/// </summary>
		/// <param name="originalString">The original <see cref="string" /> value.</param>
		public QuotedString(string originalString)
		{
			OriginalString = originalString;
		}

		/// <summary>
		/// Converts the value of this <see cref="QuotedString" /> to its equivalent <see cref="string" /> literal representation, including escape characters, like \r or \n.
		/// <para>Example: "Hello" (with the quotes) is converted to "\"Hello\"" (with the quotes)</para>
		/// </summary>
		/// <returns>
		/// An equivalent <see cref="string" /> literal representation of the value of this <see cref="QuotedString" />.
		/// </returns>
		public override string ToString()
		{
			if (OriginalString == null)
			{
				return "";
			}
			else
			{
				string str = OriginalString
					.Replace("\\", "\\\\")
					.Replace("'", "\'")
					.Replace("\"", "\\\"")
					.Replace("\0", "\\0")
					.Replace("\a", "\\a")
					.Replace("\b", "\\b")
					.Replace("\f", "\\f")
					.Replace("\n", "\\n")
					.Replace("\r", "\\r")
					.Replace("\t", "\\t")
					.Replace("\v", "\\v");

				return "\"" + str + "\"";
			}
		}
		/// <summary>
		/// Converts the value of this <see cref="QuotedString" /> to its equivalent verbatim <see cref="string" /> literal representation, including escape characters, like \r or \n.
		/// <para>Example: "Hello" (with the quotes) is converted to @"""Hello""" (with the quotes and the @ character)</para>
		/// </summary>
		/// <returns>
		/// An equivalent verbatim <see cref="string" /> literal representation of the value of this <see cref="QuotedString" />.
		/// </returns>
		public string ToVerbatimString()
		{
			return OriginalString == null ? null : "@\"" + OriginalString.Replace("\"", "\"\"") + "\"";
		}
		/// <summary>
		/// Determines whether the specified <see cref="object" /> is equal to this instance.
		/// </summary>
		/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
		/// <returns>
		/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public override bool Equals(object obj)
		{
			return obj is QuotedString quotedString && Equals(quotedString);
		}
		/// <summary>
		/// Determines whether this instance is equal to another <see cref="QuotedString" />.
		/// </summary>
		/// <param name="other">The <see cref="QuotedString" /> to compare to this instance.</param>
		/// <returns>
		/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Equals(QuotedString other)
		{
			return OriginalString == other.OriginalString;
		}
		/// <summary>
		/// Returns a hash code for this <see cref="QuotedString" />.
		/// </summary>
		/// <returns>
		/// The hash code for this <see cref="QuotedString" /> instance.
		/// </returns>
		public override int GetHashCode()
		{
			return CSharp.GetHashCode(OriginalString);
		}

		/// <summary>
		/// Compares two <see cref="QuotedString" /> values for equality.
		/// </summary>
		/// <param name="a">The first <see cref="QuotedString" /> to compare.</param>
		/// <param name="b">The second <see cref="QuotedString" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="QuotedString" /> values are equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator ==(QuotedString a, QuotedString b)
		{
			return Equals(a, b);
		}
		/// <summary>
		/// Compares two <see cref="QuotedString" /> values for inequality.
		/// </summary>
		/// <param name="a">The first <see cref="QuotedString" /> to compare.</param>
		/// <param name="b">The second <see cref="QuotedString" /> to compare.</param>
		/// <returns>
		/// <see langword="true" />, if both <see cref="QuotedString" /> values are not equal;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool operator !=(QuotedString a, QuotedString b)
		{
			return !(a == b);
		}
		/// <summary>
		/// Defines an implicit conversion of a <see cref="string" /> to a <see cref="QuotedString" />.
		/// </summary>
		/// <param name="value">The <see cref="string" /> to convert.</param>
		public static implicit operator QuotedString(string value)
		{
			return new QuotedString(value);
		}
		/// <summary>
		/// Defines an implicit conversion of a <see cref="QuotedString" /> to a <see cref="string" />. Returns the equivalent <see cref="string" /> literal representation of <see cref="OriginalString" />.
		/// </summary>
		/// <param name="value">The <see cref="QuotedString" /> to convert.</param>
		public static implicit operator string(QuotedString value)
		{
			return value.ToString();
		}
	}
}