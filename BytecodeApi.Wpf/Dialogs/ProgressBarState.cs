namespace BytecodeApi.Wpf.Dialogs;


/// <summary>
/// Specifies the state of a ProgressBar in a <see cref="Dialog" />.
/// </summary>
public enum ProgressBarState
{
	/// <summary>
	/// The ProgressBar is green, indicating a normal state.
	/// </summary>
	Normal,
	/// <summary>
	/// The ProgressBar is red, indicating an error or canceled state.
	/// </summary>
	Error,
	/// <summary>
	/// The ProgressBar is yellow, indicating a warning or paused state.
	/// </summary>
	Paused,
	/// <summary>
	/// The ProgressBar is in an indeterminate state.
	/// </summary>
	Indeterminate,
	/// <summary>
	/// The ProgressBar is in an indeterminate state, the animation is paused.
	/// </summary>
	IndeterminatePaused
}