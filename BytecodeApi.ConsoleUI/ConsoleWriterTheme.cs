namespace BytecodeApi.ConsoleUI;

/// <summary>
/// Represents a theme for the <see cref="ConsoleWriter" /> class.
/// </summary>
public sealed class ConsoleWriterTheme
{
	/// <summary>
	/// Specifies the style for normal text.
	/// <para>The default value is <see langword="new" /> <see cref="ConsoleStyle" />(<see cref="ConsoleColor.Gray" />, <see cref="ConsoleColor.Black" />).</para>
	/// </summary>
	public ConsoleStyle TextStyle { get; set; }
	/// <summary>
	/// Specifies the style for a preview message.
	/// <para>The default value is <see langword="new" /> <see cref="ConsoleStyle" />(<see cref="ConsoleColor.Gray" />, <see cref="ConsoleColor.Black" />).</para>
	/// </summary>
	public ConsoleStyle PreviewStyle { get; set; }
	/// <summary>
	/// Specifies the style for a success message.
	/// <para>The default value is <see langword="new" /> <see cref="ConsoleStyle" />(<see cref="ConsoleColor.Gray" />, <see cref="ConsoleColor.Black" />).</para>
	/// </summary>
	public ConsoleStyle SuccessStyle { get; set; }
	/// <summary>
	/// Specifies the style for a warning message.
	/// <para>The default value is <see langword="new" /> <see cref="ConsoleStyle" />(<see cref="ConsoleColor.Yellow" />, <see cref="ConsoleColor.Black" />).</para>
	/// </summary>
	public ConsoleStyle WarningStyle { get; set; }
	/// <summary>
	/// Specifies the style for an error message.
	/// <para>The default value is <see langword="new" /> <see cref="ConsoleStyle" />(<see cref="ConsoleColor.Red" />, <see cref="ConsoleColor.Black" />).</para>
	/// </summary>
	public ConsoleStyle ErrorStyle { get; set; }
	/// <summary>
	/// A <see cref="bool" /> value indicating whether to show a time stamp before each message.
	/// <para>The default value is <see langword="true" />.</para>
	/// </summary>
	public bool ShowTimeStamp { get; set; }
	/// <summary>
	/// Specifies the string format for time stamps.
	/// <para>The default value is "HH:mm:ss".</para>
	/// </summary>
	public string TimeStampFormat { get; set; }
	/// <summary>
	/// Specifies a text based icon that is used for preview messages.
	/// <para>The default value is "...".</para>
	/// </summary>
	public string PreviewIcon { get; set; }
	/// <summary>
	/// Specifies a text based icon that is used for success messages.
	/// <para>The default value is "[+]".</para>
	/// </summary>
	public string SuccessIcon { get; set; }
	/// <summary>
	/// Specifies a text based icon that is used for warning messages.
	/// <para>The default value is "[!]".</para>
	/// </summary>
	public string WarningIcon { get; set; }
	/// <summary>
	/// Specifies a text based icon that is used for error messages.
	/// <para>The default value is "[-]".</para>
	/// </summary>
	public string ErrorIcon { get; set; }
	/// <summary>
	/// Specifies the beginning character of a text based progress bar.
	/// <para>The default value is "[".</para>
	/// </summary>
	public char ProgressBarBeginChar { get; set; }
	/// <summary>
	/// Specifies the closing character of a text based progress bar.
	/// <para>The default value is "]".</para>
	/// </summary>
	public char ProgressBarEndChar { get; set; }
	/// <summary>
	/// Specifies the character of a text based progress bar that renders a full block.
	/// <para>The default value is "█".</para>
	/// </summary>
	public char ProgressBarFullChar { get; set; }
	/// <summary>
	/// Specifies the character of a text based progress bar that renders an empty block.
	/// <para>The default value is "░".</para>
	/// </summary>
	public char ProgressBarEmptyChar { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ConsoleWriterTheme" /> class with default values.
	/// </summary>
	public ConsoleWriterTheme()
	{
		TextStyle = new();
		PreviewStyle = new();
		SuccessStyle = new();
		WarningStyle = new(ConsoleColor.Yellow);
		ErrorStyle = new(ConsoleColor.Red);
		ShowTimeStamp = true;
		TimeStampFormat = "HH:mm:ss";
		PreviewIcon = "...";
		SuccessIcon = "[+]";
		WarningIcon = "[!]";
		ErrorIcon = "[-]";
		ProgressBarBeginChar = '[';
		ProgressBarEndChar = ']';
		ProgressBarFullChar = '█';
		ProgressBarEmptyChar = '░';
	}
}