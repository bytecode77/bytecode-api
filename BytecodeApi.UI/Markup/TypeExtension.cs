using BytecodeApi.Extensions;
using System;
using System.Linq;
using System.Windows.Markup;

namespace BytecodeApi.UI.Markup
{
	/// <summary>
	/// Implements <see cref="System.Type" /> support for .NET Framework XAML Services.
	/// </summary>
	public sealed class TypeExtension : MarkupExtension
	{
		/// <summary>
		/// Gets or sets the <see cref="System.Type" /> that is processed.
		/// </summary>
		public Type Type { get; set; }
		/// <summary>
		/// Specifies the method that is used to convert the <see cref="System.Type" /> value.
		/// </summary>
		public TypeExtensionMethod Method { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeExtension" /> class, initializing <paramref name="type" /> and <paramref name="method" /> based on the provided values.
		/// </summary>
		/// <param name="type">The <see cref="System.Type" /> that is processed.</param>
		/// <param name="method">The method that is used to convert the <see cref="System.Type" /> value.</param>
		public TypeExtension(Type type, TypeExtensionMethod method)
		{
			Type = type;
			Method = method;
		}

		/// <summary>
		/// Returns an <see cref="object" /> value that is converted from the parameters supplied in the constructor of <see cref="TypeExtension" />.
		/// </summary>
		/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
		/// <returns>
		/// An <see cref="object" /> value that is converted from the parameters supplied in the constructor of <see cref="TypeExtension" />.
		/// </returns>
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			Check.ArgumentNull(Type, nameof(Type));

			switch (Method)
			{
				case TypeExtensionMethod.EnumValues: return EnumEx.GetValues(Type);
				case TypeExtensionMethod.EnumDescriptions: return EnumEx.GetValues(Type).Select(value => value.GetDescription()).ToArray();
				case TypeExtensionMethod.EnumDescriptionLookup: return EnumEx.GetDescriptionLookup(Type);
				default: throw Throw.InvalidEnumArgument(nameof(Method));
			}
		}
	}
}