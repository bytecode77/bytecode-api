using BytecodeApi.Extensions;
using System.IO;
using System.Text;

namespace BytecodeApi.IO.FileSystem
{
	/// <summary>
	/// Provides <see langword="static" /> methods that extend the <see cref="Path" /> class.
	/// </summary>
	public static class PathEx
	{
		/// <summary>
		/// Returns the path of an existing file or directory in its original representation with fixed character casing. If it does not exist, the original <see cref="string" /> is returned.
		/// </summary>
		/// <param name="path">A <see cref="string" /> representing the path to a file or directory.</param>
		/// <returns>
		/// The path of an existing file or directory in its original representation with fixed character casing. If it does not exist, the original <see cref="string" /> is returned.
		/// </returns>
		public static string GetOriginalPath(string path)
		{
			Check.ArgumentNull(path, nameof(path));

			StringBuilder stringBuilder = new StringBuilder(260);

			Native.GetShortPathName(path, stringBuilder, stringBuilder.Capacity);
			int result = Native.GetLongPathName(stringBuilder.ToString(), stringBuilder, stringBuilder.Capacity);

			if (result > 0 && result < stringBuilder.Capacity)
			{
				path = stringBuilder.ToString(0, result);
				if (path[0] >= 'a' && path[0] <= 'z' && path[1] == ':') path = path[0].ToUpper() + path.Substring(1);
			}

			return path;
		}
	}
}