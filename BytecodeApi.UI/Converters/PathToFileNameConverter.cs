using System.IO;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts paths represented as a <see cref="string" /> to the filename part. If the given <see cref="bool" /> parameter is <see langword="true" />, the filename part without the extension is returned.
	/// </summary>
	public sealed class PathToFileNameConverter : ConverterBase<string, bool?, string>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PathToFileNameConverter" /> class.
		/// </summary>
		public PathToFileNameConverter()
		{
		}

		/// <summary>
		/// Converts a path represented as a <see cref="string" /> to the filename part. If the given <see cref="bool" /> parameter is <see langword="true" />, the filename part without the extension is returned.
		/// </summary>
		/// <param name="value">The <see cref="string" /> value to convert.</param>
		/// <param name="parameter"><see langword="true" /> to return the filename part without the extension.</param>
		/// <returns>
		/// An equivalent <see cref="string" /> representation of the specified value.
		/// </returns>
		public override string Convert(string value, bool? parameter)
		{
			return parameter == true ? Path.GetFileNameWithoutExtension(value) : Path.GetFileName(value);
		}
	}
}