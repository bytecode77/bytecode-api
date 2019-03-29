using System.IO;

namespace BytecodeApi.UI.Converters
{
	/// <summary>
	/// Represents the converter that converts paths represented as a <see cref="string" /> to the directory name part.
	/// </summary>
	public sealed class PathToDirectoryNameConverter : ConverterBase<string, string>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PathToDirectoryNameConverter" /> class.
		/// </summary>
		public PathToDirectoryNameConverter()
		{
		}

		/// <summary>
		/// Converts a path represented as a <see cref="string" /> to the directory name part.
		/// </summary>
		/// <param name="value">The <see cref="string" /> value to convert.</param>
		/// <returns>
		/// An equivalent <see cref="string" /> representation of the specified value.
		/// </returns>
		public override string Convert(string value)
		{
			return Path.GetDirectoryName(value);
		}
	}
}