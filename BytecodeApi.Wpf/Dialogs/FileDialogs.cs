using BytecodeApi.Extensions;
using BytecodeApi.IO;
using Microsoft.Win32;
using WinFormsDialogResult = System.Windows.Forms.DialogResult;
using WinFormsFolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace BytecodeApi.Wpf.Dialogs;

/// <summary>
/// Helper class for UI dialogs, such as Open and Save for files and directories.
/// </summary>
public static class FileDialogs
{
	/// <summary>
	/// Displays an open file dialog with a filter that allows any file to be opened. Returns a <see cref="string" /> representing the full path to the selected file and <see langword="null" />, if selection has been canceled by the user.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> representing the full path to the selected file and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string? Open()
	{
		return Open(null);
	}
	/// <summary>
	/// Displays an open file dialog with a filter that allows any file with one of the specified extensions to be opened. The extension description is retrieved using the <see cref="FileExtensionInfo" /> class. Returns a <see cref="string" /> representing the full path to the selected file and <see langword="null" />, if selection has been canceled by the user.
	/// </summary>
	/// <param name="extensions">The collection of extensions that are allowed to be opened. The extension description is retrieved using the <see cref="FileExtensionInfo" /> class.</param>
	/// <returns>
	/// A <see cref="string" /> representing the full path to the selected file and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string? Open(params string[]? extensions)
	{
		return Open(extensions, null);
	}
	/// <summary>
	/// Displays an open file dialog with a filter that allows any file with one of the specified extensions to be opened. Returns a <see cref="string" /> representing the full path to the selected file and <see langword="null" />, if selection has been canceled by the user.
	/// </summary>
	/// <param name="extensions">The collection of extensions that are allowed to be opened.</param>
	/// <param name="extensionsDescription">The description to be used. If <see langword="null" />, the <see cref="FileExtensionInfo" /> class is used to retrieve the description.</param>
	/// <returns>
	/// A <see cref="string" /> representing the full path to the selected file and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string? Open(string[]? extensions, string? extensionsDescription)
	{
		return Open(extensions, extensionsDescription, null);
	}
	/// <summary>
	/// Displays an open file dialog with a filter that allows any file with one of the specified extensions to be opened. Returns a <see cref="string" /> representing the full path to the selected file and <see langword="null" />, if selection has been canceled by the user.
	/// </summary>
	/// <param name="extensions">The collection of extensions that are allowed to be opened.</param>
	/// <param name="extensionsDescription">The description to be used. If <see langword="null" />, the <see cref="FileExtensionInfo" /> class is used to retrieve the description.</param>
	/// <param name="initialDirectory">A <see cref="string" /> specifying the initial directory.</param>
	/// <returns>
	/// A <see cref="string" /> representing the full path to the selected file and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string? Open(string[]? extensions, string? extensionsDescription, string? initialDirectory)
	{
		OpenFileDialog dialog = new()
		{
			Filter = GetFilter(extensions, extensionsDescription),
			InitialDirectory = initialDirectory ?? ""
		};

		return dialog.ShowDialog() == true ? dialog.FileName : null;
	}
	/// <summary>
	/// Displays an open file dialog with a filter that allows any file to be opened. Returns a <see cref="string" />[] representing the full path to all selected files and <see langword="null" />, if selection has been canceled by the user.
	/// </summary>
	/// <returns>
	/// A <see cref="string" />[] representing the full path to all selected files and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string[]? OpenMultiple()
	{
		return OpenMultiple(null);
	}
	/// <summary>
	/// Displays an open file dialog with a filter that allows any file with one of the specified extensions to be opened. The extension description is retrieved using the <see cref="FileExtensionInfo" /> class. Returns a <see cref="string" />[] representing the full path to all selected files and <see langword="null" />, if selection has been canceled by the user.
	/// </summary>
	/// <param name="extensions">The collection of extensions that are allowed to be opened. The extension description is retrieved using the <see cref="FileExtensionInfo" /> class.</param>
	/// <returns>
	/// A <see cref="string" />[] representing the full path to all selected files and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string[]? OpenMultiple(params string[]? extensions)
	{
		return OpenMultiple(extensions, null);
	}
	/// <summary>
	/// Displays an open file dialog with a filter that allows any file with one of the specified extensions to be opened. Returns a <see cref="string" />[] representing the full path to all selected files and <see langword="null" />, if selection has been canceled by the user.
	/// </summary>
	/// <param name="extensions">The collection of extensions that are allowed to be opened.</param>
	/// <param name="extensionsDescription">The description to be used. If <see langword="null" />, the <see cref="FileExtensionInfo" /> class is used to retrieve the description.</param>
	/// <returns>
	/// A <see cref="string" />[] representing the full path to all selected files and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string[]? OpenMultiple(string[]? extensions, string? extensionsDescription)
	{
		return OpenMultiple(extensions, extensionsDescription, null);
	}
	/// <summary>
	/// Displays an open file dialog with a filter that allows any file with one of the specified extensions to be opened. Returns a <see cref="string" />[] representing the full path to all selected files and <see langword="null" />, if selection has been canceled by the user.
	/// </summary>
	/// <param name="extensions">The collection of extensions that are allowed to be opened.</param>
	/// <param name="extensionsDescription">The description to be used. If <see langword="null" />, the <see cref="FileExtensionInfo" /> class is used to retrieve the description.</param>
	/// <param name="initialDirectory">A <see cref="string" /> specifying the initial directory.</param>
	/// <returns>
	/// A <see cref="string" />[] representing the full path to all selected files and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string[]? OpenMultiple(string[]? extensions, string? extensionsDescription, string? initialDirectory)
	{
		OpenFileDialog dialog = new()
		{
			Filter = GetFilter(extensions, extensionsDescription),
			Multiselect = true,
			InitialDirectory = initialDirectory ?? ""
		};

		return dialog.ShowDialog() == true ? dialog.FileNames : null;
	}
	/// <summary>
	/// Displays a folder browser dialog and returns a <see cref="string" /> representing the full path to the selected directory and <see langword="null" />, if selection has been canceled by the user.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> representing the full path to the selected directory and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string? OpenFolder()
	{
		return OpenFolder(null);
	}
	/// <summary>
	/// Displays a folder browser dialog and returns a <see cref="string" /> representing the full path to the selected directory and <see langword="null" />, if selection has been canceled by the user.
	/// </summary>
	/// <param name="initialDirectory">A <see cref="string" /> specifying the folder to start browsing from.</param>
	/// <returns>
	/// A <see cref="string" /> representing the full path to the selected directory and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string? OpenFolder(string? initialDirectory)
	{
		WinFormsFolderBrowserDialog dialog = new()
		{
			InitialDirectory = initialDirectory ?? ""
		};

		return dialog.ShowDialog() == WinFormsDialogResult.OK ? dialog.SelectedPath : null;
	}
	/// <summary>
	/// Displays an <see cref="IconPickerDialog" /> that allows an icon from a resource file to be selected. Returns a <see cref="string" /> representing the full path to the selected file and <see langword="null" />, if selection has been canceled by the user. The selected icon index is written to the <paramref name="index" /> parameter.
	/// </summary>
	/// <param name="index">When this method returns, a <see cref="string" /> that contains the index of the selected icon; otherwise, -1.</param>
	/// <returns>
	/// Returns a <see cref="string" /> representing the full path to the selected file and <see langword="null" />, if selection has been canceled by the user. The selected icon index is written to the <paramref name="index" /> parameter.
	/// </returns>
	public static string? SelectIcon(out int index)
	{
		IconPickerDialog dialog = new();
		if (dialog.ShowDialog() != true)
		{
			dialog.Reset();
		}

		index = dialog.IconIndex;
		return dialog.FileName;
	}
	/// <summary>
	/// Displays a save file dialog and automatically adds an extension to the filename, if the user omits an extension. The extension is taken from the <paramref name="fileName" /> parameter.
	/// </summary>
	/// <param name="fileName">A <see cref="string" /> specifying the initial filename that can be changed by the user.</param>
	/// <returns>
	/// A <see cref="string" /> representing the full path to the saved file and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string? Save(string? fileName)
	{
		return Save(fileName, null);
	}
	/// <summary>
	/// Displays a save file dialog and automatically adds an extension to the filename, if the user omits an extension. The extension description is retrieved using the <see cref="FileExtensionInfo" /> class.
	/// </summary>
	/// <param name="fileName">A <see cref="string" /> specifying the initial filename that can be changed by the user.</param>
	/// <param name="extension">A <see cref="string" /> specifying the extension to be added.</param>
	/// <returns>
	/// A <see cref="string" /> representing the full path to the saved file and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string? Save(string? fileName, string? extension)
	{
		return Save(fileName, extension, null);
	}
	/// <summary>
	/// Displays a save file dialog and automatically adds an extension to the filename, if the user omits an extension.
	/// </summary>
	/// <param name="fileName">A <see cref="string" /> specifying the initial filename that can be changed by the user.</param>
	/// <param name="extension">A <see cref="string" /> specifying the extension to be added.</param>
	/// <param name="extensionDescription">The description to be used. If <see langword="null" />, the <see cref="FileExtensionInfo" /> class is used to retrieve the description.</param>
	/// <returns>
	/// A <see cref="string" /> representing the full path to the saved file and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string? Save(string? fileName, string? extension, string? extensionDescription)
	{
		return Save(fileName, extension, extensionDescription, null);
	}
	/// <summary>
	/// Displays a save file dialog and automatically adds an extension to the filename, if the user omits an extension.
	/// </summary>
	/// <param name="fileName">A <see cref="string" /> specifying the initial filename that can be changed by the user.</param>
	/// <param name="extension">A <see cref="string" /> specifying the extension to be added.</param>
	/// <param name="extensionDescription">The description to be used. If <see langword="null" />, the <see cref="FileExtensionInfo" /> class is used to retrieve the description.</param>
	/// <param name="initialDirectory">A <see cref="string" /> specifying the initial directory.</param>
	/// <returns>
	/// A <see cref="string" /> representing the full path to the saved file and <see langword="null" />, if selection has been canceled by the user.
	/// </returns>
	public static string? Save(string? fileName, string? extension, string? extensionDescription, string? initialDirectory)
	{
		extension ??= Path.GetExtension(fileName).ToNullIfEmpty();
		extension = extension?.TrimStart('.');

		SaveFileDialog dialog = new()
		{
			FileName = fileName ?? "",
			Filter = GetFilter([extension], extensionDescription),
			DefaultExt = extension,
			AddExtension = extension != null,
			InitialDirectory = initialDirectory ?? ""
		};

		if (dialog.ShowDialog() == true)
		{
			string result = dialog.FileName;

			if (extension != null && !result.EndsWith($".{extension}", StringComparison.OrdinalIgnoreCase))
			{
				result = $"{result.TrimEnd('.')}.{extension}";
			}

			return result;
		}
		else
		{
			return null;
		}
	}

	private static string GetFilter(string?[]? extensions, string? extensionsDescription)
	{
		if (extensions.IsNullOrEmpty())
		{
			return (extensionsDescription ?? "All Files") + "|*.*";
		}
		else if (extensions.Length == 1)
		{
			return (extensionsDescription ?? new FileExtensionInfo(extensions.First() ?? "").FriendlyDocName) + "|*." + extensions.First()?.ToLower().TrimStart('.');
		}
		else
		{
			return (extensionsDescription ?? "Miscellaneous Files") + "|" + extensions.Select(extension => "*." + extension?.TrimStart('.')).AsString(";");
		}
	}
}