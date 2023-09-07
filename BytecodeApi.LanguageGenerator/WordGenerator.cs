using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using BytecodeApi.Text;

namespace BytecodeApi.LanguageGenerator;

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
	/// Gets or sets a <see cref="double" /> value that specifies the chance of a consonant being inserted twice, where 0.0 means no double consonants and 1.0 means consonants are always inserted twice.
	/// <para>The default value is 0.1f</para>
	/// </summary>
	public double DoubleConsonantChance { get; set; }
	/// <summary>
	/// Gets or sets a <see cref="double" /> value that specifies the chance of a vovel being inserted twice, where 0.0 means no double vovels and 1.0 means vovels are always inserted twice.
	/// <para>The default value is 0.1</para>
	/// </summary>
	public double DoubleVovelChance { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="WordGenerator" /> class with default values.
	/// </summary>
	public WordGenerator()
	{
		MinLength = 3;
		MaxLength = 10;
		DoubleConsonantChance = .1;
		DoubleVovelChance = .1;
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
		Check.ArgumentOutOfRangeEx.GreaterEqual0(MinLength);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(MaxLength);
		Check.ArgumentOutOfRangeEx.GreaterEqualValue(MaxLength, MinLength);
		Check.ArgumentOutOfRangeEx.Between0And1(DoubleConsonantChance);
		Check.ArgumentOutOfRangeEx.Between0And1(DoubleVovelChance);

		string word = "";
		int length;

		length = MathEx.Random.Next(MinLength, MaxLength + 1);
		bool consonant = MathEx.Random.NextBoolean();

		while (word.Length < length)
		{
			string charset = consonant ? TextResources.Consonants : TextResources.Vovels;
			double chance = consonant ? DoubleConsonantChance : DoubleVovelChance;
			consonant = !consonant;

			char c = MathEx.Random.NextObject(charset.ToCharArray());
			word += c.Repeat(MathEx.Random.NextDouble() < chance ? 2 : 1);
		}

		return word[..length].ChangeCasing(casing);
	}
}