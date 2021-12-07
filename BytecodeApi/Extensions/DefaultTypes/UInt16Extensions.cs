using System.Globalization;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="ushort" /> objects.
	/// </summary>
	public static class UInt16Extensions
	{
		/// <summary>
		/// Converts the value of this <see cref="ushort" /> to its equivalent <see cref="string" /> representation using the invariant culture.
		/// </summary>
		/// <param name="value">The <see cref="ushort" /> value to convert.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="ushort" />.
		/// </returns>
		public static string ToStringInvariant(this ushort value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Converts the value of this <see cref="ushort" /> to its equivalent <see cref="string" /> representation using a specified format and the invariant culture.
		/// </summary>
		/// <param name="value">The <see cref="ushort" /> value to convert.</param>
		/// <param name="format">A <see cref="string" /> value with the format that is used to convert this <see cref="ushort" />.</param>
		/// <returns>
		/// The equivalent <see cref="string" /> representation of this <see cref="ushort" />.
		/// </returns>
		public static string ToStringInvariant(this ushort value, string format)
		{
			return value.ToString(format, CultureInfo.InvariantCulture);
		}
		/// <summary>
		/// Returns <see langword="null" />, if this <see cref="ushort" /> value is 0, otherwise its original value.
		/// </summary>
		/// <param name="value">The <see cref="ushort" /> value to convert.</param>
		/// <returns>
		/// <see langword="null" />, if this <see cref="ushort" /> value is 0;
		/// otherwise, its original value.
		/// </returns>
		public static ushort? ToNullIfDefault(this ushort value)
		{
			return value == default(ushort) ? null : (ushort?)value;
		}
	}
}