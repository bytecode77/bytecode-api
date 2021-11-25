using BytecodeApi.Text;
using System;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		SentenceGenerator sentenceGenerator = new SentenceGenerator
		{
			MinWords = 3,                               // Sentence has between 3 and 10 words
			MaxWords = 10,
			CommaChance = .1,                           // 10 % of words have a "," behind them
			FinishPunctuation = "...?!".ToCharArray()   // 60 % ".", 20 % "?" and 20 % "!"
		};

		// SentenceGenerator is injected with a WordGenerator.
		//sentenceGenerator.WordGenerator = ...

		// Generate some sentences
		for (int i = 0; i < 10; i++)
		{
			Console.WriteLine(sentenceGenerator.Generate());
		}

		Console.ReadKey();
	}
}