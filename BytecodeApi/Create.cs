using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using BytecodeApi.Text;
using System;

namespace BytecodeApi
{
	//FEATURE: Create.Enumerable (similar to Create.Array)
	/// <summary>
	/// Provides support for creation and generation of generic objects.
	/// </summary>
	public static class Create
	{
		/// <summary>
		/// Creates an <see cref="System.Array" /> of the specified type and initialized each element with the specified value.
		/// </summary>
		/// <typeparam name="T">The type of the created <see cref="System.Array" />.</typeparam>
		/// <param name="length">The number of elements of the <see cref="System.Array" />.</param>
		/// <param name="value">The value that is assigned to each element of the <see cref="System.Array" />.</param>
		/// <returns>
		/// A new <see cref="System.Array" /> with the specified length, where each element is initialized with <paramref name="value" />.
		/// </returns>
		public static T[] Array<T>(int length, T value)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(length, nameof(length));

			T[] array = new T[length];

			if (value != null)
			{
				for (int i = 0; i < length; i++) array[i] = value;
			}

			return array;
		}
		/// <summary>
		/// Creates an <see cref="System.Array" /> of the specified type and initialized each element with a value that is retrieved from <paramref name="valueSelector" />.
		/// </summary>
		/// <typeparam name="T">The type of the created <see cref="System.Array" />.</typeparam>
		/// <param name="length">The number of elements of the <see cref="System.Array" />.</param>
		/// <param name="valueSelector">A <see cref="Func{T, TResult}" /> to retrieve new values for the <see cref="System.Array" /> based on the given index.</param>
		/// <returns>
		/// A new <see cref="System.Array" /> with the specified length, where each element is initialized with a value that is retrieved from <paramref name="valueSelector" />.
		/// </returns>
		public static T[] Array<T>(int length, Func<int, T> valueSelector)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(length, nameof(length));
			Check.ArgumentNull(valueSelector, nameof(valueSelector));

			T[] array = new T[length];
			for (int i = 0; i < length; i++) array[i] = valueSelector(i);
			return array;
		}
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