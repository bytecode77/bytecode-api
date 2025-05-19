namespace BytecodeApi.Data;

/// <summary>
/// Provides a currency converter to convert <see cref="Money" /> values between different currencies.
/// </summary>
public sealed class CurrencyConverter
{
	/// <summary>
	/// A list of conversion rules.
	/// </summary>
	public List<CurrencyConversion> Conversions { get; private init; }
	/// <summary>
	/// Specifies whether conversion rules apply only in the direction that has explicitly been specified.
	/// The default value is <see langword="false" />.
	/// </summary>
	public bool OneWay { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="CurrencyConverter" /> class.
	/// </summary>
	public CurrencyConverter()
	{
		Conversions = [];
	}

	/// <summary>
	/// Adds a new conversion rule to this <see cref="CurrencyConverter" />.
	/// </summary>
	/// <param name="from">The currency to convert from.</param>
	/// <param name="to">The currency to convert to.</param>
	/// <param name="exchangeRate">The exchange rate to convert between currencies.</param>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public CurrencyConverter HasConversion(Currency from, Currency to, double exchangeRate)
	{
		Conversions.Add(new(from, to, exchangeRate));
		return this;
	}
	/// <summary>
	/// Specifies that conversion rules apply only in the direction that has explicitly been specified.
	/// </summary>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public CurrencyConverter HasOneWay()
	{
		OneWay = true;
		return this;
	}
	/// <summary>
	/// Specifies that conversion rules apply in both directions.
	/// </summary>
	/// <returns>
	/// A reference to this instance after the operation has completed.
	/// </returns>
	public CurrencyConverter HasTwoWay()
	{
		OneWay = false;
		return this;
	}

	/// <summary>
	/// Converts <paramref name="money" /> to a new <see cref="Money" /> value with the specified <paramref name="currency" /> using the conversion rules of this instance of <see cref="CurrencyConverter" />.
	/// </summary>
	/// <param name="money">The <see cref="Money" /> value to convert.</param>
	/// <param name="currency">The <see cref="Currency" /> of the converted <see cref="Money" /> value.</param>
	/// <returns>
	/// A new <see cref="Money" /> value with the specified <paramref name="currency" />.
	/// </returns>
	public Money Convert(Money money, Currency currency)
	{
		Check.Argument(Conversions.Any(), nameof(Conversions), "At least one conversion rule is required.");

		if (money.Currency == currency)
		{
			return money;
		}
		else if (Conversions.FirstOrDefault(c => c.From == money.Currency && c.To == currency) is CurrencyConversion conversion1)
		{
			return money * conversion1.ExchangeRate;
		}
		else if (!OneWay && Conversions.FirstOrDefault(c => c.From == currency && c.To == money.Currency) is CurrencyConversion conversion2)
		{
			return money / conversion2.ExchangeRate;
		}
		else
		{
			throw Throw.InvalidOperation($"This {nameof(CurrencyConverter)} does not contain a conversion for the specified money value and currency.");
		}
	}
}