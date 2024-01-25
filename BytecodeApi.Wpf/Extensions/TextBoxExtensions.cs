using System.Reflection;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with the <see cref="TextBox" /> control.
/// </summary>
public static class TextBoxExtensions
{
	/// <summary>
	/// Sets the insertion position index of the caret to the end of this <see cref="TextBox" />.
	/// </summary>
	/// <param name="textBox">The <see cref="TextBox" /> to be processed.</param>
	public static void MoveCaretToEnd(this TextBox textBox)
	{
		Check.ArgumentNull(textBox);

		textBox.CaretIndex = textBox.Text?.Length ?? 0;
	}
	/// <summary>
	/// Selects a range of text in this <see cref="PasswordBox" />.
	/// </summary>
	/// <param name="passwordBox">The <see cref="PasswordBox" /> to be processed.</param>
	/// <param name="start">The zero-based character index of the first character in the selection.</param>
	/// <param name="length">The length of the selection, in characters.</param>
	public static void Select(this PasswordBox passwordBox, int start, int length)
	{
		Check.ArgumentNull(passwordBox);

		MethodInfo select = passwordBox
			.GetType()
			.GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)
			?? throw Throw.InvalidOperation("Method PasswordBox.Select not found.");

		select.Invoke(passwordBox, new object[] { start, length });
	}
	/// <summary>
	/// Sets the insertion position index of the caret to the end of this <see cref="PasswordBox" />.
	/// </summary>
	/// <param name="passwordBox">The <see cref="PasswordBox" /> to be processed.</param>
	public static void MoveCaretToEnd(this PasswordBox passwordBox)
	{
		Check.ArgumentNull(passwordBox);

		passwordBox.Select(passwordBox.Password.Length, 0);
	}
}