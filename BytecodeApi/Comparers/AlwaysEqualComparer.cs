using System.Collections;

namespace BytecodeApi.Comparers
{
	/// <summary>
	/// Represents an <see cref="object" /> comparison operation that always returns 0.
	/// </summary>
	public sealed class AlwaysEqualComparer : IComparer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AlwaysEqualComparer" /> class.
		/// </summary>
		public AlwaysEqualComparer()
		{
		}

		/// <summary>
		/// Takes two <see cref="object" /> parameters, but always returns 0;
		/// </summary>
		/// <param name="x">An <see cref="object" /> to compare to <paramref name="y" />.</param>
		/// <param name="y">An <see cref="object" /> to compare to <paramref name="x" />.</param>
		/// <returns>
		/// This method always returns 0.
		/// </returns>
		public int Compare(object x, object y)
		{
			return 0;
		}
	}
}