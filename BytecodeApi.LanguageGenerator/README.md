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

**Output:**

```
Ridavajoob
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
	FinishPunctuation = ['.', '.', '.', '?', '!']
};

string randomSentence = sentenceGenerator.Generate();
```

**Output:**

```
Gawunumo refuttaazi tepaba cirumos narerr, esizibe?
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
	MaxSentenceCount = 20,
	LineBreakChance = 0,
	ParagraphChance = .1
};

string randomText = textGenerator.Generate();
```

**Output:**

```
Ihotovewii yyewilezo bumi ahi inu agakooqqoz voperosi, uqigora ibisazut! Popime oddupulih, okis, ciinicoz orrib, naarruqqin furaveeto apezoyu kaliniicaa oju. Retokapag, ibbutakuyi efov! Irudis, zenexere ure evvuse ugecexi, ugeqeh duujufire lopeeyomuu jebevol, yofukodeem. Kas, unoow ussu sifiizid, izzucuci uwaxavici ipamabenna yuqa ecuupo. Ilacac ahhitiic ebeqodinni eedaffaha, enohotadi uyi urocava toovorriy, ceequww kiheetale! Gecc, pomezzi kiigozafo ecufucu, eppaxayaz, qubeh, anezaqam wuujujakib. Adulo ovevi, uurolaq? Xuloxil yyigij iyihisoqef uqaziwodos onihiccot, ucabb. Avihinedag ohedivol oqoxuti vop sepatez geguni iyufore zafejar, zaxom! Wwiforimug uxihun jicucu, efuli zuuvane, vufira, inisotila tazexxooqu ezeluseqos. Yufirokim uhhewes awewoy jufelife melacig alejeyivir? Umaj laroyemmu obotooniya livu ummibe, ezuw. Ejokanit nadijuxi igepujeq oki bob mefelaaxe ottenoonni etezoow? Aatokenizz, oqocikozey yeh sibizz uqapuff uxatiwokki, ucoca ummu ofeh ayaraccaa!

Effa umamidor zorerewebi upikob hekuxuj votoxugaa! Umitte qagacuzii fabaquk accijaa cewosu?
```
</details>

## See also

* `NameGenerator` to generate first/last names
* `LoremIpsumGenerator` to generate text placeholders with lorem ipsum paragraphs.

## Changelog

### 5.0.0 (15.02.2026)

* **change:** Targeting .NET 10.0

### 4.0.0 (15.09.2025)

* **change:** Targeting .NET 9.0

### 3.0.0 (08.09.2023)

* Initial release