using BytecodeApi.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace BytecodeApi
{
	/// <summary>
	/// Validates <see cref="string" /> objects against specific data type representations.
	/// </summary>
	public static class Validate
	{
		private static readonly Regex[] MacAddressRegex = new[]
		{
			// 000000000000
			new Regex("^[0-9a-f]{12}$", RegexOptions.IgnoreCase),
			// 00:00:00:00:00:00
			new Regex("^([0-9a-f]{2}:){5}[0-9a-f]{2}$", RegexOptions.IgnoreCase),
			// 00-00-00-00-00-00
			new Regex("^([0-9a-f]{2}-){5}[0-9a-f]{2}$", RegexOptions.IgnoreCase),
			// 0000.0000.0000
			new Regex("^([0-9a-f]{4}.){2}[0-9a-f]{4}$", RegexOptions.IgnoreCase),
			// 000.000.000.000
			new Regex("^([0-9a-f]{3}.){3}[0-9a-f]{3}$", RegexOptions.IgnoreCase),

			// 0000000000000000
			new Regex("^[0-9a-f]{16}$", RegexOptions.IgnoreCase),
			// 00:00:00:00:00:00:00:00
			new Regex("^([0-9a-f]{2}:){7}[0-9a-f]{2}$", RegexOptions.IgnoreCase),
			// 00-00-00-00-00-00-00-00
			new Regex("^([0-9a-f]{2}-){7}[0-9a-f]{2}$", RegexOptions.IgnoreCase),
			// 0000.0000.0000.0000
			new Regex("^([0-9a-f]{4}.){3}[0-9a-f]{4}$", RegexOptions.IgnoreCase)
		};

		/// <summary>
		/// Validates a <see cref="string" /> that only contains upper and lowercase letters.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be validated.</param>
		/// <returns>
		/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool OnlyLetters(string str)
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
		public static bool OnlyDigits(string str)
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
		public static bool AlphaNumeric(string str)
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
		public static bool Hexadecimal(string str)
		{
			return str?.All(CharExtensions.IsHexadecimal) == true;
		}
		/// <summary>
		/// Validates a <see cref="string" /> that is a path.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be validated.</param>
		/// <returns>
		/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool Path(string str)
		{
			return str != null && CSharp.Try(() => new FileIOPermission(FileIOPermissionAccess.Read, SingletonCollection.Array(str)).Demand());
		}
		/// <summary>
		/// Validates a <see cref="string" /> that is a base64 string, only containing the characters a-zA-Z0-9+/= and has the correct amount of padding characters.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be validated.</param>
		/// <returns>
		/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool Base64String(string str)
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
					if (c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c >= '0' && c <= '9' || c == '+' || c == '/')
					{
						if (padding) return false;
						else contentLength++;
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
					contentLength == 3 && (paddingLength == 1 || paddingLength == 2);
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
		public static bool FileName(string str)
		{
			return !str.IsNullOrWhiteSpace() && str.None(System.IO.Path.GetInvalidFileNameChars().Contains);
		}
		/// <summary>
		/// Validates a <see cref="string" /> that is a web URL with http or https scheme.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be validated.</param>
		/// <returns>
		/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool Url(string str)
		{
			return Uri.TryCreate(str, UriKind.Absolute, out Uri result) && CSharp.EqualsAny(result.Scheme, "http", "https");
		}
		/// <summary>
		/// Validates a <see cref="string" /> that is an email address.
		/// </summary>
		/// <param name="str">The <see cref="string" /> to be validated.</param>
		/// <returns>
		/// <see langword="true" />, if validation of <paramref name="str" /> succeeded;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool EmailAddress(string str)
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
		public static bool IPAddress(string str)
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
		public static bool MacAddress(string str, out byte[] result)
		{
			if (str != null && MacAddressRegex.Any(regex => regex.IsMatch(str)))
			{
				result = ConvertEx.FromHexadecimalString(str.ReplaceMultiple(null, ":", "-", "."));
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
		public static bool PhoneNumber(string str)
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
		public static bool CreditCardNumber(string str)
		{
			return str != null && new CreditCardAttribute().IsValid(str);
		}
	}
}