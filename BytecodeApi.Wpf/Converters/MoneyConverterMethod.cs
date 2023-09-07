using BytecodeApi.Data;

namespace BytecodeApi.Wpf.Converters;

/// <summary>
/// Specifies the method that is used to convert <see cref="Money" />? values.
/// </summary>
public enum MoneyConverterMethod
{
	/// <summary>
	/// Returns the amount and then the currency, separated by a space.
	/// </summary>
	AmountCurrency,
	/// <summary>
	/// Returns the amount and then the currency symbol, separated by a space. If the currency has no symbol, the currency code is used.
	/// </summary>
	AmountCurrencySymbol,
	/// <summary>
	/// Returns the currency and then the amount, separated by a space.
	/// </summary>
	CurrencyAmount,
	/// <summary>
	/// Returns the currency symbol and then the amount, separated by a space. If the currency has no symbol, the currency code is used.
	/// </summary>
	CurrencySymbolAmount,
	/// <summary>
	/// Returns the amount.
	/// </summary>
	Amount
}