using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Threading;

namespace BytecodeApi.Wpf.Controls;

/// <summary>
/// Class that wraps a <see cref="Window" /> object, implementing the <see cref="INotifyPropertyChanged" /> interface.
/// </summary>
public class ObservableWindow : Window, INotifyPropertyChanged, INotifyPropertyChanging
{
	/// <summary>
	/// Occurs when a property value is changing.
	/// </summary>
	public event PropertyChangingEventHandler? PropertyChanging;
	/// <summary>
	/// Occurs when a property value has changed.
	/// </summary>
	public event PropertyChangedEventHandler? PropertyChanged;

	/// <summary>
	/// Initializes a new instance of the <see cref="ObservableWindow" /> class.
	/// </summary>
	public ObservableWindow()
	{
	}

	/// <summary>
	/// Executes the specified <see cref="Action" /> synchronously on the thread the <see cref="Dispatcher" /> is associated with.
	/// </summary>
	/// <param name="callback">A delegate to invoke through the dispatcher.</param>
	public void Dispatch(Action callback)
	{
		Check.ArgumentNull(callback);

		Dispatcher.Invoke(callback);
	}
	/// <summary>
	/// Executes the specified <see cref="Action" /> synchronously at the specified priority on the thread the <see cref="Dispatcher" /> is associated with.
	/// </summary>
	/// <param name="callback">A delegate to invoke through the dispatcher.</param>
	/// <param name="priority">The priority that determines in what order the specified callback is invoked relative to the other pending operations in the <see cref="Dispatcher" />.</param>
	public void Dispatch(Action callback, DispatcherPriority priority)
	{
		Check.ArgumentNull(callback);

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
		Check.ArgumentNull(callback);

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
		Check.ArgumentNull(callback);

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
		Check.ArgumentNull(callback);

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
		Check.ArgumentNull(callback);

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
		Check.ArgumentNull(callback);

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
		Check.ArgumentNull(callback);

		return Dispatcher.Invoke(callback, priority, cancellationToken, timeout);
	}

	/// <summary>
	/// Method that can be used by the <see langword="set" /> accessor of a property. This method raises the <see cref="PropertyChanging" /> event and the <see cref="PropertyChanged" /> event.
	/// </summary>
	/// <typeparam name="T">The type of the property.</typeparam>
	/// <param name="field">A reference to the backing field of the property.</param>
	/// <param name="value">The new value for the property.</param>
	/// <param name="propertyName">A <see cref="string" /> specifying the name of the property. If <see langword="null" /> is provided, the <see cref="CallerMemberNameAttribute" /> is used to automatically get the property name.</param>
	/// <returns>
	/// <see langword="true" />, if the value changed; <see langword="false" />, if the new value is equal to the old value.
	/// </returns>
	protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null!)
	{
		Check.ArgumentNull(propertyName);

		if (!EqualityComparer<T>.Default.Equals(field, value))
		{
			RaisePropertyChanging(propertyName);
			field = value;
			RaisePropertyChanged(propertyName);
			return true;
		}
		else
		{
			return false;
		}
	}
	/// <summary>
	/// Raises the <see cref="PropertyChanging" /> event on a property specified by a name.
	/// </summary>
	/// <param name="propertyName">A <see cref="string" /> specifying the name of the property. If <see langword="null" /> is provided, the <see cref="CallerMemberNameAttribute" /> is used to automatically get the property name.</param>
	protected void RaisePropertyChanging([CallerMemberName] string propertyName = null!)
	{
		Check.ArgumentNull(propertyName);
		Check.ArgumentEx.StringNotEmpty(propertyName);

		OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
	}
	/// <summary>
	/// Raises the <see cref="PropertyChanged" /> event on a property specified by a name.
	/// </summary>
	/// <param name="propertyName">A <see cref="string" /> specifying the name of the property. If <see langword="null" /> is provided, the <see cref="CallerMemberNameAttribute" /> is used to automatically get the property name.</param>
	protected void RaisePropertyChanged([CallerMemberName] string propertyName = null!)
	{
		Check.ArgumentNull(propertyName);
		Check.ArgumentEx.StringNotEmpty(propertyName);

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