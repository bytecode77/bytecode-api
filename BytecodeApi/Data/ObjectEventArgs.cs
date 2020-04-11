using System;

namespace BytecodeApi.Data
{
	/// <summary>
	/// Provides data for the <see cref="ObjectEventHandler{T}" /> event handler.
	/// </summary>
	/// <typeparam name="T">The type of the event data.</typeparam>
	public sealed class ObjectEventArgs<T> : EventArgs
	{
		/// <summary>
		/// Gets or sets the event data associated with the <see cref="ObjectEventHandler{T}" /> event.
		/// </summary>
		public T Value { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectEventArgs{T}" /> class.
		/// </summary>
		public ObjectEventArgs()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectEventArgs{T}" /> class with the specified value.
		/// </summary>
		/// <param name="value">The value to initialize this <see cref="ObjectEventArgs{T}" /> with.</param>
		public ObjectEventArgs(T value) : this()
		{
			Value = value;
		}
	}
}