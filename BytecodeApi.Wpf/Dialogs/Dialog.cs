using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace BytecodeApi.Wpf.Dialogs;

/// <summary>
/// Class to display task dialogs.
/// </summary>
public class Dialog
{
	/// <summary>
	/// Gets or sets the caption for task dialogs. The initial value is <see cref="string.Empty" />.
	/// </summary>
	public static string Caption { get; set; }
	private readonly TaskDialogPage Page;
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating whether <see cref="Show(Window?)" /> or <see cref="ShowAsync(Window?)" /> was already called.
	/// </summary>
	public bool IsVisible { get; private set; }
	/// <summary>
	/// Gets a <see cref="bool" /> value indicating the state of the CheckBox, if this dialog has one. Call <see cref="CheckBox(string)" /> to create a dialog with a CheckBox.
	/// </summary>
	public bool IsChecked => Page.Verification?.Checked == true;

	static Dialog()
	{
		System.Windows.Forms.Application.EnableVisualStyles();
		Caption = "";
	}
	private Dialog()
	{
		Page = new()
		{
			Caption = Caption,
			SizeToContent = true
		};
	}

	/// <summary>
	/// Sets the title of the task dialog.
	/// </summary>
	/// <param name="title">The title of the task dialog.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public static Dialog Title(string title)
	{
		Check.ArgumentNull(title);

		Dialog dialog = new();
		dialog.Page.Heading = title;

		return dialog;
	}
	/// <summary>
	/// Sets the text of the task dialog.
	/// </summary>
	/// <param name="text">The text of the task dialog.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog Text(string text)
	{
		Check.ArgumentNull(text);

		Page.Text = text;
		return this;
	}
	/// <summary>
	/// Adds a collapsable expander to the task dialog.
	/// </summary>
	/// <param name="text">The text within the expander.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog Expander(string text)
	{
		return Expander(text, null, null);
	}
	/// <summary>
	/// Adds a collapsable expander to the task dialog.
	/// </summary>
	/// <param name="text">The text within the expander.</param>
	/// <param name="expandText">The text to display on the "expand" button. Provide <see langword="null" /> to use a default <see cref="string" />.</param>
	/// <param name="collapseText">The text to display on the "collapse" button. Provide <see langword="null" /> to use a default <see cref="string" />.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog Expander(string text, string? expandText, string? collapseText)
	{
		Check.ArgumentNull(text);

		Page.Expander ??= new();
		Page.Expander.Text = text;

		if (!IsVisible)
		{
			Page.Expander.CollapsedButtonText = expandText;
			Page.Expander.ExpandedButtonText = collapseText;
		}

		return this;
	}
	/// <summary>
	/// Sets the icon of the task dialog.
	/// </summary>
	/// <param name="icon">The icon of the task dialog.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog Icon(DialogIcon icon)
	{
		Page.Icon = icon switch
		{
			DialogIcon.None => TaskDialogIcon.None,
			DialogIcon.Information => TaskDialogIcon.Information,
			DialogIcon.Warning => TaskDialogIcon.Warning,
			DialogIcon.Error => TaskDialogIcon.Error,
			DialogIcon.Shield => TaskDialogIcon.Shield,
			DialogIcon.ShieldBlueBar => TaskDialogIcon.ShieldBlueBar,
			DialogIcon.ShieldGrayBar => TaskDialogIcon.ShieldGrayBar,
			DialogIcon.ShieldWarningYellowBar => TaskDialogIcon.ShieldWarningYellowBar,
			DialogIcon.ShieldErrorRedBar => TaskDialogIcon.ShieldErrorRedBar,
			DialogIcon.ShieldSuccessGreenBar => TaskDialogIcon.ShieldSuccessGreenBar,
			_ => throw Throw.InvalidEnumArgument(nameof(icon), icon)
		};

		return this;
	}
	/// <summary>
	/// Sets the icon of the task dialog.
	/// </summary>
	/// <param name="icon">A custom icon for the task dialog.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog Icon(System.Drawing.Bitmap icon)
	{
		Page.Icon = new(icon);
		return this;
	}
	/// <summary>
	/// Sets the icon of the task dialog.
	/// </summary>
	/// <param name="icon">A custom icon for the task dialog.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog Icon(System.Drawing.Icon icon)
	{
		Page.Icon = new(icon);
		return this;
	}
	/// <summary>
	/// Specifies that the task dialog window has a close button.
	/// </summary>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog CanClose()
	{
		Check.InvalidOperation(!IsVisible, "Closability cannot be changed, because the dialog is already shown.");

		Page.AllowCancel = true;
		return this;
	}
	/// <summary>
	/// Adds a button to the task dialog with a default text that matches <paramref name="result" />.
	/// </summary>
	/// <param name="result">The <see cref="DialogResult" /> associated with the button.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog Button(DialogResult result)
	{
		Check.InvalidOperation(!IsVisible, "A command link cannot be added, because the dialog is already shown.");

		Page.Buttons.Add(result switch
		{
			DialogResult.OK => TaskDialogButton.OK,
			DialogResult.Cancel => TaskDialogButton.Cancel,
			DialogResult.Yes => TaskDialogButton.Yes,
			DialogResult.No => TaskDialogButton.No,
			DialogResult.Abort => TaskDialogButton.Abort,
			DialogResult.Retry => TaskDialogButton.Retry,
			DialogResult.Ignore => TaskDialogButton.Ignore,
			_ => throw Throw.Argument(nameof(result), $"Cannot create button from {nameof(DialogResult)}.{result}. Please create a custom button.")
		});

		return this;
	}
	/// <summary>
	/// Adds a button to the task dialog using a custom text.
	/// </summary>
	/// <param name="result">The <see cref="DialogResult" /> associated with the button.</param>
	/// <param name="text">A custom text for the button.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog Button(DialogResult result, string text)
	{
		return Button(result, text, false);
	}
	/// <summary>
	/// Adds a button to the task dialog using a custom text.
	/// </summary>
	/// <param name="result">The <see cref="DialogResult" /> associated with the button.</param>
	/// <param name="text">A custom text for the button.</param>
	/// <param name="showShieldIcon"><see langword="true" /> to display a UAC shield on the button.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog Button(DialogResult result, string text, bool showShieldIcon)
	{
		return Button(result, text, showShieldIcon, true);
	}
	/// <summary>
	/// Adds a button to the task dialog using a custom text.
	/// </summary>
	/// <param name="result">The <see cref="DialogResult" /> associated with the button.</param>
	/// <param name="text">A custom text for the button.</param>
	/// <param name="showShieldIcon"><see langword="true" /> to display a UAC shield on the button.</param>
	/// <param name="isEnabled"><see langword="true" /> to enable the button, <see langword="false" /> to disable the button.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog Button(DialogResult result, string text, bool showShieldIcon, bool isEnabled)
	{
		return Button(result, text, showShieldIcon, isEnabled, null);
	}
	/// <summary>
	/// Adds a button to the task dialog using a custom text.
	/// </summary>
	/// <param name="result">The <see cref="DialogResult" /> associated with the button.</param>
	/// <param name="text">A custom text for the button.</param>
	/// <param name="showShieldIcon"><see langword="true" /> to display a UAC shield on the button.</param>
	/// <param name="isEnabled"><see langword="true" /> to enable the button, <see langword="false" /> to disable the button.</param>
	/// <param name="onClick">A callback that is invoked when the button is clicked.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog Button(DialogResult result, string text, bool showShieldIcon, bool isEnabled, Action? onClick)
	{
		Check.InvalidOperation(!IsVisible, "A button cannot be added, because the dialog is already shown.");
		Check.ArgumentNull(text);

		TaskDialogButton button = new(text)
		{
			Tag = result,
			ShowShieldIcon = showShieldIcon,
			Enabled = isEnabled
		};

		if (onClick != null)
		{
			button.Click += delegate (object? sender, EventArgs e)
			{
				onClick();
			};
		}

		Page.Buttons.Add(button);

		return this;
	}
	/// <summary>
	/// Adds a command link button to the task dialog.
	/// </summary>
	/// <param name="result">The <see cref="DialogResult" /> associated with the button.</param>
	/// <param name="text">The text of the command link button</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog CommandLink(DialogResult result, string text)
	{
		return CommandLink(result, text, null);
	}
	/// <summary>
	/// Adds a command link button to the task dialog.
	/// </summary>
	/// <param name="result">The <see cref="DialogResult" /> associated with the command link button.</param>
	/// <param name="text">The text of the command link button</param>
	/// <param name="description">An additional description to be displayed under <paramref name="text" />, or <see langword="null" /> to not display a description.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog CommandLink(DialogResult result, string text, string? description)
	{
		return CommandLink(result, text, description, false);
	}
	/// <summary>
	/// Adds a command link button to the task dialog.
	/// </summary>
	/// <param name="result">The <see cref="DialogResult" /> associated with the command link button.</param>
	/// <param name="text">The text of the command link button</param>
	/// <param name="description">An additional description to be displayed under <paramref name="text" />, or <see langword="null" /> to not display a description.</param>
	/// <param name="showShieldIcon"><see langword="true" /> to display a UAC shield on the command link button.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog CommandLink(DialogResult result, string text, string? description, bool showShieldIcon)
	{
		return CommandLink(result, text, description, showShieldIcon, true);
	}
	/// <summary>
	/// Adds a command link button to the task dialog.
	/// </summary>
	/// <param name="result">The <see cref="DialogResult" /> associated with the command link button.</param>
	/// <param name="text">The text of the command link button</param>
	/// <param name="description">An additional description to be displayed under <paramref name="text" />, or <see langword="null" /> to not display a description.</param>
	/// <param name="showShieldIcon"><see langword="true" /> to display a UAC shield on the command link button.</param>
	/// <param name="isEnabled"><see langword="true" /> to enable the button, <see langword="false" /> to disable the button.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog CommandLink(DialogResult result, string text, string? description, bool showShieldIcon, bool isEnabled)
	{
		return CommandLink(result, text, description, showShieldIcon, isEnabled, null);
	}
	/// <summary>
	/// Adds a command link button to the task dialog.
	/// </summary>
	/// <param name="result">The <see cref="DialogResult" /> associated with the command link button.</param>
	/// <param name="text">The text of the command link button</param>
	/// <param name="description">An additional description to be displayed under <paramref name="text" />, or <see langword="null" /> to not display a description.</param>
	/// <param name="showShieldIcon"><see langword="true" /> to display a UAC shield on the command link button.</param>
	/// <param name="isEnabled"><see langword="true" /> to enable the button, <see langword="false" /> to disable the button.</param>
	/// <param name="onClick">A callback that is invoked when the button is clicked.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog CommandLink(DialogResult result, string text, string? description, bool showShieldIcon, bool isEnabled, Action? onClick)
	{
		Check.InvalidOperation(!IsVisible, "A command link cannot be added, because the dialog is already shown.");
		Check.ArgumentNull(text);

		TaskDialogCommandLinkButton button = new(text, description)
		{
			Tag = result,
			ShowShieldIcon = showShieldIcon,
			Enabled = isEnabled
		};

		if (onClick != null)
		{
			button.Click += delegate (object? sender, EventArgs e)
			{
				onClick();
			};
		}

		Page.Buttons.Add(button);

		return this;
	}
	/// <summary>
	/// Adds a ProgressBar to the task dialog.
	/// </summary>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog ProgressBar()
	{
		return ProgressBar(ProgressBarState.Normal);
	}
	/// <summary>
	/// Adds a ProgressBar to the task dialog.
	/// </summary>
	/// <param name="state">The state of the progress bar that determines its appearance.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog ProgressBar(ProgressBarState state)
	{
		return ProgressBar(state, 0);
	}
	/// <summary>
	/// Adds a ProgressBar to the task dialog.
	/// </summary>
	/// <param name="state">The state of the progress bar that determines its appearance.</param>
	/// <param name="progress">The progress, in range of 0..1, of the progress bar..</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog ProgressBar(ProgressBarState state, double progress)
	{
		Page.ProgressBar ??= new();
		Page.ProgressBar.State = state switch
		{
			ProgressBarState.Normal => TaskDialogProgressBarState.Normal,
			ProgressBarState.Error => TaskDialogProgressBarState.Error,
			ProgressBarState.Paused => TaskDialogProgressBarState.Paused,
			ProgressBarState.Indeterminate => TaskDialogProgressBarState.Marquee,
			ProgressBarState.IndeterminatePaused => TaskDialogProgressBarState.MarqueePaused,
			_ => throw Throw.InvalidEnumArgument(nameof(state), state)
		};
		Page.ProgressBar.Minimum = 0;
		Page.ProgressBar.Maximum = 65535;
		Page.ProgressBar.Value = Math.Clamp((int)(progress * 65535), 0, 65535);

		return this;
	}
	/// <summary>
	/// Adds a CheckBox to the task dialog.
	/// </summary>
	/// <param name="text">The text of the CheckBox.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog CheckBox(string text)
	{
		return CheckBox(text, false);
	}
	/// <summary>
	/// Adds a CheckBox to the task dialog.
	/// </summary>
	/// <param name="text">The text of the CheckBox.</param>
	/// <param name="isChecked">The default check state of the CheckBox.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog CheckBox(string text, bool isChecked)
	{
		Check.ArgumentNull(text);

		Page.Verification ??= new();
		if (!IsVisible) Page.Verification.Text = text;
		Page.Verification.Checked = isChecked;

		return this;
	}

	/// <summary>
	/// Sets the enabled state of a button or command link.
	/// </summary>
	/// <param name="index">The index of the button or command link to modify.</param>
	/// <param name="isEnabled"><see langword="true" /> to enable the button, <see langword="false" /> to disable the button.</param>
	/// <returns>
	/// This instance of <see cref="Dialog" />.
	/// </returns>
	public Dialog SetButtonEnabled(int index, bool isEnabled)
	{
		Check.ArgumentEx.OffsetAndLengthOutOfBounds(index, 0, Page.Buttons.Count);

		Page.Buttons[index].Enabled = isEnabled;

		return this;
	}

	/// <summary>
	/// Shows the task dialog as a modal dialog.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <returns>
	/// The <see cref="DialogResult" /> associated with the clicked button.
	/// Returns <see cref="DialogResult.Cancel" />, if the task dialog window was closed.
	/// </returns>
	public DialogResult Show(Window? owner)
	{
		Check.InvalidOperation(!IsVisible, "The dialog was already shown.");
		Check.InvalidOperation(Page.Buttons.Any(), "A dialog must have buttons.");

		IsVisible = true;

		TaskDialogButton button = TaskDialog.ShowDialog(
			owner != null ? new WindowInteropHelper(owner).EnsureHandle() : 0,
			Page,
			TaskDialogStartupLocation.CenterOwner
		);

		return ConvertButtonToDialogResult(button);
	}
	/// <summary>
	/// Shows the task dialog as a modal dialog asynchronously.
	/// </summary>
	/// <param name="owner">A <see cref="Window" /> to use as the owner of the task dialog, or <see langword="null" /> to not specify an owner.</param>
	/// <returns>
	/// The <see cref="DialogResult" /> associated with the clicked button.
	/// Returns <see cref="DialogResult.Cancel" />, if the task dialog window was closed.
	/// </returns>
	public async Task<DialogResult> ShowAsync(Window? owner)
	{
		Check.InvalidOperation(!IsVisible, "The dialog was already shown.");
		Check.InvalidOperation(Page.Buttons.Any(), "A dialog must have buttons.");

		IsVisible = true;

		TaskDialogButton button = await TaskDialog.ShowDialogAsync(
			owner != null ? new WindowInteropHelper(owner).EnsureHandle() : 0,
			Page,
			TaskDialogStartupLocation.CenterOwner
		);

		return ConvertButtonToDialogResult(button);
	}
	/// <summary>
	/// Closes the dialog, after it has already been shown.
	/// <see cref="DialogResult.Cancel" /> is returned by <see cref="Show(Window?)" />.
	/// </summary>
	public void Close()
	{
		Page.BoundDialog?.Close();
	}

	private static DialogResult ConvertButtonToDialogResult(TaskDialogButton button)
	{
		if (button == TaskDialogButton.OK) return DialogResult.OK;
		else if (button == TaskDialogButton.Cancel) return DialogResult.Cancel;
		else if (button == TaskDialogButton.Yes) return DialogResult.Yes;
		else if (button == TaskDialogButton.No) return DialogResult.No;
		else if (button == TaskDialogButton.Abort) return DialogResult.Abort;
		else if (button == TaskDialogButton.Retry) return DialogResult.Retry;
		else if (button == TaskDialogButton.Ignore) return DialogResult.Ignore;
		else if (button.Tag is DialogResult customResult) return customResult;
		else return DialogResult.Cancel;
	}
}