using BytecodeApi.Extensions;
using System.Diagnostics;

namespace BytecodeApi.Mathematics;

/// <summary>
/// Represents a byte size value as a 64-bit integer value.
/// </summary>
[DebuggerDisplay($"{nameof(ByteSize)}: Bytes = {{Bytes}}, Format: {{Format(),nq}}")]
public readonly struct ByteSize : IComparable, IComparable<ByteSize>, IEquatable<ByteSize>
{
	private const long BytesInKiloByte = 1024L;
	private const long BytesInMegaByte = 1024L * 1024;
	private const long BytesInGigaByte = 1024L * 1024 * 1024;
	private const long BytesInTeraByte = 1024L * 1024 * 1024 * 1024;
	private const long BytesInPetaByte = 1024L * 1024 * 1024 * 1024 * 1024;
	private const long BytesInExaByte = 1024L * 1024 * 1024 * 1024 * 1024 * 1024;
	internal const int DefaultFormatDecimals = 2;

	/// <summary>
	/// Represents the zero <see cref="ByteSize" /> value. This field is read-only.
	/// </summary>
	public static readonly ByteSize Zero = 0;
	/// <summary>
	/// Represents the maximum <see cref="ByteSize" /> value. This field is read-only.
	/// </summary>
	public static readonly ByteSize MaxValue = long.MaxValue;
	/// <summary>
	/// Represents the 1 kilobyte <see cref="ByteSize" /> value. This field is read-only.
	/// </summary>
	public static readonly ByteSize KiloByte = BytesInKiloByte;
	/// <summary>
	/// Represents the 1 megabyte <see cref="ByteSize" /> value. This field is read-only.
	/// </summary>
	public static readonly ByteSize MegaByte = BytesInMegaByte;
	/// <summary>
	/// Represents the 1 gigabyte <see cref="ByteSize" /> value. This field is read-only.
	/// </summary>
	public static readonly ByteSize GigaByte = BytesInGigaByte;
	/// <summary>
	/// Represents the 1 terabyte <see cref="ByteSize" /> value. This field is read-only.
	/// </summary>
	public static readonly ByteSize TeraByte = BytesInTeraByte;
	/// <summary>
	/// Represents the 1 petabyte <see cref="ByteSize" /> value. This field is read-only.
	/// </summary>
	public static readonly ByteSize PetaByte = BytesInPetaByte;
	/// <summary>
	/// Represents the 1 exabyte <see cref="ByteSize" /> value. This field is read-only.
	/// </summary>
	public static readonly ByteSize ExaByte = BytesInExaByte;

	/// <summary>
	/// Gets the number of bytes that represent the value of this instance.
	/// </summary>
	public long Bytes { get; init; }
	/// <summary>
	/// Gets the number of kilobytes, calculated from the <see cref="Bytes" /> property.
	/// </summary>
	public double KiloBytes => Bytes / (double)BytesInKiloByte;
	/// <summary>
	/// Gets the number of megabytes, calculated from the <see cref="Bytes" /> property.
	/// </summary>
	public double MegaBytes => Bytes / (double)BytesInMegaByte;
	/// <summary>
	/// Gets the number of gigabytes, calculated from the <see cref="Bytes" /> property.
	/// </summary>
	public double GigaBytes => Bytes / (double)BytesInGigaByte;
	/// <summary>
	/// Gets the number of terabytes, calculated from the <see cref="Bytes" /> property.
	/// </summary>
	public double TeraBytes => Bytes / (double)BytesInTeraByte;
	/// <summary>
	/// Gets the number of petabytes, calculated from the <see cref="Bytes" /> property.
	/// </summary>
	public double PetaBytes => Bytes / (double)BytesInPetaByte;
	/// <summary>
	/// Gets the number of exabytes, calculated from the <see cref="Bytes" /> property.
	/// </summary>
	public double ExaBytes => Bytes / (double)BytesInExaByte;
	/// <summary>
	/// Gets the closest <see cref="ByteSizeUnit" /> in which the value of this instance can be represented as a number greater than or equal to 1.
	/// <para>Example: 1023 bytes is <see cref="ByteSizeUnit.Byte" />; 1024 bytes is <see cref="ByteSizeUnit.KiloByte" /></para>
	/// </summary>
	public ByteSizeUnit ClosestUnit
	{
		get
		{
			return Math.Abs(Bytes) switch
			{
				< BytesInKiloByte => ByteSizeUnit.Byte,
				< BytesInMegaByte => ByteSizeUnit.KiloByte,
				< BytesInGigaByte => ByteSizeUnit.MegaByte,
				< BytesInTeraByte => ByteSizeUnit.GigaByte,
				< BytesInPetaByte => ByteSizeUnit.TeraByte,
				< BytesInExaByte => ByteSizeUnit.PetaByte,
				_ => ByteSizeUnit.ExaByte
			};
		}
	}
	/// <summary>
	/// Gets the <see cref="double" /> value that represents the value of this instance in accordance to the <see cref="ClosestUnit" /> property.
	/// </summary>
	public double NumberToClosestUnit => GetNumberForUnit(ClosestUnit);

	/// <summary>
	/// Initializes a new instance of the <see cref="ByteSize" /> structure with the number of bytes.
	/// </summary>
	/// <param name="bytes">A <see cref="long" /> value specifying the number of bytes.</param>
	public ByteSize(long bytes)
	{
		Bytes = bytes;
	}
	/// <summary>
	/// Returns a <see cref="ByteSize" /> that represents a specified number kilobytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of kilobytes.</param>
	/// <returns>
	/// A <see cref="ByteSize" /> that represents <paramref name="value" />.
	/// </returns>
	public static ByteSize FromKiloBytes(long value)
	{
		return value * BytesInKiloByte;
	}
	/// <summary>
	/// Returns a <see cref="ByteSize" /> that represents a specified number megabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of megabytes.</param>
	/// <returns>
	/// A <see cref="ByteSize" /> that represents <paramref name="value" />.
	/// </returns>
	public static ByteSize FromMegaBytes(long value)
	{
		return value * BytesInMegaByte;
	}
	/// <summary>
	/// Returns a <see cref="ByteSize" /> that represents a specified number gigabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of gigabytes.</param>
	/// <returns>
	/// A <see cref="ByteSize" /> that represents <paramref name="value" />.
	/// </returns>
	public static ByteSize FromGigaBytes(long value)
	{
		return value * BytesInGigaByte;
	}
	/// <summary>
	/// Returns a <see cref="ByteSize" /> that represents a specified number terabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of terabytes.</param>
	/// <returns>
	/// A <see cref="ByteSize" /> that represents <paramref name="value" />.
	/// </returns>
	public static ByteSize FromTeraBytes(long value)
	{
		return value * BytesInTeraByte;
	}
	/// <summary>
	/// Returns a <see cref="ByteSize" /> that represents a specified number petabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of petabytes.</param>
	/// <returns>
	/// A <see cref="ByteSize" /> that represents <paramref name="value" />.
	/// </returns>
	public static ByteSize FromPetaBytes(long value)
	{
		return value * BytesInPetaByte;
	}
	/// <summary>
	/// Returns a <see cref="ByteSize" /> that represents a specified number exabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of exabytes.</param>
	/// <returns>
	/// A <see cref="ByteSize" /> that represents <paramref name="value" />.
	/// </returns>
	public static ByteSize FromExaBytes(long value)
	{
		return value * BytesInExaByte;
	}

	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the sum of this instance and the specified <see cref="ByteSize" /> value.
	/// </summary>
	/// <param name="value">The <see cref="ByteSize" /> value to add.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the sum of this instance and the specified <see cref="ByteSize" /> value.
	/// </returns>
	public ByteSize Add(ByteSize value)
	{
		return Bytes + value.Bytes;
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the sum of this instance and the specified number of kilobytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of kilobytes to add.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the sum of this instance and the specified number of kilobytes.
	/// </returns>
	public ByteSize AddKiloBytes(long value)
	{
		return Add(FromKiloBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the sum of this instance and the specified number of megabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of megabytes to add.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the sum of this instance and the specified number of megabytes.
	/// </returns>
	public ByteSize AddMegaBytes(long value)
	{
		return Add(FromMegaBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the sum of this instance and the specified number of gigabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of gigabytes to add.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the sum of this instance and the specified number of gigabytes.
	/// </returns>
	public ByteSize AddGigaBytes(long value)
	{
		return Add(FromGigaBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the sum of this instance and the specified number of terabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of terabytes to add.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the sum of this instance and the specified number of terabytes.
	/// </returns>
	public ByteSize AddTeraBytes(long value)
	{
		return Add(FromTeraBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the sum of this instance and the specified number of petabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of petabytes to add.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the sum of this instance and the specified number of petabytes.
	/// </returns>
	public ByteSize AddPetaBytes(long value)
	{
		return Add(FromPetaBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the sum of this instance and the specified number of exabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of exabytes to add.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the sum of this instance and the specified number of exabytes.
	/// </returns>
	public ByteSize AddExaBytes(long value)
	{
		return Add(FromExaBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the difference of this instance and the specified <see cref="ByteSize" /> value.
	/// </summary>
	/// <param name="value">The <see cref="ByteSize" /> value to subtract.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the difference of this instance and the specified <see cref="ByteSize" /> value.
	/// </returns>
	public ByteSize Subtract(ByteSize value)
	{
		return Bytes - value.Bytes;
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the difference of this instance and the specified number of kilobytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of kilobytes to subtract.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the difference of this instance and the specified number of kilobytes.
	/// </returns>
	public ByteSize SubtractKiloBytes(long value)
	{
		return Subtract(FromKiloBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the difference of this instance and the specified number of megabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of megabytes to subtract.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the difference of this instance and the specified number of megabytes.
	/// </returns>
	public ByteSize SubtractMegaBytes(long value)
	{
		return Subtract(FromMegaBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the difference of this instance and the specified number of gigabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of gigabytes to subtract.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the difference of this instance and the specified number of gigabytes.
	/// </returns>
	public ByteSize SubtractGigaBytes(long value)
	{
		return Subtract(FromGigaBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the difference of this instance and the specified number of terabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of terabytes to subtract.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the difference of this instance and the specified number of terabytes.
	/// </returns>
	public ByteSize SubtractTeraBytes(long value)
	{
		return Subtract(FromTeraBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the difference of this instance and the specified number of petabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of petabytes to subtract.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the difference of this instance and the specified number of petabytes.
	/// </returns>
	public ByteSize SubtractPetaBytes(long value)
	{
		return Subtract(FromPetaBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the difference of this instance and the specified number of exabytes.
	/// </summary>
	/// <param name="value">A <see cref="long" /> value specifying the number of exabytes to subtract.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the difference of this instance and the specified number of exabytes.
	/// </returns>
	public ByteSize SubtractExaBytes(long value)
	{
		return Subtract(FromExaBytes(value));
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the result of this instance, multiplied by the specified <see cref="long" /> value.
	/// </summary>
	/// <param name="value">The multiplier.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the result of this instance, multiplied by the specified <see cref="long" /> value.
	/// </returns>
	public ByteSize Multiply(long value)
	{
		return Bytes * value;
	}
	/// <summary>
	/// Returns a new <see cref="ByteSize" /> value that is the result of this instance, divided by the specified <see cref="long" /> value.
	/// </summary>
	/// <param name="value">The divisor.</param>
	/// <returns>
	/// A new <see cref="ByteSize" /> value that represents the result of this instance, divided by the specified <see cref="long" /> value.
	/// </returns>
	public ByteSize Divide(long value)
	{
		return Bytes / value;
	}

	/// <summary>
	/// Gets the <see cref="double" /> value that represents the value of this instance in accordance to the <paramref name="unit" /> parameter.
	/// </summary>
	/// <param name="unit">The <see cref="ByteSizeUnit" /> to convert the value of this instance to.</param>
	/// <returns>
	/// A <see cref="double" /> value representing the value of this instance in accordance to the <paramref name="unit" /> parameter.
	/// </returns>
	public double GetNumberForUnit(ByteSizeUnit unit)
	{
		return unit switch
		{
			ByteSizeUnit.Byte => Bytes,
			ByteSizeUnit.KiloByte => KiloBytes,
			ByteSizeUnit.MegaByte => MegaBytes,
			ByteSizeUnit.GigaByte => GigaBytes,
			ByteSizeUnit.TeraByte => TeraBytes,
			ByteSizeUnit.PetaByte => PetaBytes,
			ByteSizeUnit.ExaByte => ExaBytes,
			_ => throw Throw.InvalidEnumArgument(nameof(unit), unit)
		};
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// An equivalent <see cref="string" /> representing this instance.
	/// </returns>
	public string Format()
	{
		return Format(DefaultFormatDecimals);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance using the specified formatting parameters.
	/// </summary>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <returns>
	/// An equivalent <see cref="string" /> representing this instance.
	/// </returns>
	public string Format(int decimals)
	{
		return Format(decimals, false);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance using the specified formatting parameters.
	/// </summary>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	/// <returns>
	/// An equivalent <see cref="string" /> representing this instance.
	/// </returns>
	public string Format(int decimals, bool padDecimals)
	{
		return Format(decimals, padDecimals, false);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance using the specified formatting parameters.
	/// </summary>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	/// <param name="thousandsSeparator"><see langword="true" /> to use a thousands separator.</param>
	/// <returns>
	/// An equivalent <see cref="string" /> representing this instance.
	/// </returns>
	public string Format(int decimals, bool padDecimals, bool thousandsSeparator)
	{
		return Format(decimals, padDecimals, thousandsSeparator, false);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance using the specified formatting parameters.
	/// </summary>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	/// <param name="thousandsSeparator"><see langword="true" /> to use a thousands separator.</param>
	/// <param name="roundUp"><see langword="true" /> to always round up. The <paramref name="decimals" /> parameter should typically be 0, if this option is used.</param>
	/// <returns>
	/// An equivalent <see cref="string" /> representing this instance.
	/// </returns>
	public string Format(int decimals, bool padDecimals, bool thousandsSeparator, bool roundUp)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(decimals);
		Check.ArgumentOutOfRange(decimals <= 15, nameof(decimals), "The number of decimals must be in range of 0...15.");

		return Format(ClosestUnit, decimals, padDecimals, thousandsSeparator, roundUp);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance using a specified <see cref="ByteSizeUnit" />.
	/// </summary>
	/// <param name="unit">The <see cref="ByteSizeUnit" /> that is used to format the result <see cref="string" />.</param>
	/// <returns>
	/// An equivalent <see cref="string" /> representing this instance.
	/// </returns>
	public string Format(ByteSizeUnit unit)
	{
		return Format(unit, DefaultFormatDecimals);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance using a specified <see cref="ByteSizeUnit" /> and the specified formatting parameters.
	/// </summary>
	/// <param name="unit">The <see cref="ByteSizeUnit" /> that is used to format the result <see cref="string" />.</param>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <returns>
	/// An equivalent <see cref="string" /> representing this instance.
	/// </returns>
	public string Format(ByteSizeUnit unit, int decimals)
	{
		return Format(unit, decimals, false);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance using a specified <see cref="ByteSizeUnit" /> and the specified formatting parameters.
	/// </summary>
	/// <param name="unit">The <see cref="ByteSizeUnit" /> that is used to format the result <see cref="string" />.</param>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	/// <returns>
	/// An equivalent <see cref="string" /> representing this instance.
	/// </returns>
	public string Format(ByteSizeUnit unit, int decimals, bool padDecimals)
	{
		return Format(unit, decimals, padDecimals, false);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance using a specified <see cref="ByteSizeUnit" /> and the specified formatting parameters.
	/// </summary>
	/// <param name="unit">The <see cref="ByteSizeUnit" /> that is used to format the result <see cref="string" />.</param>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	/// <param name="thousandsSeparator"><see langword="true" /> to use a thousands separator.</param>
	/// <returns>
	/// An equivalent <see cref="string" /> representing this instance.
	/// </returns>
	public string Format(ByteSizeUnit unit, int decimals, bool padDecimals, bool thousandsSeparator)
	{
		return Format(unit, decimals, padDecimals, thousandsSeparator, false);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance using a specified <see cref="ByteSizeUnit" /> and the specified formatting parameters.
	/// </summary>
	/// <param name="unit">The <see cref="ByteSizeUnit" /> that is used to format the result <see cref="string" />.</param>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="padDecimals"><see langword="true" /> to pad zero decimal places with a '0' character.</param>
	/// <param name="thousandsSeparator"><see langword="true" /> to use a thousands separator.</param>
	/// <param name="roundUp"><see langword="true" /> to always round up. The <paramref name="decimals" /> parameter should typically be 0, if this option is used.</param>
	/// <returns>
	/// An equivalent <see cref="string" /> representing this instance.
	/// </returns>
	public string Format(ByteSizeUnit unit, int decimals, bool padDecimals, bool thousandsSeparator, bool roundUp)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(decimals);
		Check.ArgumentOutOfRange(decimals <= 15, nameof(decimals), "The number of decimals must be in range of 0...15.");

		double bytes = Math.Abs(GetNumberForUnit(unit));
		bytes = roundUp ? Math.Ceiling(bytes) : Math.Round(bytes, decimals);

		return
			(Bytes < 0 ? "-" : "") +
			bytes.ToStringInvariant((thousandsSeparator ? "#," : null) + "0." + (padDecimals ? '0' : '#').Repeat(decimals)).Swap('.', ',') +
			" " +
			unit.GetDescription();
	}

	/// <summary>
	/// Compares this instance to a specified <see cref="ByteSize" /> and returns a comparison of their relative values.
	/// </summary>
	/// <param name="obj">An <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// A value that indicates the relative order of the objects being compared.
	/// </returns>
	public int CompareTo(object? obj)
	{
		Check.Argument(obj is ByteSize, nameof(obj), $"{nameof(obj)} is not the same type as this instance.");

		return CompareTo((ByteSize)obj!);
	}
	/// <summary>
	/// Compares this instance to a specified <see cref="ByteSize" /> and returns a comparison of their relative values.
	/// </summary>
	/// <param name="other">A <see cref="ByteSize" /> to compare with this instance.</param>
	/// <returns>
	/// A value that indicates the relative order of the objects being compared.
	/// </returns>
	public int CompareTo(ByteSize other)
	{
		return Bytes.CompareTo(other.Bytes);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return Bytes.ToString();
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is ByteSize byteSize && Equals(byteSize);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="ByteSize" />.
	/// </summary>
	/// <param name="other">The <see cref="ByteSize" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(ByteSize other)
	{
		return Bytes == other.Bytes;
	}
	/// <summary>
	/// Returns a hash code for this <see cref="ByteSize" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="ByteSize" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Bytes);
	}

	/// <summary>
	/// Compares two <see cref="ByteSize" /> values for equality.
	/// </summary>
	/// <param name="a">The first <see cref="ByteSize" /> to compare.</param>
	/// <param name="b">The second <see cref="ByteSize" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ByteSize" /> values are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(ByteSize a, ByteSize b)
	{
		return a.Bytes == b.Bytes;
	}
	/// <summary>
	/// Compares two <see cref="ByteSize" /> values for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="ByteSize" /> to compare.</param>
	/// <param name="b">The second <see cref="ByteSize" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ByteSize" /> values are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(ByteSize a, ByteSize b)
	{
		return a.Bytes != b.Bytes;
	}
	/// <summary>
	/// Returns a value indicating whether a specified <see cref="ByteSize" /> value is less than another specified <see cref="ByteSize" /> value.
	/// </summary>
	/// <param name="a">The first value to compare.</param>
	/// <param name="b">The second value to compare.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="a" /> is less than <paramref name="b" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator <(ByteSize a, ByteSize b)
	{
		return a.Bytes < b.Bytes;
	}
	/// <summary>
	/// Returns a value indicating whether a specified <see cref="ByteSize" /> value is less than or equal to another specified <see cref="ByteSize" /> value.
	/// </summary>
	/// <param name="a">The first value to compare.</param>
	/// <param name="b">The second value to compare.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="a" /> is less than or equal to <paramref name="b" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator <=(ByteSize a, ByteSize b)
	{
		return a.Bytes <= b.Bytes;
	}
	/// <summary>
	/// Returns a value indicating whether a specified <see cref="ByteSize" /> value is greater than another specified <see cref="ByteSize" /> value.
	/// </summary>
	/// <param name="a">The first value to compare.</param>
	/// <param name="b">The second value to compare.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="a" /> is greater than <paramref name="b" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator >(ByteSize a, ByteSize b)
	{
		return a.Bytes > b.Bytes;
	}
	/// <summary>
	/// Returns a value indicating whether a specified <see cref="ByteSize" /> value is greater than or equal to another specified <see cref="ByteSize" /> value.
	/// </summary>
	/// <param name="a">The first value to compare.</param>
	/// <param name="b">The second value to compare.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="a" /> is greater than or equal to <paramref name="b" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator >=(ByteSize a, ByteSize b)
	{
		return a.Bytes >= b.Bytes;
	}
	/// <summary>
	/// Adds two specified <see cref="ByteSize" /> values.
	/// </summary>
	/// <param name="a">The first value to add.</param>
	/// <param name="b">The second value to add.</param>
	/// <returns>
	/// The result of adding <paramref name="a" /> and <paramref name="b" />.
	/// </returns>
	public static ByteSize operator +(ByteSize a, ByteSize b)
	{
		return a.Add(b);
	}
	/// <summary>
	/// Increments the <see cref="ByteSize" /> operand by 1.
	/// </summary>
	/// <param name="value">The value to increment.</param>
	/// <returns>
	/// <paramref name="value" /> incremented by 1.
	/// </returns>
	public static ByteSize operator ++(ByteSize value)
	{
		return value.Add(1);
	}
	/// <summary>
	/// Subtracts two specified <see cref="ByteSize" /> values.
	/// </summary>
	/// <param name="a">The minuend.</param>
	/// <param name="b">The subtrahend.</param>
	/// <returns>
	/// The result of adding <paramref name="b" /> from <paramref name="a" />.
	/// </returns>
	public static ByteSize operator -(ByteSize a, ByteSize b)
	{
		return a.Subtract(b);
	}
	/// <summary>
	/// Decrements the <see cref="ByteSize" /> operand by 1.
	/// </summary>
	/// <param name="value">The value to decrement.</param>
	/// <returns>
	/// <paramref name="value" /> decremented by 1.
	/// </returns>
	public static ByteSize operator --(ByteSize value)
	{
		return value.Subtract(1);
	}
	/// <summary>
	/// Multiplies a specified <see cref="ByteSize" /> value and a specified <see cref="long" /> value.
	/// </summary>
	/// <param name="a">The first value to multiply.</param>
	/// <param name="b">The second value to multiply.</param>
	/// <returns>
	/// The result of multiplying <paramref name="a" /> by <paramref name="b" />.
	/// </returns>
	public static ByteSize operator *(ByteSize a, long b)
	{
		return a.Multiply(b);
	}
	/// <summary>
	/// Divides a specified <see cref="ByteSize" /> value and a specified <see cref="long" /> value.
	/// </summary>
	/// <param name="a">The dividend.</param>
	/// <param name="b">The divisor.</param>
	/// <returns>
	/// The result of dividing <paramref name="a" /> by <paramref name="b" />.
	/// </returns>
	public static ByteSize operator /(ByteSize a, long b)
	{
		return a.Divide(b);
	}
	/// <summary>
	/// Defines an implicit conversion of a <see cref="long" /> to a <see cref="ByteSize" />.
	/// </summary>
	/// <param name="value">The <see cref="long" /> to convert.</param>
	public static implicit operator ByteSize(long value)
	{
		return new(value);
	}
	/// <summary>
	/// Defines an explicit conversion of a <see cref="ByteSize" /> to a <see cref="long" />.
	/// </summary>
	/// <param name="value">The <see cref="ByteSize" /> to convert.</param>
	public static explicit operator long(ByteSize value)
	{
		return value.Bytes;
	}
}