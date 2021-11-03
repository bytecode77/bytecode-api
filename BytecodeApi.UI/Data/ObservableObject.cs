using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace BytecodeApi.UI.Data
{
	/// <summary>
	/// Class that wraps an object, implementing the <see cref="INotifyPropertyChanged" /> interface. Typically, this class is set as a <see cref="DependencyProperty" /> on a <see cref="DependencyObject" />.
	/// </summary>
	public abstract class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
	{
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
		protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			Check.ArgumentNull(propertyName, nameof(propertyName));

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
		protected void RaisePropertyChanging([CallerMemberName] string propertyName = null)
		{
			Check.ArgumentNull(propertyName, nameof(propertyName));
			Check.ArgumentEx.StringNotEmpty(propertyName, nameof(propertyName));

			OnPropertyChanging(new PropertyChangingEventArgs(propertyName));
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