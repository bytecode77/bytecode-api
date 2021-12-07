using BytecodeApi.Text;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// QuotedString is a representation of a string that is quoted and escaped.
		QuotedString str = "Hello, \"world\"";

		Console.WriteLine("String:   " + str.OriginalString);
		Console.WriteLine("Quoted:   " + str.ToString());
		Console.WriteLine("Verbatim: " + str.ToVerbatimString());
		Console.ReadKey();
	}
}