namespace BytecodeApi;

/// <summary>
/// Represents a value type that can be assigned an undefined value.
/// </summary>
/// <typeparam name="T">The type of the underlying value.</typeparam>
public readonly struct Undefinable<T> : IEquatable<Undefinable<T>>
{
	/// <summary>
	/// Represents an undefined value.
	/// </summary>
	public static readonly Undefinable<T> Undefined = new();
	private readonly T _Value;
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating, whether this value is defined, or undefined.
	/// </summary>
	public bool IsDefined { get; }
	/// <summary>
	/// Gets the underlying value. An exception is thrown, if <see cref="IsDefined" /> is <see langword="false" />.
	/// </summary>
	public T Value => IsDefined ? _Value : throw Throw.InvalidOperation("Value is undefined.");

	/// <summary>
	/// Initializes a new instance of the <see cref="Undefinable{T}" /> structure to the specified value.
	/// </summary>
	/// <param name="value">The value to be assigned.</param>
	public Undefinable(T value)
	{
		IsDefined = true;
		_Value = value;
	}

	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance.
	/// </returns>
	public override string ToString()
	{
		return !IsDefined ? "Undefined" : _Value == null ? "null" : _Value.ToString() ?? "";
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
		return obj is Undefinable<T> undefinable && Equals(undefinable);
	}
	/// <summary>
	/// Determines whether this instance is equal to another <see cref="Undefinable{T}" />.
	/// </summary>
	/// <param name="other">The <see cref="Undefinable{T}" /> to compare to this instance.</param>
	/// <returns>
	/// <see langword="true" />, if this instance is equal to the <paramref name="other" /> parameter;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Equals(Undefinable<T> other)
	{
		return IsDefined == other.IsDefined && EqualityComparer<T>.Default.Equals(_Value, other._Value);
	}
	/// <summary>
	/// Returns a hash code for this <see cref="Undefinable{T}" />.
	/// </summary>
	/// <returns>
	/// The hash code for this <see cref="Undefinable{T}" /> instance.
	/// </returns>
	public override int GetHashCode()
	{
		return HashCode.Combine(IsDefined, _Value);
	}

	/// <summary>
	/// Compares two <see cref="Undefinable{T}" /> values for equality.
	/// </summary>
	/// <param name="a">The first <see cref="Undefinable{T}" /> to compare.</param>
	/// <param name="b">The second <see cref="Undefinable{T}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="Undefinable{T}" /> values are equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator ==(Undefinable<T> a, Undefinable<T> b)
	{
		return Equals(a, b);
	}
	/// <summary>
	/// Compares two <see cref="Undefinable{T}" /> values for inequality.
	/// </summary>
	/// <param name="a">The first <see cref="Undefinable{T}" /> to compare.</param>
	/// <param name="b">The second <see cref="Undefinable{T}" /> to compare.</param>
	/// <returns>
	/// <see langword="true" />, if both <see cref="Undefinable{T}" /> values are not equal;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool operator !=(Undefinable<T> a, Undefinable<T> b)
	{
		return !(a == b);
	}
	/// <summary>
	/// Defines an implicit conversion of a value to a <see cref="Undefinable{T}" />.
	/// </summary>
	/// <param name="value">The value to convert.</param>
	public static implicit operator Undefinable<T>(T value)
	{
		return new(value);
	}
	/// <summary>
	/// Defines an explicit conversion of a <see cref="Undefinable{T}" /> to its underlying value type.
	/// An exception is thrown, if <see cref="IsDefined" /> is <see langword="false" />.
	/// </summary>
	/// <param name="value">The <see cref="Undefinable{T}" /> to convert.</param>
	public static explicit operator T(Undefinable<T> value)
	{
		return value.Value;
	}
}