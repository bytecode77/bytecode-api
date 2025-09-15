using BytecodeApi.Extensions;
using BytecodeApi.Mathematics;
using System.Text;

namespace BytecodeApi.LanguageGenerator;

/// <summary>
/// Class that generates a random text sequence with lorem ipsum paragraphs.
/// </summary>
public class LoremIpsumGenerator : ILanguageStringGenerator
{
	private static readonly string[] DefaultParagraphs;
	/// <summary>
	/// Gets or sets an array of <see cref="string" /> values, each representing a paragraph to randomly select from during text generation.
	/// <para>The default value is a set of predefined paragraphs from the lorem ipsum text.</para>
	/// </summary>
	public string[] Paragraphs { get; set; }
	/// <summary>
	/// Gets or sets the minimum number of paragraphs used to build the text.
	/// <para>The default value is 3</para>
	/// </summary>
	public int MinParagraphCount { get; set; }
	/// <summary>
	/// Gets or sets the maximum number of paragraphs used to build the text.
	/// <para>The default value is 3</para>
	/// </summary>
	public int MaxParagraphCount { get; set; }
	/// <summary>
	/// <see langword="true" /> to use double line breaks between paragraphs; <see langword="false" /> to use only one line break.
	/// <para>The default value is <see langword="true" /></para>
	/// </summary>
	public bool UseDoubleLineBreaks { get; set; }

	static LoremIpsumGenerator()
	{
		DefaultParagraphs =
		[
			"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi imperdiet dignissim lectus nec fermentum. Ut interdum neque vel nisl sodales sollicitudin. Proin mattis elit odio, nec ornare erat dictum ac. Phasellus viverra sagittis enim eu laoreet. Vivamus turpis elit, efficitur vitae lectus sed, sodales blandit metus. Cras auctor ut eros interdum congue. Pellentesque malesuada commodo leo sed fringilla. Sed elementum mattis sem in consectetur. Sed sed cursus libero. Curabitur porttitor imperdiet vestibulum.",
			"Phasellus sed aliquam lectus, id aliquam tortor. Pellentesque neque magna, pharetra accumsan nisi a, faucibus rhoncus sem. Nulla ultricies tristique sagittis. Nam id odio non augue placerat accumsan. Aliquam porta eros eu tortor porttitor volutpat tristique nec nisl. Donec vel facilisis tortor. Vestibulum felis sapien, hendrerit non aliquet sit amet, sollicitudin venenatis tellus. Curabitur lacinia sapien dignissim felis mollis, in eleifend urna interdum.",
			"Praesent aliquam sapien id iaculis pretium. Integer ante ex, rhoncus a venenatis at, dictum sed sapien. Fusce at lobortis nibh. Vivamus lacinia, est in dignissim maximus, ante massa placerat est, ac molestie purus arcu et enim. Nunc congue ligula quis magna congue, sed vestibulum eros pharetra. Quisque id eleifend augue. Maecenas vitae tortor nec diam pulvinar eleifend. Nullam interdum nisl tellus, a consequat leo mattis eu. In hac habitasse platea dictumst. Nam rhoncus, ante at consequat molestie, libero augue aliquam ligula, id ullamcorper lorem turpis et augue. In nec congue nunc. Maecenas sit amet sem ut nunc ornare maximus. Nullam hendrerit egestas turpis, eget commodo metus placerat eu. Quisque tempus hendrerit facilisis. Fusce ac tincidunt enim, consequat dapibus nulla. Sed id sapien a purus egestas lacinia.",
			"Morbi at laoreet dui. Sed posuere odio eget quam porta tempor. Nam vestibulum dignissim enim, tincidunt porta velit volutpat eu. In non arcu in urna aliquam ultricies. Quisque massa augue, ornare sit amet lorem at, semper iaculis diam. Donec odio nulla, tincidunt eu malesuada in, vestibulum quis odio. Praesent tempus, sem non commodo ullamcorper, tortor arcu viverra purus, ac volutpat orci purus nec dui.",
			"Fusce cursus sapien sit amet ante scelerisque, ac viverra metus accumsan. In at rhoncus ipsum, eget luctus neque. Ut eget odio non dolor accumsan vehicula non sed diam. Sed hendrerit ac tortor id laoreet. Nunc quis eleifend mauris, at gravida elit. Suspendisse maximus faucibus massa non finibus. Vestibulum id diam mattis, ullamcorper metus pulvinar, semper odio. Ut lacus sem, porta sed tellus ac, venenatis aliquam odio. Suspendisse nec leo mi. Vivamus mollis risus risus, lacinia lacinia sapien porta ac.",
		];
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="LoremIpsumGenerator" /> class with default values.
	/// </summary>
	public LoremIpsumGenerator()
	{
		Paragraphs = DefaultParagraphs.ToArray();
		MinParagraphCount = 3;
		MaxParagraphCount = 3;
		UseDoubleLineBreaks = true;
	}

	/// <summary>
	/// Generates a random text sequence with lorem ipsum paragraphs using the specified parameters of this <see cref="LoremIpsumGenerator" /> instance.
	/// </summary>
	/// <returns>
	/// A new <see cref="string" /> with dynamically generated paragraphs.
	/// </returns>
	public string Generate()
	{
		Check.ArgumentNull(Paragraphs);
		Check.ArgumentEx.ArrayElementsRequired(Paragraphs);
		Check.ArgumentEx.ArrayValuesNotNull(Paragraphs);
		Check.ArgumentEx.ArrayValuesNotStringEmpty(Paragraphs);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(MinParagraphCount);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(MaxParagraphCount);
		Check.ArgumentOutOfRangeEx.GreaterEqualValue(MaxParagraphCount, MinParagraphCount);

		StringBuilder stringBuilder = new();
		int sentences = Random.Shared.Next(MinParagraphCount, MaxParagraphCount + 1);

		for (int i = 0; i < sentences; i++)
		{
			stringBuilder.AppendLine(Random.Shared.GetObject(Paragraphs).Trim());

			if (UseDoubleLineBreaks)
			{
				stringBuilder.AppendLine();
			}
		}

		return stringBuilder.ToString().Trim();
	}
}