namespace BytecodeApi.Mathematics;

/// <summary>
/// Class that encodes integers to and from a 7-bit variable length format.
/// </summary>
public static class SevenBitInteger
{
	/// <summary>
	/// Converts a 32-bit integer into a compressed format.
	/// </summary>
	/// <param name="value">The <see cref="int" /> value to convert.</param>
	/// <returns>
	/// A new <see cref="byte" />[] that represents the specified 32-bit integer in compressed format.
	/// </returns>
	public static byte[] Encode(int value)
	{
		List<byte> bytes = new(5);
		uint remaining = (uint)value;

		while (remaining > 127)
		{
			bytes.Add((byte)(remaining | 128));
			remaining >>= 7;
		}

		bytes.Add((byte)remaining);
		return bytes.ToArray();
	}
	/// <summary>
	/// Converts a 64-bit integer into a compressed format.
	/// </summary>
	/// <param name="value">The <see cref="long" /> value to convert.</param>
	/// <returns>
	/// A new <see cref="byte" />[] that represents the specified 64-bit integer in compressed format.
	/// </returns>
	public static byte[] Encode(long value)
	{
		List<byte> bytes = new(9);
		ulong remaining = (ulong)value;

		while (remaining > 127)
		{
			bytes.Add((byte)(remaining | 128));
			remaining >>= 7;
		}

		bytes.Add((byte)remaining);
		return bytes.ToArray();
	}
	/// <summary>
	/// Calculates the number of bytes produced by encoding the specified 32-bit integer.
	/// </summary>
	/// <param name="value">The <see cref="int" /> value to convert.</param>
	/// <returns>
	/// The number of bytes the <see cref="Encode(int)" /> method will produce for the specified number.
	/// </returns>
	public static int GetByteCount(int value)
	{
		int count = 1;
		uint remaining = (uint)value;

		while (remaining > 127)
		{
			remaining >>= 7;
			count++;
		}

		return count;
	}
	/// <summary>
	/// Calculates the number of bytes produced by encoding the specified 64-bit integer.
	/// </summary>
	/// <param name="value">The <see cref="long" /> value to convert.</param>
	/// <returns>
	/// The number of bytes the <see cref="Encode(long)" /> method will produce for the specified number.
	/// </returns>
	public static int GetByteCount(long value)
	{
		int count = 1;
		ulong remaining = (ulong)value;

		while (remaining > 127)
		{
			remaining >>= 7;
			count++;
		}

		return count;
	}
	/// <summary>
	/// Converts a compressed 32-bit integer into a <see cref="int" /> value.
	/// </summary>
	/// <param name="value">The <see cref="byte" />[] value to convert with up to 5 bytes capacity.</param>
	/// <returns>
	/// A <see cref="int" /> value that was converted from the specified 32-bit integer in binary format.
	/// </returns>
	public static int Decode(byte[] value)
	{
		Check.ArgumentNull(value);
		Check.Argument(value.Length is > 0 and <= 5, nameof(value), "A 7-bit encoded integer has between 1 and 5 bytes.");

		int returnValue = 0;
		int bitIndex = 0;

		foreach (byte b in value)
		{
			returnValue |= (b & 127) << bitIndex;
			bitIndex += 7;

			if ((b & 128) == 0)
			{
				return returnValue;
			}
		}

		throw Throw.Format("The value format is invalid.");
	}
	/// <summary>
	/// Converts a compressed 64-bit integer into a <see cref="long" /> value.
	/// </summary>
	/// <param name="value">The <see cref="byte" />[] value to convert with up to 9 bytes capacity.</param>
	/// <returns>
	/// A <see cref="long" /> value that was converted from the specified 64-bit integer in binary format.
	/// </returns>
	public static long DecodeLong(byte[] value)
	{
		Check.ArgumentNull(value);
		Check.Argument(value.Length is > 0 and <= 9, nameof(value), "A 7-bit encoded integer has between 1 and 9 bytes.");

		long returnValue = 0;
		int bitIndex = 0;

		foreach (byte b in value)
		{
			returnValue |= (long)(b & 127) << bitIndex;
			bitIndex += 7;

			if ((b & 128) == 0)
			{
				return returnValue;
			}
		}

		throw Throw.Format("The value format is invalid.");
	}
}