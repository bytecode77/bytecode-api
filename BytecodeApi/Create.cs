using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System.Security.Cryptography;

namespace BytecodeApi;

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
		Check.ArgumentOutOfRangeEx.GreaterEqual0(length);

		T[] array = new T[length];
		if (!Equals(value, default(T)))
		{
			for (int i = 0; i < length; i++)
			{
				array[i] = value;
			}
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
		Check.ArgumentOutOfRangeEx.GreaterEqual0(length);
		Check.ArgumentNull(valueSelector);

		T[] array = new T[length];
		for (int i = 0; i < length; i++)
		{
			array[i] = valueSelector(i);
		}
		return array;
	}
	/// <summary>
	/// Creates an <see cref="IEnumerable{T}" /> of the specified type and returns values that are retrieved from <paramref name="valueSelector" />.
	/// </summary>
	/// <typeparam name="T">The type of the created <see cref="IEnumerable{T}" />.</typeparam>
	/// <param name="count">The number of elements to return.</param>
	/// <param name="valueSelector">A <see cref="Func{T, TResult}" /> to retrieve new values for the <see cref="IEnumerable{T}" /> based on the given index.</param>
	/// <returns>
	/// A new <see cref="IEnumerable{T}" /> with the specified number of elements, where each element is initialized with a value that is retrieved from <paramref name="valueSelector" />.
	/// </returns>
	public static IEnumerable<T> Enumerable<T>(int count, Func<int, T> valueSelector)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);
		Check.ArgumentNull(valueSelector);

		for (int i = 0; i < count; i++)
		{
			yield return valueSelector(i);
		}
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
	public static string HexString(int size)
	{
		return HexString(size, false);
	}
	/// <summary>
	/// Generates a new <see cref="string" /> with random hexadecimal charaters. The value of <paramref name="size" /> specifies the amount of bytes to be generated. A <see cref="string" /> with a length of <paramref name="size" /> * 2 is returned.
	/// </summary>
	/// <param name="size">A <see cref="int" /> value specifying the amount of bytes to be generated.</param>
	/// <param name="cryptographic"><see langword="true" /> to use a cryptographic random number generator; <see langword="false" /> to use the default random number generator.</param>
	/// <returns>
	/// A new <see cref="string" /> with random hexadecimal charaters, where the value of <paramref name="size" /> specifies the amount of bytes to be generated. The returned <see cref="string" /> has a length of <paramref name="size" /> * 2.
	/// </returns>
	public static string HexString(int size, bool cryptographic)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(size);

		if (cryptographic)
		{
			return RandomNumberGenerator.GetBytes(size).ToHexString();
		}
		else
		{
			return Random.Shared.GetBytes(size).ToHexString();
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
		return AlphaNumericString(length, digits, lowerCase, upperCase, false);
	}
	/// <summary>
	/// Generates a new <see cref="string" /> with random characters, where each character can be either a digit, a lowercase letter, an uppercase letter, or a combination of those. At least one of these parameters needs to be <see langword="true" />.
	/// </summary>
	/// <param name="length">The length of the generated <see cref="string" />.</param>
	/// <param name="digits"><see langword="true" /> to include digits.</param>
	/// <param name="lowerCase"><see langword="true" /> to include lowercase letters.</param>
	/// <param name="upperCase"><see langword="true" /> to include uppercase letters.</param>
	/// <param name="cryptographic"><see langword="true" /> to use a cryptographic random number generator; <see langword="false" /> to use the default random number generator.</param>
	/// <returns>
	/// A new <see cref="string" /> with random characters.
	/// </returns>
	public static string AlphaNumericString(int length, bool digits, bool lowerCase, bool upperCase, bool cryptographic)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(length);
		Check.Argument(digits || lowerCase || upperCase, null, $"Either of '{nameof(digits)}', '{nameof(lowerCase)}', or '{nameof(upperCase)}' must be true.");

		string chars = "";
		if (digits) chars += TextResources.Digits;
		if (lowerCase) chars += TextResources.Alphabet;
		if (upperCase) chars += TextResources.Alphabet.ToUpper();

		char[] charset = chars.ToCharArray();
		char[] newString = new char[length];

		if (cryptographic)
		{
			for (int i = 0; i < length; i++)
			{
				newString[i] = RandomNumberGenerator.Shared.GetObject(charset);
			}
		}
		else
		{
			for (int i = 0; i < length; i++)
			{
				newString[i] = Random.Shared.GetObject(charset);
			}
		}

		return newString.AsString();
	}
}