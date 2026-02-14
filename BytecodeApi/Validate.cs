using BytecodeApi.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BytecodeApi;

/// <summary>
/// Validates <see cref="string" /> objects against specific data type representations.
/// </summary>
public static class Validate
{
	private static readonly Regex[] MacAddressRegex =
	[
		// 000000000000
		new("^[0-9a-f]{12}$", RegexOptions.IgnoreCase),
		// 00:00:00:00:00:00
		new("^([0-9a-f]{2}:){5}[0-9a-f]{2}$", RegexOptions.IgnoreCase),
		// 00-00-00-00-00-00
		new("^([0-9a-f]{2}-){5}[0-9a-f]{2}$", RegexOptions.IgnoreCase),
		// 0000.0000.0000
		new("^([0-9a-f]{4}\\.){2}[0-9a-f]{4}$", RegexOptions.IgnoreCase),
		// 000.000.000.000
		new("^([0-9a-f]{3}\\.){3}[0-9a-f]{3}$", RegexOptions.IgnoreCase),

		// 0000000000000000
		new("^[0-9a-f]{16}$", RegexOptions.IgnoreCase),
		// 00:00:00:00:00:00:00:00
		new("^([0-9a-f]{2}:){7}[0-9a-f]{2}$", RegexOptions.IgnoreCase),
		// 00-00-00-00-00-00-00-00
		new("^([0-9a-f]{2}-){7}[0-9a-f]{2}$", RegexOptions.IgnoreCase),
		// 0000.0000.0000.0000
		new("^([0-9a-f]{4}\\.){3}[0-9a-f]{4}$", RegexOptions.IgnoreCase)
	];

	/// <summary>
	/// Validates a <see cref="string" /> that only contains upper and lowercase letters.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool OnlyLetters([NotNullWhen(true)] string? str)
	{
		return str?.All(char.IsLetter) == true;
	}
	/// <summary>
	/// Validates a <see cref="string" /> that only contains numeric digits.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool OnlyDigits([NotNullWhen(true)] string? str)
	{
		return str?.All(char.IsDigit) == true;
	}
	/// <summary>
	/// Validates a <see cref="string" /> that only contains upper and lowercase letters as well as numeric digits.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool AlphaNumeric([NotNullWhen(true)] string? str)
	{
		return str?.All(char.IsLetterOrDigit) == true;
	}
	/// <summary>
	/// Validates a <see cref="string" /> that only contains upper and lowercase hexadecimal digits.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool Hexadecimal([NotNullWhen(true)] string? str)
	{
		return str?.All(CharExtensions.IsHexadecimal) == true;
	}
	/// <summary>
	/// Validates a <see cref="string" /> that is a base64 string, only containing the characters a-zA-Z0-9+/= and has the correct amount of padding characters.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool Base64String([NotNullWhen(true)] string? str)
	{
		if (str == null)
		{
			return false;
		}
		else
		{
			bool padding = false;
			int contentLength = 0;
			int paddingLength = 0;

			foreach (char c in str.Where(c => !c.IsWhiteSpace()))
			{
				if (c is >= 'a' and <= 'z' or >= 'A' and <= 'Z' or >= '0' and <= '9' or '+' or '/')
				{
					if (padding)
					{
						return false;
					}
					else
					{
						contentLength++;
					}
				}
				else if (c == '=')
				{
					padding = true;
					paddingLength++;
				}
			}

			contentLength &= 3;

			return
				contentLength == 0 && paddingLength == 0 ||
				contentLength == 2 && paddingLength == 2 ||
				contentLength == 3 && paddingLength is 1 or 2;
		}
	}
	/// <summary>
	/// Validates a <see cref="string" /> that is an absolute file path.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool Path([NotNullWhen(true)] string? str)
	{
		if (str.IsNullOrWhiteSpace())
		{
			return false;
		}
		else
		{
			string? root;
			string? fileName;

			try
			{
				root = System.IO.Path.GetPathRoot(str);
				fileName = System.IO.Path.GetFileName(str);
			}
			catch (ArgumentException)
			{
				return false;
			}

			return
				root?.Length >= 3 &&
				root[0] is >= 'a' and <= 'z' or >= 'A' and <= 'Z' &&
				root[1] == ':' &&
				root[2] is '\\' or '/' &&
				root.IndexOfAny(System.IO.Path.GetInvalidPathChars()) == -1 &&
				(fileName == null || fileName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) == -1);
		}
	}
	/// <summary>
	/// Validates a <see cref="string" /> that is a filename.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool FileName([NotNullWhen(true)] string? str)
	{
		return !str.IsNullOrWhiteSpace() && str.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) == -1;
	}
	/// <summary>
	/// Validates a <see cref="string" /> that is a web URL with http or https scheme.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool Url([NotNullWhen(true)] string? str)
	{
		return Uri.TryCreate(str, UriKind.Absolute, out Uri? result) && result.Scheme is "http" or "https";
	}
	/// <summary>
	/// Validates a <see cref="string" /> that is an email address.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool EmailAddress([NotNullWhen(true)] string? str)
	{
		return str != null && new EmailAddressAttribute().IsValid(str);
	}
	/// <summary>
	/// Validates a <see cref="string" /> that is an <see cref="System.Net.IPAddress" />. Both IPv4 and IPv6 values are validated.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool IPAddress([NotNullWhen(true)] string? str)
	{
		return System.Net.IPAddress.TryParse(str, out _);
	}
	/// <summary>
	/// Validates a <see cref="string" /> that is a MAC address in EUI-48 or EUI-64 format. When this method returns, a <see cref="byte" />[] is stored in <paramref name="result" />, representing the parsed address bytes.
	/// <para>Allowed formats are: 000000000000, 00:00:00:00:00:00, 00-00-00-00-00-00, 000.000.000.000 and 0000.0000.0000 for EUI-48; and 0000000000000000, 00:00:00:00:00:00:00:00, 00-00-00-00-00-00-00-00 and 0000.0000.0000.0000 for EUI-64.</para>
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <param name="result">When this method returns, contains the address bytes as a <see cref="byte" />[].</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool MacAddress([NotNullWhen(true)] string? str, [NotNullWhen(true)] out byte[]? result)
	{
		if (str != null && MacAddressRegex.Any(regex => regex.IsMatch(str)))
		{
			result = Convert.FromHexString(str.ReplaceMultiple(null, ":", "-", "."));
			return true;
		}
		else
		{
			result = null;
			return false;
		}
	}
	/// <summary>
	/// Validates a <see cref="string" /> that is a phone number.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool PhoneNumber([NotNullWhen(true)] string? str)
	{
		return str != null && new PhoneAttribute().IsValid(str.Replace("/", null));
	}
	/// <summary>
	/// Validates a <see cref="string" /> that is a credit card number.
	/// </summary>
	/// <param name="str">The <see cref="string" /> to be validated.</param>
	/// <returns>
	/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool CreditCardNumber([NotNullWhen(true)] string? str)
	{
		return str != null && new CreditCardAttribute().IsValid(str);
	}
}