using BytecodeApi.Extensions;
using System.Diagnostics;

namespace BytecodeApi.Data;

/// <summary>
/// Represents a money value composed of an amount and a currency.
/// </summary>
[DebuggerDisplay($"{nameof(Money)}: Amount = {{Amount}}, Currency = {{Currency}}")]
public readonly struct Money : IComparable, IComparable<Money>, IEquatable<Money>
{
	/// <summary>
	/// Gets the amount of this money value.
	/// </summary>
	public decimal Amount { get; init; }
	/// <summary>
	/// Gets the currency of this money value.
	/// </summary>
	public Currency Currency { get; init; }
	/// <summary>
	/// Gets the currency symbol. If the currency has no symbol, returns the currency code.
	/// </summary>
	public string CurrencySymbol => Currency.GetDescription() ?? Currency.ToString();

	/// <summary>
	/// Initializes a new instance of the <see cref="Money" /> structure with the specified amount and the <see cref="Currency.Unknown" /> currency.
	/// </summary>
	/// <param name="amount">The amount of this money value.</param>
	public Money(decimal amount)
	{
		Amount = amount;
		Currency = Currency.Unknown;
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="Money" /> structure with the specified amount and currency.
	/// </summary>
	/// <param name="amount">The amount of this money value.</param>
	/// <param name="currency">The currency of this money value.</param>
	public Money(decimal amount, Currency currency)
	{
		Amount = amount;
		Currency = currency;
	}

	/// <summary>
	/// Returns a formatted <see cref="string" /> of this <see cref="Money" /> value.
	/// </summary>
	/// <returns>
	/// A formatted <see cref="string" /> representing this instance.
	/// </returns>
	public string Format()
	{
		return Format(2, true);
	}
	/// <summary>
	/// Returns a formatted <see cref="string" /> of this <see cref="Money" /> value using the specified formatting parameters.
	/// </summary>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="thousandsSeparator"><see langword="true" /> to use a thousands separator.</param>
	/// <returns>
	/// A formatted <see cref="string" /> representing this instance.
	/// </returns>
	public string Format(int decimals, bool thousandsSeparator)
	{
		return Format(decimals, thousandsSeparator, true);
	}
	/// <summary>
	/// Returns a formatted <see cref="string" /> of this <see cref="Money" /> value using the specified formatting parameters.
	/// </summary>
	/// <param name="decimals">The number of decimals to round the result to. The default value is 2.</param>
	/// <param name="thousandsSeparator"><see langword="true" /> to use a thousands separator.</param>
	/// <param name="includeCurrency"><see langword="true" /> to append the currency.</param>
	/// <returns>
	/// A formatted <see cref="string" /> representing this instance.
	/// </returns>
	public string Format(int decimals, bool thousandsSeparator, bool includeCurrency)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(decimals);
		Check.ArgumentOutOfRange(decimals <= 15, nameof(decimals), "The number of decimals must be in range of 0...15.");

		string format = Math
			.Round(Amount, decimals)
			.ToStringInvariant((thousandsSeparator ? "#,#" : "#") + (decimals > 0 ? "." + '0'.Repeat(decimals) : null))
			.Swap('.', ',');

		if (includeCurrency)
		{
			format += " " + Currency;
		}

		return format;
	}

	/// <summary>
	/// Compares this instance to a specified <see cref="Money" /> and returns a comparison of their relative values.
	/// Both values must have matching currencies.
	/// </summary>
	/// <param name="obj">An <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// A value that indicates the relative order of the objects being compared.
	/// </returns>
	public int CompareTo(object? obj)
	{
		Check.Argument(obj is Money, nameof(obj), nameof(obj) + " is not the same type as this instance.");

		return CompareTo((Money)obj!);
	}
	/// <summary>
	/// Compares this instance to a specified <see cref="Money" /> and returns a comparison of their relative values.
	/// Both values must have matching currencies.
	/// </summary>
	/// <param name="other">A <see cref="Money" /> to compare with this instance.</param>
	/// <returns>
	/// A value that indicates the relative order of the objects being compared.
	/// </returns>
	public int CompareTo(Money other)
	{
		Check.Argument(Currency == other.Currency, nameof(other), "Currency of money values must match.");

		return Amount.CompareTo(other.Amount);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// <para>Example: 12.34 USD</para>
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return $"{Amount} {Currency}";
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
		return obj is Money money && Equals(money);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="Money" />.
	/// </summary>
	/// <param name="other">The <see cref="Money" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(Money other)
	{
		return Amount == other.Amount && Currency == other.Currency;
	}
	/// <summary>
	/// Returns a hash code for this <see cref="Money" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="Money" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return HashCode.Combine(Amount, Currency);
	}

	/// <summary>
	/// Compares two <see cref="Money" /> values for equality.
	/// </summary>
	/// <param name="a">The first <see cref="Money" /> to compare.</param>
	/// <param name="b">The second <see cref="Money" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="Money" /> values are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(Money a, Money b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="Money" /> values for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="Money" /> to compare.</param>
	/// <param name="b">The second <see cref="Money" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="Money" /> values are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(Money a, Money b)
	{
		return !Equals(a, b);
	}
	/// <summary>
	/// Returns a value indicating whether a specified <see cref="Money" /> value is less than another specified <see cref="Money" /> value.
	/// Both values must have matching currencies.
	/// </summary>
	/// <param name="a">The first value to compare.</param>
	/// <param name="b">The second value to compare.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="a" /> is less than <paramref name="b" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator <(Money a, Money b)
	{
		Check.Argument(a.Currency == b.Currency, null, "Currency of money values must match.");

		return a.Amount < b.Amount;
	}
	/// <summary>
	/// Returns a value indicating whether a specified <see cref="Money" /> value is less than or equal to another specified <see cref="Money" /> value.
	/// Both values must have matching currencies.
	/// </summary>
	/// <param name="a">The first value to compare.</param>
	/// <param name="b">The second value to compare.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="a" /> is less than or equal to <paramref name="b" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator <=(Money a, Money b)
	{
		Check.Argument(a.Currency == b.Currency, null, "Currency of money values must match.");

		return a.Amount <= b.Amount;
	}
	/// <summary>
	/// Returns a value indicating whether a specified <see cref="Money" /> value is greater than another specified <see cref="Money" /> value.
	/// Both values must have matching currencies.
	/// </summary>
	/// <param name="a">The first value to compare.</param>
	/// <param name="b">The second value to compare.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="a" /> is greater than <paramref name="b" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator >(Money a, Money b)
	{
		Check.Argument(a.Currency == b.Currency, null, "Currency of money values must match.");

		return a.Amount > b.Amount;
	}
	/// <summary>
	/// Returns a value indicating whether a specified <see cref="Money" /> value is greater than or equal to another specified <see cref="Money" /> value.
	/// Both values must have matching currencies.
	/// </summary>
	/// <param name="a">The first value to compare.</param>
	/// <param name="b">The second value to compare.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="a" /> is greater than or equal to <paramref name="b" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator >=(Money a, Money b)
	{
		Check.Argument(a.Currency == b.Currency, null, "Currency of money values must match.");

		return a.Amount >= b.Amount;
	}
	/// <summary>
	/// Adds two specified <see cref="Money" /> values.
	/// Both values must have matching currencies.
	/// </summary>
	/// <param name="a">The first value to add.</param>
	/// <param name="b">The second value to add.</param>
	/// <returns>
	/// The result of adding <paramref name="a" /> and <paramref name="b" />.
	/// </returns>
	public static Money operator +(Money a, Money b)
	{
		Check.Argument(a.Currency == b.Currency, null, "Currency of money values must match.");

		return new(a.Amount + b.Amount, a.Currency);
	}
	/// <summary>
	/// Subtracts two specified <see cref="Money" /> values.
	/// Both values must have matching currencies.
	/// </summary>
	/// <param name="a">The minuend.</param>
	/// <param name="b">The subtrahend.</param>
	/// <returns>
	/// The result of adding <paramref name="b" /> from <paramref name="a" />.
	/// </returns>
	public static Money operator -(Money a, Money b)
	{
		Check.Argument(a.Currency == b.Currency, null, "Currency of money values must match.");

		return new(a.Amount - b.Amount, a.Currency);
	}
	/// <summary>
	/// Multiplies a specified <see cref="Money" /> value and a specified <see cref="double" /> value.
	/// </summary>
	/// <param name="a">The first value to multiply.</param>
	/// <param name="b">The second value to multiply.</param>
	/// <returns>
	/// The result of multiplying <paramref name="a" /> by <paramref name="b" />.
	/// </returns>
	public static Money operator *(Money a, double b)
	{
		return new(a.Amount * (decimal)b, a.Currency);
	}
	/// <summary>
	/// Divides a specified <see cref="Money" /> value and a specified <see cref="double" /> value.
	/// </summary>
	/// <param name="a">The dividend.</param>
	/// <param name="b">The divisor.</param>
	/// <returns>
	/// The result of dividing <paramref name="a" /> by <paramref name="b" />.
	/// </returns>
	public static Money operator /(Money a, double b)
	{
		return new(a.Amount / (decimal)b, a.Currency);
	}
	/// <summary>
	/// Defines an explicit conversion of a <see cref="Money" /> to a <see cref="decimal" />.
	/// </summary>
	/// <param name="value">The <see cref="Money" /> to convert.</param>
	public static explicit operator decimal(Money value)
	{
		return value.Amount;
	}
}