using System.Windows;

namespace BytecodeApi.Wpf.Dialogs;

/// <summary>
/// Helper class for task dialogs using the <see cref="Dialog" /> class.
/// <para>Set <see cref="Dialog.Caption" /> to specify a window caption for dialogs globally.</para>
/// </summary>
public static class DialogMessageBoxes
{
	/// <summary>
	/// Shows a task dialog with the specified message and an "OK" button.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	public static void Information(Window? owner, string title, string text)
	{
		Information(owner, title, text, null);
	}
	/// <summary>
	/// Shows a task dialog with the specified message and an "OK" button.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="expander">A <see cref="string" /> specifying text to display in a collapsable expander, or <see langword="null" /> to not show an expander.</param>
	public static void Information(Window? owner, string title, string text, string? expander)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		Dialog dialog = Dialog
			.Title(title)
			.Text(text)
			.Icon(DialogIcon.Information)
			.CanClose()
			.Button(DialogResult.OK);

		if (expander != null)
		{
			dialog.Expander(expander);
		}

		dialog.Show(owner);
	}
	/// <summary>
	/// Shows a task dialog with the specified message and an "OK" button.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	public static void Warning(Window? owner, string title, string text)
	{
		Warning(owner, title, text, null);
	}
	/// <summary>
	/// Shows a task dialog with the specified message and an "OK" button.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="expander">A <see cref="string" /> specifying text to display in a collapsable expander, or <see langword="null" /> to not show an expander.</param>
	public static void Warning(Window? owner, string title, string text, string? expander)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		Dialog dialog = Dialog
			.Title(title)
			.Text(text)
			.Icon(DialogIcon.Warning)
			.CanClose()
			.Button(DialogResult.OK);

		if (expander != null)
		{
			dialog.Expander(expander);
		}

		dialog.Show(owner);
	}
	/// <summary>
	/// Shows a task dialog with the specified message and an "OK" button.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	public static void Error(Window? owner, string title, string text)
	{
		Error(owner, title, text, null);
	}
	/// <summary>
	/// Shows a task dialog with the specified message and an "OK" button.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="expander">A <see cref="string" /> specifying text to display in a collapsable expander, or <see langword="null" /> to not show an expander.</param>
	public static void Error(Window? owner, string title, string text, string? expander)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		Dialog dialog = Dialog
			.Title(title)
			.Text(text)
			.Icon(DialogIcon.Error)
			.CanClose()
			.Button(DialogResult.OK);

		if (expander != null)
		{
			dialog.Expander(expander);
		}

		dialog.Show(owner);
	}

	/// <summary>
	/// Shows a task dialog with the specified message and "Yes" and "No" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <returns>
	/// <see langword="true" />, if "Yes" has been selected;
	/// <see langword="false" />, if "No" has been selected.
	/// </returns>
	public static bool YesNo(Window? owner, string title, string text)
	{
		return YesNo(owner, title, text, false);
	}
	/// <summary>
	/// Shows a task dialog with the specified message and "Yes" and "No" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the question icon.</param>
	/// <returns>
	/// <see langword="true" />, if "Yes" has been selected;
	/// <see langword="false" />, if "No" has been selected.
	/// </returns>
	public static bool YesNo(Window? owner, string title, string text, bool isWarning)
	{
		return YesNo(owner, title, text, isWarning, null);
	}
	/// <summary>
	/// Shows a task dialog with the specified message and "Yes" and "No" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the question icon.</param>
	/// <param name="expander">A <see cref="string" /> specifying text to display in a collapsable expander, or <see langword="null" /> to not show an expander.</param>
	/// <returns>
	/// <see langword="true" />, if "Yes" has been selected;
	/// <see langword="false" />, if "No" has been selected.
	/// </returns>
	public static bool YesNo(Window? owner, string title, string text, bool isWarning, string? expander)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		Dialog dialog = Dialog
			.Title(title)
			.Text(text)
			.Icon(isWarning ? DialogIcon.Warning : DialogIcon.Information)
			.Button(DialogResult.Yes)
			.Button(DialogResult.No);

		if (expander != null)
		{
			dialog.Expander(expander);
		}

		return dialog.Show(owner) == DialogResult.Yes;
	}
	/// <summary>
	/// Shows a task dialog with the specified message and "Ok" and "Cancel" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <returns>
	/// <see langword="true" />, if "Ok" has been selected;
	/// <see langword="false" />, if "Cancel" has been selected.
	/// </returns>
	public static bool OkCancel(Window? owner, string title, string text)
	{
		return OkCancel(owner, title, text, false);
	}
	/// <summary>
	/// Shows a task dialog with the specified message and "Ok" and "Cancel" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the question icon.</param>
	/// <returns>
	/// <see langword="true" />, if "Ok" has been selected;
	/// <see langword="false" />, if "Cancel" has been selected.
	/// </returns>
	public static bool OkCancel(Window? owner, string title, string text, bool isWarning)
	{
		return OkCancel(owner, title, text, isWarning, null);
	}
	/// <summary>
	/// Shows a task dialog with the specified message and "Ok" and "Cancel" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the question icon.</param>
	/// <param name="expander">A <see cref="string" /> specifying text to display in a collapsable expander, or <see langword="null" /> to not show an expander.</param>
	/// <returns>
	/// <see langword="true" />, if "Ok" has been selected;
	/// <see langword="false" />, if "Cancel" has been selected.
	/// </returns>
	public static bool OkCancel(Window? owner, string title, string text, bool isWarning, string? expander)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		Dialog dialog = Dialog
			.Title(title)
			.Text(text)
			.Icon(isWarning ? DialogIcon.Warning : DialogIcon.Information)
			.CanClose()
			.Button(DialogResult.OK)
			.Button(DialogResult.Cancel);

		if (expander != null)
		{
			dialog.Expander(expander);
		}

		return dialog.Show(owner) == DialogResult.OK;
	}
	/// <summary>
	/// Shows a task dialog with the specified message and "Yes", "No" and "Cancel" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <returns>
	/// <see langword="true" />, if "Yes" has been selected;
	/// <see langword="false" />, if "No" has been selected;
	/// <see langword="null" />, if "Cancel" has been selected;
	/// </returns>
	public static bool? YesNoCancel(Window? owner, string title, string text)
	{
		return YesNoCancel(owner, title, text, false);
	}
	/// <summary>
	/// Shows a task dialog with the specified message and "Yes", "No" and "Cancel" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the question icon.</param>
	/// <returns>
	/// <see langword="true" />, if "Yes" has been selected;
	/// <see langword="false" />, if "No" has been selected;
	/// <see langword="null" />, if "Cancel" has been selected;
	/// </returns>
	public static bool? YesNoCancel(Window? owner, string title, string text, bool isWarning)
	{
		return YesNoCancel(owner, title, text, isWarning, null);
	}
	/// <summary>
	/// Shows a task dialog with the specified message and "Yes", "No" and "Cancel" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the task dialog.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the question icon.</param>
	/// <param name="expander">A <see cref="string" /> specifying text to display in a collapsable expander, or <see langword="null" /> to not show an expander.</param>
	/// <returns>
	/// <see langword="true" />, if "Yes" has been selected;
	/// <see langword="false" />, if "No" has been selected;
	/// <see langword="null" />, if "Cancel" has been selected;
	/// </returns>
	public static bool? YesNoCancel(Window? owner, string title, string text, bool isWarning, string? expander)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		Dialog dialog = Dialog
			.Title(title)
			.Text(text)
			.Icon(isWarning ? DialogIcon.Warning : DialogIcon.Information)
			.CanClose()
			.Button(DialogResult.Yes)
			.Button(DialogResult.No)
			.Button(DialogResult.Cancel);

		if (expander != null)
		{
			dialog.Expander(expander);
		}

		return dialog.Show(owner) switch
		{
			DialogResult.Yes => true,
			DialogResult.No => false,
			_ => null
		};
	}
}