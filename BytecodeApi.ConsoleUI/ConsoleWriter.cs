using BytecodeApi.Extensions;
using System.Runtime.InteropServices;

namespace BytecodeApi.ConsoleUI;

/// <summary>
/// Class that writes formatted text to the <see cref="Console" />.
/// </summary>
public static class ConsoleWriter
{
	private static readonly object SyncRoot = new();
	private static int PreviewLineCount;
	/// <summary>
	/// Gets or sets the global theme for the <see cref="ConsoleWriter" /> class.
	/// </summary>
	public static ConsoleWriterTheme Theme { get; set; }

	static ConsoleWriter()
	{
		// Remove console mode ENABLE_VIRTUAL_TERMINAL_PROCESSING, which is set in Windows Terminal app, but not in cmd.exe
		// It causes issues with Console.SetCursorPosition()

		nint console = Native.GetStdHandle(-11);
		if (console == 0 || console == -1)
		{
			throw Throw.Win32("GetStdHandle failed.");
		}

		if (!Native.GetConsoleMode(console, out int mode))
		{
			throw Throw.Win32("GetConsoleMode failed.");
		}

		if (!Native.SetConsoleMode(console, mode & ~4))
		{
			throw Throw.Win32("SetConsoleMode failed.");
		}

		Theme = new();
	}

	/// <summary>
	/// Writes a <see cref="string" /> to the console that acts as a preview. This message is erased from the console, after a successive write.
	/// </summary>
	/// <param name="message">The <see cref="string" /> to write.</param>
	public static void Preview(string message)
	{
		Check.ArgumentNull(message);
		Check.ArgumentNull(Theme);
		Check.ArgumentNull(Theme.PreviewStyle);
		Check.ArgumentNull(Theme.PreviewIcon);

		lock (SyncRoot)
		{
			RemovePreviewLines();

			message = $"{GetTimeStamp()}{Theme.PreviewIcon}{(Theme.PreviewIcon == "" ? "" : " ")}{message}";
			Theme.PreviewStyle.WriteLine(message);

			PreviewLineCount += (message.Length + Console.WindowWidth) / Console.WindowWidth;
		}
	}
	/// <summary>
	/// Writes a <see cref="string" /> to the console and renders a progress bar that act as a preview. This message is erased from the console, after a successive write.
	/// </summary>
	/// <param name="message">The <see cref="string" /> to write.</param>
	/// <param name="progress">The percentage, in range of 0..100, of the progress bar.</param>
	/// <param name="progressText">The text next to the progress bar.</param>
	public static void Preview(string message, double progress, string progressText)
	{
		Check.ArgumentNull(message);
		Check.ArgumentNull(progressText);
		Check.ArgumentNull(Theme);
		Check.ArgumentNull(Theme.PreviewStyle);

		Preview(message);

		lock (SyncRoot)
		{
			int progressLineWidth = Console.WindowWidth - 2;
			if (progressText != null)
			{
				progressText = progressText.Left(progressLineWidth - 10);
				progressLineWidth -= progressText.Length + 1;
			}

			int progressBarSize = (int)(Math.Clamp(progress, 0, 100) / 100 * progressLineWidth);
			//TODO: ░ character not visible in Windows Terminal
			string progressLine =
				Theme.ProgressBarBeginChar +
				new string(Theme.ProgressBarFullChar, progressBarSize) +
				new string(Theme.ProgressBarEmptyChar, progressLineWidth - progressBarSize) +
				Theme.ProgressBarEndChar;

			if (progressText != null)
			{
				progressLine = $"{progressText} {progressLine}";
			}

			Theme.PreviewStyle.Write(progressLine);
			PreviewLineCount++;
		}
	}
	/// <summary>
	/// Writes the specified <see cref="string" /> to the console without a timestamp.
	/// </summary>
	/// <param name="message">The <see cref="string" /> to write.</param>
	public static void Write(string message)
	{
		Check.ArgumentNull(message);
		Check.ArgumentNull(Theme);
		Check.ArgumentNull(Theme.TextStyle);

		Write(message, Theme.TextStyle);
	}
	/// <summary>
	/// Writes the specified <see cref="string" /> to the console without a timestamp.
	/// </summary>
	/// <param name="message">The <see cref="string" /> to write.</param>
	/// <param name="style">The style to use when writing to the console.</param>
	public static void Write(string message, ConsoleStyle style)
	{
		Check.ArgumentNull(message);
		Check.ArgumentNull(style);

		lock (SyncRoot)
		{
			RemovePreviewLines();
			style.Write(message);
		}
	}
	/// <summary>
	/// Writes a line terminator to the console.
	/// </summary>
	public static void WriteLine()
	{
		WriteLine("", new());
	}
	/// <summary>
	/// Writes the specified <see cref="string" /> to the console without a timestamp, followed by a line terminator.
	/// </summary>
	/// <param name="message">The <see cref="string" /> to write.</param>
	public static void WriteLine(string message)
	{
		Check.ArgumentNull(message);
		Check.ArgumentNull(Theme);
		Check.ArgumentNull(Theme.TextStyle);

		WriteLine(message, Theme.TextStyle);
	}
	/// <summary>
	/// Writes the specified <see cref="string" /> to the console without a timestamp, followed by a line terminator.
	/// </summary>
	/// <param name="message">The <see cref="string" /> to write.</param>
	/// <param name="style">The style to use when writing to the console.</param>
	public static void WriteLine(string message, ConsoleStyle style)
	{
		Check.ArgumentNull(message);
		Check.ArgumentNull(style);

		lock (SyncRoot)
		{
			RemovePreviewLines();
			style.WriteLine(message);
		}
	}
	/// <summary>
	/// Writes the specified <see cref="string" /> to the console using the success style.
	/// </summary>
	/// <param name="message">The <see cref="string" /> to write.</param>
	public static void Success(string message)
	{
		Check.ArgumentNull(message);
		Check.ArgumentNull(Theme);
		Check.ArgumentNull(Theme.SuccessStyle);
		Check.ArgumentNull(Theme.SuccessIcon);

		lock (SyncRoot)
		{
			RemovePreviewLines();
			Theme.SuccessStyle.WriteLine($"{GetTimeStamp()}{Theme.SuccessIcon}{(Theme.SuccessIcon == "" ? "" : " ")}{message}");
		}
	}
	/// <summary>
	/// Writes the specified <see cref="string" /> to the console using the warning style.
	/// </summary>
	/// <param name="message">The <see cref="string" /> to write.</param>
	public static void Warning(string message)
	{
		Check.ArgumentNull(message);
		Check.ArgumentNull(Theme);
		Check.ArgumentNull(Theme.WarningStyle);
		Check.ArgumentNull(Theme.WarningIcon);

		lock (SyncRoot)
		{
			RemovePreviewLines();
			Theme.WarningStyle.WriteLine($"{GetTimeStamp()}{Theme.WarningIcon}{(Theme.WarningIcon == "" ? "" : " ")}{message}");
		}
	}
	/// <summary>
	/// Writes the specified <see cref="string" /> to the console using the error style.
	/// </summary>
	/// <param name="message">The <see cref="string" /> to write.</param>
	public static void Error(string message)
	{
		Check.ArgumentNull(message);
		Check.ArgumentNull(Theme);
		Check.ArgumentNull(Theme.ErrorStyle);
		Check.ArgumentNull(Theme.ErrorIcon);

		lock (SyncRoot)
		{
			RemovePreviewLines();
			Theme.ErrorStyle.WriteLine($"{GetTimeStamp()}{Theme.ErrorIcon}{(Theme.ErrorIcon == "" ? "" : " ")}{message}");
		}
	}
	/// <summary>
	/// Writes the specified <see cref="Exception" />, including its full stack trace to the console using the error style.
	/// </summary>
	/// <param name="exception">The <see cref="Exception" /> to write. The message, the stack trace, and the stack traces of all inner exceptions is written.</param>
	public static void Error(Exception exception)
	{
		Check.ArgumentNull(exception);

		Error($"{exception.Message}\r\n{exception.FullStackTrace}");
	}

	private static void RemovePreviewLines()
	{
		Console.ResetColor();

		string emptyLine = new(' ', Console.WindowWidth);

		for (; PreviewLineCount > 0; PreviewLineCount--)
		{
			Console.SetCursorPosition(0, Console.CursorTop - 1);
			Console.Write(emptyLine);
			Console.SetCursorPosition(0, Console.CursorTop - 1);
		}
	}
	private static string GetTimeStamp()
	{
		if (Theme.ShowTimeStamp)
		{
			return DateTime.Now.ToStringInvariant(Theme.TimeStampFormat) + " ";
		}
		else
		{
			return "";
		}
	}
}

file static class Native
{
	[DllImport("kernel32.dll")]
	public static extern nint GetStdHandle(int stdHandle);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool GetConsoleMode(nint consoleHandle, out int mode);
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool SetConsoleMode(nint consoleHandle, int mode);
}