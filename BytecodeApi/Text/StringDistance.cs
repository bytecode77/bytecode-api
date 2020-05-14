using System;
using System.Linq;

namespace BytecodeApi.Text
{
	/// <summary>
	/// Class that provides algorithms to measure the distance between strings.
	/// </summary>
	public static class StringDistance
	{
		/// <summary>
		/// Measures the minimum number of single-character edits (i.e. insertions, deletions or substitutions) required to change one word into the other.
		/// </summary>
		/// <param name="strA">The first <see cref="string" /> to compare.</param>
		/// <param name="strB">The second <see cref="string" /> to compare.</param>
		/// <returns>
		/// The minimum number of single-character edits (i.e. insertions, deletions or substitutions) required to change one word into the other. If both strings are equal, 0 is returned.
		/// </returns>
		public static int Levenshtein(string strA, string strB)
		{
			Check.ArgumentNull(strA, nameof(strA));
			Check.ArgumentNull(strB, nameof(strB));

			return ComputeLevenshtein(strA, strB, false);
		}
		/// <summary>
		/// Measures the minimum number of single-character edits (i.e. insertions, deletions, substitutions or transpositions) required to change one word into the other.
		/// </summary>
		/// <param name="strA">The first <see cref="string" /> to compare.</param>
		/// <param name="strB">The second <see cref="string" /> to compare.</param>
		/// <returns>
		/// The minimum number of single-character edits (i.e. insertions, deletions, substitutions or transpositions) required to change one word into the other. If both strings are equal, 0 is returned.
		/// </returns>
		public static int DamerauLevenshtein(string strA, string strB)
		{
			Check.ArgumentNull(strA, nameof(strA));
			Check.ArgumentNull(strB, nameof(strB));

			return ComputeLevenshtein(strA, strB, true);
		}
		/// <summary>
		/// Measures the minimum number of substitutions required to change one <see cref="string" /> into another. The Hamming distance between two strings of equal length is the number of positions at which the corresponding characters are different.
		/// </summary>
		/// <param name="strA">The first <see cref="string" /> to compare.</param>
		/// <param name="strB">The second <see cref="string" /> to compare.</param>
		/// <returns>
		/// The minimum number of substitutions required to change one <see cref="string" /> into another. If both strings are equal, 0 is returned.
		/// </returns>
		public static int Hamming(string strA, string strB)
		{
			Check.ArgumentNull(strA, nameof(strA));
			Check.ArgumentNull(strB, nameof(strB));
			Check.Argument(strA.Length == strB.Length, nameof(strB), "Strings must have equal length.");

			if (strA == strB)
			{
				return 0;
			}
			else
			{
				return strA
					.Zip(strB, (c1, c2) => new { c1, c2 })
					.Count(m => m.c1 != m.c2);
			}
		}
		private static int ComputeLevenshtein(string strA, string strB, bool damerau)
		{
			if (strA == strB)
			{
				return 0;
			}
			else if (strA == "")
			{
				return strB.Length;
			}
			else if (strB == "")
			{
				return strA.Length;
			}
			else
			{
				int[,] matrix = new int[strA.Length + 1, strB.Length + 1];
				for (int x = 0; x <= strA.Length; x++) matrix[x, 0] = x;
				for (int y = 0; y <= strB.Length; y++) matrix[0, y] = y;

				for (int x = 1; x <= strA.Length; x++)
				{
					for (int y = 1; y <= strB.Length; y++)
					{
						int cost = (strA[x - 1] == strB[y - 1]) ? 0 : 1;
						int insertion = matrix[x, y - 1] + 1;
						int deletion = matrix[x - 1, y] + 1;
						int substitution = matrix[x - 1, y - 1];

						if (damerau)
						{
							int distance = Math.Min(insertion, Math.Min(deletion, substitution + cost));

							if (x > 1 && y > 1 && strA[x - 1] == strB[y - 2] && strA[x - 2] == strB[y - 1])
							{
								distance = Math.Min(distance, matrix[x - 2, y - 2] + cost);
							}

							matrix[x, y] = distance;
						}
						else
						{
							matrix[x, y] = Math.Min(Math.Min(deletion, insertion), substitution + cost);
						}
					}
				}

				return matrix[strA.Length, strB.Length];
			}
		}
	}
}