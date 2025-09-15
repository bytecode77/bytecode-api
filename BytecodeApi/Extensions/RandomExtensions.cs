using System.Collections;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Random" /> objects.
/// </summary>
public static class RandomExtensions
{
	/// <summary>
	/// Creates a <see cref="byte" />[] with a specified length and fills all elements with random bytes.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <param name="count">The size of the returned <see cref="byte" />[].</param>
	/// <returns>
	/// A new <see cref="byte" />[] with a specified length, filled with random bytes.
	/// </returns>
	public static byte[] GetBytes(this Random random, int count)
	{
		Check.ArgumentNull(random);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);

		byte[] buffer = new byte[count];
		random.NextBytes(buffer);
		return buffer;
	}
	/// <summary>
	/// Returns a random <see cref="bool" /> value that is either <see langword="false" /> or <see langword="true" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <returns>
	/// A random <see cref="bool" /> value that is either <see langword="false" /> or <see langword="true" />.
	/// </returns>
	public static bool GetBoolean(this Random random)
	{
		Check.ArgumentNull(random);

		return (random.Next() & 1) == 1;
	}
	/// <summary>
	/// Returns a random <see cref="byte" /> value that is greater than or equal to 0, and less than or equal to <see cref="byte.MaxValue" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <returns>
	/// A random <see cref="byte" /> value that is greater than or equal to 0, and less than or equal to <see cref="byte.MaxValue" />.
	/// </returns>
	public static byte GetByte(this Random random)
	{
		Check.ArgumentNull(random);

		return (byte)random.Next(256);
	}
	/// <summary>
	/// Returns a random <see cref="sbyte" /> value that is greater than or equal to <see cref="sbyte.MinValue" />, and less than or equal to <see cref="sbyte.MaxValue" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <returns>
	/// A random <see cref="sbyte" /> value that is greater than or equal to <see cref="sbyte.MinValue" />, and less than or equal to <see cref="sbyte.MaxValue" />.
	/// </returns>
	public static sbyte GetSByte(this Random random)
	{
		Check.ArgumentNull(random);

		return (sbyte)(random.Next(256) - 128);
	}
	/// <summary>
	/// Returns a random <see cref="char" /> value that is greater than or equal to '\0', and less than or equal to <see cref="char.MaxValue" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <returns>
	/// A random <see cref="char" /> value that is greater than or equal to '\0', and less than or equal to <see cref="char.MaxValue" />.
	/// </returns>
	public static char GetChar(this Random random)
	{
		Check.ArgumentNull(random);

		return (char)random.Next(65536);
	}
	/// <summary>
	/// Returns a random <see cref="double" /> value that is greater than or equal to 0.0f, and less than <paramref name="max" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <param name="max">The upper bound of the random number returned.</param>
	/// <returns>
	/// A random <see cref="double" /> value that is greater than or equal to 0.0f, and less than <paramref name="max" />.
	/// </returns>
	public static double NextDouble(this Random random, double max)
	{
		Check.ArgumentNull(random);

		return random.NextDouble() * max;
	}
	/// <summary>
	/// Returns a random <see cref="double" /> value that is greater than or equal to <paramref name="min" />, and less than <paramref name="max" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <param name="min">The inclusive lower bound of the random number returned.</param>
	/// <param name="max">The upper bound of the random number returned.</param>
	/// <returns>
	/// A random <see cref="double" /> value that is greater than or equal to <paramref name="min" />, and less than <paramref name="max" />.
	/// </returns>
	public static double NextDouble(this Random random, double min, double max)
	{
		Check.ArgumentNull(random);

		return random.NextDouble() * (max - min) + min;
	}
	/// <summary>
	/// Returns a random <see cref="float" /> value that is greater than or equal to 0.0f, and less than <paramref name="max" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <param name="max">The upper bound of the random number returned.</param>
	/// <returns>
	/// A random <see cref="float" /> value that is greater than or equal to 0.0f, and less than <paramref name="max" />.
	/// </returns>
	public static float NextSingle(this Random random, float max)
	{
		Check.ArgumentNull(random);

		return random.NextSingle() * max;
	}
	/// <summary>
	/// Returns a random <see cref="float" /> value that is greater than or equal to <paramref name="min" />, and less than <paramref name="max" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <param name="min">The inclusive lower bound of the random number returned.</param>
	/// <param name="max">The upper bound of the random number returned.</param>
	/// <returns>
	/// A random <see cref="float" /> value that is greater than or equal to <paramref name="min" />, and less than <paramref name="max" />.
	/// </returns>
	public static float NextSingle(this Random random, float min, float max)
	{
		Check.ArgumentNull(random);

		return random.NextSingle() * (max - min) + min;
	}
	/// <summary>
	/// Returns a random <see cref="int" /> value that is greater than or equal to <see cref="int.MinValue" />, and less than or equal to <see cref="int.MaxValue" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <returns>
	/// A random <see cref="int" /> value that is greater than or equal to <see cref="int.MinValue" />, and less than or equal to <see cref="int.MaxValue" />.
	/// </returns>
	public static int GetInt32(this Random random)
	{
		Check.ArgumentNull(random);

		return BitConverter.ToInt32(random.GetBytes(4), 0);
	}
	/// <summary>
	/// Returns a random <see cref="uint" /> value that is less than or equal to <see cref="uint.MaxValue" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <returns>
	/// A random <see cref="uint" /> value that is less than or equal to <see cref="uint.MaxValue" />.
	/// </returns>
	public static uint GetUInt32(this Random random)
	{
		Check.ArgumentNull(random);

		return BitConverter.ToUInt32(random.GetBytes(4), 0);
	}
	/// <summary>
	/// Returns a random <see cref="long" /> value that is greater than or equal to <see cref="long.MinValue" />, and less than or equal to <see cref="long.MaxValue" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <returns>
	/// A random <see cref="long" /> value that is greater than or equal to <see cref="long.MinValue" />, and less than or equal to <see cref="long.MaxValue" />.
	/// </returns>
	public static long GetInt64(this Random random)
	{
		Check.ArgumentNull(random);

		return BitConverter.ToInt64(random.GetBytes(8), 0);
	}
	/// <summary>
	/// Returns a random <see cref="ulong" /> value that is less than or equal to <see cref="ulong.MaxValue" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <returns>
	/// A random <see cref="ulong" /> value that is less than or equal to <see cref="ulong.MaxValue" />.
	/// </returns>
	public static ulong GetUInt64(this Random random)
	{
		Check.ArgumentNull(random);

		return BitConverter.ToUInt64(random.GetBytes(8), 0);
	}
	/// <summary>
	/// Returns a random <see cref="short" /> value that is greater than or equal to <see cref="short.MinValue" />, and less than or equal to <see cref="short.MaxValue" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <returns>
	/// A random <see cref="short" /> value that is greater than or equal to <see cref="short.MinValue" />, and less than or equal to <see cref="short.MaxValue" />.
	/// </returns>
	public static short GetInt16(this Random random)
	{
		Check.ArgumentNull(random);

		return (short)(random.Next(65536) - 32768);
	}
	/// <summary>
	/// Returns a random <see cref="ushort" /> value that is less than or equal to <see cref="ushort.MaxValue" />.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <returns>
	/// A random <see cref="ushort" /> value that is less than or equal to <see cref="ushort.MaxValue" />.
	/// </returns>
	public static ushort GetUInt16(this Random random)
	{
		Check.ArgumentNull(random);

		return (ushort)random.Next(65536);
	}
	/// <summary>
	/// Creates a <see cref="BitArray" /> with a specified length and fills all elements with random <see cref="bool" /> values.
	/// </summary>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <param name="count">The size of the returned <see cref="BitArray" />.</param>
	/// <returns>
	/// A new <see cref="BitArray" /> with a specified length, filled with random <see cref="bool" /> values.
	/// </returns>
	public static BitArray GetBits(this Random random, int count)
	{
		Check.ArgumentNull(random);
		Check.ArgumentOutOfRangeEx.GreaterEqual0(count);

		BitArray bits = new(count);
		byte[] buffer = new byte[16];
		int bufferPosition = int.MaxValue;

		for (int i = 0; i < count; i++)
		{
			if (bufferPosition >= buffer.Length << 3)
			{
				random.NextBytes(buffer);
				bufferPosition = 0;
			}

			bits[i] = (buffer[bufferPosition >> 3] & 1 << (bufferPosition & 7)) > 0;
			bufferPosition++;
		}

		return bits;
	}
	/// <summary>
	/// Returns a random <see cref="object" /> of the specified type from <paramref name="list" />, selected based on a random index.
	/// </summary>
	/// <typeparam name="T">The element type of <paramref name="list" />.</typeparam>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <param name="list">A <see cref="IList{T}" /> of the specified type.</param>
	/// <returns>
	/// A random <see cref="object" /> of the specified type from <paramref name="list" />.
	/// </returns>
	public static T GetObject<T>(this Random random, IList<T> list)
	{
		Check.ArgumentNull(random);
		Check.ArgumentNull(list);
		Check.ArgumentEx.ArrayElementsRequired(list);

		return list[random.Next(list.Count)];
	}
	/// <summary>
	/// Returns a random value of the specified <see langword="enum" /> type.
	/// </summary>
	/// <typeparam name="T">The type of the <see cref="Enum" /> to be returned.</typeparam>
	/// <param name="random">The <see cref="Random" /> object to be used for random number generation.</param>
	/// <returns>
	/// A random value of the specified <see langword="enum" /> type.
	/// </returns>
	public static T GetEnumValue<T>(this Random random) where T : struct, Enum
	{
		Check.ArgumentNull(random);

		T[] values = Enum.GetValues<T>();
		if (values.None()) throw Throw.Argument(nameof(T), "Enum does not have values.");

		return random.GetObject(values);
	}
}