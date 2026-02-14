using BytecodeApi.Comparers;
using BytecodeApi.IO;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="DirectoryInfo" /> objects.
/// </summary>
public static class DirectoryInfoExtensions
{
	extension(DirectoryInfo directory)
	{
		/// <summary>
		/// Computes the size of this directory including all of its subdirectories.
		/// </summary>
		/// <returns>
		/// The total size in bytes of this directory.
		/// </returns>
		public long ComputeDirectorySize()
		{
			Check.ArgumentNull(directory);
			Check.DirectoryNotFound(directory.FullName);

			return directory.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
		}
		/// <summary>
		/// Compares two directories with an <see cref="CompareDirectoryOptions" /> parameter specifying the properties to compare.
		/// </summary>
		/// <param name="other">The other <see cref="DirectoryInfo" /> to compare to this <see cref="DirectoryInfo" />.</param>
		/// <param name="options">The <see cref="CompareDirectoryOptions" /> flags specifying what properties to compare.</param>
		/// <returns>
		/// A value indicating whether the directories are equal.
		/// </returns>
		public bool Compare(DirectoryInfo other, CompareDirectoryOptions options)
		{
			Check.ArgumentNull(directory);
			Check.DirectoryNotFound(directory.FullName);
			Check.ArgumentNull(other);
			Check.DirectoryNotFound(other.FullName);

			if (!options.HasFlag(CompareDirectoryOptions.IgnoreDirectories))
			{
				DirectoryInfo[] directoriesA = directory.GetDirectories().OrderBy(d => d.Name).ToArray();
				DirectoryInfo[] directoriesB = other.GetDirectories().OrderBy(d => d.Name).ToArray();

				if (directoriesA.Length == directoriesB.Length)
				{
					DelegateEqualityComparer<DirectoryInfo> comparer = new((a, b) =>
						CompareNames(a.Name, b.Name) &&
						(!options.HasFlag(CompareDirectoryOptions.CompareDirectoryLastWriteTime) || a.LastWriteTime == b.LastWriteTime) &&
						(!options.HasFlag(CompareDirectoryOptions.Recursive) || a.Compare(b, options))
					);

					if (!directoriesA.SequenceEqual(directoriesB, comparer))
					{
						return false;
					}
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
					DelegateEqualityComparer<FileInfo> comparer = new((a, b) =>
						CompareNames(a.Name, b.Name) &&
						(!options.HasFlag(CompareDirectoryOptions.CompareFileLastWriteTime) || a.LastWriteTime == b.LastWriteTime) &&
						(!options.HasFlag(CompareDirectoryOptions.CompareFileContents) || a.CompareContents(b)) &&
						(!options.HasFlag(CompareDirectoryOptions.CompareFileSize) || a.Length == b.Length)
					);

					if (!filesA.SequenceEqual(filesB, comparer))
					{
						return false;
					}
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
		/// <param name="destDirectoryName">The path of the new location including the top directory name.</param>
		public void CopyTo(string destDirectoryName)
		{
			directory.CopyTo(destDirectoryName, false);
		}
		/// <summary>
		/// Copies this directory to a new location including all files and subdirectories recursively.
		/// </summary>
		/// <param name="destDirectoryName">The path of the new location including the top directory name.</param>
		/// <param name="overwrite"><see langword="true" /> to overwrite contents. Existing files in the destination directory that do not exist in the source directory are not deleted.</param>
		public void CopyTo(string destDirectoryName, bool overwrite)
		{
			Check.ArgumentNull(directory);
			Check.DirectoryNotFound(directory.FullName);
			Check.ArgumentNull(destDirectoryName);

			Directory.CreateDirectory(destDirectoryName);

			foreach (DirectoryInfo subDirectory in directory.EnumerateDirectories())
			{
				subDirectory.CopyTo(Path.Combine(destDirectoryName, subDirectory.Name), overwrite);
			}

			foreach (FileInfo file in directory.EnumerateFiles())
			{
				file.CopyTo(Path.Combine(destDirectoryName, file.Name), overwrite);
			}
		}
		/// <summary>
		/// Deletes all files and directories from this directory, if it exists.
		/// </summary>
		public void DeleteContents()
		{
			directory.DeleteContents(false);
		}
		/// <summary>
		/// Deletes all files and directories from this directory, if it exists. If <paramref name="create" /> is set to <see langword="true" />, an empty directory will be created.
		/// </summary>
		/// <param name="create"><see langword="true" /> to create the directory, if it does not exist.</param>
		public void DeleteContents(bool create)
		{
			Check.ArgumentNull(directory);

			if (Directory.Exists(directory.FullName))
			{
				foreach (FileInfo file in directory.GetFiles())
				{
					file.Delete();
				}

				foreach (DirectoryInfo subDirectory in directory.GetDirectories())
				{
					subDirectory.Delete(true);
				}
			}
			else if (create)
			{
				directory.Create();
			}
		}
	}
}