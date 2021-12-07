using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using System.Text;

namespace BytecodeApi.Text
{
	/// <summary>
	/// Class that generates random sentences that match the pattern of real language.
	/// A <see cref="Text.WordGenerator" /> is used to generate the words that generated sentences are composed of.
	/// </summary>
	public class SentenceGenerator : ILanguageStringGenerator
	{
		/// <summary>
		/// Gets or sets the <see cref="WordGenerator" /> that is used to generate words.
		/// </summary>
		public WordGenerator WordGenerator { get; set; }
		/// <summary>
		/// Gets or sets the minimum number words used to build sentences.
		/// <para>The default value is 3</para>
		/// </summary>
		public int MinWords { get; set; }
		/// <summary>
		/// Gets or sets the maximum number words used to build sentences.
		/// <para>The default value is 10</para>
		/// </summary>
		public int MaxWords { get; set; }
		/// <summary>
		/// Gets or sets a <see cref="double" /> value that specifies the chance of a comma being inserted after a word, where 0.0 means no commas and 1.0 means a comma between every word.
		/// <para>The default value is 0.2</para>
		/// </summary>
		public double CommaChance { get; set; }
		/// <summary>
		/// Gets or sets the characters that are used for punctuation after a sentence. Including a character multiple times increases the chance for this character. For example, this can be used to increase the likelihood of a period over a question mark or exclamation mark. Character order is not relevant.
		/// <para>The default value is "...?!"</para>
		/// </summary>
		public char[] FinishPunctuation { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SentenceGenerator" /> class with default values.
		/// </summary>
		public SentenceGenerator() : this(new WordGenerator())
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="SentenceGenerator" /> class with the specified <see cref="WordGenerator" /> and with default values (<see cref="MinWords" /> = 3, <see cref="MaxWords" /> = 10, <see cref="CommaChance" /> = 0.2, <see cref="FinishPunctuation" /> = "...?!").
		/// </summary>
		/// <param name="wordGenerator">The <see cref="WordGenerator" /> to use for word generation.</param>
		public SentenceGenerator(WordGenerator wordGenerator)
		{
			Check.ArgumentNull(wordGenerator, nameof(wordGenerator));

			WordGenerator = wordGenerator;
			MinWords = 3;
			MaxWords = 10;
			CommaChance = .2;
			FinishPunctuation = "...?!".ToCharArray();
		}

		/// <summary>
		/// Generates a random sentence using the specified parameters of this <see cref="SentenceGenerator" /> instance.
		/// </summary>
		/// <returns>
		/// A new <see cref="string" /> with a dynamically generated sentence.
		/// </returns>
		public string Generate()
		{
			Check.ArgumentNull(WordGenerator, nameof(WordGenerator));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(MinWords, nameof(MinWords));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(MaxWords, nameof(MaxWords));
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(MaxWords, MinWords, nameof(MaxWords), nameof(MinWords));
			Check.ArgumentOutOfRangeEx.Between0And1(CommaChance, nameof(CommaChance));
			Check.ArgumentNull(FinishPunctuation, nameof(FinishPunctuation));
			Check.ArgumentEx.ArrayElementsRequired(FinishPunctuation, nameof(FinishPunctuation));

			StringBuilder stringBuilder = new StringBuilder();

			lock (MathEx._Random)
			{
				int words = MathEx._Random.Next(MinWords, MaxWords + 1);

				for (int i = 0; i < words; i++)
				{
					stringBuilder.Append(WordGenerator.Generate(i == 0 ? StringCasing.CamelCase : StringCasing.Lower));
					if (i < words - 1)
					{
						if (MathEx._Random.NextDouble() < CommaChance) stringBuilder.Append(",");
						stringBuilder.Append(" ");
					}
				}

				return stringBuilder.Append(MathEx._Random.NextObject(FinishPunctuation)).ToString();
			}
		}
	}
}