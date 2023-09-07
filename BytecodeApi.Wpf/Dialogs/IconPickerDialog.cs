using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace BytecodeApi.Wpf.Dialogs;

/// <summary>
/// Represents a common dialog box that allows a user to pick an icon from a resource file.
/// </summary>
public sealed class IconPickerDialog : CommonDialog
{
	/// <summary>
	/// Gets a <see cref="string" /> containing the full path of the selected file.
	/// </summary>
	public string? FileName { get; private set; }
	/// <summary>
	/// Gets a <see cref="int" /> value specifying the selected icon index.
	/// </summary>
	public int IconIndex { get; private set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="IconPickerDialog" /> class.
	/// </summary>
	public IconPickerDialog()
	{
		Reset();
	}

	/// <summary>
	/// Is called to display the icon picker dialog.
	/// </summary>
	/// <param name="hwndOwner">Handle to the window that owns the dialog box.</param>
	/// <returns>
	/// If the user clicks the OK button of the dialog that is displayed, <see langword="true" /> is returned;
	/// otherwise, <see langword="false" />.
	/// </returns>
	protected override bool RunDialog(nint hwndOwner)
	{
		StringBuilder stringBuilder = new(FileName, 260);

		if (Native.SHPickIconDialog(hwndOwner, stringBuilder, 260, out int index))
		{
			FileName = Environment.ExpandEnvironmentVariables(stringBuilder.ToString());
			IconIndex = index;
			return true;
		}
		else
		{
			return false;
		}
	}
	/// <summary>
	/// Resets the properties of a common dialog to their default values.
	/// </summary>
	public override void Reset()
	{
		FileName = null;
		IconIndex = -1;
	}
}

file static class Native
{
	[DllImport("shell32.dll", EntryPoint = "#62", CharSet = CharSet.Unicode, SetLastError = true)]
	[SuppressUnmanagedCodeSecurity]
	public static extern bool SHPickIconDialog(nint handle, StringBuilder fileName, int fileNameLength, out int iconIndex);
}