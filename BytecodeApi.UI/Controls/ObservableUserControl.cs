using BytecodeApi.Extensions;
using BytecodeApi.UI.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace BytecodeApi.UI.Controls
{
	/// <summary>
	/// Class that wraps a <see cref="UserControl" /> object, implementing the <see cref="INotifyPropertyChanged" /> interface. Typically, properties of this class are set as a <see cref="DependencyProperty" /> on a <see cref="DependencyObject" />.
	/// </summary>
	public class ObservableUserControl : UserControl, INotifyPropertyChanged, INotifyPropertyChanging
	{
		private readonly Dictionary<string, object> BackingFields;
		/// <summary>
		/// Occurs when a property value is changing and is typically used by a <see cref="DependencyObject" />.
		/// </summary>
		public event PropertyChangingEventHandler PropertyChanging;
		/// <summary>
		/// Occurs when a property value has changed and is typically used by a <see cref="DependencyObject" />.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableUserControl" /> class.
		/// </summary>
		public ObservableUserControl()
		{
			BackingFields = new Dictionary<string, object>();
		}

		/// <summary>
		/// Returns the current effective value of a <see cref="DependencyProperty" /> on this instance of a <see cref="UserControl" />.
		/// </summary>
		/// <typeparam name="T">The return type of the <see cref="DependencyProperty" />.</typeparam>
		/// <param name="dependencyProperty">The <see cref="DependencyProperty" /> identifier of the property to retrieve the value for.</param>
		/// <returns>
		/// The effective value of <paramref name="dependencyProperty" /> on this instance of a <see cref="UserControl" />.
		/// </returns>
		public T GetValue<T>(DependencyProperty dependencyProperty)
		{
			Check.ArgumentNull(dependencyProperty, nameof(dependencyProperty));

			return DependencyObjectExtensions.GetValue<T>(this, dependencyProperty);
		}
		/// <summary>
		/// Executes the specified <see cref="Action" /> synchronously on the thread the <see cref="Dispatcher" /> is associated with.
		/// </summary>
		/// <param name="action">The <see cref="Action" /> to be invoked.</param>
		public void Dispatch(Action action)
		{
			Check.ArgumentNull(action, nameof(action));

			Dispatcher.Invoke(action);
		}
		/// <summary>
		/// Executes the specified <see cref="Func{TResult}" /> synchronously on the thread the <see cref="Dispatcher" /> is associated with.
		/// </summary>
		/// <typeparam name="T">The return type of <paramref name="func" />.</typeparam>
		/// <param name="func">The <see cref="Func{TResult}" /> to be invoked.</param>
		/// <returns>
		/// The result of <paramref name="func" />.
		/// </returns>
		public T Dispatch<T>(Func<T> func)
		{
			Check.ArgumentNull(func, nameof(func));

			return Dispatcher.Invoke(func);
		}
		/// <summary>
		/// Method that can be used by the <see langword="get" /> accessor of a property. Backing fields are managed automatically.
		/// <para>Example: <see langword="public" /> <see cref="int" /> Foo { <see langword="get" /> => Get(() => Foo); <see langword="set" /> => Set(() => Foo, <see langword="value" />); }</para>
		/// </summary>
		/// <typeparam name="T">The type of the property.</typeparam>
		/// <param name="property">The strongly typed lambda expression of the property.</param>
		/// <returns>
		/// The value of the property backing field. The default value is <see langword="default" />(<typeparamref name="T" />).
		/// </returns>
		protected T Get<T>(Expression<Func<T>> property)
		{
			Check.ArgumentNull(property, nameof(property));

			return CSharp.CastOrDefault<T>(BackingFields.ValueOrDefault(property.GetMemberName()));
		}
		/// <summary>
		/// Method that can be used by the <see langword="set" /> accessor of a property. Backing fields are managed automatically. This method raises the <see cref="PropertyChanging" /> event and the <see cref="PropertyChanged" /> event.
		/// <para>Example: <see langword="public" /> <see cref="int" /> Foo { <see langword="get" /> => Get(() => Foo); <see langword="set" /> => Set(() => Foo, <see langword="value" />); }</para>
		/// </summary>
		/// <typeparam name="T">The type of the property.</typeparam>
		/// <param name="property">The strongly typed lambda expression of the property.</param>
		/// <param name="value">The value.</param>
		protected void Set<T>(Expression<Func<T>> property, T value)
		{
			Check.ArgumentNull(property, nameof(property));

			string propertyName = property.GetMemberName();

			RaisePropertyChanging(propertyName);
			BackingFields[propertyName] = value;
			RaisePropertyChanged(propertyName);
		}
		/// <summary>
		/// Raises the <see cref="PropertyChanging" /> event on any custom property. <see cref="Get{T}(Expression{Func{T}})" /> and <see cref="Set{T}(Expression{Func{T}}, T)" /> do not necessarily need to be used in conjunction with this method. Likewise, this method is not required when using <see cref="Get{T}(Expression{Func{T}})" /> and <see cref="Set{T}(Expression{Func{T}}, T)" />.
		/// </summary>
		/// <typeparam name="T">The type of the property.</typeparam>
		/// <param name="property">The strongly typed lambda expression of the property.</param>
		protected void RaisePropertyChanging<T>(Expression<Func<T>> property)
		{
			Check.ArgumentNull(property, nameof(property));

			OnPropertyChanging(new PropertyChangingEventArgs(property.GetMemberName()));
		}
		/// <summary>
		/// Raises the <see cref="PropertyChanging" /> event on a property specified by a name.
		/// </summary>
		/// <param name="propertyName">A <see cref="string" /> specifying the name of the property. If <see langword="null" /> is provided, the <see cref="CallerMemberNameAttribute" /> is used to automatically get the property name./></param>
		protected void RaisePropertyChanging([CallerMemberName] string propertyName = null)
		{
			Check.ArgumentNull(propertyName, nameof(propertyName));
			Check.ArgumentEx.StringNotEmpty(propertyName, nameof(propertyName));

			OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
		}
		/// <summary>
		/// Raises the <see cref="PropertyChanged" /> event on any custom property. <see cref="Get{T}(Expression{Func{T}})" /> and <see cref="Set{T}(Expression{Func{T}}, T)" /> do not necessarily need to be used in conjunction with this method. Likewise, this method is not required when using <see cref="Get{T}(Expression{Func{T}})" /> and <see cref="Set{T}(Expression{Func{T}}, T)" />.
		/// </summary>
		/// <typeparam name="T">The type of the property.</typeparam>
		/// <param name="property">The strongly typed lambda expression of the property.</param>
		protected void RaisePropertyChanged<T>(Expression<Func<T>> property)
		{
			Check.ArgumentNull(property, nameof(property));

			OnPropertyChanged(new PropertyChangedEventArgs(property.GetMemberName()));
		}
		/// <summary>
		/// Raises the <see cref="PropertyChanged" /> event on a property specified by a name.
		/// </summary>
		/// <param name="propertyName">A <see cref="string" /> specifying the name of the property. If <see langword="null" /> is provided, the <see cref="CallerMemberNameAttribute" /> is used to automatically get the property name./></param>
		protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			Check.ArgumentNull(propertyName, nameof(propertyName));
			Check.ArgumentEx.StringNotEmpty(propertyName, nameof(propertyName));

			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		/// <summary>
		/// Raises the <see cref="PropertyChanging" /> event.
		/// </summary>
		/// <param name="e">The event data for the <see cref="PropertyChangingEventArgs" /> event.</param>
		protected void OnPropertyChanging(PropertyChangingEventArgs e)
		{
			PropertyChanging?.Invoke(this, e);
		}
		/// <summary>
		/// Raises the <see cref="PropertyChanged" /> event.
		/// </summary>
		/// <param name="e">The event data for the <see cref="PropertyChangedEventArgs" /> event.</param>
		protected void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, e);
		}
	}
}