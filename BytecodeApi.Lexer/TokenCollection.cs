using BytecodeApi.Extensions;
using System.Collections;

namespace BytecodeApi.Lexer;

/// <summary>
/// Represents a collection of <see cref="Token{TTokenType}" /> objects.
/// </summary>
/// <typeparam name="TTokenType">The <see langword="enum" /> type of the token.</typeparam>
public sealed class TokenCollection<TTokenType> : ICollection<Token<TTokenType>> where TTokenType : struct, IConvertible
{
	private readonly List<Token<TTokenType>> Tokens;
	/// <summary>
	/// Gets or sets the <see cref="Token{TTokenType}" /> at the specified index.
	/// </summary>
	/// <param name="index">The index at which to retrieve the <see cref="Token{TTokenType}" />.</param>
	public Token<TTokenType> this[int index]
	{
		get => Tokens[index];
		set => Tokens[index] = value;
	}
	/// <summary>
	/// Gets the number of rows in this <see cref="TokenCollection{TTokenType}" />.
	/// </summary>
	public int Count => Tokens.Count;
	/// <summary>
	/// Gets a value indicating whether the <see cref="TokenCollection{TTokenType}" /> is read-only.
	/// </summary>
	public bool IsReadOnly => false;

	/// <summary>
	/// Initializes a new instance of the <see cref="TokenCollection{TTokenType}" /> class.
	/// </summary>
	public TokenCollection()
	{
		Tokens = new();
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="TokenCollection{TTokenType}" /> class with the specified collection of tokens.
	/// </summary>
	/// <param name="tokens">A collection of <see cref="Token{TTokenType}" /> objects to add to this <see cref="TokenCollection{TTokenType}" />.</param>
	public TokenCollection(IEnumerable<Token<TTokenType>> tokens) : this()
	{
		Tokens.AddRange(tokens);
	}

	/// <summary>
	/// Adds a <see cref="Token{TTokenType}" /> to the end of the <see cref="TokenCollection{TTokenType}" />.
	/// </summary>
	/// <param name="item">The <see cref="Token{TTokenType}" /> to be added to the end of the <see cref="TokenCollection{TTokenType}" />.</param>
	public void Add(Token<TTokenType> item)
	{
		Tokens.Add(item);
	}
	/// <summary>
	/// Removes the first occurrence of a specific <see cref="Token{TTokenType}" /> from the <see cref="TokenCollection{TTokenType}" />.
	/// </summary>
	/// <param name="item">The <see cref="Token{TTokenType}" /> to remove from the <see cref="TokenCollection{TTokenType}" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="item" /> is successfully removed;
	/// otherwise, <see langword="false" />.
	/// This method also returns <see langword="false" />, if <paramref name="item" /> was not found in the <see cref="TokenCollection{TTokenType}" />.</returns>
	public bool Remove(Token<TTokenType> item)
	{
		return Tokens.Remove(item);
	}
	/// <summary>
	/// Removes all elements from the <see cref="TokenCollection{TTokenType}" />.
	/// </summary>
	public void Clear()
	{
		Tokens.Clear();
	}
	/// <summary>
	/// Determines whether an element is in the <see cref="TokenCollection{TTokenType}" />.
	/// </summary>
	/// <param name="item">The <see cref="Token{TTokenType}" /> to locate in the <see cref="TokenCollection{TTokenType}" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="item" /> is found in the <see cref="TokenCollection{TTokenType}" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Contains(Token<TTokenType> item)
	{
		return Tokens.Contains(item);
	}
	void ICollection<Token<TTokenType>>.CopyTo(Token<TTokenType>[] array, int arrayIndex)
	{
		Tokens.CopyTo(array, arrayIndex);
	}

	/// <summary>
	/// Splits this collection of <see cref="Token{TTokenType}" /> objects by a given separator.
	/// </summary>
	/// <param name="separator">The separator to split the tokens by.</param>
	/// <returns>
	/// A new array of token collections, where each collection represents the tokens between the separators.
	/// </returns>
	public TokenCollection<TTokenType>[] Split(string separator)
	{
		return Split(separator, false);
	}
	/// <summary>
	/// Splits this collection of <see cref="Token{TTokenType}" /> objects by a given separator.
	/// </summary>
	/// <param name="separator">The separator to split the tokens by.</param>
	/// <param name="throwIfHasEmptyParts"><see langword="true" /> to throw an exception, if more than one consecutive separator was found, or a separator is at the beginning or at the end.</param>
	/// <returns>
	/// A new array of token collections, where each collection represents the tokens between the separators.
	/// </returns>
	public TokenCollection<TTokenType>[] Split(string separator, bool throwIfHasEmptyParts)
	{
		List<TokenCollection<TTokenType>> parts = new();

		for (int index = 0; ;)
		{
			int separatorIndex = Tokens.Skip(index).IndexOf(token => token.Value == separator);

			if (separatorIndex != -1)
			{
				parts.Add(new(Tokens.Skip(index).Take(separatorIndex)));
				index += separatorIndex + 1;
			}
			else
			{
				parts.Add(new(Tokens.Skip(index)));
				break;
			}
		}

		if (throwIfHasEmptyParts && parts.Any(part => part.None()))
		{
			throw new ParseException(Tokens.FirstOrDefault()?.LineNumber ?? -1, $"Expression is empty.");
		}

		return parts.ToArray();
	}

	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <returns>
	/// A <see cref="string" /> that represents this instance containing all tokens separated by a whitespace character.
	/// </returns>
	public override string ToString()
	{
		return Tokens.Select(token => token.Value).AsString(" ");
	}
	/// <summary>
	/// Returns an enumerator that iterates through the <see cref="TokenCollection{TTokenType}" />.
	/// </summary>
	/// <returns>
	/// An enumerator that can be used to iterate through the <see cref="TokenCollection{TTokenType}" />.
	/// </returns>
	public IEnumerator<Token<TTokenType>> GetEnumerator()
	{
		return Tokens.GetEnumerator();
	}
	IEnumerator IEnumerable.GetEnumerator()
	{
		return Tokens.GetEnumerator();
	}
}