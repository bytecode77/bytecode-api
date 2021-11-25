using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using BytecodeApi.Text;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// The Wording class manipulates strings in a linguistic fashion to create readable objects.

		Console.WriteLine("Formatted binary output:");
		Console.WriteLine(Wording.FormatBinary(MathEx.Random.NextBytes(50)));

		Console.WriteLine("Join strings, but use 'and' as last separator:");
		Console.WriteLine(Wording.JoinStrings(", ", " and ", "Apples", "Pears", "Bananas"));
		Console.WriteLine();

		Console.WriteLine("Formatted timespan:");
		Console.WriteLine(Wording.FormatTimeSpan(new TimeSpan(2, 30, 11))); // To include seconds, specify the maxElements parameter
		Console.WriteLine();

		Console.WriteLine("Wrapping at 50 characters, without overflow:");
		Console.WriteLine();
		Console.WriteLine(Wording.WrapText(TextResources.LoremIpsum, 50, false));
		Console.WriteLine();
	}
}