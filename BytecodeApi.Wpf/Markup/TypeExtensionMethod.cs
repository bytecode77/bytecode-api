using System.ComponentModel;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Specifies the method that is used to convert <see cref="Type" /> values.
/// </summary>
public enum TypeExtensionMethod
{
	/// <summary>
	/// Returns an array of the values of the constants in the specified <see cref="Enum" /> type.
	/// </summary>
	EnumValues,
	/// <summary>
	/// Returns an array of the description values of the constants in the specified <see cref="Enum" /> type. The description is taken from the <see cref="DescriptionAttribute.Description" /> attribute, or <see langword="null" />, if the attribute was not found.
	/// </summary>
	EnumDescriptions,
	/// <summary>
	/// Gets a lookup <see cref="Dictionary{TKey, TValue}" /> with all distinct <see langword="enum" /> values and descriptions for the specified <see cref="Enum" /> type. The description is taken from the <see cref="DescriptionAttribute.Description" /> attribute, or <see langword="null" />, if the attribute was not found.
	/// </summary>
	EnumDescriptionLookup
}