using System.Globalization;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="byte" /> objects.
	/// </summary>
	public static class ByteExtensions
	{
		/// <summary>
		/// Converts the value of this <see cref="byte" /> to its equivalent <see cref="string" /> representation using the invariant culture.
		/// </summary>
		/// <param name="value">The <see cref="byte" /> value to convert.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="byte" />.
		/// </returns>
		public static string ToStringInvariant(this byte value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Converts the value of this <see cref="byte" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="value">The <see cref="byte" /> value to convert.</param>
		/// <param name="format">A <see cref="string" /> value with the format that is used to convert this <see cref="byte" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="byte" />.
		/// </returns>
		public static string ToStringInvariant(this byte value, string format)
		{
			return value.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="byte" /> value is 0, otherwise its original value.
		/// </summary>
		/// <param name="value">The <see cref="byte" /> value to convert.</param>
		/// <returns>
		/// <see langword="null" />, if this <see cref="byte" /> value is 0;
		/// otherwise, its original value.
		/// </returns>
		public static byte? ToNullIfDefault(this byte value)
		{
			return value != default(byte) ? value : (byte?)null;
		}
	}
}