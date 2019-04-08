using System;
using System.Globalization;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for conversion of <see cref="string" /> objects to different data types.
	/// </summary>
	public static class StringConvertExtensions
	{
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="bool" /> value or returns <see langword="false" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="bool" /> value.</param>
		/// <returns>
		/// The <see cref="bool" /> value that was converted from <paramref name="str" /> and
		/// <see langword="false" />, if conversion failed.
		/// </returns>
		public static bool ToBooleanOrDefault(this string str)
		{
			return str.ToBooleanOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="bool" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="bool" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="bool" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static bool ToBooleanOrDefault(this string str, bool defaultValue)
		{
			return str.ToBooleanOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="bool" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="bool" /> value.</param>
		/// <returns>
		/// The <see cref="bool" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static bool? ToBooleanOrNull(this string str)
		{
			return bool.TryParse(str, out bool result) ? (bool?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="byte" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="byte" /> value.</param>
		/// <returns>
		/// The <see cref="byte" /> value that was converted from <paramref name="str" /> and
		/// 0 if conversion failed.
		/// </returns>
		public static byte ToByteOrDefault(this string str)
		{
			return str.ToByteOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="byte" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="byte" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="byte" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static byte ToByteOrDefault(this string str, byte defaultValue)
		{
			return str.ToByteOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="byte" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="byte" /> value.</param>
		/// <returns>
		/// The <see cref="byte" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static byte? ToByteOrNull(this string str)
		{
			return byte.TryParse(str, out byte result) ? (byte?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="sbyte" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="sbyte" /> value.</param>
		/// <returns>
		/// The <see cref="sbyte" /> value that was converted from <paramref name="str" /> and
		/// 0 if conversion failed.
		/// </returns>
		public static sbyte ToSByteOrDefault(this string str)
		{
			return str.ToSByteOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="sbyte" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="sbyte" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="sbyte" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static sbyte ToSByteOrDefault(this string str, sbyte defaultValue)
		{
			return str.ToSByteOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="sbyte" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="sbyte" /> value.</param>
		/// <returns>
		/// The <see cref="sbyte" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static sbyte? ToSByteOrNull(this string str)
		{
			return sbyte.TryParse(str, out sbyte result) ? (sbyte?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="char" /> value or returns '\0' if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="char" /> value.</param>
		/// <returns>
		/// The <see cref="char" /> value that was converted from <paramref name="str" /> and
		/// '\0' if conversion failed.
		/// </returns>
		public static char ToCharOrDefault(this string str)
		{
			return str.ToCharOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="char" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="char" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="char" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static char ToCharOrDefault(this string str, char defaultValue)
		{
			return str.ToCharOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="char" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="char" /> value.</param>
		/// <returns>
		/// The <see cref="char" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static char? ToCharOrNull(this string str)
		{
			return char.TryParse(str, out char result) ? (char?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="decimal" /> value or returns 0.0m if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="decimal" /> value.</param>
		/// <returns>
		/// The <see cref="decimal" /> value that was converted from <paramref name="str" /> and
		/// 0.0m if conversion failed.
		/// </returns>
		public static decimal ToDecimalOrDefault(this string str)
		{
			return str.ToDecimalOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="decimal" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="decimal" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="decimal" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static decimal ToDecimalOrDefault(this string str, decimal defaultValue)
		{
			return str.ToDecimalOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="decimal" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="decimal" /> value.</param>
		/// <returns>
		/// The <see cref="decimal" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static decimal? ToDecimalOrNull(this string str)
		{
			return decimal.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal result) ? (decimal?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="double" /> value or returns 0.0 if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="double" /> value.</param>
		/// <returns>
		/// The <see cref="double" /> value that was converted from <paramref name="str" /> and
		/// 0.0 if conversion failed.
		/// </returns>
		public static double ToDoubleOrDefault(this string str)
		{
			return str.ToDoubleOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="double" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="double" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="double" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static double ToDoubleOrDefault(this string str, double defaultValue)
		{
			return str.ToDoubleOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="double" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="double" /> value.</param>
		/// <returns>
		/// The <see cref="double" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static double? ToDoubleOrNull(this string str)
		{
			return double.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out double result) ? (double?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="float" /> value or returns 0.0f if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="float" /> value.</param>
		/// <returns>
		/// The <see cref="float" /> value that was converted from <paramref name="str" /> and
		/// 0.0f if conversion failed.
		/// </returns>
		public static float ToSingleOrDefault(this string str)
		{
			return str.ToSingleOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="float" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="float" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="float" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static float ToSingleOrDefault(this string str, float defaultValue)
		{
			return str.ToSingleOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="float" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="float" /> value.</param>
		/// <returns>
		/// The <see cref="float" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static float? ToSingleOrNull(this string str)
		{
			return float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out float result) ? (float?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="int" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="int" /> value.</param>
		/// <returns>
		/// The <see cref="int" /> value that was converted from <paramref name="str" /> and
		/// 0 if conversion failed.
		/// </returns>
		public static int ToInt32OrDefault(this string str)
		{
			return str.ToInt32OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="int" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="int" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="int" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static int ToInt32OrDefault(this string str, int defaultValue)
		{
			return str.ToInt32OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="int" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="int" /> value.</param>
		/// <returns>
		/// The <see cref="int" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static int? ToInt32OrNull(this string str)
		{
			return int.TryParse(str, out int result) ? (int?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="uint" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="uint" /> value.</param>
		/// <returns>
		/// The <see cref="uint" /> value that was converted from <paramref name="str" /> and
		/// 0 if conversion failed.
		/// </returns>
		public static uint ToUInt32OrDefault(this string str)
		{
			return str.ToUInt32OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="uint" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="uint" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="uint" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static uint ToUInt32OrDefault(this string str, uint defaultValue)
		{
			return str.ToUInt32OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="uint" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="uint" /> value.</param>
		/// <returns>
		/// The <see cref="uint" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static uint? ToUInt32OrNull(this string str)
		{
			return uint.TryParse(str, out uint result) ? (uint?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="long" /> value or returns 0L if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="long" /> value.</param>
		/// <returns>
		/// The <see cref="long" /> value that was converted from <paramref name="str" /> and
		/// 0L if conversion failed.
		/// </returns>
		public static long ToInt64OrDefault(this string str)
		{
			return str.ToInt64OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="long" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="long" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="long" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static long ToInt64OrDefault(this string str, long defaultValue)
		{
			return str.ToInt64OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="long" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="long" /> value.</param>
		/// <returns>
		/// The <see cref="long" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static long? ToInt64OrNull(this string str)
		{
			return long.TryParse(str, out long result) ? (long?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ulong" /> value or returns 0UL if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="ulong" /> value.</param>
		/// <returns>
		/// The <see cref="ulong" /> value that was converted from <paramref name="str" /> and
		/// 0UL if conversion failed.
		/// </returns>
		public static ulong ToUInt64OrDefault(this string str)
		{
			return str.ToUInt64OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ulong" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="ulong" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="ulong" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static ulong ToUInt64OrDefault(this string str, ulong defaultValue)
		{
			return str.ToUInt64OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ulong" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="ulong" /> value.</param>
		/// <returns>
		/// The <see cref="ulong" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static ulong? ToUInt64OrNull(this string str)
		{
			return ulong.TryParse(str, out ulong result) ? (ulong?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="short" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="short" /> value.</param>
		/// <returns>
		/// The <see cref="short" /> value that was converted from <paramref name="str" /> and
		/// 0 if conversion failed.
		/// </returns>
		public static short ToInt16OrDefault(this string str)
		{
			return str.ToInt16OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="short" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="short" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="short" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static short ToInt16OrDefault(this string str, short defaultValue)
		{
			return str.ToInt16OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="short" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="short" /> value.</param>
		/// <returns>
		/// The <see cref="short" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static short? ToInt16OrNull(this string str)
		{
			return short.TryParse(str, out short result) ? (short?)result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ushort" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="ushort" /> value.</param>
		/// <returns>
		/// The <see cref="ushort" /> value that was converted from <paramref name="str" /> and
		/// 0 if conversion failed.
		/// </returns>
		public static ushort ToUInt16OrDefault(this string str)
		{
			return str.ToUInt16OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ushort" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="ushort" /> value.</param>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="ushort" /> value that was converted from <paramref name="str" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public static ushort ToUInt16OrDefault(this string str, ushort defaultValue)
		{
			return str.ToUInt16OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ushort" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="ushort" /> value.</param>
		/// <returns>
		/// The <see cref="ushort" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static ushort? ToUInt16OrNull(this string str)
		{
			return ushort.TryParse(str, out ushort result) ? (ushort?)result : null;
		}

		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="DateTime" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="DateTime" /> value.</param>
		/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="DateTime" />.</param>
		/// <returns>
		/// The <see cref="DateTime" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static DateTime? ToDateTime(this string str, string format)
		{
			Check.ArgumentNull(format, nameof(format));

			return DateTime.TryParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result) ? result : (DateTime?)null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="TimeSpan" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="TimeSpan" /> value.</param>
		/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// The <see cref="TimeSpan" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static TimeSpan? ToTimeSpan(this string str, string format)
		{
			Check.ArgumentNull(format, nameof(format));

			return TimeSpan.TryParseExact(str, format, CultureInfo.InvariantCulture, out TimeSpan result) ? result : (TimeSpan?)null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="Uri" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="str">The <see cref="string" /> with a convertible representation of a <see cref="Uri" /> value.</param>
		/// <param name="uriKind">The <see cref="UriKind" /> that is used for conversion.</param>
		/// <returns>
		/// The <see cref="UriKind" /> value that was converted from <paramref name="str" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public static Uri ToUri(this string str, UriKind uriKind)
		{
			return Uri.TryCreate(str, uriKind, out Uri result) ? result : null;
		}
	}
}