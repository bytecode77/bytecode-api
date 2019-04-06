using System;

namespace BytecodeApi
{
	/// <summary>
	/// Represents a wrapper for a value that is cached in a backing field and retrieved only once, or only after a certain period of time has passed.
	/// </summary>
	/// <typeparam name="T">The type of the wrapped value.</typeparam>
	public sealed class CachedProperty<T>
	{
		private readonly Func<T> Getter;
		private DateTime LastAssignment;
		/// <summary>
		/// Gets or sets a <see cref="TimeSpan" /> value that specifies a duration, after which the value is retrieved again, or <see langword="null" />, if the value should only be retrieved on the first time it is accessed. If not <see langword="null" />, the value is retrieved only after both the timeout period has been reached and it is accessed.
		/// </summary>
		public TimeSpan? Timeout { get; set; }
		/// <summary>
		/// Gets a <see cref="bool" /> value indicating whether the value is currently set. Returns <see langword="true" /> after the getter has been invoked, regardless of the timeout period. After calling <see cref="Invalidate" />, this property is set to <see langword="false" />.
		/// </summary>
		public bool HasValue { get; private set; }
		/// <summary>
		/// Gets the current value, but does not invoke the getter. Prior to getter invocation, or if <see cref="HasValue" /> is <see langword="false" />, this property is <see langword="default" />(<typeparamref name="T" />).
		/// </summary>
		public T Value { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CachedProperty{T}" /> class with the specified getter function.
		/// </summary>
		/// <param name="getter">The getter, which is invoked when the value needs to be retrieved (lazy loading). This is either on the first time the value is accessed, or if the value is accessed and a timeout is specified and reached.</param>
		public CachedProperty(Func<T> getter)
		{
			Check.ArgumentNull(getter, nameof(getter));

			Getter = getter;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="CachedProperty{T}" /> class with the specified getter function.
		/// </summary>
		/// <param name="getter">The getter, which is invoked when the value needs to be retrieved (lazy loading). This is either on the first time the value is accessed, or if the value is accessed and a timeout is specified and reached.</param>
		/// <param name="timeout">A <see cref="TimeSpan" /> value that specifies a duration, after which the value is retrieved again, or <see langword="null" />, if the value should only be retrieved on the first time it is accessed. If not <see langword="null" />, the value is retrieved only after both the timeout period has been reached and it is accessed.</param>
		public CachedProperty(Func<T> getter, TimeSpan? timeout) : this(getter)
		{
			Timeout = timeout;
		}

		/// <summary>
		/// Returns the value of this <see cref="CachedProperty{T}" /> instance. This will invoke the getter, if required.
		/// </summary>
		/// <returns>
		/// The value of this <see cref="CachedProperty{T}" /> instance.
		/// </returns>
		public T Get()
		{
			if (!HasValue || Timeout != null && DateTime.Now - LastAssignment > Timeout) InvokeGetter();
			return Value;
		}
		/// <summary>
		/// Resets the value to its initial state. The next time the value is accessed, the getter will be invoked.
		/// </summary>
		public void Invalidate()
		{
			HasValue = false;
			Value = default;
			LastAssignment = DateTime.MinValue;
		}
		/// <summary>
		/// Invokes the getter and sets the value accordingly, regardless of its existence or the timeout period.
		/// </summary>
		/// <returns>
		/// The value that has been retrieved, which is the equivalent to <see cref="Value" />.
		/// </returns>
		public T InvokeGetter()
		{
			Value = Getter();
			HasValue = true;
			LastAssignment = DateTime.Now;

			return Value;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="CachedProperty{T}" /> to the underlying value and returns this value. The getter will be invoked, if required. This is equivalent to <see cref="Get" />.
		/// </summary>
		/// <param name="property">The <see cref="CachedProperty{T}" /> instance to retrieve the value from.</param>
		/// <returns>
		/// The value that this <see cref="CachedProperty{T}" /> instance retrieved.
		/// </returns>
		public static implicit operator T(CachedProperty<T> property)
		{
			return property.Get();
		}
	}
}