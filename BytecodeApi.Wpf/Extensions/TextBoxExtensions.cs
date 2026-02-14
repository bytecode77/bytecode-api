using BytecodeApi.Extensions;
using System.Reflection;
using System.Windows.Controls;
using System.Windows.Documents;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with the <see cref="TextBox" /> control.
/// </summary>
public static class TextBoxExtensions
{
	extension(TextBox textBox)
	{
		/// <summary>
		/// Sets the insertion position index of the caret to the end of this <see cref="TextBox" />.
		/// </summary>
		public void MoveCaretToEnd()
		{
			Check.ArgumentNull(textBox);

			textBox.CaretIndex = textBox.Text?.Length ?? 0;
		}
	}

	extension(PasswordBox passwordBox)
	{
		/// <summary>
		/// Gets a character index for the beginning of the current selection.
		/// </summary>
		public int SelectionStart
		{
			get
			{
				Check.ArgumentNull(passwordBox);

				passwordBox.GetSelectionStartAndLength(out int start, out _);
				return start;
			}
		}
		/// <summary>
		/// Gets a value indicating the number of characters in the current selection in the <see cref="PasswordBox" />.
		/// </summary>
		public int SelectionLength
		{
			get
			{
				Check.ArgumentNull(passwordBox);

				passwordBox.GetSelectionStartAndLength(out _, out int length);
				return length;
			}
		}

		private void GetSelectionStartAndLength(out int start, out int length)
		{
			PropertyInfo selectionProperty = passwordBox
				.GetType()
				?.GetProperty("Selection", BindingFlags.Instance | BindingFlags.NonPublic)
				?? throw Throw.InvalidOperation("Property PasswordBox.Selection not found.");

			TextSelection selection = selectionProperty
				.GetValue<TextSelection>(passwordBox)
				?? throw Throw.InvalidOperation("PasswordBox.Selection is null.");

			Type textRangeType = selection
				.GetType()
				.GetInterfaces()
				.FirstOrDefault(x => x.Name == "ITextRange")
				?? throw Throw.InvalidOperation("ITextRange interface not implemented.");

			PropertyInfo startProperty = textRangeType
				.GetProperty("Start")
				?? throw Throw.InvalidOperation("Property ITextRange.Start not found.");

			PropertyInfo endProperty = textRangeType
				.GetProperty("End")
				?? throw Throw.InvalidOperation("Property ITextRange.End not found.");

			object startTextPointer = startProperty
				.GetValue(selection)
				?? throw Throw.InvalidOperation("TextPointer.Start is null.");

			object endTextPointer = endProperty
				.GetValue(selection)
				?? throw Throw.InvalidOperation("TextPointer.End is null.");

			PropertyInfo offsetProperty = startTextPointer
				.GetType()
				?.GetProperty("Offset", BindingFlags.Instance | BindingFlags.NonPublic)
				?? throw Throw.InvalidOperation("TextPointer.End is null.");

			start = offsetProperty.GetValue<int>(startTextPointer);
			length = offsetProperty.GetValue<int>(endTextPointer) - start;
		}
		/// <summary>
		/// Selects a range of text in this <see cref="PasswordBox" />.
		/// </summary>
		/// <param name="start">The zero-based character index of the first character in the selection.</param>
		/// <param name="length">The length of the selection, in characters.</param>
		public void Select(int start, int length)
		{
			Check.ArgumentNull(passwordBox);

			MethodInfo select = passwordBox
				.GetType()
				.GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic)
				?? throw Throw.InvalidOperation("Method PasswordBox.Select not found.");

			select.Invoke(passwordBox, [start, length]);
		}
		/// <summary>
		/// Sets the insertion position index of the caret to the end of this <see cref="PasswordBox" />.
		/// </summary>
		public void MoveCaretToEnd()
		{
			Check.ArgumentNull(passwordBox);

			passwordBox.Select(passwordBox.Password.Length, 0);
		}
	}
}