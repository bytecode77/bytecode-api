# BytecodeApi.Lexer

General purpose tokenizer for parsing of any syntax.

## Examples

### ![](http://bytecode77.com/public/vs/namespace.png) BytecodeApi.Lexer

<details>
<summary>Lexer</summary>

Lexical analysis is the first phase that a parser or compiler needs to do in order to break down code into its smallest parts.

First, an enum is needed that represents the type of a token:

```
public enum FormulaTokenType
{
	OpenParenthesis,
	CloseParenthesis,
	Operator,
	Integer
}
```

Next, create a `Lexer<YourTokenType>` with regular expressions for each grammar rule:

```
Lexer<FormulaTokenType> lexer = Lexer
	.Create<FormulaTokenType>()
	.Ignore(@"[ \t\r\n]+")
	.Match(FormulaTokenType.OpenParenthesis, @"\(")
	.Match(FormulaTokenType.CloseParenthesis, @"\)")
	.Match(FormulaTokenType.Integer, @"[\+-]?[0-9]+")
	.Match(FormulaTokenType.Operator, @"\+|\-|\*|\/");
```

Finally, to tokenize a string, call the `Parse` method. The result is a collection of tokens for further processing.

```
string formula = "(3 + 4) * 15 / ((-10 - 5) * 3)";
TokenCollection<FormulaTokenType> tokens = lexer.Parse(formula);
```
</details>

## Changelog

### 5.0.0 (15.02.2026)

* **change:** Targeting .NET 10.0

### 4.0.0 (15.09.2025)

* **change:** Targeting .NET 9.0
* **new:** `Token.Is` method overloads

### 3.0.1 (27.09.2023)

* **new:** `Lexer` method overloads with `RegexOptions` parameter
* **change:** Replaced `Func<string, string>?` parameters with `Func<Match, string>?`

### 3.0.0 (08.09.2023)

* Initial release