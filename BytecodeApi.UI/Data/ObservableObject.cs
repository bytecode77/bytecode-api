using BytecodeApi.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BytecodeApi.UI.Data
{
	/// <summary>
	/// Class that wraps an object, implementing the <see cref="INotifyPropertyChanged" /> interface. Typically, this class is set as a <see cref="DependencyProperty" /> on a <see cref="DependencyObject" />.
	/// </summary>
	public class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
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
		/// Initializes a new instance of the <see cref="ObservableObject" /> class.
		/// </summary>
		protected ObservableObject()
		{
			BackingFields = new Dictionary<string, object>();
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