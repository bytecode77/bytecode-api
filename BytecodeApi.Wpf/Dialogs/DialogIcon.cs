namespace BytecodeApi.Wpf.Dialogs;

/// <summary>
/// Specifies the icon of a <see cref="Dialog" />.
/// </summary>
public enum DialogIcon
{
	/// <summary>
	/// The dialog is displayed without an icon.
	/// </summary>
	None,
	/// <summary>
	/// An asterisk icon is displayed.
	/// </summary>
	Information,
	/// <summary>
	/// A warning triangle icon is displayed.
	/// </summary>
	Warning,
	/// <summary>
	/// An error icon is displayed.
	/// </summary>
	Error,
	/// <summary>
	/// A shield icon is displayed.
	/// </summary>
	Shield,
	/// <summary>
	/// A shield icon with a blue background bar is displayed.
	/// </summary>
	ShieldBlueBar,
	/// <summary>
	/// A shield icon with a gray background bar is displayed.
	/// </summary>
	ShieldGrayBar,
	/// <summary>
	/// A warning shield icon with a yellow background bar is displayed.
	/// </summary>
	ShieldWarningYellowBar,
	/// <summary>
	/// An error shield icon with a red background bar is displayed.
	/// </summary>
	ShieldErrorRedBar,
	/// <summary>
	/// A successful shield icon with a green background bar is displayed.
	/// </summary>
	ShieldSuccessGreenBar
}