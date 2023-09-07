namespace BytecodeApi.ConsoleUI;

/// <summary>
/// Represents a style for console text.
/// </summary>
public sealed class ConsoleStyle
{
	/// <summary>
	/// Gets or sets the text color.
	/// </summary>
	public ConsoleColor Foreground { get; set; }
	/// <summary>
	/// Gets or sets the background color.
	/// </summary>
	public ConsoleColor Background { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="ConsoleStyle" /> class.
	/// </summary>
	public ConsoleStyle() : this(ConsoleColor.Gray)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ConsoleStyle" /> class.
	/// </summary>
	/// <param name="foreground">The text color.</param>
	public ConsoleStyle(ConsoleColor foreground) : this(foreground, ConsoleColor.Black)
	{
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="ConsoleStyle" /> class.
	/// </summary>
	/// <param name="foreground">The text color.</param>
	/// <param name="background">The background color.</param>
	public ConsoleStyle(ConsoleColor foreground, ConsoleColor background)
	{
		Foreground = foreground;
		Background = background;
	}

	internal void Write(string value)
	{
		Do(() => Console.Write(value));
	}
	internal void WriteLine(string value)
	{
		Write(value);
		Console.WriteLine();
	}
	internal string ReadLine()
	{
		return Do(() => Console.ReadLine() ?? "");
	}
	private void Do(Action action)
	{
		Do<object?>(() =>
		{
			action();
			return null;
		});
	}
	private T Do<T>(Func<T> func)
	{
		try
		{
			Console.ForegroundColor = Foreground;
			Console.BackgroundColor = Background;

			return func();
		}
		finally
		{
			Console.ResetColor();
		}
	}
}