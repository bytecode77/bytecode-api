using BytecodeApi.Wpf.Cui.Controls;

namespace BytecodeApi.Wpf.Cui;

/// <summary>
/// Specifies the state of a <see cref="UiProgressBar" />.
/// </summary>
public enum ProgressBarState
{
    /// <summary>
    /// The <see cref="UiProgressBar" /> is green, indicating a normal state.
    /// </summary>
    Normal,
    /// <summary>
    /// The <see cref="UiProgressBar" /> is red, indicating an error or canceled state.
    /// </summary>
	Error,
    /// <summary>
    /// The <see cref="UiProgressBar" /> is yellow, indicating a warning or paused state.
    /// </summary>
	Paused
}