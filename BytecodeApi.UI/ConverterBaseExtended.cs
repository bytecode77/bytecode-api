using System;
using System.Globalization;
using System.Windows.Data;

namespace BytecodeApi.UI
{
	/// <summary>
	/// Represents the base class that wraps the <see cref="IValueConverter" /> and provides simplified wrapper methods for Convert and ConvertBack.
	/// </summary>
	/// <typeparam name="TValue">The type of the value to convert.</typeparam>
	/// <typeparam name="TResult">The type of the conversion result.</typeparam>
	/// <typeparam name="TBackValue">The type of the value when converted back. This is usually equal to <typeparamref name="TValue" />.</typeparam>
	/// <typeparam name="TBackResult">The type of the conversion result when converted back. This is usually equal to <typeparamref name="TResult" />.</typeparam>
	public abstract class ConverterBaseExtended<TValue, TResult, TBackValue, TBackResult> : ConverterBase<TValue, TResult>, IValueConverter
	{
		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <returns>
		/// A converted value. If the method returns <see langword="null" />, the valid null value is used.
		/// </returns>
		public abstract TBackResult ConvertBack(TBackValue value);

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertBack(CSharp.CastOrDefault<TBackValue>(value));
		}
	}

	/// <summary>
	/// Represents the base class that wraps the <see cref="IValueConverter" /> and provides simplified wrapper methods for Convert and ConvertBack.
	/// </summary>
	/// <typeparam name="TValue">The type of the value to convert.</typeparam>
	/// <typeparam name="TParameter">The type of the parameter used for conversion.</typeparam>
	/// <typeparam name="TResult">The type of the conversion result.</typeparam>
	/// <typeparam name="TBackValue">The type of the value when converted back. This is usually equal to <typeparamref name="TValue" />.</typeparam>
	/// <typeparam name="TBackResult">The type of the conversion result when converted back. This is usually equal to <typeparamref name="TResult" />.</typeparam>
	public abstract class ConverterBaseExtended<TValue, TParameter, TResult, TBackValue, TBackResult> : ConverterBase<TValue, TParameter, TResult>, IValueConverter
	{
		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <returns>
		/// A converted value. If the method returns <see langword="null" />, the valid null value is used.
		/// </returns>
		public abstract TBackResult ConvertBack(TBackValue value, TParameter parameter);

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertBack(CSharp.CastOrDefault<TBackValue>(value), CSharp.CastOrDefault<TParameter>(parameter));
		}
	}
}