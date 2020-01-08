using BytecodeApi.IO.FileSystem;
using System.IO;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts path strings. The <see cref="Convert(string, string)" /> method returns a <see cref="string" /> based on the specified <see cref="PathConverterMethod" /> parameter.
	/// </summary>
	public sealed class PathConverter : ConverterBase<string, string, string>
	{
		/// <summary>
		/// Specifies the method that is used to convert the path in the <see cref="string" /> value.
		/// </summary>
		public PathConverterMethod Method { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="PathConverter" /> class with the specified conversion method.
		/// </summary>
		/// <param name="method">The method that is used to convert the path in the <see cref="string" /> value.</param>
		public PathConverter(PathConverterMethod method)
		{
			Method = method;
		}

		/// <summary>
		/// Converts the path in the <see cref="string" /> value based on the specified <see cref="PathConverterMethod" /> parameter.
		/// </summary>
		/// <param name="value">The <see cref="string" /> value to convert.</param>
		/// <param name="parameter">A parameter <see cref="string" /> that specifies the parameter used in some of the <see cref="PathConverterMethod" /> methods.</param>
		/// <returns>
		/// A <see cref="string" /> with the result of the conversion.
		/// </returns>
		public override string Convert(string value, string parameter)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				return Method switch
				{
					PathConverterMethod.FileName => Path.GetFileName(value),
					PathConverterMethod.FileNameWithoutExtension => Path.GetFileNameWithoutExtension(value),
					PathConverterMethod.Extension => Path.GetExtension(value),
					PathConverterMethod.ExtensionWithoutDot => Path.GetExtension(value).TrimStart('.'),
					PathConverterMethod.DirectoryName => Path.GetDirectoryName(value),
					PathConverterMethod.Root => Path.GetPathRoot(value),
					PathConverterMethod.Combine => Path.Combine(value, parameter),
					PathConverterMethod.ChangeExtension => Path.ChangeExtension(value, parameter),
					PathConverterMethod.OriginalPath => PathEx.GetOriginalPath(value),
					_ => throw Throw.InvalidEnumArgument(nameof(Method), Method)
				};
			}
		}
	}
}