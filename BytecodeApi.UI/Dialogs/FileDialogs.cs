using BytecodeApi.Extensions;
using BytecodeApi.IO.SystemInfo;
using Microsoft.Win32;
using System.IO;
using System.Linq;
using WinFormsDialogResult = System.Windows.Forms.DialogResult;
using WinFormsFolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;

namespace BytecodeApi.UI.Dialogs
{
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
		public static string Open()
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
		public static string Open(params string[] extensions)
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
		public static string Open(string[] extensions, string extensionsDescription)
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Filter = GetFilter(extensions, extensionsDescription)
			};
			return dialog.ShowDialog() == true ? dialog.FileName : null;
		}
		/// <summary>
		/// Displays an open file dialog with a filter that allows any file to be opened. Returns a <see cref="string" />[] representing the full path to all selected files and <see langword="null" />, if selection has been canceled by the user.
		/// </summary>
		/// <returns>
		/// A <see cref="string" />[] representing the full path to all selected files and <see langword="null" />, if selection has been canceled by the user.
		/// </returns>
		public static string[] OpenMultiple()
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
		public static string[] OpenMultiple(params string[] extensions)
		{
			OpenFileDialog dialog = new OpenFileDialog
			{
				Filter = GetFilter(extensions),
				Multiselect = true
			};
			return dialog.ShowDialog() == true ? dialog.FileNames : null;
		}
		/// <summary>
		/// Displays a folder browser dialog and returns a <see cref="string" /> representing the full path to the selected directory and <see langword="null" />, if selection has been canceled by the user.
		/// </summary>
		/// <returns>
		/// A <see cref="string" /> representing the full path to the selected directory and <see langword="null" />, if selection has been canceled by the user.
		/// </returns>
		public static string OpenFolder()
		{
			return OpenFolder(null);
		}
		/// <summary>
		/// Displays a folder browser dialog, starting from the specified root folder, and returns a <see cref="string" /> representing the full path to the selected directory and <see langword="null" />, if selection has been canceled by the user.
		/// </summary>
		/// <param name="rootFolder">A <see cref="string" /> specifying the root folder to start browsing from.</param>
		/// <returns>
		/// A <see cref="string" /> representing the full path to the selected directory and <see langword="null" />, if selection has been canceled by the user.
		/// </returns>
		public static string OpenFolder(string rootFolder)
		{
			WinFormsFolderBrowserDialog dialog = new WinFormsFolderBrowserDialog
			{
				SelectedPath = rootFolder
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
		public static string SelectIcon(out int index)
		{
			IconPickerDialog dialog = new IconPickerDialog();
			if (dialog.ShowDialog() != true) dialog.Reset();

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
		public static string Save(string fileName)
		{
			return Save(fileName, null);
		}
		/// <summary>
		/// Displays a save file dialog and automatically adds an extension to the filename, if the user omits an extension.
		/// </summary>
		/// <param name="fileName">A <see cref="string" /> specifying the initial filename that can be changed by the user.</param>
		/// <param name="extension">A <see cref="string" /> specifying the extension to be added.</param>
		/// <returns>
		/// A <see cref="string" /> representing the full path to the saved file and <see langword="null" />, if selection has been canceled by the user.
		/// </returns>
		public static string Save(string fileName, string extension)
		{
			extension ??= Path.GetExtension(fileName).ToNullIfEmpty()?.TrimStart('.');
			SaveFileDialog dialog = new SaveFileDialog
			{
				FileName = fileName,
				Filter = GetFilter(extension),
				DefaultExt = extension?.TrimStart('.'),
				AddExtension = extension != null
			};

			if (dialog.ShowDialog() == true)
			{
				string result = dialog.FileName;
				if (extension != null && !result.EndsWith(extension, SpecialStringComparisons.IgnoreCase))
				{
					result = result.TrimEnd('.') + "." + extension.TrimStart('.');
				}

				return result;
			}
			else
			{
				return null;
			}
		}

		private static string GetFilter(params string[] extensions)
		{
			return GetFilter(extensions, null);
		}
		private static string GetFilter(string[] extensions, string extensionsDescription)
		{
			if (extensions.IsNullOrEmpty())
			{
				return (extensionsDescription ?? "All Files") + "|*.*";
			}
			else if (extensions.Length == 1)
			{
				return (extensionsDescription ?? new FileExtensionInfo(extensions.First()).FriendlyDocName) + "|*." + extensions.First().ToLower().TrimStart('.');
			}
			else
			{
				return (extensionsDescription ?? "Miscellaneous Files") + "|" + extensions.Select(extension => "*." + extension.TrimStart('.')).AsString(";");
			}
		}
	}
}