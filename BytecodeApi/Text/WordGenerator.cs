using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;

namespace BytecodeApi.Text
{
	/// <summary>
	/// Class that generates random words that match the pattern of real language.
	/// Word generation is typically used to create arbitrary text that looks like language.
	/// </summary>
	public class WordGenerator : ILanguageStringGenerator
	{
		/// <summary>
		/// Gets or sets the minimum length of a generated word.
		/// <para>The default value is 3</para>
		/// </summary>
		public int MinLength { get; set; }
		/// <summary>
		/// Gets or sets the maximum length of a generated word.
		/// <para>The default value is 10</para>
		/// </summary>
		public int MaxLength { get; set; }
		/// <summary>
		/// Gets or sets a <see cref="float" /> value that specifies the chance of a consonant being inserted twice, where 0.0f means no double consonants and 1.0f means consonants are always inserted twice.
		/// <para>The default value is 0.1f</para>
		/// </summary>
		public float DoubleConsonantChance { get; set; }
		/// <summary>
		/// Gets or sets a <see cref="float" /> value that specifies the chance of a vovel being inserted twice, where 0.0f means no double vovels and 1.0f means vovels are always inserted twice.
		/// <para>The default value is 0.1f</para>
		/// </summary>
		public float DoubleVovelChance { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="WordGenerator" /> class with default values.
		/// </summary>
		public WordGenerator()
		{
			MinLength = 3;
			MaxLength = 10;
			DoubleConsonantChance = .1f;
			DoubleVovelChance = .1f;
		}

		/// <summary>
		/// Generates a random word in camel case character casing using the specified parameters of this <see cref="WordGenerator" /> instance.
		/// </summary>
		/// <returns>
		/// A new <see cref="string" /> with a dynamically generated word in camel case character casing.
		/// </returns>
		public string Generate()
		{
			return Generate(StringCasing.CamelCase);
		}
		/// <summary>
		/// Generates a random word in the specified <see cref="StringCasing" /> using the specified parameters of this <see cref="WordGenerator" /> instance.
		/// </summary>
		/// <param name="casing">The <see cref="StringCasing" /> to be used for characters in the generated word.</param>
		/// <returns>
		/// A new <see cref="string" /> with a dynamically generated word in the specified <see cref="StringCasing" />.
		/// </returns>
		public string Generate(StringCasing casing)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(MinLength, nameof(MinLength));
			Check.ArgumentOutOfRangeEx.GreaterEqual0(MaxLength, nameof(MaxLength));
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(MaxLength, MinLength, nameof(MaxLength), nameof(MinLength));
			Check.ArgumentOutOfRangeEx.Between0And1(DoubleConsonantChance, nameof(DoubleConsonantChance));
			Check.ArgumentOutOfRangeEx.Between0And1(DoubleVovelChance, nameof(DoubleVovelChance));

			string word = "";
			int length;

			lock (MathEx._Random)
			{
				length = MathEx._Random.Next(MinLength, MaxLength + 1);
				bool consonant = MathEx._Random.NextBoolean();

				while (word.Length < length)
				{
					string charset = consonant ? TextResources.Consonants : TextResources.Vovels;
					float chance = consonant ? DoubleConsonantChance : DoubleVovelChance;
					consonant = !consonant;

					char c = MathEx._Random.NextObject(charset.ToCharArray());
					word += c.Repeat(MathEx._Random.NextSingle() < chance ? 2 : 1);
				}
			}

			return word.Left(length).ChangeCasing(casing);
		}
	}
}