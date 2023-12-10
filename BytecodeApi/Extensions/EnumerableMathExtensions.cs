using System.Collections;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for computations with data structures that implement <see cref="IEnumerable" />.
/// </summary>
public static class EnumerableMathExtensions
{
	/// <summary>
	/// Computes the sum of the sequence of <see cref="TimeSpan" /> values.
	/// </summary>
	/// <param name="source">A sequence of <see cref="TimeSpan" /> values that are used to calculate a sum.</param>
	/// <returns>
	/// A new <see cref="TimeSpan" /> with the sum of the values.
	/// </returns>
	public static TimeSpan Sum(this IEnumerable<TimeSpan> source)
	{
		Check.ArgumentNull(source);

		long sum = 0;
		foreach (TimeSpan timeSpan in source)
		{
			sum += timeSpan.Ticks;
		}

		return new(sum);
	}
	/// <summary>
	/// Computes the sum of the sequence of <see cref="TimeSpan" /> values.
	/// </summary>
	/// <param name="source">A sequence of <see cref="TimeSpan" /> values that are used to calculate a sum.</param>
	/// <returns>
	/// A new <see cref="TimeSpan" /> with the sum of the values, or <see langword="null" /> if the source sequence is empty or contains only values that are <see langword="null" />.
	/// </returns>
	public static TimeSpan? Sum(this IEnumerable<TimeSpan?> source)
	{
		Check.ArgumentNull(source);

		long sum = 0;
		int count = 0;

		foreach (TimeSpan? timeSpan in source)
		{
			if (timeSpan != null)
			{
				sum += timeSpan.Value.Ticks;
				count++;
			}
		}

		return count == 0 ? null : new(sum);
	}
	/// <summary>
	/// Computes the average of a sequence of <see cref="TimeSpan" /> values.
	/// </summary>
	/// <param name="source">A sequence of <see cref="TimeSpan" /> values that are used to calculate a sum.</param>
	/// <returns>
	/// The average of the sequence of <paramref name="source" />.
	/// </returns>
	public static TimeSpan Average(this IEnumerable<TimeSpan> source)
	{
		Check.ArgumentNull(source);

		long sum = 0;
		int count = 0;

		foreach (TimeSpan timeSpan in source)
		{
			sum += timeSpan.Ticks;
			count++;
		}

		Check.ArgumentEx.EnumerableElementsRequired(count > 0, nameof(source));
		return new(sum / count);
	}
	/// <summary>
	/// Computes the average of a sequence of <see cref="TimeSpan" /> values.
	/// </summary>
	/// <param name="source">A sequence of <see cref="TimeSpan" /> values that are used to calculate a sum.</param>
	/// <returns>
	/// The average of the sequence of <paramref name="source" />, or <see langword="null" /> if the source sequence is empty or contains only values that are <see langword="null" />.
	/// </returns>
	public static TimeSpan? Average(this IEnumerable<TimeSpan?> source)
	{
		Check.ArgumentNull(source);

		long sum = 0;
		int count = 0;

		foreach (TimeSpan? timeSpan in source)
		{
			if (timeSpan != null)
			{
				sum += timeSpan.Value.Ticks;
				count++;
			}
		}

		return count == 0 ? null : new(sum / count);
	}
	/// <summary>
	/// Returns the median number of a sequence of <see cref="int" /> values. If the sequence contains an even number of elements, the mean of both medians is returned.
	/// </summary>
	/// <param name="source">A sequence of <see cref="int" /> values that are used to calculate a median.</param>
	/// <returns>
	/// The median of the sequence of <paramref name="source" />.
	/// </returns>
	public static double Median(this IEnumerable<int> source)
	{
		Check.ArgumentNull(source);

		int[] sorted = source.Order().ToArray();
		Check.ArgumentEx.EnumerableElementsRequired(sorted.Length > 0, nameof(source));

		if (sorted.Length % 2 == 0)
		{
			return (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]) / 2.0;
		}
		else
		{
			return sorted[sorted.Length / 2];
		}
	}
	/// <summary>
	/// Returns the median number of a sequence of <see cref="int" /> values. If the sequence contains an even number of elements, the mean of both medians is returned.
	/// </summary>
	/// <param name="source">A sequence of <see cref="int" /> values that are used to calculate a median.</param>
	/// <returns>
	/// The median of the sequence of <paramref name="source" />, or <see langword="null" /> if the source sequence is empty or contains only values that are <see langword="null" />.
	/// </returns>
	public static double? Median(this IEnumerable<int?> source)
	{
		Check.ArgumentNull(source);

		int[] sorted = source.ExceptNull().Order().ToArray();

		if (sorted.Length == 0)
		{
			return null;
		}
		else if (sorted.Length % 2 == 0)
		{
			return (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]) / 2.0;
		}
		else
		{
			return sorted[sorted.Length / 2];
		}
	}
	/// <summary>
	/// Returns the median number of a sequence of <see cref="long" /> values. If the sequence contains an even number of elements, the mean of both medians is returned.
	/// </summary>
	/// <param name="source">A sequence of <see cref="long" /> values that are used to calculate a median.</param>
	/// <returns>
	/// The median of the sequence of <paramref name="source" />.
	/// </returns>
	public static double Median(this IEnumerable<long> source)
	{
		Check.ArgumentNull(source);

		long[] sorted = source.Order().ToArray();
		Check.ArgumentEx.EnumerableElementsRequired(sorted.Length > 0, nameof(source));

		if (sorted.Length % 2 == 0)
		{
			return (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]) / 2.0;
		}
		else
		{
			return sorted[sorted.Length / 2];
		}
	}
	/// <summary>
	/// Returns the median number of a sequence of <see cref="long" /> values. If the sequence contains an even number of elements, the mean of both medians is returned.
	/// </summary>
	/// <param name="source">A sequence of <see cref="long" /> values that are used to calculate a median.</param>
	/// <returns>
	/// The median of the sequence of <paramref name="source" />, or <see langword="null" /> if the source sequence is empty or contains only values that are <see langword="null" />.
	/// </returns>
	public static double? Median(this IEnumerable<long?> source)
	{
		Check.ArgumentNull(source);

		long[] sorted = source.ExceptNull().Order().ToArray();

		if (sorted.Length == 0)
		{
			return null;
		}
		else if (sorted.Length % 2 == 0)
		{
			return (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]) / 2.0;
		}
		else
		{
			return sorted[sorted.Length / 2];
		}
	}
	/// <summary>
	/// Returns the median number of a sequence of <see cref="float" /> values. If the sequence contains an even number of elements, the mean of both medians is returned.
	/// </summary>
	/// <param name="source">A sequence of <see cref="float" /> values that are used to calculate a median.</param>
	/// <returns>
	/// The median of the sequence of <paramref name="source" />.
	/// </returns>
	public static double Median(this IEnumerable<float> source)
	{
		Check.ArgumentNull(source);

		float[] sorted = source.Order().ToArray();
		Check.ArgumentEx.EnumerableElementsRequired(sorted.Length > 0, nameof(source));

		if (sorted.Length % 2 == 0)
		{
			return (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]) / 2.0;
		}
		else
		{
			return sorted[sorted.Length / 2];
		}
	}
	/// <summary>
	/// Returns the median number of a sequence of <see cref="float" /> values. If the sequence contains an even number of elements, the mean of both medians is returned.
	/// </summary>
	/// <param name="source">A sequence of <see cref="float" /> values that are used to calculate a median.</param>
	/// <returns>
	/// The median of the sequence of <paramref name="source" />, or <see langword="null" /> if the source sequence is empty or contains only values that are <see langword="null" />.
	/// </returns>
	public static double? Median(this IEnumerable<float?> source)
	{
		Check.ArgumentNull(source);

		float[] sorted = source.ExceptNull().Order().ToArray();

		if (sorted.Length == 0)
		{
			return null;
		}
		else if (sorted.Length % 2 == 0)
		{
			return (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]) / 2.0;
		}
		else
		{
			return sorted[sorted.Length / 2];
		}
	}
	/// <summary>
	/// Returns the median number of a sequence of <see cref="double" /> values. If the sequence contains an even number of elements, the mean of both medians is returned.
	/// </summary>
	/// <param name="source">A sequence of <see cref="double" /> values that are used to calculate a median.</param>
	/// <returns>
	/// The median of the sequence of <paramref name="source" />.
	/// </returns>
	public static double Median(this IEnumerable<double> source)
	{
		Check.ArgumentNull(source);

		double[] sorted = source.Order().ToArray();
		Check.ArgumentEx.EnumerableElementsRequired(sorted.Length > 0, nameof(source));

		if (sorted.Length % 2 == 0)
		{
			return (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]) / 2.0;
		}
		else
		{
			return sorted[sorted.Length / 2];
		}
	}
	/// <summary>
	/// Returns the median number of a sequence of <see cref="double" /> values. If the sequence contains an even number of elements, the mean of both medians is returned.
	/// </summary>
	/// <param name="source">A sequence of <see cref="double" /> values that are used to calculate a median.</param>
	/// <returns>
	/// The median of the sequence of <paramref name="source" />, or <see langword="null" /> if the source sequence is empty or contains only values that are <see langword="null" />.
	/// </returns>
	public static double? Median(this IEnumerable<double?> source)
	{
		Check.ArgumentNull(source);

		double[] sorted = source.ExceptNull().Order().ToArray();

		if (sorted.Length == 0)
		{
			return null;
		}
		else if (sorted.Length % 2 == 0)
		{
			return (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]) / 2.0;
		}
		else
		{
			return sorted[sorted.Length / 2];
		}
	}
	/// <summary>
	/// Returns the median number of a sequence of <see cref="decimal" /> values. If the sequence contains an even number of elements, the mean of both medians is returned.
	/// </summary>
	/// <param name="source">A sequence of <see cref="decimal" /> values that are used to calculate a median.</param>
	/// <returns>
	/// The median of the sequence of <paramref name="source" />.
	/// </returns>
	public static decimal Median(this IEnumerable<decimal> source)
	{
		Check.ArgumentNull(source);

		decimal[] sorted = source.Order().ToArray();
		Check.ArgumentEx.EnumerableElementsRequired(sorted.Length > 0, nameof(source));

		if (sorted.Length % 2 == 0)
		{
			return (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]) / 2m;
		}
		else
		{
			return sorted[sorted.Length / 2];
		}
	}
	/// <summary>
	/// Returns the median number of a sequence of <see cref="decimal" /> values. If the sequence contains an even number of elements, the mean of both medians is returned.
	/// </summary>
	/// <param name="source">A sequence of <see cref="decimal" /> values that are used to calculate a median.</param>
	/// <returns>
	/// The median of the sequence of <paramref name="source" />, or <see langword="null" /> if the source sequence is empty or contains only values that are <see langword="null" />.
	/// </returns>
	public static decimal? Median(this IEnumerable<decimal?> source)
	{
		Check.ArgumentNull(source);

		decimal[] sorted = source.ExceptNull().Order().ToArray();

		if (sorted.Length == 0)
		{
			return null;
		}
		else if (sorted.Length % 2 == 0)
		{
			return (sorted[sorted.Length / 2 - 1] + sorted[sorted.Length / 2]) / 2m;
		}
		else
		{
			return sorted[sorted.Length / 2];
		}
	}
}