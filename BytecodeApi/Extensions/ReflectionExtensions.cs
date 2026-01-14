using System.Reflection;
using System.Text.RegularExpressions;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with .NET reflection objects, such as <see cref="Type" /> and <see cref="MethodBase" />.
/// </summary>
public static class ReflectionExtensions
{
	private static readonly Dictionary<string, string> TypeNames = new()
	{
		["Void"] = "void",
		["Boolean"] = "bool",
		["Byte"] = "byte",
		["SByte"] = "sbyte",
		["Char"] = "char",
		["Decimal"] = "decimal",
		["Double"] = "double",
		["Single"] = "float",
		["Int32"] = "int",
		["UInt32"] = "uint",
		["Int64"] = "long",
		["UInt64"] = "ulong",
		["Object"] = "object",
		["Int16"] = "short",
		["UInt16"] = "ushort",
		["IntPtr"] = "nint",
		["UIntPtr"] = "nuint",
		["String"] = "string"
	};

	extension(Type type)
	{
		/// <summary>
		/// Gets the name of this <see cref="Type" />, including its nested class names.
		/// </summary>
		public string NestedName
		{
			get
			{
				Check.ArgumentNull(type);

				List<string> nestedTypes = [];
				Type? t = type;

				do
				{
					nestedTypes.Add(t.Name);
					t = t.IsNested ? t.DeclaringType : null;
				}
				while (t != null);

				return nestedTypes.Reverse<string>().AsString(".");
			}
		}
		/// <summary>
		/// Gets the fully qualified name of this <see cref="Type" />, including its nested class names and its namespace but not its assembly.
		/// </summary>
		public string NestedFullName
		{
			get
			{
				Check.ArgumentNull(type);

				return (type.Namespace == null ? null : type.Namespace + ".") + type.NestedName;
			}
		}

		/// <summary>
		/// Determines whether an instance of this <see cref="Type" /> can be assigned to the current <see cref="Type" /> instance.
		/// </summary>
		/// <param name="c">The <see cref="Type" /> to compare with this <see cref="Type" />.</param>
		/// <param name="excludeSelf"><see langword="true" /> to exclude this <see cref="Type" /> from being checked. If this parameter is <see langword="true" /> and this <see cref="Type" /> equals <paramref name="c" />, <see langword="false" /> is returned.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="c" /> is derived either directly or indirectly derived from this <see cref="Type" />, this <see cref="Type" /> is an interface that <paramref name="c" /> implements, <paramref name="c" /> is a generic type parameter and this <see cref="Type" /> represents one of the constraints of <paramref name="c" />, <paramref name="c" /> represents a value type and this <see cref="Type" /> represents <see cref="Nullable" />&lt;<paramref name="c" />&gt;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool IsAssignableFrom(Type c, bool excludeSelf)
		{
			Check.ArgumentNull(type);
			Check.ArgumentNull(c);

			return type.IsAssignableFrom(c) && (!excludeSelf || type != c);
		}
		/// <summary>
		/// Returns the equivalent <see cref="string" /> representation of the name of this <see cref="Type" /> using the <see cref="TypeNaming.CSharp" /> naming convention.
		/// </summary>
		/// <returns>
		/// An equivalent <see cref="string" /> representation of the name of this <see cref="Type" /> using the <see cref="TypeNaming.CSharp" /> naming convention.
		/// </returns>
		public string ToCSharpName()
		{
			return type.ToCSharpName(TypeNaming.CSharp);
		}
		/// <summary>
		/// Returns the equivalent <see cref="string" /> representation of the name of this <see cref="Type" /> using the specified naming convention.
		/// </summary>
		/// <param name="namingConvention">The <see cref="TypeNaming" /> that specifies the naming convention for the converted <see cref="Type" />.</param>
		/// <returns>
		/// An equivalent <see cref="string" /> representation of the name of this <see cref="Type" /> using the specified naming convention.
		/// </returns>
		public string ToCSharpName(TypeNaming namingConvention)
		{
			Check.ArgumentNull(type);

			string name = namingConvention == TypeNaming.FullName ? type.NestedFullName : type.NestedName;
			string suffix = "";
			Type? t = type;

			while (t.HasElementType)
			{
				Match match = Regex.Match(name, @"\[,*\]|\*|&", RegexOptions.RightToLeft);
				if (!match.Success)
				{
					throw Throw.InvalidOperation("Could not parse element type.");
				}

				suffix = match.Value + suffix;

				if (t.GetElementType() is Type elementType)
				{
					t = elementType;
				}
				else
				{
					break;
				}

				name = namingConvention == TypeNaming.FullName ? t.NestedFullName : t.NestedName;
			}

			if (namingConvention == TypeNaming.CSharp)
			{
				name = TypeNames.GetValueOrDefault(name, name);
			}

			if (t.IsGenericType)
			{
				string genericClass = name.SubstringUntil('`');
				Type[] genericArguments = t.GetGenericArguments();
				if (genericClass == nameof(Nullable<>) && genericArguments.Length == 1)
				{
					string nullableType = genericArguments.First().ToCSharpName(namingConvention);
					return (namingConvention == TypeNaming.CSharp ? nullableType + "?" : nameof(Nullable<>) + "<" + nullableType + ">") + suffix;
				}
				else
				{
					return genericClass + "<" + genericArguments.Select(t => t.ToCSharpName(namingConvention)).AsString(", ") + ">" + suffix;
				}
			}
			else
			{
				return name + suffix;
			}
		}
	}

	extension(FieldInfo field)
	{
		/// <summary>
		/// Returns the value of a static field.
		/// </summary>
		/// <typeparam name="T">The type to which the returned value is casted to.</typeparam>
		/// <returns>
		/// The value of a static field.
		/// </returns>
		public T? GetValue<T>()
		{
			return field.GetValue<T>(null);
		}
		/// <summary>
		/// Returns the value of a field supported by a given <see cref="object" />.
		/// </summary>
		/// <typeparam name="T">The type to which the returned value is casted to.</typeparam>
		/// <param name="obj">The <see cref="object" /> to retrieve the value from.</param>
		/// <returns>
		/// The value of a field supported by a given <see cref="object" />.
		/// </returns>
		public T? GetValue<T>(object? obj)
		{
			Check.ArgumentNull(field);

			return (T?)field.GetValue(obj);
		}
	}

	extension(PropertyInfo property)
	{
		/// <summary>
		/// Returns the value of a static property.
		/// </summary>
		/// <typeparam name="T">The type to which the returned value is casted to.</typeparam>
		/// <returns>
		/// The value of a static property.
		/// </returns>
		public T? GetValue<T>()
		{
			return property.GetValue<T>(null);
		}
		/// <summary>
		/// Returns the value of a property supported by a given <see cref="object" />.
		/// </summary>
		/// <typeparam name="T">The type to which the returned value is casted to.</typeparam>
		/// <param name="obj">The <see cref="object" /> to retrieve the value from.</param>
		/// <returns>
		/// The value of a property supported by a given <see cref="object" />.
		/// </returns>
		public T? GetValue<T>(object? obj)
		{
			Check.ArgumentNull(property);

			return (T?)property.GetValue(obj);
		}
	}

	extension(MethodBase method)
	{
		/// <summary>
		/// Invokes the method or constructor represented by the current instance with no parameters.
		/// </summary>
		/// <returns>
		/// An <see cref="object" /> containing the return value of the invoked method, or null in the case of a constructor.
		/// </returns>
		public object? Invoke()
		{
			Check.ArgumentNull(method);

			return method.Invoke(null, null);
		}
		/// <summary>
		/// Invokes the method or constructor represented by the current instance with no parameters.
		/// </summary>
		/// <typeparam name="T">The return type of this <see cref="MethodInfo" />.</typeparam>
		/// <returns>
		/// An <see cref="object" /> containing the return value of the invoked method, or null in the case of a constructor.
		/// </returns>
		public T? Invoke<T>()
		{
			return method.Invoke<T>(null, null);
		}
		/// <summary>
		/// Invokes the method or constructor represented by the current instance with, using the specified parameters.
		/// </summary>
		/// <typeparam name="T">The return type of this <see cref="MethodInfo" />.</typeparam>
		/// <param name="obj">The <see cref="object" /> on which to invoke the method or constructor. If a method is static, this argument is ignored. If a constructor is static, this argument must be <see langword="null" /> or an instance of the class that defines the constructor.</param>
		/// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, parameters should be <see langword="null" />. If the method or constructor represented by this instance takes a ref parameter, no special attribute is required for that parameter in order to invoke the method or constructor using this function. Any <see cref="object" /> in this array that is not explicitly initialized with a value will contain the default value for that <see cref="object" /> type.</param>
		/// <returns>
		/// An <see cref="object" /> containing the return value of the invoked method, or null in the case of a constructor.
		/// </returns>
		public T? Invoke<T>(object? obj, object?[]? parameters)
		{
			Check.ArgumentNull(method);

			return (T?)method.Invoke(obj, parameters);
		}
	}
}