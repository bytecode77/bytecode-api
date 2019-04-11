using BytecodeApi.IO.FileSystem;
using System.IO;

namespace BytecodeApi.UI.Converters
{
	public sealed class PathConverter : ConverterBase<string, string, string>
	{
		public PathConverterResult ResultType { get; set; }

		public PathConverter(PathConverterResult resultType)
		{
			ResultType = resultType;
		}

		public override string Convert(string value, string parameter)
		{
			if (value == null)
			{
				return null;
			}
			else
			{
				switch (ResultType)
				{
					case PathConverterResult.FileName: return Path.GetFileName(value);
					case PathConverterResult.FileNameWithoutExtension: return Path.GetFileNameWithoutExtension(value);
					case PathConverterResult.Extension: return Path.GetExtension(value);
					case PathConverterResult.ExtensionWithoutDot: return Path.GetExtension(value).TrimStart('.');
					case PathConverterResult.DirectoryName: return Path.GetDirectoryName(value);
					case PathConverterResult.Root: return Path.GetPathRoot(value);
					case PathConverterResult.Combine: return Path.Combine(value, parameter);
					case PathConverterResult.ChangeExtension: return Path.ChangeExtension(value, parameter);
					case PathConverterResult.CaseSensitiveName:
						if (File.Exists(value)) return FileEx.GetCaseSensitiveName(value);
						else if (Directory.Exists(value)) return DirectoryEx.GetCaseSensitiveName(value);
						else return value;
					case PathConverterResult.CaseSensitiveFullName:
						if (File.Exists(value)) return FileEx.GetCaseSensitiveFullName(value);
						else if (Directory.Exists(value)) return DirectoryEx.GetCaseSensitiveFullName(value);
						else return value;
					default: throw Throw.InvalidEnumArgument(nameof(ResultType));
				}
			}
		}
	}
}