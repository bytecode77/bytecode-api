using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using BytecodeApi.Text;

namespace BytecodeApi
{
	/// <summary>
	/// Provides support for creation and generation of generic objects.
	/// </summary>
	public static class Create
	{
		/// <summary>
		/// Generates a new <see cref="System.Guid" /> using the <see cref="GuidFormat.Default" /> format.
		/// </summary>
		/// <returns>
		/// A new <see cref="System.Guid" />.
		/// </returns>
		public static string Guid()
		{
			return Guid(GuidFormat.Default);
		}
		/// <summary>
		/// Generates a new <see cref="System.Guid" /> using the specified format.
		/// </summary>
		/// <param name="format">The <see cref="GuidFormat" /> to be used for <see cref="string" /> formatting.</param>
		/// <returns>
		/// A new <see cref="System.Guid" />.
		/// </returns>
		public static string Guid(GuidFormat format)
		{
			return System.Guid.NewGuid().ToString(format);
		}
		/// <summary>
		/// Generates a new <see cref="string" /> with random hexadecimal charaters. The value of <paramref name="size" /> specifies the amount of bytes to be generated. A <see cref="string" /> with a length of <paramref name="size" /> * 2 is returned.
		/// </summary>
		/// <param name="size">A <see cref="int" /> value specifying the amount of bytes to be generated.</param>
		/// <returns>
		/// A new <see cref="string" /> with random hexadecimal charaters, where the value of <paramref name="size" /> specifies the amount of bytes to be generated. The returned <see cref="string" /> has a length of <paramref name="size" /> * 2.
		/// </returns>
		public static string HexadecimalString(int size)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(size, nameof(size));

			lock (MathEx._Random)
			{
				return MathEx._Random.NextBytes(size).ToHexadecimalString();
			}
		}
		/// <summary>
		/// Generates a new <see cref="string" /> with random characters, where each character is either a digit, a lowercase letter or an uppercase letter.
		/// </summary>
		/// <param name="length">The length of the generated <see cref="string" />.</param>
		/// <returns>
		/// A new <see cref="string" /> with random characters.
		/// </returns>
		public static string AlphaNumericString(int length)
		{
			return AlphaNumericString(length, true, true, true);
		}
		/// <summary>
		/// Generates a new <see cref="string" /> with random characters, where each character can be either a digit, a lowercase letter, an uppercase letter, or a combination of those. At least one of these parameters needs to be <see langword="true" />.
		/// </summary>
		/// <param name="length">The length of the generated <see cref="string" />.</param>
		/// <param name="digits"><see langword="true" /> to include digits.</param>
		/// <param name="lowerCase"><see langword="true" /> to include lowercase letters.</param>
		/// <param name="upperCase"><see langword="true" /> to include uppercase letters.</param>
		/// <returns>
		/// A new <see cref="string" /> with random characters.
		/// </returns>
		public static string AlphaNumericString(int length, bool digits, bool lowerCase, bool upperCase)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(length, nameof(length));
			Check.Argument(digits || lowerCase || upperCase, null, "Either of '" + nameof(digits) + "', '" + nameof(lowerCase) + "', or '" + nameof(upperCase) + "' must be true.");

			string chars = null;
			if (digits) chars += TextResources.Digits;
			if (lowerCase) chars += TextResources.Alphabet;
			if (upperCase) chars += TextResources.Alphabet.ToUpper();

			char[] charset = chars.ToCharArray();
			char[] newString = new char[length];

			lock (MathEx._Random)
			{
				for (int i = 0; i < length; i++)
				{
					newString[i] = MathEx._Random.NextObject(charset);
				}
			}

			return newString.AsString();
		}
	}
}