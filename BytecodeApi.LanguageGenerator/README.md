# BytecodeApi.LanguageGenerator

Library for arbitrary generation of words, sentences, names, and other language elements.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.LanguageGenerator

<details>
<summary>WordGenerator</summary>

Generate a random word. The number of characters and the chance of two consecutive consonants or vocals can be specified in the constructor of `WordGenerator`.

```
WordGenerator wordGenerator = new()
{
	MinLength = 3,
	MaxLength = 10,
	DoubleConsonantChance = .1,
	DoubleVovelChance = .1
};

string randomWord = wordGenerator.Generate();
```
</details>

<details>
<summary>SentenceGenerator</summary>

Generate a random sentence. A `WordGenerator` is passed to this instance that generates each word. Additional settings, like how many commas are inserted, can be configured in the constructor of `SentenceGenerator`.

```
WordGenerator wordGenerator = ...;

SentenceGenerator sentenceGenerator = new()
{
	WordGenerator = wordGenerator,
	MinWords = 3,
	MaxWords = 10,
	CommaChance = .2,
	FinishPunctuation = new[] { '.', '.', '.', '?', '!' }
};

string randomSentence = sentenceGenerator.Generate();
```
</details>

<details>
<summary>TextGenerator</summary>

Generate random text with multiple sentences. A `SentenceGenerator` is passed to this instance that generates each sentence. Additional settings, like how many sentences are used, can be configured in the constructor of `TextGenerator`.

```
SentenceGenerator sentenceGenerator = ...;

TextGenerator textGenerator = new()
{
	SentenceGenerator = sentenceGenerator,
	MinSentenceCount = 10,
	MinSentenceCount = 20,
	LineBreakChance = 0,
	ParagraphChance = .1
};

string randomText = textGenerator.Generate();
```
</details>

## See also

* `NameGenerator` to generate first/last names
* `LoremIpsumGenerator` to generate text placeholders with lorem ipsum paragraphs.