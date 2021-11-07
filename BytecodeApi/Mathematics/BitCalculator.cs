using System.Collections;

namespace BytecodeApi.Mathematics
{
	/// <summary>
	/// Provides <see langword="static" /> methods that process single bits in integer datatypes and <see cref="byte" />[] objects.
	/// </summary>
	public static class BitCalculator
	{
		/// <summary>
		/// Gets the bit at the specified index from the specified <see cref="byte" /> value.
		/// </summary>
		/// <param name="value">The <see cref="byte" /> value to retrieve the bit from.</param>
		/// <param name="index">The index, at which to retrieve the bit from, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..7, <see langword="false" /> is returned.</param>
		/// <returns>
		/// The bit retrieved from <paramref name="value" />. If <paramref name="index" /> is not in the range of 0..7, <see langword="false" /> is returned.
		/// </returns>
		public static bool GetBit(byte value, int index)
		{
			return index >= 0 && index < 8 && (1 << index & value) == 1 << index;
		}
		/// <summary>
		/// Gets the bit at the specified index from the specified <see cref="sbyte" /> value.
		/// </summary>
		/// <param name="value">The <see cref="sbyte" /> value to retrieve the bit from.</param>
		/// <param name="index">The index, at which to retrieve the bit from, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..7, <see langword="false" /> is returned.</param>
		/// <returns>
		/// The bit retrieved from <paramref name="value" />. If <paramref name="index" /> is not in the range of 0..7, <see langword="false" /> is returned.
		/// </returns>
		public static bool GetBit(sbyte value, int index)
		{
			return index >= 0 && index < 8 && (1 << index & value) == 1 << index;
		}
		/// <summary>
		/// Gets the bit at the specified index from the specified <see cref="int" /> value.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value to retrieve the bit from.</param>
		/// <param name="index">The index, at which to retrieve the bit from, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..31, <see langword="false" /> is returned.</param>
		/// <returns>
		/// The bit retrieved from <paramref name="value" />. If <paramref name="index" /> is not in the range of 0..31, <see langword="false" /> is returned.
		/// </returns>
		public static bool GetBit(int value, int index)
		{
			return index >= 0 && index < 32 && (1 << index & value) == 1 << index;
		}
		/// <summary>
		/// Gets the bit at the specified index from the specified <see cref="uint" /> value.
		/// </summary>
		/// <param name="value">The <see cref="uint" /> value to retrieve the bit from.</param>
		/// <param name="index">The index, at which to retrieve the bit from, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..31, <see langword="false" /> is returned.</param>
		/// <returns>
		/// The bit retrieved from <paramref name="value" />. If <paramref name="index" /> is not in the range of 0..31, <see langword="false" /> is returned.
		/// </returns>
		public static bool GetBit(uint value, int index)
		{
			return index >= 0 && index < 32 && (1U << index & value) == 1U << index;
		}
		/// <summary>
		/// Gets the bit at the specified index from the specified <see cref="long" /> value.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to retrieve the bit from.</param>
		/// <param name="index">The index, at which to retrieve the bit from, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..63, <see langword="false" /> is returned.</param>
		/// <returns>
		/// The bit retrieved from <paramref name="value" />. If <paramref name="index" /> is not in the range of 0..63, <see langword="false" /> is returned.
		/// </returns>
		public static bool GetBit(long value, int index)
		{
			return index >= 0 && index < 64 && (1L << index & value) == 1L << index;
		}
		/// <summary>
		/// Gets the bit at the specified index from the specified <see cref="ulong" /> value.
		/// </summary>
		/// <param name="value">The <see cref="ulong" /> value to retrieve the bit from.</param>
		/// <param name="index">The index, at which to retrieve the bit from, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..63, <see langword="false" /> is returned.</param>
		/// <returns>
		/// The bit retrieved from <paramref name="value" />. If <paramref name="index" /> is not in the range of 0..63, <see langword="false" /> is returned.
		/// </returns>
		public static bool GetBit(ulong value, int index)
		{
			return index >= 0 && index < 64 && (1UL << index & value) == 1UL << index;
		}
		/// <summary>
		/// Gets the bit at the specified index from the specified <see cref="short" /> value.
		/// </summary>
		/// <param name="value">The <see cref="short" /> value to retrieve the bit from.</param>
		/// <param name="index">The index, at which to retrieve the bit from, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..15, <see langword="false" /> is returned.</param>
		/// <returns>
		/// The bit retrieved from <paramref name="value" />. If <paramref name="index" /> is not in the range of 0..15, <see langword="false" /> is returned.
		/// </returns>
		public static bool GetBit(short value, int index)
		{
			return index >= 0 && index < 16 && (1 << index & value) == 1 << index;
		}
		/// <summary>
		/// Gets the bit at the specified index from the specified <see cref="ushort" /> value.
		/// </summary>
		/// <param name="value">The <see cref="ushort" /> value to retrieve the bit from.</param>
		/// <param name="index">The index, at which to retrieve the bit from, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..15, <see langword="false" /> is returned.</param>
		/// <returns>
		/// The bit retrieved from <paramref name="value" />. If <paramref name="index" /> is not in the range of 0..15, <see langword="false" /> is returned.
		/// </returns>
		public static bool GetBit(ushort value, int index)
		{
			return index >= 0 && index < 16 && (1U << index & value) == 1U << index;
		}
		/// <summary>
		/// Gets the bit at the specified index from the specified <see cref="byte" />[] binary.
		/// </summary>
		/// <param name="value">The <see cref="byte" />[] binary to retrieve the bit from.</param>
		/// <param name="index">The index, at which to retrieve the bit from, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of the <see cref="byte" />[], <see langword="false" /> is returned.</param>
		/// <returns>
		/// The bit retrieved from <paramref name="value" />. If <paramref name="index" /> is not in the range of the <see cref="byte" />[], <see langword="false" /> is returned.
		/// </returns>
		public static bool GetBit(byte[] value, int index)
		{
			Check.ArgumentNull(value, nameof(value));

			return index >= 0 && index < value.Length << 3 && (value[index >> 3] & 1 << (index & 7)) > 0;
		}
		/// <summary>
		/// Sets the bit of <paramref name="value" /> at the specified index to either 0 or 1 according to <paramref name="bit" />.
		/// </summary>
		/// <param name="value">The <see cref="byte" /> value to be processed.</param>
		/// <param name="index">The index of the changed bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..7, <paramref name="value" /> is returned.</param>
		/// <param name="bit"><see langword="false" /> to set the bit to 0; <see langword="true" /> to set the bit to 1.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..7, <paramref name="value" /> is returned.
		/// </returns>
		public static byte SetBit(byte value, int index, bool bit)
		{
			if (index >= 0 && index < 8) return (byte)(bit ? value | 1 << index : value & ~(1 << index));
			else return value;
		}
		/// <summary>
		/// Sets the bit of <paramref name="value" /> at the specified index to either 0 or 1 according to <paramref name="bit" />.
		/// </summary>
		/// <param name="value">The <see cref="sbyte" /> value to be processed.</param>
		/// <param name="index">The index of the changed bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..7, <paramref name="value" /> is returned.</param>
		/// <param name="bit"><see langword="false" /> to set the bit to 0; <see langword="true" /> to set the bit to 1.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..7, <paramref name="value" /> is returned.
		/// </returns>
		public static sbyte SetBit(sbyte value, int index, bool bit)
		{
			if (index >= 0 && index < 8) return (sbyte)(bit ? value | (sbyte)(1 << index) : value & ~(1 << index));
			else return value;
		}
		/// <summary>
		/// Sets the bit of <paramref name="value" /> at the specified index to either 0 or 1 according to <paramref name="bit" />.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value to be processed.</param>
		/// <param name="index">The index of the changed bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..31, <paramref name="value" /> is returned.</param>
		/// <param name="bit"><see langword="false" /> to set the bit to 0; <see langword="true" /> to set the bit to 1.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..31, <paramref name="value" /> is returned.
		/// </returns>
		public static int SetBit(int value, int index, bool bit)
		{
			if (index >= 0 && index < 32) return bit ? value | 1 << index : value & ~(1 << index);
			else return value;
		}
		/// <summary>
		/// Sets the bit of <paramref name="value" /> at the specified index to either 0 or 1 according to <paramref name="bit" />.
		/// </summary>
		/// <param name="value">The <see cref="uint" /> value to be processed.</param>
		/// <param name="index">The index of the changed bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..31, <paramref name="value" /> is returned.</param>
		/// <param name="bit"><see langword="false" /> to set the bit to 0; <see langword="true" /> to set the bit to 1.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..31, <paramref name="value" /> is returned.
		/// </returns>
		public static uint SetBit(uint value, int index, bool bit)
		{
			if (index >= 0 && index < 32) return bit ? value | 1U << index : value & ~(1U << index);
			else return value;
		}
		/// <summary>
		/// Sets the bit of <paramref name="value" /> at the specified index to either 0 or 1 according to <paramref name="bit" />.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to be processed.</param>
		/// <param name="index">The index of the changed bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..63, <paramref name="value" /> is returned.</param>
		/// <param name="bit"><see langword="false" /> to set the bit to 0; <see langword="true" /> to set the bit to 1.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..63, <paramref name="value" /> is returned.
		/// </returns>
		public static long SetBit(long value, int index, bool bit)
		{
			if (index >= 0 && index < 64) return bit ? value | 1L << index : value & ~(1L << index);
			else return value;
		}
		/// <summary>
		/// Sets the bit of <paramref name="value" /> at the specified index to either 0 or 1 according to <paramref name="bit" />.
		/// </summary>
		/// <param name="value">The <see cref="ulong" /> value to be processed.</param>
		/// <param name="index">The index of the changed bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..63, <paramref name="value" /> is returned.</param>
		/// <param name="bit"><see langword="false" /> to set the bit to 0; <see langword="true" /> to set the bit to 1.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..63, <paramref name="value" /> is returned.
		/// </returns>
		public static ulong SetBit(ulong value, int index, bool bit)
		{
			if (index >= 0 && index < 64) return bit ? value | 1UL << index : value & ~(1UL << index);
			else return value;
		}
		/// <summary>
		/// Sets the bit of <paramref name="value" /> at the specified index to either 0 or 1 according to <paramref name="bit" />.
		/// </summary>
		/// <param name="value">The <see cref="short" /> value to be processed.</param>
		/// <param name="index">The index of the changed bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..15, <paramref name="value" /> is returned.</param>
		/// <param name="bit"><see langword="false" /> to set the bit to 0; <see langword="true" /> to set the bit to 1.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..15, <paramref name="value" /> is returned.
		/// </returns>
		public static short SetBit(short value, int index, bool bit)
		{
			if (index >= 0 && index < 16) return (short)(bit ? value | (short)(1 << index) : value & ~(1 << index));
			else return value;
		}
		/// <summary>
		/// Sets the bit of <paramref name="value" /> at the specified index to either 0 or 1 according to <paramref name="bit" />.
		/// </summary>
		/// <param name="value">The <see cref="ushort" /> value to be processed.</param>
		/// <param name="index">The index of the changed bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..15, <paramref name="value" /> is returned.</param>
		/// <param name="bit"><see langword="false" /> to set the bit to 0; <see langword="true" /> to set the bit to 1.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..15, <paramref name="value" /> is returned.
		/// </returns>
		public static ushort SetBit(ushort value, int index, bool bit)
		{
			if (index >= 0 && index < 16) return (ushort)(bit ? value | 1U << index : value & ~(1U << index));
			else return value;
		}
		/// <summary>
		/// Sets the bit of <paramref name="value" /> at the specified index to either 0 or 1 according to <paramref name="bit" />.
		/// </summary>
		/// <param name="value">The <see cref="byte" />[] binary to be processed.</param>
		/// <param name="index">The index of the changed bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of the <see cref="byte" />[], this method returns.</param>
		/// <param name="bit"><see langword="false" /> to set the bit to 0; <see langword="true" /> to set the bit to 1.</param>
		public static void SetBit(byte[] value, int index, bool bit)
		{
			Check.ArgumentNull(value, nameof(value));

			if (index >= 0 && index < value.Length << 3)
			{
				if (bit) value[index >> 3] |= (byte)(1 << (index & 7));
				else value[index >> 3] &= (byte)~(1 << (index & 7));
			}
		}
		/// <summary>
		/// Toggles the bit of <paramref name="value" /> at the specified index either from 0 to 1 or from 1 to 0.
		/// </summary>
		/// <param name="value">The <see cref="byte" /> value to be processed.</param>
		/// <param name="index">The index of the toggled bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..7, <paramref name="value" /> is returned.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..7, <paramref name="value" /> is returned.
		/// </returns>
		public static byte ToggleBit(byte value, int index)
		{
			if (index >= 0 && index < 8) return (byte)(value ^ 1 << index);
			else return value;
		}
		/// <summary>
		/// Toggles the bit of <paramref name="value" /> at the specified index either from 0 to 1 or from 1 to 0.
		/// </summary>
		/// <param name="value">The <see cref="sbyte" /> value to be processed.</param>
		/// <param name="index">The index of the toggled bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..7, <paramref name="value" /> is returned.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..7, <paramref name="value" /> is returned.
		/// </returns>
		public static sbyte ToggleBit(sbyte value, int index)
		{
			if (index >= 0 && index < 8) return (sbyte)(value ^ 1 << index);
			else return value;
		}
		/// <summary>
		/// Toggles the bit of <paramref name="value" /> at the specified index either from 0 to 1 or from 1 to 0.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value to be processed.</param>
		/// <param name="index">The index of the toggled bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..31, <paramref name="value" /> is returned.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..31, <paramref name="value" /> is returned.
		/// </returns>
		public static int ToggleBit(int value, int index)
		{
			if (index >= 0 && index < 32) return value ^ 1 << index;
			else return value;
		}
		/// <summary>
		/// Toggles the bit of <paramref name="value" /> at the specified index either from 0 to 1 or from 1 to 0.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value to be processed.</param>
		/// <param name="index">The index of the toggled bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..31, <paramref name="value" /> is returned.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..31, <paramref name="value" /> is returned.
		/// </returns>
		public static uint ToggleBit(uint value, int index)
		{
			if (index >= 0 && index < 32) return value ^ 1U << index;
			else return value;
		}
		/// <summary>
		/// Toggles the bit of <paramref name="value" /> at the specified index either from 0 to 1 or from 1 to 0.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to be processed.</param>
		/// <param name="index">The index of the toggled bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..63, <paramref name="value" /> is returned.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..63, <paramref name="value" /> is returned.
		/// </returns>
		public static long ToggleBit(long value, int index)
		{
			if (index >= 0 && index < 64) return value ^ 1 << index;
			else return value;
		}
		/// <summary>
		/// Toggles the bit of <paramref name="value" /> at the specified index either from 0 to 1 or from 1 to 0.
		/// </summary>
		/// <param name="value">The <see cref="ulong" /> value to be processed.</param>
		/// <param name="index">The index of the toggled bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..63, <paramref name="value" /> is returned.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..63, <paramref name="value" /> is returned.
		/// </returns>
		public static ulong ToggleBit(ulong value, int index)
		{
			if (index >= 0 && index < 64) return value ^ 1UL << index;
			else return value;
		}
		/// <summary>
		/// Toggles the bit of <paramref name="value" /> at the specified index either from 0 to 1 or from 1 to 0.
		/// </summary>
		/// <param name="value">The <see cref="short" /> value to be processed.</param>
		/// <param name="index">The index of the toggled bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..15, <paramref name="value" /> is returned.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..15, <paramref name="value" /> is returned.
		/// </returns>
		public static short ToggleBit(short value, int index)
		{
			if (index >= 0 && index < 16) return (short)(value ^ 1 << index);
			else return value;
		}
		/// <summary>
		/// Toggles the bit of <paramref name="value" /> at the specified index either from 0 to 1 or from 1 to 0.
		/// </summary>
		/// <param name="value">The <see cref="ushort" /> value to be processed.</param>
		/// <param name="index">The index of the toggled bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of 0..15, <paramref name="value" /> is returned.</param>
		/// <returns>
		/// The modified value. If <paramref name="index" /> is not in the range of 0..15, <paramref name="value" /> is returned.
		/// </returns>
		public static ushort ToggleBit(ushort value, int index)
		{
			if (index >= 0 && index < 16) return (ushort)(value ^ 1U << index);
			else return value;
		}
		/// <summary>
		/// Toggles the bit of <paramref name="value" /> at the specified index either from 0 to 1 or from 1 to 0.
		/// </summary>
		/// <param name="value">The <see cref="byte" />[] binary to be processed.</param>
		/// <param name="index">The index of the toggled bit, where 0 is the least significant bit. If <paramref name="index" /> is not in the range of the <see cref="byte" />[], this method returns.</param>
		public static void ToggleBit(byte[] value, int index)
		{
			Check.ArgumentNull(value, nameof(value));

			if (index >= 0 && index < value.Length << 3)
			{
				value[index >> 3] ^= (byte)(1 << (index & 7));
			}
		}
		/// <summary>
		/// Converts the specified <see cref="byte" /> value to a <see cref="BitArray" />.
		/// </summary>
		/// <param name="value">The <see cref="byte" /> value to retrieve the bits from.</param>
		/// <returns>
		/// An equivalent <see cref="BitArray" /> value containing all bits from <paramref name="value" />.
		/// </returns>
		public static BitArray GetBitArray(byte value)
		{
			return new BitArray(new[] { value });
		}
		/// <summary>
		/// Converts the specified <see cref="sbyte" /> value to a <see cref="BitArray" />.
		/// </summary>
		/// <param name="value">The <see cref="sbyte" /> value to retrieve the bits from.</param>
		/// <returns>
		/// An equivalent <see cref="BitArray" /> value containing all bits from <paramref name="value" />.
		/// </returns>
		public static BitArray GetBitArray(sbyte value)
		{
			return GetBitArray((byte)value);
		}
		/// <summary>
		/// Converts the specified <see cref="int" /> value to a <see cref="BitArray" />.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value to retrieve the bits from.</param>
		/// <returns>
		/// An equivalent <see cref="BitArray" /> value containing all bits from <paramref name="value" />.
		/// </returns>
		public static BitArray GetBitArray(int value)
		{
			return new BitArray(new[] { value });
		}
		/// <summary>
		/// Converts the specified <see cref="uint" /> value to a <see cref="BitArray" />.
		/// </summary>
		/// <param name="value">The <see cref="uint" /> value to retrieve the bits from.</param>
		/// <returns>
		/// An equivalent <see cref="BitArray" /> value containing all bits from <paramref name="value" />.
		/// </returns>
		public static BitArray GetBitArray(uint value)
		{
			return GetBitArray((int)value);
		}
		/// <summary>
		/// Converts the specified <see cref="long" /> value to a <see cref="BitArray" />.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to retrieve the bits from.</param>
		/// <returns>
		/// An equivalent <see cref="BitArray" /> value containing all bits from <paramref name="value" />.
		/// </returns>
		public static BitArray GetBitArray(long value)
		{
			return new BitArray(new int[] { (int)value, (int)(value >> 32) });
		}
		/// <summary>
		/// Converts the specified <see cref="ulong" /> value to a <see cref="BitArray" />.
		/// </summary>
		/// <param name="value">The <see cref="ulong" /> value to retrieve the bits from.</param>
		/// <returns>
		/// An equivalent <see cref="BitArray" /> value containing all bits from <paramref name="value" />.
		/// </returns>
		public static BitArray GetBitArray(ulong value)
		{
			return GetBitArray((long)value);
		}
		/// <summary>
		/// Converts the specified <see cref="short" /> value to a <see cref="BitArray" />.
		/// </summary>
		/// <param name="value">The <see cref="short" /> value to retrieve the bits from.</param>
		/// <returns>
		/// An equivalent <see cref="BitArray" /> value containing all bits from <paramref name="value" />.
		/// </returns>
		public static BitArray GetBitArray(short value)
		{
			return new BitArray(new byte[] { (byte)value, (byte)(value >> 8) });
		}
		/// <summary>
		/// Converts the specified <see cref="ushort" /> value to a <see cref="BitArray" />.
		/// </summary>
		/// <param name="value">The <see cref="ushort" /> value to retrieve the bits from.</param>
		/// <returns>
		/// An equivalent <see cref="BitArray" /> value containing all bits from <paramref name="value" />.
		/// </returns>
		public static BitArray GetBitArray(ushort value)
		{
			return GetBitArray((short)value);
		}
	}
}