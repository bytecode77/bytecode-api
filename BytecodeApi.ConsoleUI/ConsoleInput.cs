using BytecodeApi.Extensions;

namespace BytecodeApi.ConsoleUI;

/// <summary>
/// Class that retrieves user input from the <see cref="Console" /> in form of questions or confirmations.
/// </summary>
public static class ConsoleInput
{
	/// <summary>
	/// Gets or sets the global theme for the <see cref="ConsoleInput" /> class.
	/// </summary>
	public static ConsoleInputTheme Theme { get; set; }

	static ConsoleInput()
	{
		Theme = new();
	}

	/// <summary>
	/// Writes <paramref name="question" /> to the console and prompts the user to input a <see cref="string" />. This process is repeated, until the entered <see cref="string" /> is not empty.
	/// </summary>
	/// <param name="question">A <see cref="string" /> to display before the user input prompt.</param>
	/// <returns>
	/// The text that the user has entered. This <see cref="string" /> is not empty.
	/// </returns>
	public static string Text(string question)
	{
		Check.ArgumentNull(question);
		Check.ArgumentNull(Theme);
		Check.ArgumentNull(Theme.QuestionStyle);
		Check.ArgumentNull(Theme.UserInputStyle);

		Flush();

		string text;
		do
		{
			Theme.QuestionStyle.Write(question);
			Theme.UserInputStyle.Write(" ");

			text = Theme.UserInputStyle.ReadLine().Trim();
		}
		while (text == "");

		return text;
	}
	/// <summary>
	/// Writes <paramref name="question" /> to the console and prompts the user to input yes ("y") or no ("n").
	/// </summary>
	/// <param name="question">A <see cref="string" /> to display before the user input prompt.</param>
	/// <returns>
	/// <see langword="true" />, if the user selected yes;
	/// <see langword="false" />, if the user selected no.
	/// </returns>
	public static bool Confirmation(string question)
	{
		return Confirmation(question, null);
	}
	/// <summary>
	/// Writes <paramref name="question" /> to the console and prompts the user to input yes ("y") or no ("n").
	/// </summary>
	/// <param name="question">A <see cref="string" /> to display before the user input prompt.</param>
	/// <param name="defaultChoice">A <see cref="bool" /> value that is returned, if the user did not enter any text, or <see langword="null" /> to require the user to input yes or no.</param>
	/// <returns>
	/// <see langword="true" />, if the user selected yes;
	/// <see langword="false" />, if the user selected no.
	/// </returns>
	public static bool Confirmation(string question, bool? defaultChoice)
	{
		Check.ArgumentNull(question);
		Check.ArgumentNull(Theme);
		Check.ArgumentNull(Theme.QuestionStyle);
		Check.ArgumentNull(Theme.UserInputStyle);
		Check.ArgumentNull(Theme.ConfirmationPostfix);
		Check.ArgumentNull(Theme.ConfirmationPostfixDefaultYes);
		Check.ArgumentNull(Theme.ConfirmationPostfixDefaultNo);
		Check.ArgumentNull(Theme.ConfirmationAnswerYes);
		Check.ArgumentEx.StringNotEmpty(Theme.ConfirmationAnswerYes);
		Check.ArgumentNull(Theme.ConfirmationAnswerNo);
		Check.ArgumentEx.StringNotEmpty(Theme.ConfirmationAnswerNo);

		string defaultChoiceString = defaultChoice switch
		{
			null => Theme.ConfirmationPostfix,
			true => Theme.ConfirmationPostfixDefaultYes,
			false => Theme.ConfirmationPostfixDefaultNo
		};

		Flush();

		bool? choice;
		do
		{
			Theme.QuestionStyle.Write($"{question} {defaultChoiceString}?");
			Theme.UserInputStyle.Write(" ");

			string answer = Theme.UserInputStyle.ReadLine().Trim();

			if (answer == "") choice = defaultChoice;
			else if (answer.Equals(Theme.ConfirmationAnswerYes, StringComparison.OrdinalIgnoreCase)) choice = true;
			else if (answer.Equals(Theme.ConfirmationAnswerNo, StringComparison.OrdinalIgnoreCase)) choice = false;
			else choice = null;
		}
		while (choice == null);

		return choice.Value;
	}
	/// <summary>
	/// Writes <paramref name="question" /> and an ordered list with <paramref name="options" /> to the console and prompts the user to select an option.
	/// </summary>
	/// <param name="question">A <see cref="string" /> to display before the user input prompt.</param>
	/// <param name="options">A <see cref="string" />[] with options that the user can select from.</param>
	/// <returns>
	/// A one-based index within <paramref name="options" /> that represents the user selection.
	/// </returns>
	public static int Select(string question, params string[] options)
	{
		Check.ArgumentNull(question);
		Check.ArgumentNull(options);
		Check.ArgumentEx.ArrayValuesNotNull(options);
		Check.ArgumentNull(Theme);
		Check.ArgumentNull(Theme.QuestionStyle);
		Check.ArgumentNull(Theme.SelectOptionsStyle);
		Check.ArgumentNull(Theme.UserInputStyle);

		Flush();

		Theme.QuestionStyle.WriteLine(question);

		for (int i = 0; i < options.Length; i++)
		{
			Theme.SelectOptionsStyle.WriteLine($"  [{i + 1}] {options[i]}");
		}

		while (true)
		{
			Theme.QuestionStyle.Write(Theme.SelectOptionsPromptText);
			Theme.UserInputStyle.Write(" ");

			if (Theme.UserInputStyle.ReadLine().ToInt32OrNull() is int selection &&
				selection >= 1 &&
				selection <= options.Length)
			{
				return selection;
			}
		}
	}

	private static void Flush()
	{
		// Important: If the user presses return, this would skip a confirmation that appears even a minute later.
		// So, flush the input buffer directly before prompting user input.

		while (Console.KeyAvailable)
		{
			Console.ReadKey(true);
		}
	}
}