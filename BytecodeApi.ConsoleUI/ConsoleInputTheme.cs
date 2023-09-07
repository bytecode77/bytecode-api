namespace BytecodeApi.ConsoleUI;

/// <summary>
/// Represents a theme for the <see cref="ConsoleInput" /> class.
/// </summary>
public sealed class ConsoleInputTheme
{
	/// <summary>
	/// Specifies the style for questions.
	/// <para>The default value is <see langword="new" /> <see cref="ConsoleStyle" />(<see cref="ConsoleColor.White" />, <see cref="ConsoleColor.DarkGray" />).</para>
	/// </summary>
	public ConsoleStyle QuestionStyle { get; set; }
	/// <summary>
	/// Specifies the style for options when <see cref="ConsoleInput.Select(string, string[])" /> is called.
	/// <para>The default value is <see langword="new" /> <see cref="ConsoleStyle" />(<see cref="ConsoleColor.White" />, <see cref="ConsoleColor.Black" />).</para>
	/// </summary>
	public ConsoleStyle SelectOptionsStyle { get; set; }
	/// <summary>
	/// Specifies the text that is displayed before the user input prompt when <see cref="ConsoleInput.Select(string, string[])" /> is called.
	/// <para>The default value is "Select:".</para>
	/// </summary>
	public string SelectOptionsPromptText { get; set; }
	/// <summary>
	/// Specifies the style for user input.
	/// <para>The default value is <see langword="new" /> <see cref="ConsoleStyle" />(<see cref="ConsoleColor.White" />, <see cref="ConsoleColor.Black" />).</para>
	/// </summary>
	public ConsoleStyle UserInputStyle { get; set; }
	/// <summary>
	/// Specifies the question postfix for a confirmation.
	/// <para>The default value is "[y/n]".</para>
	/// </summary>
	public string ConfirmationPostfix { get; set; }
	/// <summary>
	/// Specifies the question postfix for a confirmation, if the default answer is <see langword="true" />.
	/// <para>The default value is "[Y/n]".</para>
	/// </summary>
	public string ConfirmationPostfixDefaultYes { get; set; }
	/// <summary>
	/// Specifies the question postfix for a confirmation, if the default answer is <see langword="false" />.
	/// <para>The default value is "[y/N]".</para>
	/// </summary>
	public string ConfirmationPostfixDefaultNo { get; set; }
	/// <summary>
	/// Specifies the expected user input for a "yes" answer.
	/// <para>The default value is "y".</para>
	/// </summary>
	public string ConfirmationAnswerYes { get; set; }
	/// <summary>
	/// Specifies the expected user input for a "no" answer.
	/// <para>The default value is "n".</para>
	/// </summary>
	public string ConfirmationAnswerNo { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ConsoleInputTheme" /> class with default values.
	/// </summary>
	public ConsoleInputTheme()
	{
		QuestionStyle = new(ConsoleColor.White, ConsoleColor.DarkGray);
		SelectOptionsStyle = new(ConsoleColor.White);
		SelectOptionsPromptText = "Select:";
		UserInputStyle = new(ConsoleColor.White);
		ConfirmationPostfix = "[y/n]";
		ConfirmationPostfixDefaultYes = "[Y/n]";
		ConfirmationPostfixDefaultNo = "[y/N]";
		ConfirmationAnswerYes = "y";
		ConfirmationAnswerNo = "n";
	}
}