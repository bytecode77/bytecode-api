using BytecodeApi;
using BytecodeApi.Text;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		WordGenerator wordGenerator = new WordGenerator
		{
			MinLength = 4,              // The word has between 4 and 9 characters
			MaxLength = 9,
			DoubleConsonantChance = .1, // 10 % chance of a double consonant (bb, cc, dd, ..)
			DoubleVovelChance = .3      // 30 % chance of a double vovel (aa, ii, oo, ...)
		};

		// Generate some words
		for (int i = 0; i < 10; i++)
		{
			Console.WriteLine(wordGenerator.Generate(StringCasing.CamelCase));
		}

		Console.ReadKey();
	}
}