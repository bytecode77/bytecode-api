namespace BytecodeApi;

/// <summary>
/// Provides an indexed property that exposes a getter and a setter.
/// </summary>
/// <typeparam name="TIndex">The type of the index of the property.</typeparam>
/// <typeparam name="TValue">The return type of the property when accessed through the index.</typeparam>
public sealed class IndexedProperty<TIndex, TValue>
{
	private readonly Func<TIndex, TValue> Getter;
	private readonly Action<TIndex, TValue> Setter;
	/// <summary>
	/// Gets or sets this property.
	/// </summary>
	/// <param name="index">The index to access this property.</param>
	/// <returns>
	/// The value returned by the getter of this property.
	/// </returns>
	public TValue this[TIndex index]
	{
		get => Getter(index);
		set => Setter(index, value);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="IndexedProperty{TIndex, TValue}" /> class.
	/// </summary>
	/// <param name="getter">The getter delegate that is called when this property is accessed.</param>
	/// <param name="setter">The setter delegate that is called when this property is set.</param>
	public IndexedProperty(Func<TIndex, TValue> getter, Action<TIndex, TValue> setter)
	{
		Check.ArgumentNull(getter);
		Check.ArgumentNull(setter);

		Getter = getter;
		Setter = setter;
	}
}