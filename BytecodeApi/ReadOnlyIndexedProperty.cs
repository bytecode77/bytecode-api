namespace BytecodeApi;

/// <summary>
/// Provides an indexed property that exposes a getter.
/// </summary>
/// <typeparam name="TIndex">The type of the index of the property.</typeparam>
/// <typeparam name="TValue">The return type of the property when accessed through the index.</typeparam>
public sealed class ReadOnlyIndexedProperty<TIndex, TValue>
{
	private readonly Func<TIndex, TValue> Getter;
	/// <summary>
	/// Gets this property.
	/// </summary>
	/// <param name="index">The index to access this property.</param>
	/// <returns>
	/// The value returned by the getter of this property.
	/// </returns>
	public TValue this[TIndex index] => Getter(index);

	/// <summary>
	/// Initializes a new instance of the <see cref="ReadOnlyIndexedProperty{TIndex, TValue}" /> class.
	/// </summary>
	/// <param name="getter">The getter delegate that is called when this property is accessed.</param>
	public ReadOnlyIndexedProperty(Func<TIndex, TValue> getter)
	{
		Check.ArgumentNull(getter);

		Getter = getter;
	}
}