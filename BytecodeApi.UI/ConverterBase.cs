using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BytecodeApi.UI
{
	/// <summary>
	/// Represents the base class that wraps the <see cref="IValueConverter" /> and provides a simplified wrapper method for Convert. ConvertBack always returns <see cref="DependencyProperty.UnsetValue" />.
	/// </summary>
	/// <typeparam name="TValue">The type of the value to convert.</typeparam>
	/// <typeparam name="TResult">The type of the conversion result.</typeparam>
	public abstract class ConverterBase<TValue, TResult> : MarkupExtension, IValueConverter
	{
		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <returns>
		/// A converted value. If the method returns <see langword="null" />, the valid null value is used.
		/// </returns>
		public abstract TResult Convert(TValue value);

		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Check.InvalidCast(value == null || value is TValue, nameof(value));

			return Convert(CSharp.CastOrDefault<TValue>(value));
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}

	/// <summary>
	/// Represents the base class that wraps the <see cref="IValueConverter" /> and provides a simplified wrapper method for Convert. ConvertBack always returns <see cref="DependencyProperty.UnsetValue" />.
	/// </summary>
	/// <typeparam name="TValue">The type of the value to convert.</typeparam>
	/// <typeparam name="TParameter">The type of the parameter used for conversion.</typeparam>
	/// <typeparam name="TResult">The type of the conversion result.</typeparam>
	public abstract class ConverterBase<TValue, TParameter, TResult> : MarkupExtension, IValueConverter
	{
		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <returns>
		/// A converted value. If the method returns <see langword="null" />, the valid null value is used.
		/// </returns>
		public abstract TResult Convert(TValue value, TParameter parameter);

		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			Check.InvalidCast(value == null || value is TValue, nameof(value));
			Check.InvalidCast(parameter == null || parameter is TParameter, nameof(parameter));

			return Convert(CSharp.CastOrDefault<TValue>(value), CSharp.CastOrDefault<TParameter>(parameter));
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}

		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}