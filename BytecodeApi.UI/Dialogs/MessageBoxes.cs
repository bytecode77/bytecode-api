using System.Windows;

namespace BytecodeApi.UI.Dialogs
{
	/// <summary>
	/// Helper class for dialogs using the <see cref="MessageBox" /> class.
	/// </summary>
	public static class MessageBoxes
	{
		/// <summary>
		/// Gets or sets the <see cref="Window" /> object that is used as the owner parameter for calls to the <see cref="MessageBox" /> class. If <see cref="Window" /> is <see langword="null" />, no owner is specified.
		/// </summary>
		public static Window Window { get; set; }
		/// <summary>
		/// Gets or sets the caption for message boxes shown by the <see cref="Information" /> method. The initial value is "Information".
		/// </summary>
		public static string CaptionForInformation { get; set; }
		/// <summary>
		/// Gets or sets the caption for message boxes shown by the <see cref="Confirmation(string)" /> method. The initial value is "Confirmation".
		/// </summary>
		public static string CaptionForConfirmation { get; set; }
		/// <summary>
		/// Gets or sets the caption for message boxes shown by the <see cref="Warning" /> method. The initial value is "Warning".
		/// </summary>
		public static string CaptionForWarning { get; set; }
		/// <summary>
		/// Gets or sets the caption for message boxes shown by the <see cref="Error" /> method. The initial value is "Error".
		/// </summary>
		public static string CaptionForError { get; set; }

		static MessageBoxes()
		{
			CaptionForInformation = "Information";
			CaptionForConfirmation = "Confirmation";
			CaptionForWarning = "Warning";
			CaptionForError = "Error";
		}

		/// <summary>
		/// Shows a <see cref="MessageBox" /> dialog with the specified message, using <see cref="CaptionForInformation" /> as title and an "OK" button. The title is a static <see cref="string" /> value, set to "Information" by default.
		/// </summary>
		/// <param name="message">A <see cref="string" /> specifying the message to be displayed.</param>
		public static void Information(string message)
		{
			ShowMessageBox(message, CaptionForInformation, MessageBoxButton.OK, MessageBoxImage.Information);
		}
		/// <summary>
		/// Shows a <see cref="MessageBox" /> dialog with the specified message, using <see cref="CaptionForConfirmation" /> as title and "Yes" and "No" buttons. The title is a static <see cref="string" /> value, set to "Confirmation" by default.
		/// </summary>
		/// <param name="message">A <see cref="string" /> specifying the message to be displayed.</param>
		/// <returns>
		/// <see langword="true" />, if "Yes" has been selected;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool Confirmation(string message)
		{
			return Confirmation(message, false);
		}
		/// <summary>
		/// Shows a <see cref="MessageBox" /> dialog with the specified message, using <see cref="CaptionForConfirmation" /> as title and "Yes" and "No" buttons. The title is a static <see cref="string" /> value, set to "Confirmation" by default.
		/// </summary>
		/// <param name="message">A <see cref="string" /> specifying the message to be displayed.</param>
		/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the information icon.</param>
		/// <returns>
		/// <see langword="true" />, if "Yes" has been selected;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool Confirmation(string message, bool isWarning)
		{
			return Confirmation(message, isWarning, false);
		}
		/// <summary>
		/// Shows a <see cref="MessageBox" /> dialog with the specified message, using <see cref="CaptionForConfirmation" /> as title and "Yes" and "No" buttons. The title is a static <see cref="string" /> value, set to "Confirmation" by default.
		/// </summary>
		/// <param name="message">A <see cref="string" /> specifying the message to be displayed.</param>
		/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the information icon.</param>
		/// <param name="useOkCancel"><see langword="true" /> to use "Yes" and "No"; <see langword="false" /> to use "OK" and "Cancel" buttons.</param>
		/// <returns>
		/// <see langword="true" />, if "Yes" or "OK" has been selected;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool Confirmation(string message, bool isWarning, bool useOkCancel)
		{
			return ShowMessageBox(message, CaptionForConfirmation, useOkCancel ? MessageBoxButton.OKCancel : MessageBoxButton.YesNo, isWarning ? MessageBoxImage.Warning : MessageBoxImage.Question) == (useOkCancel ? MessageBoxResult.OK : MessageBoxResult.Yes);
		}
		/// <summary>
		/// Shows a <see cref="MessageBox" /> dialog with the specified message, using <see cref="CaptionForConfirmation" /> as title and "Yes", "No" and "Cancel" buttons. The title is a static <see cref="string" /> value, set to "Confirmation" by default.
		/// </summary>
		/// <param name="message">A <see cref="string" /> specifying the message to be displayed.</param>
		/// <returns>
		/// <see langword="true" />, if "Yes" has been selected;
		/// <see langword="false" />, if "No" has been selected;
		/// <see langword="null" />, if "Cancel" has been selected.
		/// </returns>
		public static bool? ConfirmationWithCancel(string message)
		{
			return ConfirmationWithCancel(message, false);
		}
		/// <summary>
		/// Shows a <see cref="MessageBox" /> dialog with the specified message, using <see cref="CaptionForConfirmation" /> as title and "Yes", "No" and "Cancel" buttons. The title is a static <see cref="string" /> value, set to "Confirmation" by default.
		/// </summary>
		/// <param name="message">A <see cref="string" /> specifying the message to be displayed.</param>
		/// <param name="isWarning"><see langword="true" /> to use the warning icon; <see langword="false" /> to use the information icon.</param>
		/// <returns>
		/// <see langword="true" />, if "Yes" has been selected;
		/// <see langword="false" />, if "No" has been selected;
		/// <see langword="null" />, if "Cancel" has been selected.
		/// </returns>
		public static bool? ConfirmationWithCancel(string message, bool isWarning)
		{
			return ShowMessageBox(message, CaptionForConfirmation, MessageBoxButton.YesNoCancel, isWarning ? MessageBoxImage.Warning : MessageBoxImage.Question) switch
			{
				MessageBoxResult.Yes => true,
				MessageBoxResult.No => false,
				_ => (bool?)null
			};
		}
		/// <summary>
		/// Shows a <see cref="MessageBox" /> dialog with the specified message, using <see cref="CaptionForWarning" /> as title and an "OK" button. The title is a static <see cref="string" /> value, set to "Warning" by default.
		/// </summary>
		/// <param name="message">A <see cref="string" /> specifying the message to be displayed.</param>
		public static void Warning(string message)
		{
			ShowMessageBox(message, CaptionForWarning, MessageBoxButton.OK, MessageBoxImage.Warning);
		}
		/// <summary>
		/// Shows a <see cref="MessageBox" /> dialog with the specified message, using <see cref="CaptionForError" /> as title and an "OK" button. The title is a static <see cref="string" /> value, set to "Error" by default.
		/// </summary>
		/// <param name="message">A <see cref="string" /> specifying the message to be displayed.</param>
		public static void Error(string message)
		{
			ShowMessageBox(message, CaptionForError, MessageBoxButton.OK, MessageBoxImage.Error);
		}

		private static MessageBoxResult ShowMessageBox(string message, string caption, MessageBoxButton button, MessageBoxImage image)
		{
			if (Window == null) return MessageBox.Show(message ?? "", caption ?? "", button, image);
			else return MessageBox.Show(Window, message ?? "", caption ?? "", button, image);
		}
	}
}