using System.Globalization;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for conversion of <see cref="string" /> objects to different data types.
/// </summary>
public static class StringConvertExtensions
{
	extension(string? str)
	{
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="bool" /> value or returns <see langword="false" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="bool" /> value that was converted from this <see cref="string" /> and
		/// <see langword="false" />, if conversion failed.
		/// </returns>
		public bool ToBooleanOrDefault()
		{
			return str.ToBooleanOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="bool" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="bool" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public bool ToBooleanOrDefault(bool defaultValue)
		{
			return str.ToBooleanOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="bool" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="bool" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public bool? ToBooleanOrNull()
		{
			return bool.TryParse(str, out bool result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="byte" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="byte" /> value that was converted from this <see cref="string" /> and
		/// 0 if conversion failed.
		/// </returns>
		public byte ToByteOrDefault()
		{
			return str.ToByteOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="byte" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="byte" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public byte ToByteOrDefault(byte defaultValue)
		{
			return str.ToByteOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="byte" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="byte" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public byte? ToByteOrNull()
		{
			return byte.TryParse(str, out byte result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="sbyte" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="sbyte" /> value that was converted from this <see cref="string" /> and
		/// 0 if conversion failed.
		/// </returns>
		public sbyte ToSByteOrDefault()
		{
			return str.ToSByteOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="sbyte" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="sbyte" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public sbyte ToSByteOrDefault(sbyte defaultValue)
		{
			return str.ToSByteOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="sbyte" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="sbyte" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public sbyte? ToSByteOrNull()
		{
			return sbyte.TryParse(str, out sbyte result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="char" /> value or returns '\0' if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="char" /> value that was converted from this <see cref="string" /> and
		/// '\0' if conversion failed.
		/// </returns>
		public char ToCharOrDefault()
		{
			return str.ToCharOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="char" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="char" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public char ToCharOrDefault(char defaultValue)
		{
			return str.ToCharOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="char" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="char" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public char? ToCharOrNull()
		{
			return char.TryParse(str, out char result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="decimal" /> value or returns 0.0m if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="decimal" /> value that was converted from this <see cref="string" /> and
		/// 0.0m if conversion failed.
		/// </returns>
		public decimal ToDecimalOrDefault()
		{
			return str.ToDecimalOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="decimal" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="decimal" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public decimal ToDecimalOrDefault(decimal defaultValue)
		{
			return str.ToDecimalOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="decimal" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="decimal" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public decimal? ToDecimalOrNull()
		{
			return decimal.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="double" /> value or returns 0.0 if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="double" /> value that was converted from this <see cref="string" /> and
		/// 0.0 if conversion failed.
		/// </returns>
		public double ToDoubleOrDefault()
		{
			return str.ToDoubleOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="double" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="double" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public double ToDoubleOrDefault(double defaultValue)
		{
			return str.ToDoubleOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="double" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="double" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public double? ToDoubleOrNull()
		{
			return double.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out double result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="float" /> value or returns 0.0f if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="float" /> value that was converted from this <see cref="string" /> and
		/// 0.0f if conversion failed.
		/// </returns>
		public float ToSingleOrDefault()
		{
			return str.ToSingleOrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="float" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="float" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public float ToSingleOrDefault(float defaultValue)
		{
			return str.ToSingleOrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="float" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="float" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public float? ToSingleOrNull()
		{
			return float.TryParse(str, NumberStyles.Float, CultureInfo.InvariantCulture, out float result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="int" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="int" /> value that was converted from this <see cref="string" /> and
		/// 0 if conversion failed.
		/// </returns>
		public int ToInt32OrDefault()
		{
			return str.ToInt32OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="int" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="int" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public int ToInt32OrDefault(int defaultValue)
		{
			return str.ToInt32OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="int" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="int" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public int? ToInt32OrNull()
		{
			return int.TryParse(str, out int result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="uint" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="uint" /> value that was converted from this <see cref="string" /> and
		/// 0 if conversion failed.
		/// </returns>
		public uint ToUInt32OrDefault()
		{
			return str.ToUInt32OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="uint" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="uint" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public uint ToUInt32OrDefault(uint defaultValue)
		{
			return str.ToUInt32OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="uint" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="uint" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public uint? ToUInt32OrNull()
		{
			return uint.TryParse(str, out uint result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="long" /> value or returns 0L if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="long" /> value that was converted from this <see cref="string" /> and
		/// 0L if conversion failed.
		/// </returns>
		public long ToInt64OrDefault()
		{
			return str.ToInt64OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="long" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="long" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public long ToInt64OrDefault(long defaultValue)
		{
			return str.ToInt64OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="long" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="long" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public long? ToInt64OrNull()
		{
			return long.TryParse(str, out long result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ulong" /> value or returns 0UL if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="ulong" /> value that was converted from this <see cref="string" /> and
		/// 0UL if conversion failed.
		/// </returns>
		public ulong ToUInt64OrDefault()
		{
			return str.ToUInt64OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ulong" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="ulong" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public ulong ToUInt64OrDefault(ulong defaultValue)
		{
			return str.ToUInt64OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ulong" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="ulong" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public ulong? ToUInt64OrNull()
		{
			return ulong.TryParse(str, out ulong result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="short" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="short" /> value that was converted from this <see cref="string" /> and
		/// 0 if conversion failed.
		/// </returns>
		public short ToInt16OrDefault()
		{
			return str.ToInt16OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="short" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="short" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public short ToInt16OrDefault(short defaultValue)
		{
			return str.ToInt16OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="short" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="short" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public short? ToInt16OrNull()
		{
			return short.TryParse(str, out short result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ushort" /> value or returns 0 if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="ushort" /> value that was converted from this <see cref="string" /> and
		/// 0 if conversion failed.
		/// </returns>
		public ushort ToUInt16OrDefault()
		{
			return str.ToUInt16OrDefault(default);
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ushort" /> value or returns <paramref name="defaultValue" />, if conversion failed.
		/// </summary>
		/// <param name="defaultValue">The value that is used if conversion failed.</param>
		/// <returns>
		/// The <see cref="ushort" /> value that was converted from this <see cref="string" /> and
		/// <paramref name="defaultValue" />, if conversion failed.
		/// </returns>
		public ushort ToUInt16OrDefault(ushort defaultValue)
		{
			return str.ToUInt16OrNull() ?? defaultValue;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="ushort" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <returns>
		/// The <see cref="ushort" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public ushort? ToUInt16OrNull()
		{
			return ushort.TryParse(str, out ushort result) ? result : null;
		}

		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="DateTime" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="DateTime" />.</param>
		/// <returns>
		/// The <see cref="DateTime" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public DateTime? ToDateTime(string format)
		{
			Check.ArgumentNull(format);

			return DateTime.TryParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="TimeSpan" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="TimeSpan" />.</param>
		/// <returns>
		/// The <see cref="TimeSpan" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public TimeSpan? ToTimeSpan(string format)
		{
			Check.ArgumentNull(format);

			return TimeSpan.TryParseExact(str, format, CultureInfo.InvariantCulture, out TimeSpan result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="DateOnly" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="DateOnly" />.</param>
		/// <returns>
		/// The <see cref="DateOnly" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public DateOnly? ToDateOnly(string format)
		{
			Check.ArgumentNull(format);

			return DateOnly.TryParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateOnly result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="TimeOnly" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="format">A <see cref="string" /> value specifying the format that is used to convert this <see cref="TimeOnly" />.</param>
		/// <returns>
		/// The <see cref="TimeOnly" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public TimeOnly? ToTimeOnly(string format)
		{
			Check.ArgumentNull(format);

			return TimeOnly.TryParseExact(str, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out TimeOnly result) ? result : null;
		}
		/// <summary>
		/// Tries to convert this <see cref="string" /> to an equivalent <see cref="Uri" /> value or returns <see langword="null" />, if conversion failed.
		/// </summary>
		/// <param name="uriKind">The <see cref="UriKind" /> that is used for conversion.</param>
		/// <returns>
		/// The <see cref="UriKind" /> value that was converted from this <see cref="string" /> and
		/// <see langword="null" />, if conversion failed.
		/// </returns>
		public Uri? ToUri(UriKind uriKind)
		{
			return Uri.TryCreate(str, uriKind, out Uri? result) ? result : null;
		}
	}
}