using BytecodeApi.IO.Interop;
using System;
using System.Windows.Forms;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Global keyboard hook:
		// Run application, switch to any other application and start typing
		// Captured keystrokes are displayed in the console window

		using (GlobalKeyboardHook hook = new GlobalKeyboardHook())
		{
			hook.KeyPressed += Hook_KeyPressed;

			Application.Run();
		}
	}

	private static void Hook_KeyPressed(object sender, KeyboardHookEventArgs e)
	{
		Console.WriteLine("Char = " + (e.KeyChar == '\0' ? "NULL" : e.KeyChar.ToString()) + ", ScanCode = " + e.ScanCode + ", KeyCode = " + e.KeyCode);
	}
}