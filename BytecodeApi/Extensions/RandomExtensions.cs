using System.Collections;

namespace BytecodeApi.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Random" /> objects.
/// </summary>
public static class RandomExtensions
{
	extension(Random random)
	{
		/// <summary>
		/// Creates a <see cref="byte" />[] with a specified length and fills all elements with random bytes.
		/// </summary>
		/// <param name="count">The size of the returned <see cref="byte" />[].</param>
		/// <returns>
		/// A new <see cref="byte" />[] with a specified length, filled with random bytes.
		/// </returns>
		public byte[] GetBytes(int count)
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
		/// <returns>
		/// A random <see cref="bool" /> value that is either <see langword="false" /> or <see langword="true" />.
		/// </returns>
		public bool GetBoolean()
		{
			Check.ArgumentNull(random);

			return (random.Next() & 1) == 1;
		}
		/// <summary>
		/// Returns a random <see cref="byte" /> value that is greater than or equal to 0, and less than or equal to <see cref="byte.MaxValue" />.
		/// </summary>
		/// <returns>
		/// A random <see cref="byte" /> value that is greater than or equal to 0, and less than or equal to <see cref="byte.MaxValue" />.
		/// </returns>
		public byte GetByte()
		{
			Check.ArgumentNull(random);

			return (byte)random.Next(256);
		}
		/// <summary>
		/// Returns a random <see cref="sbyte" /> value that is greater than or equal to <see cref="sbyte.MinValue" />, and less than or equal to <see cref="sbyte.MaxValue" />.
		/// </summary>
		/// <returns>
		/// A random <see cref="sbyte" /> value that is greater than or equal to <see cref="sbyte.MinValue" />, and less than or equal to <see cref="sbyte.MaxValue" />.
		/// </returns>
		public sbyte GetSByte()
		{
			Check.ArgumentNull(random);

			return (sbyte)(random.Next(256) - 128);
		}
		/// <summary>
		/// Returns a random <see cref="char" /> value that is greater than or equal to '\0', and less than or equal to <see cref="char.MaxValue" />.
		/// </summary>
		/// <returns>
		/// A random <see cref="char" /> value that is greater than or equal to '\0', and less than or equal to <see cref="char.MaxValue" />.
		/// </returns>
		public char GetChar()
		{
			Check.ArgumentNull(random);

			return (char)random.Next(65536);
		}
		/// <summary>
		/// Returns a random <see cref="double" /> value that is greater than or equal to 0.0f, and less than <paramref name="max" />.
		/// </summary>
		/// <param name="max">The upper bound of the random number returned.</param>
		/// <returns>
		/// A random <see cref="double" /> value that is greater than or equal to 0.0f, and less than <paramref name="max" />.
		/// </returns>
		public double NextDouble(double max)
		{
			Check.ArgumentNull(random);

			return random.NextDouble() * max;
		}
		/// <summary>
		/// Returns a random <see cref="double" /> value that is greater than or equal to <paramref name="min" />, and less than <paramref name="max" />.
		/// </summary>
		/// <param name="min">The inclusive lower bound of the random number returned.</param>
		/// <param name="max">The upper bound of the random number returned.</param>
		/// <returns>
		/// A random <see cref="double" /> value that is greater than or equal to <paramref name="min" />, and less than <paramref name="max" />.
		/// </returns>
		public double NextDouble(double min, double max)
		{
			Check.ArgumentNull(random);

			return random.NextDouble() * (max - min) + min;
		}
		/// <summary>
		/// Returns a random <see cref="float" /> value that is greater than or equal to 0.0f, and less than <paramref name="max" />.
		/// </summary>
		/// <param name="max">The upper bound of the random number returned.</param>
		/// <returns>
		/// A random <see cref="float" /> value that is greater than or equal to 0.0f, and less than <paramref name="max" />.
		/// </returns>
		public float NextSingle(float max)
		{
			Check.ArgumentNull(random);

			return random.NextSingle() * max;
		}
		/// <summary>
		/// Returns a random <see cref="float" /> value that is greater than or equal to <paramref name="min" />, and less than <paramref name="max" />.
		/// </summary>
		/// <param name="min">The inclusive lower bound of the random number returned.</param>
		/// <param name="max">The upper bound of the random number returned.</param>
		/// <returns>
		/// A random <see cref="float" /> value that is greater than or equal to <paramref name="min" />, and less than <paramref name="max" />.
		/// </returns>
		public float NextSingle(float min, float max)
		{
			Check.ArgumentNull(random);

			return random.NextSingle() * (max - min) + min;
		}
		/// <summary>
		/// Returns a random <see cref="int" /> value that is greater than or equal to <see cref="int.MinValue" />, and less than or equal to <see cref="int.MaxValue" />.
		/// </summary>
		/// <returns>
		/// A random <see cref="int" /> value that is greater than or equal to <see cref="int.MinValue" />, and less than or equal to <see cref="int.MaxValue" />.
		/// </returns>
		public int GetInt32()
		{
			Check.ArgumentNull(random);

			return BitConverter.ToInt32(random.GetBytes(4), 0);
		}
		/// <summary>
		/// Returns a random <see cref="uint" /> value that is less than or equal to <see cref="uint.MaxValue" />.
		/// </summary>
		/// <returns>
		/// A random <see cref="uint" /> value that is less than or equal to <see cref="uint.MaxValue" />.
		/// </returns>
		public uint GetUInt32()
		{
			Check.ArgumentNull(random);

			return BitConverter.ToUInt32(random.GetBytes(4), 0);
		}
		/// <summary>
		/// Returns a random <see cref="long" /> value that is greater than or equal to <see cref="long.MinValue" />, and less than or equal to <see cref="long.MaxValue" />.
		/// </summary>
		/// <returns>
		/// A random <see cref="long" /> value that is greater than or equal to <see cref="long.MinValue" />, and less than or equal to <see cref="long.MaxValue" />.
		/// </returns>
		public long GetInt64()
		{
			Check.ArgumentNull(random);

			return BitConverter.ToInt64(random.GetBytes(8), 0);
		}
		/// <summary>
		/// Returns a random <see cref="ulong" /> value that is less than or equal to <see cref="ulong.MaxValue" />.
		/// </summary>
		/// <returns>
		/// A random <see cref="ulong" /> value that is less than or equal to <see cref="ulong.MaxValue" />.
		/// </returns>
		public ulong GetUInt64()
		{
			Check.ArgumentNull(random);

			return BitConverter.ToUInt64(random.GetBytes(8), 0);
		}
		/// <summary>
		/// Returns a random <see cref="short" /> value that is greater than or equal to <see cref="short.MinValue" />, and less than or equal to <see cref="short.MaxValue" />.
		/// </summary>
		/// <returns>
		/// A random <see cref="short" /> value that is greater than or equal to <see cref="short.MinValue" />, and less than or equal to <see cref="short.MaxValue" />.
		/// </returns>
		public short GetInt16()
		{
			Check.ArgumentNull(random);

			return (short)(random.Next(65536) - 32768);
		}
		/// <summary>
		/// Returns a random <see cref="ushort" /> value that is less than or equal to <see cref="ushort.MaxValue" />.
		/// </summary>
		/// <returns>
		/// A random <see cref="ushort" /> value that is less than or equal to <see cref="ushort.MaxValue" />.
		/// </returns>
		public ushort GetUInt16()
		{
			Check.ArgumentNull(random);

			return (ushort)random.Next(65536);
		}
		/// <summary>
		/// Creates a <see cref="BitArray" /> with a specified length and fills all elements with random <see cref="bool" /> values.
		/// </summary>
		/// <param name="count">The size of the returned <see cref="BitArray" />.</param>
		/// <returns>
		/// A new <see cref="BitArray" /> with a specified length, filled with random <see cref="bool" /> values.
		/// </returns>
		public BitArray GetBits(int count)
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
		/// <param name="list">A <see cref="IList{T}" /> of the specified type.</param>
		/// <returns>
		/// A random <see cref="object" /> of the specified type from <paramref name="list" />.
		/// </returns>
		public T GetObject<T>(IList<T> list)
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
		/// <returns>
		/// A random value of the specified <see langword="enum" /> type.
		/// </returns>
		public T GetEnumValue<T>() where T : struct, Enum
		{
			Check.ArgumentNull(random);

			T[] values = Enum.GetValues<T>();
			if (values.None()) throw Throw.Argument(nameof(T), "Enum does not have values.");

			return random.GetObject(values);
		}
	}
}