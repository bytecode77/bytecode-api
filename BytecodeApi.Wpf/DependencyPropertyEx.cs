using System.Diagnostics;
using System.Reflection;
using System.Windows;

namespace BytecodeApi.Wpf;

/// <summary>
/// Provides <see langword="static" /> methods that extend the <see cref="DependencyProperty" /> class.
/// </summary>
public static class DependencyPropertyEx
{
	/// <summary>
	/// Registers a dependency property with the specified property name. Property and owner types are automatically detected.
	/// <para>Example: <see langword="public" /> <see langword="static" /> <see langword="readonly" /> <see cref="DependencyProperty" /> FooProperty = <see cref="DependencyPropertyEx" />.Register(<see langword="nameof" />(Foo));</para>
	/// </summary>
	/// <param name="name">The name of the dependency property to register. The name must be unique within the registration namespace of the owner type.</param>
	/// <returns>
	/// A dependency property identifier that should be used to set the value of a <see langword="public" /> <see langword="static" /> <see langword="readonly" /> field in your class. That identifier is then used to reference the dependency property later, for operations such as setting its value programmatically or obtaining metadata.
	/// </returns>
	public static DependencyProperty Register(string name)
	{
		return Register(name, null);
	}
	/// <summary>
	/// Registers a dependency property with the specified property name and property metadata. Property and owner types are automatically detected.
	/// <para>Example: <see langword="public" /> <see langword="static" /> <see langword="readonly" /> <see cref="DependencyProperty" /> FooProperty = <see cref="DependencyPropertyEx" />.Register(<see langword="nameof" />(Foo), new <see cref="PropertyMetadata" />());</para>
	/// </summary>
	/// <param name="name">The name of the dependency property to register. The name must be unique within the registration namespace of the owner type.</param>
	/// <param name="typeMetadata">Property metadata for the dependency property.</param>
	/// <returns>
	/// A dependency property identifier that should be used to set the value of a <see langword="public" /> <see langword="static" /> <see langword="readonly" /> field in your class. That identifier is then used to reference the dependency property later, for operations such as setting its value programmatically or obtaining metadata.
	/// </returns>
	public static DependencyProperty Register(string name, PropertyMetadata? typeMetadata)
	{
		return Register(name, typeMetadata, null);
	}
	/// <summary>
	/// Registers a dependency property with the specified property name, property metadata, and a value validation callback for the property. Property and owner types are automatically detected.
	/// <para>Example: <see langword="public" /> <see langword="static" /> <see langword="readonly" /> <see cref="DependencyProperty" /> FooProperty = <see cref="DependencyPropertyEx" />.Register(<see langword="nameof" />(Foo), new <see cref="PropertyMetadata" />(), new <see cref="ValidateValueCallback" />());</para>
	/// </summary>
	/// <param name="name">The name of the dependency property to register. The name must be unique within the registration namespace of the owner type.</param>
	/// <param name="typeMetadata">Property metadata for the dependency property.</param>
	/// <param name="validateValueCallback">A reference to a callback that should perform any custom validation of the dependency property value beyond typical type validation.</param>
	/// <returns>
	/// A dependency property identifier that should be used to set the value of a <see langword="public" /> <see langword="static" /> <see langword="readonly" /> field in your class. That identifier is then used to reference the dependency property later, for operations such as setting its value programmatically or obtaining metadata.
	/// </returns>
	public static DependencyProperty Register(string name, PropertyMetadata? typeMetadata, ValidateValueCallback? validateValueCallback)
	{
		Check.ArgumentNull(name);
		Check.ArgumentEx.StringNotEmpty(name);

		GetTypes(name, out Type ownerType, out Type propertyType);
		return DependencyProperty.Register(name, propertyType, ownerType, typeMetadata, validateValueCallback);
	}
	/// <summary>
	/// Registers an attached property.
	/// </summary>
	/// <typeparam name="TOwner">The owner type that is registering the dependency property.</typeparam>
	/// <typeparam name="TProperty">The type of the property.</typeparam>
	/// <param name="name">The name of the dependency property to register.</param>
	/// <returns>
	/// A dependency property identifier that should be used to set the value of a <see langword="public" /> <see langword="static" /> <see langword="readonly" /> field in your class. That identifier is then used to reference the dependency property later, for operations such as setting its value programmatically or obtaining metadata.
	/// </returns>
	public static DependencyProperty RegisterAttached<TOwner, TProperty>(string name)
	{
		return RegisterAttached<TOwner, TProperty>(name, null);
	}
	/// <summary>
	/// Registers an attached property.
	/// </summary>
	/// <typeparam name="TOwner">The owner type that is registering the dependency property.</typeparam>
	/// <typeparam name="TProperty">The type of the property.</typeparam>
	/// <param name="name">The name of the dependency property to register.</param>
	/// <param name="defaultMetadata">Property metadata for the dependency property. This can include the default value as well as other characteristics.</param>
	/// <returns>
	/// A dependency property identifier that should be used to set the value of a <see langword="public" /> <see langword="static" /> <see langword="readonly" /> field in your class. That identifier is then used to reference the dependency property later, for operations such as setting its value programmatically or obtaining metadata.
	/// </returns>
	public static DependencyProperty RegisterAttached<TOwner, TProperty>(string name, PropertyMetadata? defaultMetadata)
	{
		return RegisterAttached<TOwner, TProperty>(name, defaultMetadata, null);
	}
	/// <summary>
	/// Registers an attached property.
	/// </summary>
	/// <typeparam name="TOwner">The owner type that is registering the dependency property.</typeparam>
	/// <typeparam name="TProperty">The type of the property.</typeparam>
	/// <param name="name">The name of the dependency property to register.</param>
	/// <param name="defaultMetadata">Property metadata for the dependency property. This can include the default value as well as other characteristics.</param>
	/// <param name="validateValueCallback">A reference to a callback that should perform any custom validation of the dependency property value beyond typical type validation.</param>
	/// <returns>
	/// A dependency property identifier that should be used to set the value of a <see langword="public" /> <see langword="static" /> <see langword="readonly" /> field in your class. That identifier is then used to reference the dependency property later, for operations such as setting its value programmatically or obtaining metadata.
	/// </returns>
	public static DependencyProperty RegisterAttached<TOwner, TProperty>(string name, PropertyMetadata? defaultMetadata, ValidateValueCallback? validateValueCallback)
	{
		return DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), defaultMetadata, validateValueCallback);
	}
	/// <summary>
	/// Registers a read-only attached property, with the specified property metadata. The owner type is automatically detected.
	/// </summary>
	/// <typeparam name="TProperty">The type of the property.</typeparam>
	/// <param name="name">The name of the dependency property to register.</param>
	/// <param name="defaultMetadata">Property metadata for the dependency property.</param>
	/// <returns>
	/// A dependency property identifier that should be used to set the value of a <see langword="public" /> <see langword="static" /> <see langword="readonly" /> field in your class. That identifier is then used to reference the dependency property later, for operations such as setting its value programmatically or obtaining metadata.
	/// </returns>
	public static DependencyPropertyKey RegisterAttachedReadOnly<TProperty>(string name, PropertyMetadata defaultMetadata)
	{
		return RegisterAttachedReadOnly<TProperty>(name, defaultMetadata, null);
	}
	/// <summary>
	/// Registers a read-only attached property, with the specified property metadata. The owner type is automatically detected.
	/// </summary>
	/// <typeparam name="TProperty">The type of the property.</typeparam>
	/// <param name="name">The name of the dependency property to register.</param>
	/// <param name="defaultMetadata">Property metadata for the dependency property.</param>
	/// <param name="validateValueCallback">A reference to a user-created callback that should perform any custom validation of the dependency property value beyond typical type validation.</param>
	/// <returns>
	/// A dependency property identifier that should be used to set the value of a <see langword="public" /> <see langword="static" /> <see langword="readonly" /> field in your class. That identifier is then used to reference the dependency property later, for operations such as setting its value programmatically or obtaining metadata.
	/// </returns>
	public static DependencyPropertyKey RegisterAttachedReadOnly<TProperty>(string name, PropertyMetadata defaultMetadata, ValidateValueCallback? validateValueCallback)
	{
		GetTypes(name, out Type ownerType);
		return DependencyProperty.RegisterAttachedReadOnly(name, typeof(TProperty), ownerType, defaultMetadata, validateValueCallback);
	}
	/// <summary>
	/// Registers a read-only dependency property, with the specified property name and property metadata. Property and owner types are automatically detected.
	/// <para>Example: <see langword="public" /> <see langword="static" /> <see langword="readonly" /> <see cref="DependencyPropertyKey" /> FooKey = <see cref="DependencyPropertyEx" />.RegisterReadOnly(<see langword="nameof" />(Foo), new <see cref="PropertyMetadata" />());</para>
	/// </summary>
	/// <param name="name">The name of the dependency property to register. The name must be unique within the registration namespace of the owner type.</param>
	/// <param name="typeMetadata">Property metadata for the dependency property.</param>
	/// <returns>
	/// A dependency property key that should be used to set the value of a <see langword="static" /> <see langword="readonly" /> field in your class, which is then used to reference the dependency property.
	/// </returns>
	public static DependencyPropertyKey RegisterReadOnly(string name, PropertyMetadata? typeMetadata)
	{
		return RegisterReadOnly(name, typeMetadata, null);
	}
	/// <summary>
	/// Registers a read-only dependency property, with the specified property name, property metadata, and a validation callback. Property and owner types are automatically detected.
	/// <para>Example: <see langword="public" /> <see langword="static" /> <see langword="readonly" /> <see cref="DependencyPropertyKey" /> FooKey = <see cref="DependencyPropertyEx" />.RegisterReadOnly(<see langword="nameof" />(Foo), new <see cref="PropertyMetadata" />(), new <see cref="ValidateValueCallback" />());</para>
	/// </summary>
	/// <param name="name">The name of the dependency property to register. The name must be unique within the registration namespace of the owner type.</param>
	/// <param name="typeMetadata">Property metadata for the dependency property.</param>
	/// <param name="validateValueCallback">A reference to a user-created callback that should perform any custom validation of the dependency property value beyond typical type validation.</param>
	/// <returns>
	/// A dependency property key that should be used to set the value of a <see langword="static" /> <see langword="readonly" /> field in your class, which is then used to reference the dependency property.
	/// </returns>
	public static DependencyPropertyKey RegisterReadOnly(string name, PropertyMetadata? typeMetadata, ValidateValueCallback? validateValueCallback)
	{
		Check.ArgumentNull(name);
		Check.ArgumentEx.StringNotEmpty(name);

		GetTypes(name, out Type ownerType, out Type propertyType);
		return DependencyProperty.RegisterReadOnly(name, propertyType, ownerType, typeMetadata, validateValueCallback);
	}

#pragma warning disable IDE0060 // Remove unused parameter
	private static void GetTypes(string propertyName, out Type ownerType)
	{
		ownerType = new StackTrace()
			.GetFrames()
			.FirstOrDefault(frame => frame.GetMethod()?.Name == ".cctor")
			?.GetMethod()
			?.DeclaringType ?? throw Throw.InvalidOperation("Could not determine owner type.");
	}
#pragma warning restore IDE0060 // Remove unused parameter
	private static void GetTypes(string propertyName, out Type ownerType, out Type propertyType)
	{
		GetTypes(propertyName, out ownerType);

		propertyType = ownerType
			.GetProperty(propertyName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
			?.PropertyType ?? throw Throw.InvalidOperation("Could not determine property type.");
	}
}