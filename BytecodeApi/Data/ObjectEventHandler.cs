namespace BytecodeApi.Data
{
	/// <summary>
	/// Represents the method that will handle an event with data of a specified type.
	/// </summary>
	/// <typeparam name="T">The type of the event data associated with the event.</typeparam>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">An <see cref="ObjectEventArgs{T}" /> object that contains the event data.</param>
	public delegate void ObjectEventHandler<T>(object sender, ObjectEventArgs<T> e);
}