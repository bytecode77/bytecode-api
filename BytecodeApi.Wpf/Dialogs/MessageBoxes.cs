using System.Windows;

namespace BytecodeApi.Wpf.Dialogs;

/// <summary>
/// Helper class for dialogs using the <see cref="MessageBox" /> class.
/// </summary>
public static class MessageBoxes
{
	/// <summary>
	/// Shows a <see cref="MessageBox" /> dialog with the specified message and an "OK" button.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the message box, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the message box.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	public static void Information(Window? owner, string title, string text)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		ShowMessageBox(owner, title, text, MessageBoxButton.OK, MessageBoxImage.Information);
	}
	/// <summary>
	/// Shows a <see cref="MessageBox" /> dialog with the specified message and an "OK" button.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the message box, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the message box.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	public static void Warning(Window? owner, string title, string text)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		ShowMessageBox(owner, title, text, MessageBoxButton.OK, MessageBoxImage.Warning);
	}
	/// <summary>
	/// Shows a <see cref="MessageBox" /> dialog with the specified message and an "OK" button.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the message box, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the message box.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	public static void Error(Window? owner, string title, string text)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		ShowMessageBox(owner, title, text, MessageBoxButton.OK, MessageBoxImage.Error);
	}

	/// <summary>
	/// Shows a <see cref="MessageBox" /> dialog with the specified message and "Yes" and "No" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the message box, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the message box.</param>
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
	/// Shows a <see cref="MessageBox" /> dialog with the specified message and "Yes" and "No" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the message box, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the message box.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the question icon.</param>
	/// <returns>
	/// <see langword="true" />, if "Yes" has been selected;
	/// <see langword="false" />, if "No" has been selected.
	/// </returns>
	public static bool YesNo(Window? owner, string title, string text, bool isWarning)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		return ShowMessageBox(owner, title, text, MessageBoxButton.YesNo, isWarning ? MessageBoxImage.Warning : MessageBoxImage.Question) == MessageBoxResult.Yes;
	}
	/// <summary>
	/// Shows a <see cref="MessageBox" /> dialog with the specified message and "Ok" and "Cancel" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the message box, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the message box.</param>
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
	/// Shows a <see cref="MessageBox" /> dialog with the specified message and "Ok" and "Cancel" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the message box, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the message box.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the question icon.</param>
	/// <returns>
	/// <see langword="true" />, if "Ok" has been selected;
	/// <see langword="false" />, if "Cancel" has been selected.
	/// </returns>
	public static bool OkCancel(Window? owner, string title, string text, bool isWarning)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		return ShowMessageBox(owner, title, text, MessageBoxButton.OKCancel, isWarning ? MessageBoxImage.Warning : MessageBoxImage.Question) == MessageBoxResult.OK;
	}
	/// <summary>
	/// Shows a <see cref="MessageBox" /> dialog with the specified message and "Yes", "No" and "Cancel" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the message box, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the message box.</param>
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
	/// Shows a <see cref="MessageBox" /> dialog with the specified message and "Yes", "No" and "Cancel" buttons.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the message box, or <see langword="null" /> to not specify an owner.</param>
	/// <param name="title">A <see cref="string" /> specifying the title of the message box.</param>
	/// <param name="text">A <see cref="string" /> specifying the message to be displayed.</param>
	/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the question icon.</param>
	/// <returns>
	/// <see langword="true" />, if "Yes" has been selected;
	/// <see langword="false" />, if "No" has been selected;
	/// <see langword="null" />, if "Cancel" has been selected;
	/// </returns>
	public static bool? YesNoCancel(Window? owner, string title, string text, bool isWarning)
	{
		Check.ArgumentNull(title);
		Check.ArgumentNull(text);

		return ShowMessageBox(owner, title, text, MessageBoxButton.YesNoCancel, isWarning ? MessageBoxImage.Warning : MessageBoxImage.Question) switch
		{
			MessageBoxResult.Yes => true,
			MessageBoxResult.No => false,
			_ => null
		};
	}

	private static MessageBoxResult ShowMessageBox(Window? owner, string title, string text, MessageBoxButton button, MessageBoxImage image)
	{
		if (owner == null)
		{
			return MessageBox.Show(text, title, button, image);
		}
		else
		{
			return MessageBox.Show(owner, text, title, button, image);
		}
	}
}