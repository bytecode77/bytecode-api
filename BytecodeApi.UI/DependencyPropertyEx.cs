using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace BytecodeApi.UI
{
	/// <summary>
	/// Provides <see langword="static" /> methods that extend the <see cref="DependencyProperty" /> class.
	/// </summary>
	public static class DependencyPropertyEx
	{
		/// <summary>
		/// Registers a dependency property with the specified property name. Property and owner types are automatically detected.
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
		/// </summary>
		/// <param name="name">The name of the dependency property to register. The name must be unique within the registration namespace of the owner type.</param>
		/// <param name="typeMetadata">Property metadata for the dependency property.</param>
		/// <returns>
		/// A dependency property identifier that should be used to set the value of a <see langword="public" /> <see langword="static" /> <see langword="readonly" /> field in your class. That identifier is then used to reference the dependency property later, for operations such as setting its value programmatically or obtaining metadata.
		/// </returns>
		public static DependencyProperty Register(string name, PropertyMetadata typeMetadata)
		{
			return Register(name, typeMetadata, null);
		}
		/// <summary>
		/// Registers a dependency property with the specified property name, property metadata, and a value validation callback for the property. Property and owner types are automatically detected.
		/// </summary>
		/// <param name="name">The name of the dependency property to register. The name must be unique within the registration namespace of the owner type.</param>
		/// <param name="typeMetadata">Property metadata for the dependency property.</param>
		/// <param name="validateValueCallback">A reference to a callback that should perform any custom validation of the dependency property value beyond typical type validation.</param>
		/// <returns>
		/// A dependency property identifier that should be used to set the value of a <see langword="public" /> <see langword="static" /> <see langword="readonly" /> field in your class. That identifier is then used to reference the dependency property later, for operations such as setting its value programmatically or obtaining metadata.
		/// </returns>
		public static DependencyProperty Register(string name, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback)
		{
			Check.ArgumentNull(name, nameof(name));
			Check.ArgumentEx.StringNotEmpty(name, nameof(name));

			GetTypes(name, out Type propertyType, out Type ownerType);
			return DependencyProperty.Register(name, propertyType, ownerType, typeMetadata, validateValueCallback);
		}
		/// <summary>
		/// Registers a read-only dependency property, with the specified property name and property metadata. Property and owner types are automatically detected.
		/// </summary>
		/// <param name="name">The name of the dependency property to register. The name must be unique within the registration namespace of the owner type.</param>
		/// <param name="typeMetadata">Property metadata for the dependency property.</param>
		/// <returns>
		/// A dependency property key that should be used to set the value of a <see langword="static" /> <see langword="readonly" /> field in your class, which is then used to reference the dependency property.
		/// </returns>
		public static DependencyPropertyKey RegisterReadOnly(string name, PropertyMetadata typeMetadata)
		{
			return RegisterReadOnly(name, typeMetadata, null);
		}
		/// <summary>
		/// Registers a read-only dependency property, with the specified property name, property metadata, and a validation callback. Property and owner types are automatically detected.
		/// </summary>
		/// <param name="name">The name of the dependency property to register. The name must be unique within the registration namespace of the owner type.</param>
		/// <param name="typeMetadata">Property metadata for the dependency property.</param>
		/// <param name="validateValueCallback">A reference to a user-created callback that should perform any custom validation of the dependency property value beyond typical type validation.</param>
		/// <returns>
		/// A dependency property key that should be used to set the value of a <see langword="static" /> <see langword="readonly" /> field in your class, which is then used to reference the dependency property.
		/// </returns>
		public static DependencyPropertyKey RegisterReadOnly(string name, PropertyMetadata typeMetadata, ValidateValueCallback validateValueCallback)
		{
			Check.ArgumentNull(name, nameof(name));
			Check.ArgumentEx.StringNotEmpty(name, nameof(name));

			GetTypes(name, out Type propertyType, out Type ownerType);
			return DependencyProperty.RegisterReadOnly(name, propertyType, ownerType, typeMetadata, validateValueCallback);
		}

		private static void GetTypes(string name, out Type propertyType, out Type ownerType)
		{
			ownerType = new StackTrace()
				.GetFrames()
				.FirstOrDefault(frame => frame.GetMethod().Name == ".cctor")
				?.GetMethod()
				.DeclaringType ?? throw Throw.InvalidOperation("Could not determine owner type.");

			propertyType = ownerType
				.GetProperty(name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
				?.PropertyType ?? throw Throw.InvalidOperation("Could not determine property type.");
		}
	}
}