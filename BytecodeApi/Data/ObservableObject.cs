using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BytecodeApi.Data;

/// <summary>
/// Class that wraps an object, implementing the <see cref="INotifyPropertyChanged" /> interface.
/// </summary>
public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
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
	/// Initializes a new instance of the <see cref="ObservableObject" /> class.
	/// </summary>
	protected ObservableObject()
	{
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

		OnPropertyChanging(new(propertyName));
	}
	/// <summary>
	/// Raises the <see cref="PropertyChanged" /> event on a property specified by a name.
	/// </summary>
	/// <param name="propertyName">A <see cref="string" /> specifying the name of the property. If <see langword="null" /> is provided, the <see cref="CallerMemberNameAttribute" /> is used to automatically get the property name.</param>
	protected void RaisePropertyChanged([CallerMemberName] string propertyName = null!)
	{
		Check.ArgumentNull(propertyName);
		Check.ArgumentEx.StringNotEmpty(propertyName);

		OnPropertyChanged(new(propertyName));
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