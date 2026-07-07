using BytecodeApi.Extensions;
using BytecodeApi.IO;
using Microsoft.Win32;
using System.Windows;

namespace BytecodeApi.Wpf.Dialogs;

/// <summary>
/// Class to display UI dialogs, such as Open and Save for files and directories.
/// </summary>
public static class FileDialogs
{
	/// <summary>
	/// Creates an open file dialog.
	/// </summary>
	/// <returns>
	/// A <see cref="OpenFileDialogBuilder" /> that can be used to configure and display the dialog.
	/// </returns>
	public static OpenFileDialogBuilder Open()
	{
		return new();
	}
	/// <summary>
	/// Creates an open file dialog that opens multiple files.
	/// </summary>
	/// <returns>
	/// A <see cref="OpenMultipleFilesDialogBuilder" /> that can be used to configure and display the dialog.
	/// </returns>
	public static OpenMultipleFilesDialogBuilder OpenMultiple()
	{
		return new();
	}
	/// <summary>
	/// Creates an open folder dialog.
	/// </summary>
	/// <returns>
	/// A <see cref="OpenFolderDialogBuilder" /> that can be used to configure and display the dialog.
	/// </returns>
	public static OpenFolderDialogBuilder OpenFolder()
	{
		return new();
	}
	/// <summary>
	/// Creates an open folder dialog that opens multiple folders.
	/// </summary>
	/// <returns>
	/// A <see cref="OpenMultipleFoldersDialogBuilder" /> that can be used to configure and display the dialog.
	/// </returns>
	public static OpenMultipleFoldersDialogBuilder OpenMultipleFolders()
	{
		return new();
	}
	/// <summary>
	/// Creates an icon selection dialog.
	/// </summary>
	/// <returns>
	/// A <see cref="SelectIconDialogBuilder" /> that can be used to configure and display the dialog.
	/// </returns>
	public static SelectIconDialogBuilder SelectIcon()
	{
		return new();
	}
	/// <summary>
	/// Creates a save file dialog.
	/// </summary>
	/// <returns>
	/// A <see cref="SaveFileDialogBuilder" /> that can be used to configure and display the dialog.
	/// </returns>
	public static SaveFileDialogBuilder Save()
	{
		return new();
	}

	private static string GetFilter(IEnumerable<DialogFileType> fileTypes)
	{
		return fileTypes.Any()
			? fileTypes.Select(fileType => GetFilter(fileType.Extensions, fileType.Description)).AsString("|")
			: GetFilter(null, null);
	}
	private static string GetFilter(string?[]? extensions, string? description)
	{
		if (extensions.IsNullOrEmpty())
		{
			return $"{description ?? "All Files"}|*.*";
		}
		else
		{
			string[] descriptions = description != null
				? [description]
				: extensions.Select(extension => new FileExtensionInfo(extension ?? "").FriendlyDocName).ExceptNull().Distinct().ToArray();

			return $"{(descriptions.Length == 1 ? descriptions.First() : "Miscellaneous Files")}|{extensions.Select(extension => $"*.{NormalizeExtension(extension)}").AsString(";")}";
		}
	}
	private static string? NormalizeExtension(string? extension)
	{
		return extension.ToNullIfEmptyOrWhiteSpace()?.Trim().TrimStart('.').ToLower();
	}
	private static bool ShowDialog(CommonDialog dialog, Window? owner)
	{
		if (owner != null)
		{
			return dialog.ShowDialog(owner) == true;
		}
		else
		{
			return dialog.ShowDialog() == true;
		}
	}

	/// <summary>
	/// Provides a fluent builder for configuring and displaying an open file dialog.
	/// </summary>
	public sealed class OpenFileDialogBuilder
	{
		private Window? _Owner;
		private readonly List<DialogFileType> _FileTypes;
		private string? _InitialDirectory;

		internal OpenFileDialogBuilder()
		{
			_FileTypes = [];
		}

		/// <summary>
		/// Sets the owner of the dialog.
		/// </summary>
		/// <param name="owner">A <see cref="Window" /> to use as the owner of the dialog, or <see langword="null" /> to not specify an owner.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenFileDialogBuilder Owner(Window? owner)
		{
			_Owner = owner;
			return this;
		}
		/// <summary>
		/// Specifies the file extensions that are allowed to be opened.
		/// This method can be called multiple times to specify multiple sets of extensions to choose from.
		/// </summary>
		/// <param name="extensions">The extensions that are allowed to be opened.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenFileDialogBuilder FileType(params string[] extensions)
		{
			return FileType(extensions, null);
		}
		/// <summary>
		/// Specifies the file extensions that are allowed to be opened.
		/// This method can be called multiple times to specify multiple sets of extensions to choose from.
		/// </summary>
		/// <param name="extensions">The extensions that are allowed to be opened.</param>
		/// <param name="description">The description to be used. If set to <see langword="null" />, the description is retrieved automatically from the shell.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenFileDialogBuilder FileType(string[] extensions, string? description)
		{
			Check.ArgumentNull(extensions);
			Check.ArgumentEx.ArrayElementsRequired(extensions);
			Check.ArgumentEx.ArrayValuesNotNull(extensions);
			Check.ArgumentEx.ArrayValuesNotStringEmptyOrWhiteSpace(extensions);

			_FileTypes.Add(new(extensions.Select(extension => NormalizeExtension(extension)!).ToArray(), description));
			return this;
		}
		/// <summary>
		/// Sets the initial directory for the dialog. If set to <see langword="null" />, the dialog will open in the last used directory or a default directory determined by the system.
		/// </summary>
		/// <param name="initialDirectory">A <see cref="string" /> specifying the initial directory for the dialog.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenFileDialogBuilder InitialDirectory(string? initialDirectory)
		{
			_InitialDirectory = initialDirectory;
			return this;
		}
		/// <summary>
		/// Displays the dialog. If the user clicks the OK button, this method returns <see langword="true" /> and the selected file name is returned in the <paramref name="fileName" /> parameter.
		/// </summary>
		/// <param name="fileName">When this method returns, contains the selected file name if the user clicked the OK button; otherwise, <see langword="null" />.</param>
		/// <returns>
		/// <see langword="true" />, if the user clicked the OK button;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Show([NotNullWhen(true)] out string? fileName)
		{
			OpenFileDialog dialog = new()
			{
				Filter = GetFilter(_FileTypes),
				InitialDirectory = _InitialDirectory ?? ""
			};

			if (ShowDialog(dialog, _Owner))
			{
				fileName = dialog.FileName;
				return true;
			}
			else
			{
				fileName = null;
				return false;
			}
		}
	}

	/// <summary>
	/// Provides a fluent builder for configuring and displaying an open file dialog that opens multiple files.
	/// </summary>
	public sealed class OpenMultipleFilesDialogBuilder
	{
		private Window? _Owner;
		private readonly List<DialogFileType> _FileTypes;
		private string? _InitialDirectory;

		internal OpenMultipleFilesDialogBuilder()
		{
			_FileTypes = [];
		}

		/// <summary>
		/// Sets the owner of the dialog.
		/// </summary>
		/// <param name="owner">A <see cref="Window" /> to use as the owner of the dialog, or <see langword="null" /> to not specify an owner.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenMultipleFilesDialogBuilder Owner(Window? owner)
		{
			_Owner = owner;
			return this;
		}
		/// <summary>
		/// Specifies the file extensions that are allowed to be opened.
		/// This method can be called multiple times to specify multiple sets of extensions to choose from.
		/// </summary>
		/// <param name="extensions">The extensions that are allowed to be opened.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenMultipleFilesDialogBuilder FileType(params string[] extensions)
		{
			return FileType(extensions, null);
		}
		/// <summary>
		/// Specifies the file extensions that are allowed to be opened.
		/// This method can be called multiple times to specify multiple sets of extensions to choose from.
		/// </summary>
		/// <param name="extensions">The extensions that are allowed to be opened.</param>
		/// <param name="description">The description to be used. If set to <see langword="null" />, the description is retrieved automatically from the shell.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenMultipleFilesDialogBuilder FileType(string[] extensions, string? description)
		{
			Check.ArgumentNull(extensions);
			Check.ArgumentEx.ArrayElementsRequired(extensions);
			Check.ArgumentEx.ArrayValuesNotNull(extensions);
			Check.ArgumentEx.ArrayValuesNotStringEmptyOrWhiteSpace(extensions);

			_FileTypes.Add(new(extensions.Select(extension => NormalizeExtension(extension)!).ToArray(), description));
			return this;
		}
		/// <summary>
		/// Sets the initial directory for the dialog. If set to <see langword="null" />, the dialog will open in the last used directory or a default directory determined by the system.
		/// </summary>
		/// <param name="initialDirectory">A <see cref="string" /> specifying the initial directory for the dialog.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenMultipleFilesDialogBuilder InitialDirectory(string? initialDirectory)
		{
			_InitialDirectory = initialDirectory;
			return this;
		}
		/// <summary>
		/// Displays the dialog. If the user clicks the OK button, this method returns <see langword="true" /> and the selected file names are returned in the <paramref name="fileNames" /> parameter.
		/// </summary>
		/// <param name="fileNames">When this method returns, contains the selected file names if the user clicked the OK button; otherwise, an empty array.</param>
		/// <returns>
		/// <see langword="true" />, if the user clicked the OK button;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Show(out string[] fileNames)
		{
			OpenFileDialog dialog = new()
			{
				Filter = GetFilter(_FileTypes),
				Multiselect = true,
				InitialDirectory = _InitialDirectory ?? ""
			};

			if (ShowDialog(dialog, _Owner))
			{
				fileNames = dialog.FileNames;
				return true;
			}
			else
			{
				fileNames = [];
				return false;
			}
		}
	}

	/// <summary>
	/// Provides a fluent builder for configuring and displaying an open folder dialog.
	/// </summary>
	public sealed class OpenFolderDialogBuilder
	{
		private Window? _Owner;
		private string? _InitialDirectory;

		internal OpenFolderDialogBuilder()
		{
		}

		/// <summary>
		/// Sets the owner of the dialog.
		/// </summary>
		/// <param name="owner">A <see cref="Window" /> to use as the owner of the dialog, or <see langword="null" /> to not specify an owner.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenFolderDialogBuilder Owner(Window? owner)
		{
			_Owner = owner;
			return this;
		}
		/// <summary>
		/// Sets the initial directory for the dialog. If set to <see langword="null" />, the dialog will open in the last used directory or a default directory determined by the system.
		/// </summary>
		/// <param name="initialDirectory">A <see cref="string" /> specifying the initial directory for the dialog.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenFolderDialogBuilder InitialDirectory(string? initialDirectory)
		{
			_InitialDirectory = initialDirectory;
			return this;
		}
		/// <summary>
		/// Displays the dialog. If the user clicks the OK button, this method returns <see langword="true" /> and the selected folder path is returned in the <paramref name="path" /> parameter.
		/// </summary>
		/// <param name="path">When this method returns, contains the selected folder path if the user clicked the OK button; otherwise, <see langword="null" />.</param>
		/// <returns>
		/// <see langword="true" />, if the user clicked the OK button;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Show([NotNullWhen(true)] out string? path)
		{
			OpenFolderDialog dialog = new()
			{
				InitialDirectory = _InitialDirectory ?? ""
			};

			if (ShowDialog(dialog, _Owner))
			{
				path = dialog.FolderName;
				return true;
			}
			else
			{
				path = null;
				return false;
			}
		}
	}

	/// <summary>
	/// Provides a fluent builder for configuring and displaying an open folder dialog that opens multiple folders.
	/// </summary>
	public sealed class OpenMultipleFoldersDialogBuilder
	{
		private Window? _Owner;
		private string? _InitialDirectory;

		internal OpenMultipleFoldersDialogBuilder()
		{
		}

		/// <summary>
		/// Sets the owner of the dialog.
		/// </summary>
		/// <param name="owner">A <see cref="Window" /> to use as the owner of the dialog, or <see langword="null" /> to not specify an owner.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenMultipleFoldersDialogBuilder Owner(Window? owner)
		{
			_Owner = owner;
			return this;
		}
		/// <summary>
		/// Sets the initial directory for the dialog. If set to <see langword="null" />, the dialog will open in the last used directory or a default directory determined by the system.
		/// </summary>
		/// <param name="initialDirectory">A <see cref="string" /> specifying the initial directory for the dialog.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public OpenMultipleFoldersDialogBuilder InitialDirectory(string? initialDirectory)
		{
			_InitialDirectory = initialDirectory;
			return this;
		}
		/// <summary>
		/// Displays the dialog. If the user clicks the OK button, this method returns <see langword="true" /> and the selected folder paths are returned in the <paramref name="paths" /> parameter.
		/// </summary>
		/// <param name="paths">When this method returns, contains the selected folder paths if the user clicked the OK button; otherwise, an empty array.</param>
		/// <returns>
		/// <see langword="true" />, if the user clicked the OK button;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Show(out string[] paths)
		{
			OpenFolderDialog dialog = new()
			{
				Multiselect = true,
				InitialDirectory = _InitialDirectory ?? ""
			};

			if (ShowDialog(dialog, _Owner))
			{
				paths = dialog.FolderNames;
				return true;
			}
			else
			{
				paths = [];
				return false;
			}
		}
	}

	/// <summary>
	/// Provides a fluent builder for configuring and displaying an icon selection dialog.
	/// </summary>
	public sealed class SelectIconDialogBuilder
	{
		private Window? _Owner;
		private string? _FileName;

		internal SelectIconDialogBuilder()
		{
		}

		/// <summary>
		/// Sets the owner of the dialog.
		/// </summary>
		/// <param name="owner">A <see cref="Window" /> to use as the owner of the dialog, or <see langword="null" /> to not specify an owner.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public SelectIconDialogBuilder Owner(Window? owner)
		{
			_Owner = owner;
			return this;
		}
		/// <summary>
		/// Sets the initial filename of the icon selection dialog.
		/// </summary>
		/// <param name="fileName">The initial filename of the icon selection dialog.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public SelectIconDialogBuilder FileName(string? fileName)
		{
			_FileName = fileName;
			return this;
		}
		/// <summary>
		/// Displays the dialog. If the user clicks the OK button, this method returns <see langword="true" />, the selected file name is returned in the <paramref name="fileName" /> parameter, and the selected icon index is returned in the <paramref name="index" /> parameter.
		/// </summary>
		/// <param name="fileName">When this method returns, contains the selected file name if the user clicked the OK button; otherwise, <see langword="null" />.</param>
		/// <param name="index">When this method returns, contains the selected icon index if the user clicked the OK button; otherwise, 0.</param>
		/// <returns>
		/// <see langword="true" />, if the user clicked the OK button;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Show([NotNullWhen(true)] out string? fileName, out int index)
		{
			IconPickerDialog dialog = new()
			{
				FileName = _FileName
			};

			if (ShowDialog(dialog, _Owner) && dialog.FileName != null)
			{
				fileName = dialog.FileName;
				index = dialog.IconIndex;
				return true;
			}
			else
			{
				fileName = null;
				index = 0;
				return false;
			}
		}
	}

	/// <summary>
	/// Provides a fluent builder for configuring and displaying a save file dialog.
	/// </summary>
	public sealed class SaveFileDialogBuilder
	{
		private Window? _Owner;
		private string? _FileName;
		private readonly List<DialogFileType> _FileTypes;
		private string? _InitialDirectory;

		internal SaveFileDialogBuilder()
		{
			_FileTypes = [];
		}

		/// <summary>
		/// Sets the owner of the dialog.
		/// </summary>
		/// <param name="owner">A <see cref="Window" /> to use as the owner of the dialog, or <see langword="null" /> to not specify an owner.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public SaveFileDialogBuilder Owner(Window? owner)
		{
			_Owner = owner;
			return this;
		}
		/// <summary>
		/// Sets the initial filename of the save file dialog.
		/// </summary>
		/// <param name="fileName">The initial filename of the save file dialog.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public SaveFileDialogBuilder FileName(string? fileName)
		{
			_FileName = fileName;
			return this;
		}
		/// <summary>
		/// Specifies the file extensions that are allowed to be saved.
		/// This method can be called multiple times to specify multiple sets of extensions to choose from.
		/// If no extension is specified, the extension of the initial file name is used.
		/// </summary>
		/// <param name="extensions">The extensions that are allowed to be saved.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public SaveFileDialogBuilder FileType(params string[] extensions)
		{
			return FileType(extensions, null);
		}
		/// <summary>
		/// Specifies the file extensions that are allowed to be saved.
		/// This method can be called multiple times to specify multiple sets of extensions to choose from.
		/// If no extension is specified, the extension of the initial file name is used.
		/// </summary>
		/// <param name="extensions">The extensions that are allowed to be saved.</param>
		/// <param name="description">The description to be used. If set to <see langword="null" />, the description is retrieved automatically from the shell.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public SaveFileDialogBuilder FileType(string[] extensions, string? description)
		{
			Check.ArgumentNull(extensions);
			Check.ArgumentEx.ArrayElementsRequired(extensions);
			Check.ArgumentEx.ArrayValuesNotNull(extensions);
			Check.ArgumentEx.ArrayValuesNotStringEmptyOrWhiteSpace(extensions);

			_FileTypes.Add(new(extensions.Select(extension => NormalizeExtension(extension)!).ToArray(), description));
			return this;
		}
		/// <summary>
		/// Sets the initial directory for the dialog. If set to <see langword="null" />, the dialog will open in the last used directory or a default directory determined by the system.
		/// </summary>
		/// <param name="initialDirectory">A <see cref="string" /> specifying the initial directory for the dialog.</param>
		/// <returns>
		/// A reference to this instance after the operation has completed.
		/// </returns>
		public SaveFileDialogBuilder InitialDirectory(string? initialDirectory)
		{
			_InitialDirectory = initialDirectory;
			return this;
		}
		/// <summary>
		/// Displays the dialog. If the user clicks the OK button, this method returns <see langword="true" /> and the selected file name is returned in the <paramref name="fileName" /> parameter.
		/// </summary>
		/// <param name="fileName">When this method returns, contains the selected file name if the user clicked the OK button; otherwise, <see langword="null" />.</param>
		/// <returns>
		/// <see langword="true" />, if the user clicked the OK button;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Show([NotNullWhen(true)] out string? fileName)
		{
			DialogFileType[] fileTypes = _FileTypes.Any()
				? _FileTypes.ToArray()
				: NormalizeExtension(Path.GetExtension(_FileName)) is string fileNameExtension
				? [new([fileNameExtension], null)]
				: [];

			SaveFileDialog dialog = new()
			{
				FileName = _FileName ?? "",
				Filter = GetFilter(fileTypes),
				InitialDirectory = _InitialDirectory ?? ""
			};

			if (ShowDialog(dialog, _Owner))
			{
				string result = dialog.FileName;
				string[] allExtensions = fileTypes.SelectMany(fileType => fileType.Extensions).ToArray();
				string? selectedExtension = dialog.FilterIndex > 0 && dialog.FilterIndex <= fileTypes.Length ? fileTypes[dialog.FilterIndex - 1].Extensions.First() : null;

				if (selectedExtension != null &&
					allExtensions.Any() &&
					allExtensions.None(extension => result.EndsWith($".{extension}", StringComparison.OrdinalIgnoreCase)))
				{
					result = $"{result.TrimEnd('.')}.{selectedExtension}";
				}

				fileName = result;
				return true;
			}
			else
			{
				fileName = null;
				return false;
			}
		}
	}

	private sealed class DialogFileType
	{
		public string[] Extensions { get; }
		public string? Description { get; }

		public DialogFileType(string[] extensions, string? description)
		{
			Extensions = extensions;
			Description = description;
		}
	}
}