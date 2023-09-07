using System.Globalization;
using System.Windows.Data;

namespace BytecodeApi.Wpf;

/// <summary>
/// Represents the base class that wraps the <see cref="IValueConverter" /> and provides simplified wrapper methods for Convert and ConvertBack. This is an abstract class.
/// </summary>
/// <typeparam name="TValue">The type of the value to convert.</typeparam>
public abstract class TwoWayConverterBase<TValue> : ConverterBase<TValue>, IValueConverter
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TwoWayConverterBase{TValue}" /> class.
	/// </summary>
	protected TwoWayConverterBase()
	{
	}

	/// <summary>
	/// Converts a value back.
	/// </summary>
	/// <param name="value">The value that is produced by the binding target.</param>
	/// <returns>
	/// A converted value. If the method returns <see langword="null" />, the valid null value is used.
	/// </returns>
	public abstract object? ConvertBack(object? value);
	object? IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		object? result = ConvertBack(value);
		return Then == null ? result : Then.Convert(result, targetType, parameter, culture);
	}
}

/// <summary>
/// Represents the base class that wraps the <see cref="IValueConverter" /> and provides simplified wrapper methods for Convert and ConvertBack. This is an abstract class.
/// </summary>
/// <typeparam name="TValue">The type of the value to convert.</typeparam>
/// <typeparam name="TParameter">The type of the parameter used for conversion.</typeparam>
public abstract class TwoWayConverterBase<TValue, TParameter> : ConverterBase<TValue, TParameter>, IValueConverter
{
	/// <summary>
	/// Initializes a new instance of the <see cref="TwoWayConverterBase{TValue, TParameter}" /> class.
	/// </summary>
	protected TwoWayConverterBase()
	{
	}

	/// <summary>
	/// Converts a value back.
	/// </summary>
	/// <param name="value">The value that is produced by the binding target.</param>
	/// <param name="parameter">The converter parameter to use.</param>
	/// <returns>
	/// A converted value. If the method returns <see langword="null" />, the valid null value is used.
	/// </returns>
	public abstract object? ConvertBack(object? value, TParameter? parameter);
	object? IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		object? result = ConvertBack(value, CSharp.CastOrDefault<TParameter>(parameter));
		return Then == null ? result : Then.Convert(result, targetType, parameter, culture);
	}
}