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
		Tokens = [];
	}
	/// <summary>
	/// Initializes a new instance of the <see cref="TokenCollection{TTokenType}" /> class with the specified collection of tokens.
	/// </summary>
	/// <param name="tokens">A collection of <see cref="Token{TTokenType}" /> objects to add to this <see cref="TokenCollection{TTokenType}" />.</param>
	public TokenCollection(IEnumerable<Token<TTokenType>> tokens) : this()
	{
		Check.ArgumentNull(tokens);

		Tokens.AddRange(tokens);
	}

	/// <summary>
	/// Adds a <see cref="Token{TTokenType}" /> to the end of the <see cref="TokenCollection{TTokenType}" />.
	/// </summary>
	/// <param name="token">The <see cref="Token{TTokenType}" /> to be added to the end of the <see cref="TokenCollection{TTokenType}" />.</param>
	public void Add(Token<TTokenType> token)
	{
		Check.ArgumentNull(token);

		Tokens.Add(token);
	}
	/// <summary>
	/// Inserts the token at the specified index.
	/// </summary>
	/// <param name="index">The index at which the token should be inserted.</param>
	/// <param name="token">The <see cref="Token{TTokenType}" /> to be inserted.</param>
	public void Insert(int index, Token<TTokenType> token)
	{
		Check.ArgumentNull(token);

		Tokens.Insert(index, token);
	}
	/// <summary>
	/// Inserts the tokens of the given collection at a given index.
	/// </summary>
	/// <param name="index">The index at which the tokens should be inserted.</param>
	/// <param name="tokens">A collection of <see cref="Token{TTokenType}" /> objects to insert.</param>
	public void InsertRange(int index, IEnumerable<Token<TTokenType>> tokens)
	{
		Check.ArgumentNull(tokens);

		Tokens.InsertRange(index, tokens);
	}
	/// <summary>
	/// Removes the first occurrence of a specific <see cref="Token{TTokenType}" /> from the <see cref="TokenCollection{TTokenType}" />.
	/// </summary>
	/// <param name="token">The <see cref="Token{TTokenType}" /> to remove from the <see cref="TokenCollection{TTokenType}" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="token" /> is successfully removed;
	/// otherwise, <see langword="false" />.
	/// This method also returns <see langword="false" />, if <paramref name="token" /> was not found in the <see cref="TokenCollection{TTokenType}" />.
	/// </returns>
	public bool Remove(Token<TTokenType> token)
	{
		Check.ArgumentNull(token);

		return Tokens.Remove(token);
	}
	/// <summary>
	/// Removes a range of tokens from the <see cref="TokenCollection{TTokenType}" />.
	/// </summary>
	/// <param name="index">The starting index of the range of tokens to remove.</param>
	/// <param name="count">The number of tokens to remove.</param>
	public void RemoveRange(int index, int count)
	{
		Tokens.RemoveRange(index, count);
	}
	/// <summary>
	/// Removes the <see cref="Token{TTokenType}" /> at the given index.
	/// </summary>
	/// <param name="index">The index of the <see cref="Token{TTokenType}" /> to remove.</param>
	public void RemoveAt(int index)
	{
		Tokens.RemoveAt(index);
	}
	/// <summary>
	/// Removes all tokens from the <see cref="TokenCollection{TTokenType}" />.
	/// </summary>
	public void Clear()
	{
		Tokens.Clear();
	}
	/// <summary>
	/// Determines whether a <see cref="Token{TTokenType}" /> is in the <see cref="TokenCollection{TTokenType}" />.
	/// </summary>
	/// <param name="token">The <see cref="Token{TTokenType}" /> to locate in the <see cref="TokenCollection{TTokenType}" />.</param>
	/// <returns>
	/// <see langword="true" />, if <paramref name="token" /> is found in the <see cref="TokenCollection{TTokenType}" />;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public bool Contains(Token<TTokenType> token)
	{
		return Tokens.Contains(token);
	}
	void ICollection<Token<TTokenType>>.CopyTo(Token<TTokenType>[] array, int arrayIndex)
	{
		Tokens.CopyTo(array, arrayIndex);
	}

	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type)
	{
		return Find(type, 0);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, BracketType track)
	{
		return Find(type, 0, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, int startIndex)
	{
		return Find(type, startIndex, Tokens.Count - startIndex);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, int startIndex, BracketType track)
	{
		return Find(type, startIndex, Tokens.Count - startIndex, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, int startIndex, int count)
	{
		return Find(type, startIndex, count, BracketType.None);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, int startIndex, int count, BracketType track)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(startIndex);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);
		Check.ArgumentEx.OffsetAndLengthOutOfBounds(startIndex, count, Tokens.Count);

		int parenthesisCount = 0;
		int bracketCount = 0;
		int braceCount = 0;

		for (int i = startIndex; i < startIndex + count; i++)
		{
			if (Tokens[i].Is("(")) parenthesisCount++;
			else if (Tokens[i].Is(")")) parenthesisCount--;
			else if (Tokens[i].Is("[")) bracketCount++;
			else if (Tokens[i].Is("]")) bracketCount--;
			else if (Tokens[i].Is("{")) braceCount++;
			else if (Tokens[i].Is("}")) braceCount--;

			if ((!track.HasFlag(BracketType.Parenthesis) || parenthesisCount == 0) &&
				(!track.HasFlag(BracketType.Bracket) || bracketCount == 0) &&
				(!track.HasFlag(BracketType.Brace) || braceCount == 0) &&
				Tokens[i].Is(type))
			{
				return i;
			}
		}

		return -1;
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given types. If not found, returns -1.
	/// </summary>
	/// <param name="types">A collection of types to check this token against.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(params TTokenType[] types)
	{
		return Find(types, 0);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given types. If not found, returns -1.
	/// </summary>
	/// <param name="types">A collection of types to check this token against.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType[] types, BracketType track)
	{
		return Find(types, 0, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given types. If not found, returns -1.
	/// </summary>
	/// <param name="types">A collection of types to check this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType[] types, int startIndex)
	{
		return Find(types, startIndex, Tokens.Count - startIndex);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given types. If not found, returns -1.
	/// </summary>
	/// <param name="types">A collection of types to check this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType[] types, int startIndex, BracketType track)
	{
		return Find(types, startIndex, Tokens.Count - startIndex, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given types. If not found, returns -1.
	/// </summary>
	/// <param name="types">A collection of types to check this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType[] types, int startIndex, int count)
	{
		return Find(types, startIndex, count, BracketType.None);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given types. If not found, returns -1.
	/// </summary>
	/// <param name="types">A collection of types to check this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType[] types, int startIndex, int count, BracketType track)
	{
		Check.ArgumentNull(types);
		Check.ArgumentEx.ArrayElementsRequired(types);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(startIndex);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);
		Check.ArgumentEx.OffsetAndLengthOutOfBounds(startIndex, count, Tokens.Count);

		int parenthesisCount = 0;
		int bracketCount = 0;
		int braceCount = 0;

		for (int i = startIndex; i < startIndex + count; i++)
		{
			if (Tokens[i].Is("(")) parenthesisCount++;
			else if (Tokens[i].Is(")")) parenthesisCount--;
			else if (Tokens[i].Is("[")) bracketCount++;
			else if (Tokens[i].Is("]")) bracketCount--;
			else if (Tokens[i].Is("{")) braceCount++;
			else if (Tokens[i].Is("}")) braceCount--;

			if ((!track.HasFlag(BracketType.Parenthesis) || parenthesisCount == 0) &&
				(!track.HasFlag(BracketType.Bracket) || bracketCount == 0) &&
				(!track.HasFlag(BracketType.Brace) || braceCount == 0) &&
				Tokens[i].Is(types))
			{
				return i;
			}
		}

		return -1;
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value)
	{
		return Find(type, value, 0);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value, BracketType track)
	{
		return Find(type, value, 0, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value, int startIndex)
	{
		return Find(type, value, startIndex, Tokens.Count - startIndex);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value, int startIndex, BracketType track)
	{
		return Find(type, value, startIndex, Tokens.Count - startIndex, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value, int startIndex, int count)
	{
		return Find(type, value, startIndex, count, BracketType.None);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value, int startIndex, int count, BracketType track)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(startIndex);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);
		Check.ArgumentEx.OffsetAndLengthOutOfBounds(startIndex, count, Tokens.Count);

		int parenthesisCount = 0;
		int bracketCount = 0;
		int braceCount = 0;

		for (int i = startIndex; i < startIndex + count; i++)
		{
			if (Tokens[i].Is("(")) parenthesisCount++;
			else if (Tokens[i].Is(")")) parenthesisCount--;
			else if (Tokens[i].Is("[")) bracketCount++;
			else if (Tokens[i].Is("]")) bracketCount--;
			else if (Tokens[i].Is("{")) braceCount++;
			else if (Tokens[i].Is("}")) braceCount--;

			if ((!track.HasFlag(BracketType.Parenthesis) || parenthesisCount == 0) &&
				(!track.HasFlag(BracketType.Bracket) || bracketCount == 0) &&
				(!track.HasFlag(BracketType.Brace) || braceCount == 0) &&
				Tokens[i].Is(type, value))
			{
				return i;
			}
		}

		return -1;
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value, bool ignoreCase)
	{
		return Find(type, value, ignoreCase, 0);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value, bool ignoreCase, BracketType track)
	{
		return Find(type, value, ignoreCase, 0, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value, bool ignoreCase, int startIndex)
	{
		return Find(type, value, ignoreCase, startIndex, Tokens.Count - startIndex);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value, bool ignoreCase, int startIndex, BracketType track)
	{
		return Find(type, value, ignoreCase, startIndex, Tokens.Count - startIndex, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value, bool ignoreCase, int startIndex, int count)
	{
		return Find(type, value, ignoreCase, startIndex, count, BracketType.None);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token of a given type that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="type">The type to check this token against.</param>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(TTokenType type, string value, bool ignoreCase, int startIndex, int count, BracketType track)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(startIndex);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);
		Check.ArgumentEx.OffsetAndLengthOutOfBounds(startIndex, count, Tokens.Count);

		int parenthesisCount = 0;
		int bracketCount = 0;
		int braceCount = 0;

		for (int i = startIndex; i < startIndex + count; i++)
		{
			if (Tokens[i].Is("(")) parenthesisCount++;
			else if (Tokens[i].Is(")")) parenthesisCount--;
			else if (Tokens[i].Is("[")) bracketCount++;
			else if (Tokens[i].Is("]")) bracketCount--;
			else if (Tokens[i].Is("{")) braceCount++;
			else if (Tokens[i].Is("}")) braceCount--;

			if ((!track.HasFlag(BracketType.Parenthesis) || parenthesisCount == 0) &&
				(!track.HasFlag(BracketType.Bracket) || bracketCount == 0) &&
				(!track.HasFlag(BracketType.Brace) || braceCount == 0) &&
				Tokens[i].Is(type, value, ignoreCase))
			{
				return i;
			}
		}

		return -1;
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value)
	{
		return Find(value, 0);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value, BracketType track)
	{
		return Find(value, 0, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value, int startIndex)
	{
		return Find(value, startIndex, Tokens.Count - startIndex);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value, int startIndex, BracketType track)
	{
		return Find(value, startIndex, Tokens.Count - startIndex, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value, int startIndex, int count)
	{
		return Find(value, startIndex, count, BracketType.None);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value, int startIndex, int count, BracketType track)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(startIndex);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);
		Check.ArgumentEx.OffsetAndLengthOutOfBounds(startIndex, count, Tokens.Count);

		int parenthesisCount = 0;
		int bracketCount = 0;
		int braceCount = 0;

		for (int i = startIndex; i < startIndex + count; i++)
		{
			if (Tokens[i].Is("(")) parenthesisCount++;
			else if (Tokens[i].Is(")")) parenthesisCount--;
			else if (Tokens[i].Is("[")) bracketCount++;
			else if (Tokens[i].Is("]")) bracketCount--;
			else if (Tokens[i].Is("{")) braceCount++;
			else if (Tokens[i].Is("}")) braceCount--;

			if ((!track.HasFlag(BracketType.Parenthesis) || parenthesisCount == 0) &&
				(!track.HasFlag(BracketType.Bracket) || bracketCount == 0) &&
				(!track.HasFlag(BracketType.Brace) || braceCount == 0) &&
				Tokens[i].Is(value))
			{
				return i;
			}
		}

		return -1;
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value, bool ignoreCase)
	{
		return Find(value, ignoreCase, 0);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value, bool ignoreCase, BracketType track)
	{
		return Find(value, ignoreCase, 0, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value, bool ignoreCase, int startIndex)
	{
		return Find(value, ignoreCase, startIndex, Tokens.Count - startIndex);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value, bool ignoreCase, int startIndex, BracketType track)
	{
		return Find(value, ignoreCase, startIndex, Tokens.Count - startIndex, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value, bool ignoreCase, int startIndex, int count)
	{
		return Find(value, ignoreCase, startIndex, count, BracketType.None);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches a given value. If not found, returns -1.
	/// </summary>
	/// <param name="value">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string value, bool ignoreCase, int startIndex, int count, BracketType track)
	{
		Check.ArgumentOutOfRangeEx.GreaterEqual0(startIndex);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);
		Check.ArgumentEx.OffsetAndLengthOutOfBounds(startIndex, count, Tokens.Count);

		int parenthesisCount = 0;
		int bracketCount = 0;
		int braceCount = 0;

		for (int i = startIndex; i < startIndex + count; i++)
		{
			if (Tokens[i].Is("(")) parenthesisCount++;
			else if (Tokens[i].Is(")")) parenthesisCount--;
			else if (Tokens[i].Is("[")) bracketCount++;
			else if (Tokens[i].Is("]")) bracketCount--;
			else if (Tokens[i].Is("{")) braceCount++;
			else if (Tokens[i].Is("}")) braceCount--;

			if ((!track.HasFlag(BracketType.Parenthesis) || parenthesisCount == 0) &&
				(!track.HasFlag(BracketType.Bracket) || bracketCount == 0) &&
				(!track.HasFlag(BracketType.Brace) || braceCount == 0) &&
				Tokens[i].Is(value, ignoreCase))
			{
				return i;
			}
		}

		return -1;
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(params string[] values)
	{
		return Find(values, 0);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string[] values, BracketType track)
	{
		return Find(values, 0, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string[] values, int startIndex)
	{
		return Find(values, startIndex, Tokens.Count - startIndex);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string[] values, int startIndex, BracketType track)
	{
		return Find(values, startIndex, Tokens.Count - startIndex, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string[] values, int startIndex, int count)
	{
		return Find(values, startIndex, count, BracketType.None);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string[] values, int startIndex, int count, BracketType track)
	{
		Check.ArgumentNull(values);
		Check.ArgumentEx.ArrayElementsRequired(values);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(startIndex);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);
		Check.ArgumentEx.OffsetAndLengthOutOfBounds(startIndex, count, Tokens.Count);

		int parenthesisCount = 0;
		int bracketCount = 0;
		int braceCount = 0;

		for (int i = startIndex; i < startIndex + count; i++)
		{
			if (Tokens[i].Is("(")) parenthesisCount++;
			else if (Tokens[i].Is(")")) parenthesisCount--;
			else if (Tokens[i].Is("[")) bracketCount++;
			else if (Tokens[i].Is("]")) bracketCount--;
			else if (Tokens[i].Is("{")) braceCount++;
			else if (Tokens[i].Is("}")) braceCount--;

			if ((!track.HasFlag(BracketType.Parenthesis) || parenthesisCount == 0) &&
				(!track.HasFlag(BracketType.Bracket) || bracketCount == 0) &&
				(!track.HasFlag(BracketType.Brace) || braceCount == 0) &&
				Tokens[i].Is(values))
			{
				return i;
			}
		}

		return -1;
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string[] values, bool ignoreCase)
	{
		return Find(values, ignoreCase, 0);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string[] values, bool ignoreCase, BracketType track)
	{
		return Find(values, ignoreCase, 0, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string[] values, bool ignoreCase, int startIndex)
	{
		return Find(values, ignoreCase, startIndex, Tokens.Count - startIndex);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string[] values, bool ignoreCase, int startIndex, BracketType track)
	{
		return Find(values, ignoreCase, startIndex, Tokens.Count - startIndex, track);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string[] values, bool ignoreCase, int startIndex, int count)
	{
		return Find(values, ignoreCase, startIndex, count, BracketType.None);
	}
	/// <summary>
	/// Searches the <see cref="TokenCollection{TTokenType}" /> for the first occurrence of a token that matches any of the given values. If not found, returns -1.
	/// </summary>
	/// <param name="values">The value to match this token against.</param>
	/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during value comparison.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Matches inside these brackets will be ignored.</param>
	/// <returns>
	/// The index of the first occurrence of the token and -1, if not found.
	/// </returns>
	public int Find(string[] values, bool ignoreCase, int startIndex, int count, BracketType track)
	{
		Check.ArgumentNull(values);
		Check.ArgumentEx.ArrayElementsRequired(values);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(startIndex);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);
		Check.ArgumentEx.OffsetAndLengthOutOfBounds(startIndex, count, Tokens.Count);

		int parenthesisCount = 0;
		int bracketCount = 0;
		int braceCount = 0;

		for (int i = startIndex; i < startIndex + count; i++)
		{
			if (Tokens[i].Is("(")) parenthesisCount++;
			else if (Tokens[i].Is(")")) parenthesisCount--;
			else if (Tokens[i].Is("[")) bracketCount++;
			else if (Tokens[i].Is("]")) bracketCount--;
			else if (Tokens[i].Is("{")) braceCount++;
			else if (Tokens[i].Is("}")) braceCount--;

			if ((!track.HasFlag(BracketType.Parenthesis) || parenthesisCount == 0) &&
				(!track.HasFlag(BracketType.Bracket) || bracketCount == 0) &&
				(!track.HasFlag(BracketType.Brace) || braceCount == 0) &&
				Tokens[i].Is(values, ignoreCase))
			{
				return i;
			}
		}

		return -1;
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
		return Split(separator, 0, Tokens.Count);
	}
	/// <summary>
	/// Splits this collection of <see cref="Token{TTokenType}" /> objects by a given separator.
	/// </summary>
	/// <param name="separator">The separator to split the tokens by.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Separators inside these brackets will be ignored.</param>
	/// <returns>
	/// A new array of token collections, where each collection represents the tokens between the separators.
	/// </returns>
	public TokenCollection<TTokenType>[] Split(string separator, BracketType track)
	{
		return Split(separator, 0, Tokens.Count, track);
	}
	/// <summary>
	/// Splits this collection of <see cref="Token{TTokenType}" /> objects by a given separator.
	/// </summary>
	/// <param name="separator">The separator to split the tokens by.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Separators inside these brackets will be ignored.</param>
	/// <param name="throwIfHasEmptyParts"><see langword="true" /> to throw an exception, if more than one consecutive separator was found, or a separator is at the beginning or at the end.</param>
	/// <returns>
	/// A new array of token collections, where each collection represents the tokens between the separators.
	/// </returns>
	public TokenCollection<TTokenType>[] Split(string separator, BracketType track, bool throwIfHasEmptyParts)
	{
		return Split(separator, 0, Tokens.Count, track, throwIfHasEmptyParts);
	}
	/// <summary>
	/// Splits this collection of <see cref="Token{TTokenType}" /> objects by a given separator.
	/// </summary>
	/// <param name="separator">The separator to split the tokens by.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <returns>
	/// A new array of token collections, where each collection represents the tokens between the separators.
	/// </returns>
	public TokenCollection<TTokenType>[] Split(string separator, int startIndex)
	{
		return Split(separator, startIndex, Tokens.Count - startIndex);
	}
	/// <summary>
	/// Splits this collection of <see cref="Token{TTokenType}" /> objects by a given separator.
	/// </summary>
	/// <param name="separator">The separator to split the tokens by.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Separators inside these brackets will be ignored.</param>
	/// <returns>
	/// A new array of token collections, where each collection represents the tokens between the separators.
	/// </returns>
	public TokenCollection<TTokenType>[] Split(string separator, int startIndex, BracketType track)
	{
		return Split(separator, startIndex, Tokens.Count - startIndex, track);
	}
	/// <summary>
	/// Splits this collection of <see cref="Token{TTokenType}" /> objects by a given separator.
	/// </summary>
	/// <param name="separator">The separator to split the tokens by.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Separators inside these brackets will be ignored.</param>
	/// <param name="throwIfHasEmptyParts"><see langword="true" /> to throw an exception, if more than one consecutive separator was found, or a separator is at the beginning or at the end.</param>
	/// <returns>
	/// A new array of token collections, where each collection represents the tokens between the separators.
	/// </returns>
	public TokenCollection<TTokenType>[] Split(string separator, int startIndex, BracketType track, bool throwIfHasEmptyParts)
	{
		return Split(separator, startIndex, Tokens.Count - startIndex, track, throwIfHasEmptyParts);
	}
	/// <summary>
	/// Splits this collection of <see cref="Token{TTokenType}" /> objects by a given separator.
	/// </summary>
	/// <param name="separator">The separator to split the tokens by.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <returns>
	/// A new array of token collections, where each collection represents the tokens between the separators.
	/// </returns>
	public TokenCollection<TTokenType>[] Split(string separator, int startIndex, int count)
	{
		return Split(separator, startIndex, count, BracketType.None);
	}
	/// <summary>
	/// Splits this collection of <see cref="Token{TTokenType}" /> objects by a given separator.
	/// </summary>
	/// <param name="separator">The separator to split the tokens by.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Separators inside these brackets will be ignored.</param>
	/// <returns>
	/// A new array of token collections, where each collection represents the tokens between the separators.
	/// </returns>
	public TokenCollection<TTokenType>[] Split(string separator, int startIndex, int count, BracketType track)
	{
		return Split(separator, startIndex, count, track, false);
	}
	/// <summary>
	/// Splits this collection of <see cref="Token{TTokenType}" /> objects by a given separator.
	/// </summary>
	/// <param name="separator">The separator to split the tokens by.</param>
	/// <param name="startIndex">The zero-based starting position to start searching from.</param>
	/// <param name="count">The number of tokens to include in the search.</param>
	/// <param name="track">The <see cref="BracketType" /> flags that specify, which types of brackets to keep track of. Separators inside these brackets will be ignored.</param>
	/// <param name="throwIfHasEmptyParts"><see langword="true" /> to throw an exception, if more than one consecutive separator was found, or a separator is at the beginning or at the end.</param>
	/// <returns>
	/// A new array of token collections, where each collection represents the tokens between the separators.
	/// </returns>
	public TokenCollection<TTokenType>[] Split(string separator, int startIndex, int count, BracketType track, bool throwIfHasEmptyParts)
	{
		Check.ArgumentNull(separator);
		Check.ArgumentEx.StringNotEmpty(separator);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(startIndex);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);
		Check.ArgumentEx.OffsetAndLengthOutOfBounds(startIndex, count, Tokens.Count);

		List<TokenCollection<TTokenType>> parts = [];

		TokenCollection<TTokenType> currentPart = [];
		int parenthesisCount = 0;
		int bracketCount = 0;
		int braceCount = 0;

		for (int i = startIndex; i < startIndex + count; i++)
		{
			if (Tokens[i].Is("(")) parenthesisCount++;
			else if (Tokens[i].Is(")")) parenthesisCount--;
			else if (Tokens[i].Is("[")) bracketCount++;
			else if (Tokens[i].Is("]")) bracketCount--;
			else if (Tokens[i].Is("{")) braceCount++;
			else if (Tokens[i].Is("}")) braceCount--;

			if ((!track.HasFlag(BracketType.Parenthesis) || parenthesisCount == 0) &&
				(!track.HasFlag(BracketType.Bracket) || bracketCount == 0) &&
				(!track.HasFlag(BracketType.Brace) || braceCount == 0) &&
				Tokens[i].Is(separator))
			{
				parts.Add(currentPart);
				currentPart = [];
			}
			else
			{
				currentPart.Add(Tokens[i]);
			}
		}

		if (currentPart.Any())
		{
			parts.Add(currentPart);
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
	/// A <see cref="string" /> with all tokens concatenated.
	/// </returns>
	public override string ToString()
	{
		return ToString(null);
	}
	/// <summary>
	/// Returns a <see cref="string" /> that represents this instance.
	/// </summary>
	/// <param name="separator">A <see cref="string" /> value specifying the separator between each token.</param>
	/// <returns>
	/// A <see cref="string" /> with all tokens concatenated, separated by the specified separator.
	/// </returns>
	public string ToString(string? separator)
	{
		return Tokens.Select(token => token.Value).AsString(separator);
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