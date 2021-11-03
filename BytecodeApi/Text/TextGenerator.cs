using BytecodeApi.Mathematics;
using System.Text;

namespace BytecodeApi.Text
{
	/// <summary>
	/// Class that generates a random text sequence that match the pattern of real language.
	/// A <see cref="Text.SentenceGenerator" /> is used to generate the sentences that generated text are composed of.
	/// </summary>
	public class TextGenerator : ILanguageStringGenerator
	{
		/// <summary>
		/// Gets or sets the <see cref="SentenceGenerator" /> that is used to generate sentences.
		/// </summary>
		public SentenceGenerator SentenceGenerator { get; set; }
		/// <summary>
		/// Gets or sets the minimum number sentences used to build text.
		/// <para>The default value is 10</para>
		/// </summary>
		public int MinSentenceCount { get; set; }
		/// <summary>
		/// Gets or sets the maximum number sentences used to build text.
		/// <para>The default value is 20</para>
		/// </summary>
		public int MaxSentenceCount { get; set; }
		/// <summary>
		/// Gets or sets a <see cref="double" /> value that specifies the chance of a linebreak being inserted after a sentence, where 0.0 means no linebreaks and 1.0 means a linebreak between every sentence.
		/// <para>The default value is 0.0</para>
		/// </summary>
		public double LineBreakChance { get; set; }
		/// <summary>
		/// Gets or sets a <see cref="double" /> value that specifies the chance of a paragraph being inserted after a sentence, where 0.0 means no paragraphs and 1.0 means a paragraph between every sentence. Randomly picked paragraphs have precedence over linebreaks and do not occur consecutively.
		/// <para>The default value is 0.1</para>
		/// </summary>
		public double ParagraphChance { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TextGenerator" /> class with default values.
		/// </summary>
		public TextGenerator() : this(new SentenceGenerator())
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="TextGenerator" /> class with the specified <see cref="SentenceGenerator" /> and with default values (<see cref="MinSentenceCount" /> = 10, <see cref="MaxSentenceCount" /> = 20, <see cref="LineBreakChance" /> = 0.0, <see cref="ParagraphChance" /> = 0.1).
		/// </summary>
		/// <param name="sentenceGenerator">The <see cref="SentenceGenerator" /> to use for sentence generation.</param>
		public TextGenerator(SentenceGenerator sentenceGenerator)
		{
			Check.ArgumentNull(sentenceGenerator, nameof(sentenceGenerator));

			SentenceGenerator = sentenceGenerator;
			MinSentenceCount = 10;
			MaxSentenceCount = 20;
			LineBreakChance = 0;
			ParagraphChance = .1;
		}

		/// <summary>
		/// Generates a random sequence of sentences using the specified parameters of this <see cref="TextGenerator" /> instance.
		/// </summary>
		/// <returns>
		/// A new <see cref="string" /> with dynamically generated text.
		/// </returns>
		public string Generate()
		{
			Check.ArgumentNull(SentenceGenerator, nameof(SentenceGenerator));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(MinSentenceCount, nameof(MinSentenceCount));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(MaxSentenceCount, nameof(MaxSentenceCount));
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(MaxSentenceCount, MinSentenceCount, nameof(MaxSentenceCount), nameof(MinSentenceCount));
			Check.ArgumentOutOfRangeEx.Between0And1(LineBreakChance, nameof(LineBreakChance));
			Check.ArgumentOutOfRangeEx.Between0And1(ParagraphChance, nameof(ParagraphChance));

			StringBuilder stringBuilder = new StringBuilder();

			lock (MathEx._Random)
			{
				int sentences = MathEx._Random.Next(MinSentenceCount, MaxSentenceCount + 1);

				for (int i = 0; i < sentences; i++)
				{
					stringBuilder.Append(SentenceGenerator.Generate());

					if (MathEx._Random.NextDouble() < ParagraphChance) stringBuilder.AppendLine().AppendLine();
					else if (MathEx._Random.NextDouble() < LineBreakChance) stringBuilder.AppendLine();
					else stringBuilder.Append(" ");
				}
			}

			return stringBuilder.ToString().Trim();
		}
	}
}