using BytecodeApi.Comparers;
using BytecodeApi.IO.FileSystem;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="DirectoryInfo" /> objects.
	/// </summary>
	public static class DirectoryInfoExtensions
	{
		/// <summary>
		/// Gets the UNC path of this directory. If the path cannot be converted to a UNC path, the original path is returned.
		/// </summary>
		/// <param name="directory">The <see cref="DirectoryInfo" /> to process.</param>
		/// <returns>
		/// The UNC path of this directory, if conversion is possible;
		/// otherwise, the original path.
		/// </returns>
		public static string ToUncPath(this DirectoryInfo directory)
		{
			Check.ArgumentNull(directory, nameof(directory));
			Check.DirectoryNotFound(directory.FullName);

			string path = directory.FullName;

			if (path.Length > 2 && path[1] == ':' && path[0].ToUpper() >= 'A' && path[0].ToUpper() <= 'Z')
			{
				StringBuilder result = new StringBuilder(512);
				int length = result.Capacity;
				if (Native.WNetGetConnection(path.Left(2), result, ref length) == 0)
				{
					return Path.Combine(result.ToString().TrimEnd(), path.Substring(Path.GetPathRoot(path).Length));
				}
			}

			return path;
		}
		/// <summary>
		/// Computes the size of this directory including all of its subdirectories.
		/// </summary>
		/// <param name="directory">The <see cref="DirectoryInfo" /> to process.</param>
		/// <returns>
		/// The total size in bytes of this directory.
		/// </returns>
		public static long ComputeDirectorySize(this DirectoryInfo directory)
		{
			Check.ArgumentNull(directory, nameof(directory));
			Check.DirectoryNotFound(directory.FullName);

			return directory.EnumerateFiles("*", System.IO.SearchOption.AllDirectories).Sum(file => file.Length);
		}
		/// <summary>
		/// Compares two directories with an <see cref="CompareDirectoryOptions" /> parameter specifying the properties to compare.
		/// </summary>
		/// <param name="directory">The <see cref="DirectoryInfo" /> to process.</param>
		/// <param name="other">The other <see cref="DirectoryInfo" /> to compare to <paramref name="directory" />.</param>
		/// <param name="options">The <see cref="CompareDirectoryOptions" /> flags specifying what properties to compare.</param>
		/// <returns>
		/// A value indicating whether the directories are equal.
		/// </returns>
		public static bool Compare(this DirectoryInfo directory, DirectoryInfo other, CompareDirectoryOptions options)
		{
			Check.ArgumentNull(directory, nameof(directory));
			Check.DirectoryNotFound(directory.FullName);
			Check.ArgumentNull(other, nameof(other));
			Check.DirectoryNotFound(other.FullName);

			if (!options.HasFlag(CompareDirectoryOptions.IgnoreDirectories))
			{
				DirectoryInfo[] directoriesA = directory.GetDirectories().OrderBy(d => d.Name).ToArray();
				DirectoryInfo[] directoriesB = other.GetDirectories().OrderBy(d => d.Name).ToArray();

				if (directoriesA.Length == directoriesB.Length)
				{
					DelegateEqualityComparer<DirectoryInfo> comparer = new DelegateEqualityComparer<DirectoryInfo>((a, b) =>
						CompareNames(a.Name, b.Name) &&
						(!options.HasFlag(CompareDirectoryOptions.CompareDirectoryLastWriteTime) || a.LastWriteTime == b.LastWriteTime) &&
						(!options.HasFlag(CompareDirectoryOptions.Recursive) || a.Compare(b, options))
					);

					if (!directoriesA.SequenceEqual(directoriesB, comparer)) return false;
				}
				else
				{
					return false;
				}
			}

			if (!options.HasFlag(CompareDirectoryOptions.IgnoreFiles))
			{
				FileInfo[] filesA = directory.GetFiles().OrderBy(f => f.Name).ToArray();
				FileInfo[] filesB = other.GetFiles().OrderBy(f => f.Name).ToArray();

				if (filesA.Length == filesB.Length)
				{
					DelegateEqualityComparer<FileInfo> comparer = new DelegateEqualityComparer<FileInfo>((a, b) =>
						CompareNames(a.Name, b.Name) &&
						(!options.HasFlag(CompareDirectoryOptions.CompareFileLastWriteTime) || a.LastWriteTime == b.LastWriteTime) &&
						(!options.HasFlag(CompareDirectoryOptions.CompareFileContents) || a.CompareContents(b)) &&
						(!options.HasFlag(CompareDirectoryOptions.CompareFileSize) || a.Length == b.Length)
					);

					if (!filesA.SequenceEqual(filesB, comparer)) return false;
				}
				else
				{
					return false;
				}
			}

			return true;

			bool CompareNames(string nameA, string nameB)
			{
				return nameA.Equals(nameB, options.HasFlag(CompareDirectoryOptions.IgnoreCase) ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
			}
		}
		/// <summary>
		/// Copies this directory to a new location including all files and subdirectories recursively.
		/// </summary>
		/// <param name="directory">The <see cref="DirectoryInfo" /> to process.</param>
		/// <param name="destDirectoryName">The path of the new location including the top directory name.</param>
		public static void CopyTo(this DirectoryInfo directory, string destDirectoryName)
		{
			directory.CopyTo(destDirectoryName, false);
		}
		/// <summary>
		/// Copies this directory to a new location including all files and subdirectories recursively.
		/// </summary>
		/// <param name="directory">The <see cref="DirectoryInfo" /> to process.</param>
		/// <param name="destDirectoryName">The path of the new location including the top directory name.</param>
		/// <param name="overwrite"><see langword="true" /> to overwrite contents. Existing files in the destination directory that do not exist in the source directory are not deleted.</param>
		public static void CopyTo(this DirectoryInfo directory, string destDirectoryName, bool overwrite)
		{
			Check.ArgumentNull(directory, nameof(directory));
			Check.DirectoryNotFound(directory.FullName);
			Check.ArgumentNull(destDirectoryName, nameof(destDirectoryName));

			Directory.CreateDirectory(destDirectoryName);
			foreach (DirectoryInfo subDirectory in directory.EnumerateDirectories()) subDirectory.CopyTo(Path.Combine(destDirectoryName, subDirectory.Name), overwrite);
			foreach (FileInfo file in directory.EnumerateFiles()) file.CopyTo(Path.Combine(destDirectoryName, file.Name), overwrite);
		}
		/// <summary>
		/// Deletes all files and directories from this directory, if it exists.
		/// </summary>
		/// <param name="directory">The <see cref="DirectoryInfo" /> to process.</param>
		public static void DeleteContents(this DirectoryInfo directory)
		{
			directory.DeleteContents(false);
		}
		/// <summary>
		/// Deletes all files and directories from this directory, if it exists. If <paramref name="create" /> is set to <see langword="true" />, an empty directory will be created.
		/// </summary>
		/// <param name="directory">The <see cref="DirectoryInfo" /> to process.</param>
		/// <param name="create"><see langword="true" /> to create the directory, if it does not exist.</param>
		public static void DeleteContents(this DirectoryInfo directory, bool create)
		{
			Check.ArgumentNull(directory, nameof(directory));

			if (Directory.Exists(directory.FullName))
			{
				foreach (FileInfo file in directory.GetFiles()) file.Delete();
				foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
			}
			else if (create)
			{
				directory.Create();
			}
		}
	}
}