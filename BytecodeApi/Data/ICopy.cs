namespace BytecodeApi.Data;

/// <summary>
/// Supports the creation of a new instance, where all properties are copied.
/// </summary>
/// <typeparam name="T">The type of the object to copy.</typeparam>
public interface ICopy<T>
{
	/// <summary>
	/// Creates a new instance and copies all properties.
	/// </summary>
	/// <returns>
	/// A new instance with all properties copied from this instance.
	/// </returns>
	public T Copy();
}