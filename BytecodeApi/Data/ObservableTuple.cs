namespace BytecodeApi.Data;

/// <summary>
/// Provides static methods for creating observable tuple objects.
/// </summary>
public static class ObservableTuple
{
	/// <summary>
	/// Creates a new 1-tuple with an observable value.
	/// </summary>
	/// <typeparam name="T1">The type of the tuple's only component.</typeparam>
	/// <param name="item1">The value of the tuple's only component.</param>
	/// <returns>
	/// A new <see cref="ObservableTuple{T1}" />.
	/// </returns>
	public static ObservableTuple<T1> Create<T1>(T1 item1)
	{
		return new(item1);
	}
	/// <summary>
	/// Creates a new 2-tuple with observable values.
	/// </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <returns>
	/// A new <see cref="ObservableTuple{T1, T2}" />.
	/// </returns>
	public static ObservableTuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
	{
		return new(item1, item2);
	}
	/// <summary>
	/// Creates a new 3-tuple with observable values.
	/// </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	/// <returns>
	/// A new <see cref="ObservableTuple{T1, T2, T3}" />.
	/// </returns>
	public static ObservableTuple<T1, T2, T3> Create<T1, T2, T3>(T1 item1, T2 item2, T3 item3)
	{
		return new(item1, item2, item3);
	}
	/// <summary>
	/// Creates a new 4-tuple with observable values.
	/// </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	/// <param name="item4">The value of the tuple's fourth component.</param>
	/// <returns>
	/// A new <see cref="ObservableTuple{T1, T2, T3, T4}" />.
	/// </returns>
	public static ObservableTuple<T1, T2, T3, T4> Create<T1, T2, T3, T4>(T1 item1, T2 item2, T3 item3, T4 item4)
	{
		return new(item1, item2, item3, item4);
	}
	/// <summary>
	/// Creates a new 5-tuple with observable values.
	/// </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	/// <param name="item4">The value of the tuple's fourth component.</param>
	/// <param name="item5">The value of the tuple's fifth component.</param>
	/// <returns>
	/// A new <see cref="ObservableTuple{T1, T2, T3, T4, T5}" />.
	/// </returns>
	public static ObservableTuple<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
	{
		return new(item1, item2, item3, item4, item5);
	}
	/// <summary>
	/// Creates a new 6-tuple with observable values.
	/// </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	/// <param name="item4">The value of the tuple's fourth component.</param>
	/// <param name="item5">The value of the tuple's fifth component.</param>
	/// <param name="item6">The value of the tuple's sixth component.</param>
	/// <returns>
	/// A new <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" />.
	/// </returns>
	public static ObservableTuple<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
	{
		return new(item1, item2, item3, item4, item5, item6);
	}
	/// <summary>
	/// Creates a new 7-tuple with observable values.
	/// </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
	/// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	/// <param name="item4">The value of the tuple's fourth component.</param>
	/// <param name="item5">The value of the tuple's fifth component.</param>
	/// <param name="item6">The value of the tuple's sixth component.</param>
	/// <param name="item7">The value of the tuple's seventh component.</param>
	/// <returns>
	/// A new <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" />.
	/// </returns>
	public static ObservableTuple<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
	{
		return new(item1, item2, item3, item4, item5, item6, item7);
	}
	/// <summary>
	/// Creates a new 8-tuple with observable values.
	/// </summary>
	/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
	/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
	/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
	/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
	/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
	/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
	/// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
	/// <typeparam name="T8">The type of the tuple's eighth component.</typeparam>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	/// <param name="item4">The value of the tuple's fourth component.</param>
	/// <param name="item5">The value of the tuple's fifth component.</param>
	/// <param name="item6">The value of the tuple's sixth component.</param>
	/// <param name="item7">The value of the tuple's seventh component.</param>
	/// <param name="item8">The value of the tuple's eighth component.</param>
	/// <returns>
	/// A new <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" />.
	/// </returns>
	public static ObservableTuple<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
	{
		return new(item1, item2, item3, item4, item5, item6, item7, item8);
	}
}

/// <summary>
/// Represents a 1-tuple with an observable value.
/// </summary>
/// <typeparam name="T1">The type of the tuple's only component.</typeparam>
public sealed class ObservableTuple<T1> : ObservableObject, IEquatable<ObservableTuple<T1>>
{
	private T1 _Item1;
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1}" /> object's single value.
	/// </summary>
	public T1 Item1
	{
		get => _Item1;
		set => Set(ref _Item1, value);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ObservableTuple{T1}" /> class.
	/// </summary>
	/// <param name="item1">The value of the tuple's only component.</param>
	public ObservableTuple(T1 item1)
	{
		_Item1 = item1;
	}

	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return $"({Item1?.ToString()})";
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is ObservableTuple<T1> observableTuple && Equals(observableTuple);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="ObservableTuple{T1}" />.
	/// </summary>
	/// <param name="other">The <see cref="ObservableTuple{T1}" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(ObservableTuple<T1>? other)
	{
		return
			other != null &&
			Equals(Item1, other.Item1);
	}
	/// <summary>
	/// Returns a hash code for this <see cref="ObservableTuple{T1}" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="ObservableTuple{T1}" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Item1);
	}

	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1}" /> instances for equality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1}" /> are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(ObservableTuple<T1>? a, ObservableTuple<T1>? b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1}" /> instances for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1}" /> are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(ObservableTuple<T1>? a, ObservableTuple<T1>? b)
	{
		return !(a == b);
	}
}

/// <summary>
/// Represents a 2-tuple with observable values.
/// </summary>
/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
public sealed class ObservableTuple<T1, T2> : ObservableObject, IEquatable<ObservableTuple<T1, T2>>
{
	private T1 _Item1;
	private T2 _Item2;
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2}" /> object's first value.
	/// </summary>
	public T1 Item1
	{
		get => _Item1;
		set => Set(ref _Item1, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2}" /> object's second value.
	/// </summary>
	public T2 Item2
	{
		get => _Item2;
		set => Set(ref _Item2, value);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ObservableTuple{T1, T2}" /> class.
	/// </summary>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	public ObservableTuple(T1 item1, T2 item2)
	{
		_Item1 = item1;
		_Item2 = item2;
	}

	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return $"({Item1?.ToString()}, {Item2?.ToString()})";
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is ObservableTuple<T1, T2> observableTuple && Equals(observableTuple);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="ObservableTuple{T1, T2}" />.
	/// </summary>
	/// <param name="other">The <see cref="ObservableTuple{T1, T2}" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(ObservableTuple<T1, T2>? other)
	{
		return
			other != null &&
			Equals(Item1, other.Item1) &&
			Equals(Item2, other.Item2);
	}
	/// <summary>
	/// Returns a hash code for this <see cref="ObservableTuple{T1, T2}" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="ObservableTuple{T1, T2}" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Item1, Item2);
	}

	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2}" /> instances for equality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2}" /> are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(ObservableTuple<T1, T2>? a, ObservableTuple<T1, T2>? b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2}" /> instances for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2}" /> are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(ObservableTuple<T1, T2>? a, ObservableTuple<T1, T2>? b)
	{
		return !(a == b);
	}
}

/// <summary>
/// Represents a 3-tuple with observable values.
/// </summary>
/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
public sealed class ObservableTuple<T1, T2, T3> : ObservableObject, IEquatable<ObservableTuple<T1, T2, T3>>
{
	private T1 _Item1;
	private T2 _Item2;
	private T3 _Item3;
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3}" /> object's first value.
	/// </summary>
	public T1 Item1
	{
		get => _Item1;
		set => Set(ref _Item1, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3}" /> object's second value.
	/// </summary>
	public T2 Item2
	{
		get => _Item2;
		set => Set(ref _Item2, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3}" /> object's third value.
	/// </summary>
	public T3 Item3
	{
		get => _Item3;
		set => Set(ref _Item3, value);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ObservableTuple{T1, T2, T3}" /> class.
	/// </summary>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	public ObservableTuple(T1 item1, T2 item2, T3 item3)
	{
		_Item1 = item1;
		_Item2 = item2;
		_Item3 = item3;
	}

	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return $"({Item1?.ToString()}, {Item2?.ToString()}, {Item3?.ToString()})";
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is ObservableTuple<T1, T2, T3> observableTuple && Equals(observableTuple);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="ObservableTuple{T1, T2, T3}" />.
	/// </summary>
	/// <param name="other">The <see cref="ObservableTuple{T1, T2, T3}" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(ObservableTuple<T1, T2, T3>? other)
	{
		return
			other != null &&
			Equals(Item1, other.Item1) &&
			Equals(Item2, other.Item2) &&
			Equals(Item3, other.Item3);
	}
	/// <summary>
	/// Returns a hash code for this <see cref="ObservableTuple{T1, T2, T3}" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="ObservableTuple{T1, T2, T3}" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Item1, Item2, Item3);
	}

	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3}" /> instances for equality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3}" /> are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(ObservableTuple<T1, T2, T3>? a, ObservableTuple<T1, T2, T3>? b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3}" /> instances for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3}" /> are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(ObservableTuple<T1, T2, T3>? a, ObservableTuple<T1, T2, T3>? b)
	{
		return !(a == b);
	}
}

/// <summary>
/// Represents a 4-tuple with observable values.
/// </summary>
/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
public sealed class ObservableTuple<T1, T2, T3, T4> : ObservableObject, IEquatable<ObservableTuple<T1, T2, T3, T4>>
{
	private T1 _Item1;
	private T2 _Item2;
	private T3 _Item3;
	private T4 _Item4;
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4}" /> object's first value.
	/// </summary>
	public T1 Item1
	{
		get => _Item1;
		set => Set(ref _Item1, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4}" /> object's second value.
	/// </summary>
	public T2 Item2
	{
		get => _Item2;
		set => Set(ref _Item2, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4}" /> object's third value.
	/// </summary>
	public T3 Item3
	{
		get => _Item3;
		set => Set(ref _Item3, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4}" /> object's third value.
	/// </summary>
	public T4 Item4
	{
		get => _Item4;
		set => Set(ref _Item4, value);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ObservableTuple{T1, T2, T3, T4}" /> class.
	/// </summary>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	/// <param name="item4">The value of the tuple's fourth component.</param>
	public ObservableTuple(T1 item1, T2 item2, T3 item3, T4 item4)
	{
		_Item1 = item1;
		_Item2 = item2;
		_Item3 = item3;
		_Item4 = item4;
	}

	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return $"({Item1?.ToString()}, {Item2?.ToString()}, {Item3?.ToString()}, {Item4?.ToString()})";
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is ObservableTuple<T1, T2, T3, T4> observableTuple && Equals(observableTuple);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="ObservableTuple{T1, T2, T3, T4}" />.
	/// </summary>
	/// <param name="other">The <see cref="ObservableTuple{T1, T2, T3, T4}" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(ObservableTuple<T1, T2, T3, T4>? other)
	{
		return
			other != null &&
			Equals(Item1, other.Item1) &&
			Equals(Item2, other.Item2) &&
			Equals(Item3, other.Item3) &&
			Equals(Item4, other.Item4);
	}
	/// <summary>
	/// Returns a hash code for this <see cref="ObservableTuple{T1, T2, T3, T4}" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="ObservableTuple{T1, T2, T3, T4}" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Item1, Item2, Item3, Item4);
	}

	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3, T4}" /> instances for equality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3, T4}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3, T4}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3, T4}" /> are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(ObservableTuple<T1, T2, T3, T4>? a, ObservableTuple<T1, T2, T3, T4>? b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3, T4}" /> instances for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3, T4}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3, T4}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3, T4}" /> are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(ObservableTuple<T1, T2, T3, T4>? a, ObservableTuple<T1, T2, T3, T4>? b)
	{
		return !(a == b);
	}
}

/// <summary>
/// Represents a 5-tuple with observable values.
/// </summary>
/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
public sealed class ObservableTuple<T1, T2, T3, T4, T5> : ObservableObject, IEquatable<ObservableTuple<T1, T2, T3, T4, T5>>
{
	private T1 _Item1;
	private T2 _Item2;
	private T3 _Item3;
	private T4 _Item4;
	private T5 _Item5;
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> object's first value.
	/// </summary>
	public T1 Item1
	{
		get => _Item1;
		set => Set(ref _Item1, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> object's second value.
	/// </summary>
	public T2 Item2
	{
		get => _Item2;
		set => Set(ref _Item2, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> object's third value.
	/// </summary>
	public T3 Item3
	{
		get => _Item3;
		set => Set(ref _Item3, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> object's third value.
	/// </summary>
	public T4 Item4
	{
		get => _Item4;
		set => Set(ref _Item4, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> object's third value.
	/// </summary>
	public T5 Item5
	{
		get => _Item5;
		set => Set(ref _Item5, value);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> class.
	/// </summary>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	/// <param name="item4">The value of the tuple's fourth component.</param>
	/// <param name="item5">The value of the tuple's fifth component.</param>
	public ObservableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
	{
		_Item1 = item1;
		_Item2 = item2;
		_Item3 = item3;
		_Item4 = item4;
		_Item5 = item5;
	}

	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return $"({Item1?.ToString()}, {Item2?.ToString()}, {Item3?.ToString()}, {Item4?.ToString()}, {Item5?.ToString()})";
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is ObservableTuple<T1, T2, T3, T4, T5> observableTuple && Equals(observableTuple);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="ObservableTuple{T1, T2, T3, T4, T5}" />.
	/// </summary>
	/// <param name="other">The <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(ObservableTuple<T1, T2, T3, T4, T5>? other)
	{
		return
			other != null &&
			Equals(Item1, other.Item1) &&
			Equals(Item2, other.Item2) &&
			Equals(Item3, other.Item3) &&
			Equals(Item4, other.Item4) &&
			Equals(Item5, other.Item5);
	}
	/// <summary>
	/// Returns a hash code for this <see cref="ObservableTuple{T1, T2, T3, T4, T5}" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Item1, Item2, Item3, Item4, Item5);
	}

	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> instances for equality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(ObservableTuple<T1, T2, T3, T4, T5>? a, ObservableTuple<T1, T2, T3, T4, T5>? b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> instances for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3, T4, T5}" /> are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(ObservableTuple<T1, T2, T3, T4, T5>? a, ObservableTuple<T1, T2, T3, T4, T5>? b)
	{
		return !(a == b);
	}
}

/// <summary>
/// Represents a 6-tuple with observable values.
/// </summary>
/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
public sealed class ObservableTuple<T1, T2, T3, T4, T5, T6> : ObservableObject, IEquatable<ObservableTuple<T1, T2, T3, T4, T5, T6>>
{
	private T1 _Item1;
	private T2 _Item2;
	private T3 _Item3;
	private T4 _Item4;
	private T5 _Item5;
	private T6 _Item6;
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> object's first value.
	/// </summary>
	public T1 Item1
	{
		get => _Item1;
		set => Set(ref _Item1, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> object's second value.
	/// </summary>
	public T2 Item2
	{
		get => _Item2;
		set => Set(ref _Item2, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> object's third value.
	/// </summary>
	public T3 Item3
	{
		get => _Item3;
		set => Set(ref _Item3, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> object's third value.
	/// </summary>
	public T4 Item4
	{
		get => _Item4;
		set => Set(ref _Item4, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> object's third value.
	/// </summary>
	public T5 Item5
	{
		get => _Item5;
		set => Set(ref _Item5, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> object's third value.
	/// </summary>
	public T6 Item6
	{
		get => _Item6;
		set => Set(ref _Item6, value);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> class.
	/// </summary>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	/// <param name="item4">The value of the tuple's fourth component.</param>
	/// <param name="item5">The value of the tuple's fifth component.</param>
	/// <param name="item6">The value of the tuple's sixth component.</param>
	public ObservableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
	{
		_Item1 = item1;
		_Item2 = item2;
		_Item3 = item3;
		_Item4 = item4;
		_Item5 = item5;
		_Item6 = item6;
	}

	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return $"({Item1?.ToString()}, {Item2?.ToString()}, {Item3?.ToString()}, {Item4?.ToString()}, {Item5?.ToString()}, {Item6?.ToString()})";
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is ObservableTuple<T1, T2, T3, T4, T5, T6> observableTuple && Equals(observableTuple);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" />.
	/// </summary>
	/// <param name="other">The <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(ObservableTuple<T1, T2, T3, T4, T5, T6>? other)
	{
		return
			other != null &&
			Equals(Item1, other.Item1) &&
			Equals(Item2, other.Item2) &&
			Equals(Item3, other.Item3) &&
			Equals(Item4, other.Item4) &&
			Equals(Item5, other.Item5) &&
			Equals(Item6, other.Item6);
	}
	/// <summary>
	/// Returns a hash code for this <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Item1, Item2, Item3, Item4, Item5, Item6);
	}

	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> instances for equality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(ObservableTuple<T1, T2, T3, T4, T5, T6>? a, ObservableTuple<T1, T2, T3, T4, T5, T6>? b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> instances for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6}" /> are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(ObservableTuple<T1, T2, T3, T4, T5, T6>? a, ObservableTuple<T1, T2, T3, T4, T5, T6>? b)
	{
		return !(a == b);
	}
}

/// <summary>
/// Represents a 7-tuple with observable values.
/// </summary>
/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
/// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
public sealed class ObservableTuple<T1, T2, T3, T4, T5, T6, T7> : ObservableObject, IEquatable<ObservableTuple<T1, T2, T3, T4, T5, T6, T7>>
{
	private T1 _Item1;
	private T2 _Item2;
	private T3 _Item3;
	private T4 _Item4;
	private T5 _Item5;
	private T6 _Item6;
	private T7 _Item7;
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> object's first value.
	/// </summary>
	public T1 Item1
	{
		get => _Item1;
		set => Set(ref _Item1, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> object's second value.
	/// </summary>
	public T2 Item2
	{
		get => _Item2;
		set => Set(ref _Item2, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> object's third value.
	/// </summary>
	public T3 Item3
	{
		get => _Item3;
		set => Set(ref _Item3, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> object's third value.
	/// </summary>
	public T4 Item4
	{
		get => _Item4;
		set => Set(ref _Item4, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> object's third value.
	/// </summary>
	public T5 Item5
	{
		get => _Item5;
		set => Set(ref _Item5, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> object's third value.
	/// </summary>
	public T6 Item6
	{
		get => _Item6;
		set => Set(ref _Item6, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> object's third value.
	/// </summary>
	public T7 Item7
	{
		get => _Item7;
		set => Set(ref _Item7, value);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> class.
	/// </summary>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	/// <param name="item4">The value of the tuple's fourth component.</param>
	/// <param name="item5">The value of the tuple's fifth component.</param>
	/// <param name="item6">The value of the tuple's sixth component.</param>
	/// <param name="item7">The value of the tuple's seventh component.</param>
	public ObservableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
	{
		_Item1 = item1;
		_Item2 = item2;
		_Item3 = item3;
		_Item4 = item4;
		_Item5 = item5;
		_Item6 = item6;
		_Item7 = item7;
	}

	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return $"({Item1?.ToString()}, {Item2?.ToString()}, {Item3?.ToString()}, {Item4?.ToString()}, {Item5?.ToString()}, {Item6?.ToString()}, {Item7?.ToString()})";
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is ObservableTuple<T1, T2, T3, T4, T5, T6, T7> observableTuple && Equals(observableTuple);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" />.
	/// </summary>
	/// <param name="other">The <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(ObservableTuple<T1, T2, T3, T4, T5, T6, T7>? other)
	{
		return
			other != null &&
			Equals(Item1, other.Item1) &&
			Equals(Item2, other.Item2) &&
			Equals(Item3, other.Item3) &&
			Equals(Item4, other.Item4) &&
			Equals(Item5, other.Item5) &&
			Equals(Item6, other.Item6) &&
			Equals(Item7, other.Item7);
	}
	/// <summary>
	/// Returns a hash code for this <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Item1, Item2, Item3, Item4, Item5, Item6, Item7);
	}

	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> instances for equality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(ObservableTuple<T1, T2, T3, T4, T5, T6, T7>? a, ObservableTuple<T1, T2, T3, T4, T5, T6, T7>? b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> instances for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7}" /> are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(ObservableTuple<T1, T2, T3, T4, T5, T6, T7>? a, ObservableTuple<T1, T2, T3, T4, T5, T6, T7>? b)
	{
		return !(a == b);
	}
}

/// <summary>
/// Represents a 8-tuple with observable values.
/// </summary>
/// <typeparam name="T1">The type of the tuple's first component.</typeparam>
/// <typeparam name="T2">The type of the tuple's second component.</typeparam>
/// <typeparam name="T3">The type of the tuple's third component.</typeparam>
/// <typeparam name="T4">The type of the tuple's fourth component.</typeparam>
/// <typeparam name="T5">The type of the tuple's fifth component.</typeparam>
/// <typeparam name="T6">The type of the tuple's sixth component.</typeparam>
/// <typeparam name="T7">The type of the tuple's seventh component.</typeparam>
/// <typeparam name="T8">The type of the tuple's eighth component.</typeparam>
public sealed class ObservableTuple<T1, T2, T3, T4, T5, T6, T7, T8> : ObservableObject, IEquatable<ObservableTuple<T1, T2, T3, T4, T5, T6, T7, T8>>
{
	private T1 _Item1;
	private T2 _Item2;
	private T3 _Item3;
	private T4 _Item4;
	private T5 _Item5;
	private T6 _Item6;
	private T7 _Item7;
	private T8 _Item8;
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> object's first value.
	/// </summary>
	public T1 Item1
	{
		get => _Item1;
		set => Set(ref _Item1, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> object's second value.
	/// </summary>
	public T2 Item2
	{
		get => _Item2;
		set => Set(ref _Item2, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> object's third value.
	/// </summary>
	public T3 Item3
	{
		get => _Item3;
		set => Set(ref _Item3, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> object's third value.
	/// </summary>
	public T4 Item4
	{
		get => _Item4;
		set => Set(ref _Item4, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> object's third value.
	/// </summary>
	public T5 Item5
	{
		get => _Item5;
		set => Set(ref _Item5, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> object's third value.
	/// </summary>
	public T6 Item6
	{
		get => _Item6;
		set => Set(ref _Item6, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> object's third value.
	/// </summary>
	public T7 Item7
	{
		get => _Item7;
		set => Set(ref _Item7, value);
	}
	/// <summary>
	/// Gets or sets the value of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> object's third value.
	/// </summary>
	public T8 Item8
	{
		get => _Item8;
		set => Set(ref _Item8, value);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> class.
	/// </summary>
	/// <param name="item1">The value of the tuple's first component.</param>
	/// <param name="item2">The value of the tuple's second component.</param>
	/// <param name="item3">The value of the tuple's third component.</param>
	/// <param name="item4">The value of the tuple's fourth component.</param>
	/// <param name="item5">The value of the tuple's fifth component.</param>
	/// <param name="item6">The value of the tuple's sixth component.</param>
	/// <param name="item7">The value of the tuple's seventh component.</param>
	/// <param name="item8">The value of the tuple's eighth component.</param>
	public ObservableTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
	{
		_Item1 = item1;
		_Item2 = item2;
		_Item3 = item3;
		_Item4 = item4;
		_Item5 = item5;
		_Item6 = item6;
		_Item7 = item7;
		_Item8 = item8;
	}

	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return $"({Item1?.ToString()}, {Item2?.ToString()}, {Item3?.ToString()}, {Item4?.ToString()}, {Item5?.ToString()}, {Item6?.ToString()}, {Item7?.ToString()}, {Item8?.ToString()})";
	}
	/// <summary>
	/// Determines whether the specified <see cref="object" /> is equal to this instance.
	/// </summary>
	/// <param name="obj">The <see cref="object" /> to compare with this instance.</param>
	/// <returns>
	/// <see langword="true" />, if the specified <see cref="object" /> is equal to this instance;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public override bool Equals([NotNullWhen(true)] object? obj)
	{
		return obj is ObservableTuple<T1, T2, T3, T4, T5, T6, T7, T8> observableTuple && Equals(observableTuple);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" />.
	/// </summary>
	/// <param name="other">The <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(ObservableTuple<T1, T2, T3, T4, T5, T6, T7, T8>? other)
	{
		return
			other != null &&
			Equals(Item1, other.Item1) &&
			Equals(Item2, other.Item2) &&
			Equals(Item3, other.Item3) &&
			Equals(Item4, other.Item4) &&
			Equals(Item5, other.Item5) &&
			Equals(Item6, other.Item6) &&
			Equals(Item7, other.Item7) &&
			Equals(Item8, other.Item8);
	}
	/// <summary>
	/// Returns a hash code for this <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return CSharp.GetHashCode(Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8);
	}

	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> instances for equality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(ObservableTuple<T1, T2, T3, T4, T5, T6, T7, T8>? a, ObservableTuple<T1, T2, T3, T4, T5, T6, T7, T8>? b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> instances for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> to compare.</param>
	/// <param name="b">The second <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="ObservableTuple{T1, T2, T3, T4, T5, T6, T7, T8}" /> are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(ObservableTuple<T1, T2, T3, T4, T5, T6, T7, T8>? a, ObservableTuple<T1, T2, T3, T4, T5, T6, T7, T8>? b)
	{
		return !(a == b);
	}
}