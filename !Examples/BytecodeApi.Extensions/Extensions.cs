using BytecodeApi.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// There are a lot of extensions. To not overwhelm the examples, here are just a few of them.
		// Each method represents a category of examples

		IEnumerableExtensions();
		StringExtensions();
		DefaultTypeExtensions();
		ArrayAndCollectionExtensions();

		// And many more...
		// See documentation of the BytecodeApi.Extensions namespace
	}

	private static void IEnumerableExtensions()
	{
		int[] array = new int[1000];
		for (int i = 0; i < array.Length; i++) array[i] = i;

		// Break down an array into chunks with a chunk size of 17
		foreach (IEnumerable<int> chunk in array.Chunk(17))
		{
			// ...
		}

		// Concatenate string array with CRLF
		string multiLineString = new[] { "line1", "line2", "line3" }.AsMultilineString();

		// Random sort an array
		int[] randomSorted = array.SortRandom().ToArray();

		// Equivalent of .OrderBy(i => i)
		int[] sorted = array.Sort().ToArray();

		// See the documentation for a full overview of all extension methods
	}
	private static void StringExtensions()
	{
		// Trivial, but useful - especially for producing readable LINQ queries

		string str1 = "".ToNullIfEmpty(); // null
		string str2 = "   ".ToNullIfEmptyOrWhiteSpace(); // null

		string str3 = "hello, world".SubstringFrom(", "); // "world"
		string str4 = "hello, world".SubstringFrom("xxx"); // "hello, world"
		string str5 = "hello, world".SubstringUntil(", "); // "hello"
		string str6 = "begin:hello".TrimStartString("begin"); // "hello"
		string str7 = str6.EnsureStartsWith("begin:"); // "begin:hello"
		byte[] bytes = "hello".ToAnsiBytes();

		int int1 = "123".ToInt32OrDefault(); // 123
		int int2 = "abc".ToInt32OrDefault(); // 0
		int? int3 = "abc".ToInt32OrNull(); // null
		DateTime? date = "2021-11-15".ToDateTime("yyyy-MM-dd");

		// And for all other default datatypes...
	}
	private static void DefaultTypeExtensions()
	{
		int? int1 = 0.ToNullIfDefault(); // null
		int? int2 = 123.ToNullIfDefault(); // 123

		// Equivalent methods implemented for all other default datatypes...

		bool isUpper = 'A'.IsUpper(); // true
		bool isHex = 'A'.IsHexadecimal(); // true
	}
	private static void ArrayAndCollectionExtensions()
	{
		// IsNullOrEmpty returns true for any of these cases
		bool empty1 = ((int[])null).IsNullOrEmpty();
		bool empty2 = new int[0].IsNullOrEmpty();
		bool empty3 = new object[] { null, null }.IsNullOrEmpty();

		bool equal = new byte[] { 1, 2, 3 }.Compare(new byte[] { 1, 2, 3 }); // true

		int index = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.FindSequence(new byte[] { 4, 5 }); // 3

		// BitArray extensions
		BitArray bitArray = new BitArray(10);

		bool[] convertedBitArray = bitArray.ToBooleanArray();
		int countTrue = bitArray.CountTrue(); // 0

		bitArray.SetRandomValues(true); // true = cryptographic random numbers
	}
}