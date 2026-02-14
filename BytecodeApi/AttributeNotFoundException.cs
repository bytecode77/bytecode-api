namespace BytecodeApi;

/// <summary>
/// The exception that is thrown when an <see cref="Attribute" /> was not found.
/// </summary>
public sealed class AttributeNotFoundException : Exception
{
	/// <summary>
	/// A <see cref="Type" /> that represents the expected attribute class.
	/// </summary>
	public Type? AttributeClass { get; }

	/// <summary>
	/// Initializes a new instance of the <see cref="AttributeNotFoundException" /> class.
	/// </summary>
	public AttributeNotFoundException() : this(null)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="AttributeNotFoundException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	public AttributeNotFoundException(string? message) : this(message, (Type?)null)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="AttributeNotFoundException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null" />, if no inner exception is specified.</param>
	public AttributeNotFoundException(string? message, Exception? innerException) : this(message, null, innerException)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="AttributeNotFoundException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="attributeClass">A <see cref="Type" /> that represents the expected attribute class.</param>
	public AttributeNotFoundException(string? message, Type? attributeClass) : this(message, attributeClass, null)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="AttributeNotFoundException" /> class.
	/// </summary>
	/// <param name="message">The message that describes the error.</param>
	/// <param name="attributeClass">A <see cref="Type" /> that represents the expected attribute class.</param>
	/// <param name="innerException">The exception that is the cause of the current exception, or <see langword="null" />, if no inner exception is specified.</param>
	public AttributeNotFoundException(string? message, Type? attributeClass, Exception? innerException) : base(message ?? (attributeClass == null ? "The expected attribute was not found." : $"The expected attribute of type '{attributeClass}' was not found."), innerException)
	{
		AttributeClass = attributeClass;
	}
}