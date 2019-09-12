using BytecodeApi.Extensions;
using BytecodeApi.UI.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace BytecodeApi.UI.Controls
{
	/// <summary>
	/// Class that wraps a <see cref="Window" /> object, implementing the <see cref="INotifyPropertyChanged" /> interface. Typically, properties of this class are set as a <see cref="DependencyProperty" /> on a <see cref="DependencyObject" />.
	/// </summary>
	public class ObservableWindow : Window, INotifyPropertyChanged, INotifyPropertyChanging
	{
		private readonly Dictionary<string, object> BackingFields;
		private bool IsLoadedOnce;
		/// <summary>
		/// Occurs when a property value is changing and is typically used by a <see cref="DependencyObject" />.
		/// </summary>
		public event PropertyChangingEventHandler PropertyChanging;
		/// <summary>
		/// Occurs when a property value has changed and is typically used by a <see cref="DependencyObject" />.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// Occurs when the element is laid out, rendered, and ready for interaction. This event is fired only once.
		/// </summary>
		public event RoutedEventHandler LoadedOnce;

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableWindow" /> class.
		/// </summary>
		public ObservableWindow()
		{
			BackingFields = new Dictionary<string, object>();
			Loaded += ObservableWindow_Loaded;
		}
		private void ObservableWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (!IsLoadedOnce)
			{
				IsLoadedOnce = true;
				LoadedOnce?.Invoke(sender, e);
			}
		}

		/// <summary>
		/// Returns the current effective value of a <see cref="DependencyProperty" /> on this instance of a <see cref="Window" />.
		/// The name of the dependency property must be related to the name of the property. Example: "Foo" and "FooProperty".
		/// <para>Example: <see langword="public" /> <see cref="int" /> Foo { <see langword="get" /> => GetValue(() => Foo); <see langword="set" /> => SetValue(() => Foo, <see langword="value" />); }</para>
		/// </summary>
		/// <typeparam name="T">The type of the <see cref="DependencyProperty" />.</typeparam>
		/// <param name="dependencyProperty">The strongly typed lambda expression of the dependency property.</param>
		/// <returns>
		/// The current effective value of a <see cref="DependencyProperty" /> on this instance of a <see cref="Window" />.
		/// </returns>
		public T GetValue<T>(Expression<Func<T>> dependencyProperty)
		{
			Check.ArgumentNull(dependencyProperty, nameof(dependencyProperty));

			return (T)GetValue(this.GetDependencyProperty(dependencyProperty));
		}
		/// <summary>
		/// Sets the local value of a dependency property, specified by its dependency property identifier.
		/// The name of the dependency property must be related to the name of the property. Example: "Foo" and "FooProperty".
		/// <para>Example: <see langword="public" /> <see cref="int" /> Foo { <see langword="get" /> => GetValue(() => Foo); <see langword="set" /> => SetValue(() => Foo, <see langword="value" />); }</para>
		/// </summary>
		/// <typeparam name="T">The type of the <see cref="DependencyProperty" />.</typeparam>
		/// <param name="dependencyProperty">The strongly typed lambda expression of the dependency property.</param>
		/// <param name="value">The new local value.</param>
		public void SetValue<T>(Expression<Func<T>> dependencyProperty, T value)
		{
			Check.ArgumentNull(dependencyProperty, nameof(dependencyProperty));

			SetValue(this.GetDependencyProperty(dependencyProperty), value);
		}
		/// <summary>
		/// Executes the specified <see cref="Action" /> synchronously on the thread the <see cref="Dispatcher" /> is associated with.
		/// </summary>
		/// <param name="callback">A delegate to invoke through the dispatcher.</param>
		public void Dispatch(Action callback)
		{
			Check.ArgumentNull(callback, nameof(callback));

			Dispatcher.Invoke(callback);
		}
		/// <summary>
		/// Executes the specified <see cref="Action" /> synchronously at the specified priority on the thread the <see cref="Dispatcher" /> is associated with.
		/// </summary>
		/// <param name="callback">A delegate to invoke through the dispatcher.</param>
		/// <param name="priority">The priority that determines in what order the specified callback is invoked relative to the other pending operations in the <see cref="Dispatcher" />.</param>
		public void Dispatch(Action callback, DispatcherPriority priority)
		{
			Check.ArgumentNull(callback, nameof(callback));

			Dispatcher.Invoke(callback, priority);
		}
		/// <summary>
		/// Executes the specified <see cref="Action" /> synchronously at the specified priority on the thread the <see cref="Dispatcher" /> is associated with.
		/// </summary>
		/// <param name="callback">A delegate to invoke through the dispatcher.</param>
		/// <param name="priority">The priority that determines in what order the specified callback is invoked relative to the other pending operations in the <see cref="Dispatcher" />.</param>
		/// <param name="cancellationToken">An object that indicates whether to cancel the action.</param>
		public void Dispatch(Action callback, DispatcherPriority priority, CancellationToken cancellationToken)
		{
			Check.ArgumentNull(callback, nameof(callback));

			Dispatcher.Invoke(callback, priority, cancellationToken);
		}
		/// <summary>
		/// Executes the specified <see cref="Action" /> synchronously at the specified priority on the thread the <see cref="Dispatcher" /> is associated with.
		/// </summary>
		/// <param name="callback">A delegate to invoke through the dispatcher.</param>
		/// <param name="priority">The priority that determines in what order the specified callback is invoked relative to the other pending operations in the <see cref="Dispatcher" />.</param>
		/// <param name="cancellationToken">An object that indicates whether to cancel the action.</param>
		/// <param name="timeout">The minimum amount of time to wait for the operation to start.</param>
		public void Dispatch(Action callback, DispatcherPriority priority, CancellationToken cancellationToken, TimeSpan timeout)
		{
			Check.ArgumentNull(callback, nameof(callback));

			Dispatcher.Invoke(callback, priority, cancellationToken, timeout);
		}
		/// <summary>
		/// Executes the specified <see cref="Func{TResult}" /> synchronously on the thread the <see cref="Dispatcher" /> is associated with.
		/// </summary>
		/// <typeparam name="T">The return type of <paramref name="callback" />.</typeparam>
		/// <param name="callback">A delegate to invoke through the dispatcher.</param>
		/// <returns>
		/// The result of <paramref name="callback" />.
		/// </returns>
		public T Dispatch<T>(Func<T> callback)
		{
			Check.ArgumentNull(callback, nameof(callback));

			return Dispatcher.Invoke(callback);
		}
		/// <summary>
		/// Executes the specified <see cref="Func{TResult}" /> synchronously at the specified priority on the thread the <see cref="Dispatcher" /> is associated with.
		/// </summary>
		/// <typeparam name="T">The return type of <paramref name="callback" />.</typeparam>
		/// <param name="callback">A delegate to invoke through the dispatcher.</param>
		/// <param name="priority">The priority that determines in what order the specified callback is invoked relative to the other pending operations in the <see cref="Dispatcher" />.</param>
		/// <returns>
		/// The result of <paramref name="callback" />.
		/// </returns>
		public T Dispatch<T>(Func<T> callback, DispatcherPriority priority)
		{
			Check.ArgumentNull(callback, nameof(callback));

			return Dispatcher.Invoke(callback, priority);
		}
		/// <summary>
		/// Executes the specified <see cref="Func{TResult}" /> synchronously at the specified priority on the thread the <see cref="Dispatcher" /> is associated with.
		/// </summary>
		/// <typeparam name="T">The return type of <paramref name="callback" />.</typeparam>
		/// <param name="callback">A delegate to invoke through the dispatcher.</param>
		/// <param name="priority">The priority that determines in what order the specified callback is invoked relative to the other pending operations in the <see cref="Dispatcher" />.</param>
		/// <param name="cancellationToken">An object that indicates whether to cancel the action.</param>
		/// <returns>
		/// The result of <paramref name="callback" />.
		/// </returns>
		public T Dispatch<T>(Func<T> callback, DispatcherPriority priority, CancellationToken cancellationToken)
		{
			Check.ArgumentNull(callback, nameof(callback));

			return Dispatcher.Invoke(callback, priority, cancellationToken);
		}
		/// <summary>
		/// Executes the specified <see cref="Func{TResult}" /> synchronously at the specified priority on the thread the <see cref="Dispatcher" /> is associated with.
		/// </summary>
		/// <typeparam name="T">The return type of <paramref name="callback" />.</typeparam>
		/// <param name="callback">A delegate to invoke through the dispatcher.</param>
		/// <param name="priority">The priority that determines in what order the specified callback is invoked relative to the other pending operations in the <see cref="Dispatcher" />.</param>
		/// <param name="cancellationToken">An object that indicates whether to cancel the action.</param>
		/// <param name="timeout">The minimum amount of time to wait for the operation to start.</param>
		/// <returns>
		/// The result of <paramref name="callback" />.
		/// </returns>
		public T Dispatch<T>(Func<T> callback, DispatcherPriority priority, CancellationToken cancellationToken, TimeSpan timeout)
		{
			Check.ArgumentNull(callback, nameof(callback));

			return Dispatcher.Invoke(callback, priority, cancellationToken, timeout);
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
			return Get(property, null);
		}
		/// <summary>
		/// Method that can be used by the <see langword="get" /> accessor of a property. Backing fields are managed automatically.
		/// <para>Example: <see langword="public" /> <see cref="int" /> Foo { <see langword="get" /> => Get(() => Foo, 1); <see langword="set" /> => Set(() => Foo, <see langword="value" />); }</para>
		/// </summary>
		/// <typeparam name="T">The type of the property.</typeparam>
		/// <param name="property">The strongly typed lambda expression of the property.</param>
		/// <param name="defaultValue">A default value. This parameter can be used as a property initializer.</param>
		/// <returns>
		/// The value of the property backing field. The default value is <see langword="default" />(<typeparamref name="T" />).
		/// </returns>
		protected T Get<T>(Expression<Func<T>> property, T defaultValue)
		{
			return Get(property, () => defaultValue);
		}
		/// <summary>
		/// Method that can be used by the <see langword="get" /> accessor of a property. Backing fields are managed automatically.
		/// <para>Example: <see langword="public" /> <see cref="int" /> Foo { <see langword="get" /> => Get(() => Foo, () => 1); <see langword="set" /> => Set(() => Foo, <see langword="value" />); }</para>
		/// </summary>
		/// <typeparam name="T">The type of the property.</typeparam>
		/// <param name="property">The strongly typed lambda expression of the property.</param>
		/// <param name="defaultValue">A <see cref="Func{TResult}" /> that retrieves a default value. This delegate can be used as a property initializer. <paramref name="defaultValue" /> is invoked, if the property is retrieved for the first time and is not set.</param>
		/// <returns>
		/// The value of the property backing field. The default value is <see langword="default" />(<typeparamref name="T" />).
		/// </returns>
		protected T Get<T>(Expression<Func<T>> property, Func<T> defaultValue)
		{
			Check.ArgumentNull(property, nameof(property));

			string propertyName = property.GetMemberName();
			if (!BackingFields.ContainsKey(propertyName)) BackingFields[propertyName] = defaultValue == null ? default : defaultValue();

			return CSharp.CastOrDefault<T>(BackingFields[propertyName]);
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
		/// <param name="propertyName">A <see cref="string" /> specifying the name of the property. If <see langword="null" /> is provided, the <see cref="CallerMemberNameAttribute" /> is used to automatically get the property name.</param>
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
		/// <param name="propertyName">A <see cref="string" /> specifying the name of the property. If <see langword="null" /> is provided, the <see cref="CallerMemberNameAttribute" /> is used to automatically get the property name.</param>
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
		protected virtual void OnPropertyChanging(PropertyChangingEventArgs e)
		{
			PropertyChanging?.Invoke(this, e);
		}
		/// <summary>
		/// Raises the <see cref="PropertyChanged" /> event.
		/// </summary>
		/// <param name="e">The event data for the <see cref="PropertyChangedEventArgs" /> event.</param>
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			PropertyChanged?.Invoke(this, e);
		}
	}
}