namespace BytecodeApi.Data;

/// <summary>
/// Specifies a currency conversion rate.
/// </summary>
public sealed class CurrencyConversion
{
	/// <summary>
	/// Specifies the currency to convert from.
	/// </summary>
	public Currency From { get; set; }
	/// <summary>
	/// Specifies the currency to convert to.
	/// </summary>
	public Currency To { get; set; }
	/// <summary>
	/// Specifies the exchange rate to convert between currencies.
	/// </summary>
	public double ExchangeRate { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="CurrencyConversion" /> class.
	/// </summary>
	/// <param name="from">The currency to convert from.</param>
	/// <param name="to">The currency to convert to.</param>
	/// <param name="exchangeRate">The exchange rate to convert between currencies.</param>
	public CurrencyConversion(Currency from, Currency to, double exchangeRate)
	{
		From = from;
		To = to;
		ExchangeRate = exchangeRate;
	}
}