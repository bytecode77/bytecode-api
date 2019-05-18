using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace BytecodeApi.UI
{
	/// <summary>
	/// Represents the base class that wraps the <see cref="IValueConverter" /> and provides a simplified wrapper method for Convert. ConvertBack always returns <see cref="DependencyProperty.UnsetValue" />. This is an abstract class.
	/// </summary>
	/// <typeparam name="TValue">The type of the value to convert.</typeparam>
	/// <typeparam name="TResult">The type of the conversion result.</typeparam>
	public abstract class ConverterBase<TValue, TResult> : MarkupExtension, IValueConverter
	{
		/// <summary>
		/// Specifies a converter that is used to convert the result of this converter. This can be used to apply multiple conversions on a value.
		/// </summary>
		public IValueConverter Then { get; set; }
		/// <summary>
		/// If <see cref="Then" /> is not <see langword="null" />, specifies a converter parameter that is used to convert the result of this converter.
		/// </summary>
		public object ThenParameter { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ConverterBase{TValue, TResult}" /> class.
		/// </summary>
		protected ConverterBase()
		{
		}

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
			TResult result = Convert(CSharp.CastOrDefault<TValue>(value));
			return Then == null ? result : Then.Convert(result, targetType, ThenParameter, culture);
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}

		/// <summary>
		/// Returns this instance of <see cref="IValueConverter" />.
		/// </summary>
		/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
		/// <returns>
		/// This instance of <see cref="IValueConverter" />.
		/// </returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}

	/// <summary>
	/// Represents the base class that wraps the <see cref="IValueConverter" /> and provides a simplified wrapper method for Convert. ConvertBack always returns <see cref="DependencyProperty.UnsetValue" />. This is an abstract class.
	/// </summary>
	/// <typeparam name="TValue">The type of the value to convert.</typeparam>
	/// <typeparam name="TParameter">The type of the parameter used for conversion.</typeparam>
	/// <typeparam name="TResult">The type of the conversion result.</typeparam>
	public abstract class ConverterBase<TValue, TParameter, TResult> : MarkupExtension, IValueConverter
	{
		/// <summary>
		/// Specifies a converter that is used to convert the result of this converter. This can be used to apply multiple conversions on a value.
		/// </summary>
		public IValueConverter Then { get; set; }
		/// <summary>
		/// If <see cref="Then" /> is not <see langword="null" />, specifies a converter parameter that is used to convert the result of this converter.
		/// </summary>
		public object ThenParameter { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ConverterBase{TValue, TParameter, TResult}" /> class.
		/// </summary>
		protected ConverterBase()
		{
		}

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
			TResult result = Convert(CSharp.CastOrDefault<TValue>(value), CSharp.CastOrDefault<TParameter>(parameter));
			return Then == null ? result : Then.Convert(result, targetType, ThenParameter, culture);
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return DependencyProperty.UnsetValue;
		}

		/// <summary>
		/// Returns this instance of <see cref="IValueConverter" />.
		/// </summary>
		/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
		/// <returns>
		/// This instance of <see cref="IValueConverter" />.
		/// </returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return this;
		}
	}
}