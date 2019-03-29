using System.Windows.Controls;

namespace BytecodeApi.UI.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with WPF controls.
	/// </summary>
	public static class ControlExtensions
	{
		/// <summary>
		/// Sets the insertion position index of the caret to the end of this <see cref="TextBox" />.
		/// </summary>
		/// <param name="textBox">The <see cref="TextBox" /> to be processed.</param>
		public static void MoveCaretToEnd(this TextBox textBox)
		{
			Check.ArgumentNull(textBox, nameof(textBox));

			textBox.CaretIndex = textBox.Text?.Length ?? 0;
		}
	}
}