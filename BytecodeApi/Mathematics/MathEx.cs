using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace BytecodeApi.Mathematics
{
	//FEATURE: ComputePrimeNumbers(int from, int to)
	/// <summary>
	/// Provides <see langword="static" /> methods that extend the <see cref="Math" /> class.
	/// </summary>
	public static class MathEx
	{
		internal static readonly Random _Random = new Random();
		internal static readonly RandomNumberGenerator _RandomNumberGenerator = RandomNumberGenerator.Create();
		/// <summary>
		/// Represents a singleton <see cref="System.Random" /> object. This field is read-only.
		/// </summary>
		public static readonly Random Random = new Random();
		/// <summary>
		/// Represents a singleton <see cref="System.Security.Cryptography.RandomNumberGenerator" /> object. This field is read-only.
		/// </summary>
		public static readonly RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();

		/// <summary>
		/// Rotates the bits in the specified <see cref="byte" /> value to the right.
		/// </summary>
		/// <param name="value">The <see cref="byte" /> value to rotate right.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static byte Ror(byte value, int bits)
		{
			bits &= 7;
			return (byte)(value >> bits | value << (8 - bits));
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="sbyte" /> value to the right.
		/// </summary>
		/// <param name="value">The <see cref="sbyte" /> value to rotate right.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static sbyte Ror(sbyte value, int bits)
		{
			bits &= 7;
			return (sbyte)(value >> bits | value << (8 - bits));
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="int" /> value to the right.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value to rotate right.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static int Ror(int value, int bits)
		{
			bits &= 31;
			return value >> bits | value << (32 - bits);
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="uint" /> value to the right.
		/// </summary>
		/// <param name="value">The <see cref="uint" /> value to rotate right.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static uint Ror(uint value, int bits)
		{
			bits &= 31;
			return value >> bits | value << (32 - bits);
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="long" /> value to the right.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to rotate right.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static long Ror(long value, int bits)
		{
			bits &= 63;
			return value >> bits | value << (64 - bits);
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="ulong" /> value to the right.
		/// </summary>
		/// <param name="value">The <see cref="ulong" /> value to rotate right.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static ulong Ror(ulong value, int bits)
		{
			bits &= 63;
			return value >> bits | value << (64 - bits);
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="short" /> value to the right.
		/// </summary>
		/// <param name="value">The <see cref="short" /> value to rotate right.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static short Ror(short value, int bits)
		{
			bits &= 15;
			return (short)(value >> bits | value << (16 - bits));
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="ushort" /> value to the right.
		/// </summary>
		/// <param name="value">The <see cref="ushort" /> value to rotate right.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static ushort Ror(ushort value, int bits)
		{
			bits &= 15;
			return (ushort)(value >> bits | value << (16 - bits));
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="byte" /> value to the left.
		/// </summary>
		/// <param name="value">The <see cref="byte" /> value to rotate left.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static byte Rol(byte value, int bits)
		{
			bits &= 7;
			return (byte)(value << bits | value >> (8 - bits));
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="sbyte" /> value to the left.
		/// </summary>
		/// <param name="value">The <see cref="sbyte" /> value to rotate left.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static sbyte Rol(sbyte value, int bits)
		{
			bits &= 7;
			return (sbyte)(value << bits | value >> (8 - bits));
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="int" /> value to the left.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value to rotate left.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static int Rol(int value, int bits)
		{
			bits &= 31;
			return value << bits | value >> (32 - bits);
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="uint" /> value to the left.
		/// </summary>
		/// <param name="value">The <see cref="uint" /> value to rotate left.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static uint Rol(uint value, int bits)
		{
			bits &= 31;
			return value << bits | value >> (32 - bits);
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="long" /> value to the left.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to rotate left.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static long Rol(long value, int bits)
		{
			bits &= 63;
			return value << bits | value >> (64 - bits);
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="ulong" /> value to the left.
		/// </summary>
		/// <param name="value">The <see cref="ulong" /> value to rotate left.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static ulong Rol(ulong value, int bits)
		{
			bits &= 63;
			return value << bits | value >> (64 - bits);
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="short" /> value to the left.
		/// </summary>
		/// <param name="value">The <see cref="short" /> value to rotate left.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static short Rol(short value, int bits)
		{
			bits &= 15;
			return (short)(value << bits | value >> (16 - bits));
		}
		/// <summary>
		/// Rotates the bits in the specified <see cref="ushort" /> value to the left.
		/// </summary>
		/// <param name="value">The <see cref="ushort" /> value to rotate left.</param>
		/// <param name="bits">The number of bits to rotate <paramref name="value" />.</param>
		/// <returns>
		/// The result of <paramref name="value" /> ror <paramref name="bits" />.
		/// </returns>
		public static ushort Rol(ushort value, int bits)
		{
			bits &= 15;
			return (ushort)(value << bits | value >> (16 - bits));
		}

		/// <summary>
		/// Returns the sine of the specified angle, considering the specified <see cref="AngleType" />.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value representing an angle, measured in the specified <see cref="AngleType" />.</param>
		/// <param name="angleType">An <see cref="AngleType" /> specifying the type of angle to be measured.</param>
		/// <returns>
		/// The sine of <paramref name="value" />.
		/// If <paramref name="value" /> is equal to <see cref="double.NaN" />, <see cref="double.NegativeInfinity" />, <see cref="double.PositiveInfinity" />, this method returns <see cref="double.NaN" />.
		/// </returns>
		public static double Sin(double value, AngleType angleType)
		{
			return Math.Sin(ConvertAngle(value, angleType, AngleType.Radians));
		}
		/// <summary>
		/// Returns the hyperbolic sine of the specified angle, considering the specified <see cref="AngleType" />.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value representing an angle, measured in the specified <see cref="AngleType" />.</param>
		/// <param name="angleType">An <see cref="AngleType" /> specifying the type of angle to be measured.</param>
		/// <returns>
		/// The hyperbolic sine of <paramref name="value" />.
		/// If <paramref name="value" /> is equal to <see cref="double.NaN" />, <see cref="double.NegativeInfinity" />, <see cref="double.PositiveInfinity" />, this method returns a <see cref="double" /> equal to <paramref name="value" />.
		/// </returns>
		public static double Sinh(double value, AngleType angleType)
		{
			return Math.Sinh(ConvertAngle(value, angleType, AngleType.Radians));
		}
		/// <summary>
		/// Returns the cosine of the specified angle, considering the specified <see cref="AngleType" />.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value representing an angle, measured in the specified <see cref="AngleType" />.</param>
		/// <param name="angleType">An <see cref="AngleType" /> specifying the type of angle to be measured.</param>
		/// <returns>
		/// The cosine of <paramref name="value" />.
		/// If <paramref name="value" /> is equal to <see cref="double.NaN" />, <see cref="double.NegativeInfinity" />, <see cref="double.PositiveInfinity" />, this method returns <see cref="double.NaN" />.
		/// </returns>
		public static double Cos(double value, AngleType angleType)
		{
			return Math.Cos(ConvertAngle(value, angleType, AngleType.Radians));
		}
		/// <summary>
		/// Returns the hyperbolic cosine of the specified angle, considering the specified <see cref="AngleType" />.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value representing an angle, measured in the specified <see cref="AngleType" />.</param>
		/// <param name="angleType">An <see cref="AngleType" /> specifying the type of angle to be measured.</param>
		/// <returns>
		/// The hyperbolic cosine of <paramref name="value" />.
		/// If <paramref name="value" /> is equal to <see cref="double.NegativeInfinity" /> or <see cref="double.PositiveInfinity" />, <see cref="double.PositiveInfinity" /> is returned.
		/// If <paramref name="value" /> is equal to <see cref="double.NaN" />, <see cref="double.NaN" /> is returned.
		/// </returns>
		public static double Cosh(double value, AngleType angleType)
		{
			return Math.Cosh(ConvertAngle(value, angleType, AngleType.Radians));
		}
		/// <summary>
		/// Returns the tangent of the specified angle, considering the specified <see cref="AngleType" />.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value representing an angle, measured in the specified <see cref="AngleType" />.</param>
		/// <param name="angleType">An <see cref="AngleType" /> specifying the type of angle to be measured.</param>
		/// <returns>
		/// The tangent of <paramref name="value" />.
		/// If <paramref name="value" /> is equal to <see cref="double.NaN" />, <see cref="double.NegativeInfinity" />, <see cref="double.PositiveInfinity" />, this method returns <see cref="double.NaN" />.
		/// </returns>
		public static double Tan(double value, AngleType angleType)
		{
			return Math.Tan(ConvertAngle(value, angleType, AngleType.Radians));
		}
		/// <summary>
		/// Returns the hyperbolic tangent of the specified angle, considering the specified <see cref="AngleType" />.
		/// </summary>
		/// <param name="value">A <see cref="double" /> value representing an angle, measured in the specified <see cref="AngleType" />.</param>
		/// <param name="angleType">An <see cref="AngleType" /> specifying the type of angle to be measured.</param>
		/// <returns>
		/// The hyperbolic tangent of <paramref name="value" />.
		/// If <paramref name="value" /> is equal to <see cref="double.NegativeInfinity" />, this method returns -1.0.
		/// If <paramref name="value" /> is equal to <see cref="double.PositiveInfinity" />, this method returns 1.0.
		/// If <paramref name="value" /> is equal to <see cref="double.NaN" />, this method returns <see cref="double.NaN" />.
		/// </returns>
		public static double Tanh(double value, AngleType angleType)
		{
			return Math.Tanh(ConvertAngle(value, angleType, AngleType.Radians));
		}
		/// <summary>
		/// Returns the angle whose sine is the specified number, considering the specified <see cref="AngleType" />.
		/// </summary>
		/// <param name="value">A number representing a sine, where <paramref name="value" /> must be greater than or equal to -1.0 and less than or equal to 1.0.</param>
		/// <param name="angleType">An <see cref="AngleType" /> specifying the type of angle to be measured.</param>
		/// <returns>
		/// An angle,
		/// or <see cref="double.NaN" />, if <paramref name="value" /> &lt; -1.0, <paramref name="value" /> &gt; 1.0 or if <paramref name="value" /> equals <see cref="double.NaN" />.
		/// </returns>
		public static double Asin(double value, AngleType angleType)
		{
			return ConvertAngle(Math.Asin(value), AngleType.Radians, angleType);
		}
		/// <summary>
		/// Returns the angle whose cosine is the specified number, considering the specified <see cref="AngleType" />.
		/// </summary>
		/// <param name="value">A number representing a cosine, where <paramref name="value" /> must be greater than or equal to -1.0 and less than or equal to 1.0.</param>
		/// <param name="angleType">An <see cref="AngleType" /> specifying the type of angle to be measured.</param>
		/// <returns>
		/// An angle,
		/// or <see cref="double.NaN" />, if <paramref name="value" /> &lt; -1.0, <paramref name="value" /> &gt; 1.0 or if <paramref name="value" /> equals <see cref="double.NaN" />.
		/// </returns>
		public static double Acos(double value, AngleType angleType)
		{
			return ConvertAngle(Math.Acos(value), AngleType.Radians, angleType);
		}
		/// <summary>
		/// Returns the angle whose tangent is the specified number, considering the specified <see cref="AngleType" />.
		/// </summary>
		/// <param name="value">A number representing a tangent.</param>
		/// <param name="angleType">An <see cref="AngleType" /> specifying the type of angle to be measured.</param>
		/// <returns>
		/// An angle, such that -π/2 ≤ θ ≤ π/2,
		/// or <see cref="double.NaN" />, if <paramref name="value" /> equals <see cref="double.NaN" />,
		/// -π/2, if <paramref name="value" /> equals <see cref="double.NegativeInfinity" />,
		/// or π/2, if <paramref name="value" /> equals <see cref="double.PositiveInfinity" />.
		/// </returns>
		public static double Atan(double value, AngleType angleType)
		{
			return ConvertAngle(Math.Atan(value), AngleType.Radians, angleType);
		}
		/// <summary>
		/// Returns the angle whose tangent is the quotient of two specified numbers, considering the specified <see cref="AngleType" />.
		/// </summary>
		/// <param name="y">The y coordinate of a point.</param>
		/// <param name="x">The x coordinate of a point.</param>
		/// <param name="angleType">An <see cref="AngleType" /> specifying the type of angle to be measured.</param>
		/// <returns>
		/// An angle, such that -π ≤ θ ≤ π, and tan(θ) = <paramref name="y" /> / x, where (x, <paramref name="y" />) is a point in the Cartesian plane.
		/// If <paramref name="x" /> or <paramref name="y" /> is
		/// <see cref="double.NaN" />, or if <paramref name="x" /> and <paramref name="y" /> are either <see cref="double.PositiveInfinity" /> or
		/// <see cref="double.NegativeInfinity" />, the method returns <see cref="double.NaN" />.
		/// </returns>
		public static double Atan2(double y, double x, AngleType angleType)
		{
			return ConvertAngle(Math.Atan2(y, x), AngleType.Radians, angleType);
		}
		/// <summary>
		/// Returns the hypotenuse of a triangle.
		/// </summary>
		/// <param name="x">The first side of the triangle.</param>
		/// <param name="y">The second side of the triangle.</param>
		/// <returns>
		/// The hypotenuse of the triangle.
		/// </returns>
		public static double Hypotenuse(double x, double y)
		{
			return Math.Sqrt(x * x + y * y);
		}
		/// <summary>
		/// Returns the hypotenuse of a triangle.
		/// </summary>
		/// <param name="x">The first side of the triangle.</param>
		/// <param name="y">The second side of the triangle.</param>
		/// <param name="z">The third side of the triangle.</param>
		/// <returns>
		/// The hypotenuse of the triangle.
		/// </returns>
		public static double Hypotenuse(double x, double y, double z)
		{
			return Math.Sqrt(x * x + y * y + z * z);
		}
		/// <summary>
		/// Converts an angle from one <see cref="AngleType" /> to another.
		/// </summary>
		/// <param name="angle">A <see cref="double" /> value representing an angle, measured in the <see cref="AngleType" /> specifyed by <paramref name="sourceType" />.</param>
		/// <param name="sourceType">The <see cref="AngleType" /> in which <paramref name="angle" /> is represented.</param>
		/// <param name="destType">The <see cref="AngleType" /> to which <paramref name="angle" /> is converted.</param>
		/// <returns>
		/// A <see cref="double" /> value representing the angle that has been converted from <paramref name="sourceType" /> to <paramref name="destType" />.
		/// </returns>
		public static double ConvertAngle(double angle, AngleType sourceType, AngleType destType)
		{
			if (sourceType == destType)
			{
				return angle;
			}
			else if (sourceType == AngleType.Radians)
			{
				if (destType == AngleType.Degrees) return angle * 180.0 / Math.PI;
				else if (destType == AngleType.Gradians) return angle * 200.0 / Math.PI;
				else throw Throw.InvalidEnumArgument(nameof(destType), destType);
			}
			else if (sourceType == AngleType.Degrees)
			{
				if (destType == AngleType.Radians) return angle * Math.PI / 180.0;
				else if (destType == AngleType.Gradians) return angle * 200.0 / 180.0;
				else throw Throw.InvalidEnumArgument(nameof(destType), destType);
			}
			else if (sourceType == AngleType.Gradians)
			{
				if (destType == AngleType.Radians) return angle * Math.PI / 200.0;
				else if (destType == AngleType.Degrees) return angle * 180.0 / 200.0;
				else throw Throw.InvalidEnumArgument(nameof(destType), destType);
			}
			else
			{
				throw Throw.InvalidEnumArgument(nameof(sourceType), sourceType);
			}
		}

		/// <summary>
		/// Returns an integer that indicates the sign of a <see cref="TimeSpan" /> value.
		/// </summary>
		/// <param name="value"><see cref="TimeSpan" /> value.</param>
		/// <returns>
		/// A number that indicates the sign of <paramref name="value" />.
		/// If <paramref name="value" /> is less than zero, -1 is returned; If <paramref name="value" /> is greater zero, 1 is returned; otherwise, 0 is returned.
		/// </returns>
		public static int Sign(TimeSpan value)
		{
			return Math.Sign(value.Ticks);
		}
		/// <summary>
		/// Returns the smaller of two <see cref="DateTime" /> objects.
		/// </summary>
		/// <param name="a">The first of two <see cref="DateTime" /> objects to compare.</param>
		/// <param name="b">The second of two <see cref="DateTime" /> objects to compare.</param>
		/// <returns>
		/// Parameter <paramref name="a" /> or <paramref name="b" />, whichever is smaller.
		/// </returns>
		public static DateTime Min(DateTime a, DateTime b)
		{
			return a < b ? a : b;
		}
		/// <summary>
		/// Returns the smaller of two <see cref="TimeSpan" /> objects.
		/// </summary>
		/// <param name="a">The first of two <see cref="TimeSpan" /> objects to compare.</param>
		/// <param name="b">The second of two <see cref="TimeSpan" /> objects to compare.</param>
		/// <returns>
		/// Parameter <paramref name="a" /> or <paramref name="b" />, whichever is smaller.
		/// </returns>
		public static TimeSpan Min(TimeSpan a, TimeSpan b)
		{
			return a < b ? a : b;
		}
		/// <summary>
		/// Returns the smaller of two <see cref="IComparable" /> objects.
		/// </summary>
		/// <typeparam name="T">The type of the parameters to compare.</typeparam>
		/// <param name="a">The first of two <see cref="IComparable" /> objects to compare.</param>
		/// <param name="b">The second of two <see cref="IComparable" /> objects to compare.</param>
		/// <returns>
		/// Parameter <paramref name="a" /> or <paramref name="b" />, whichever is smaller.
		/// </returns>
		public static T Min<T>(T a, T b) where T : IComparable
		{
			if (a == null) return a;
			else if (b == null) return b;
			else return a.CompareTo(b) < 0 ? a : b;
		}
		/// <summary>
		/// Returns the larger of two <see cref="DateTime" /> objects.
		/// </summary>
		/// <param name="a">The first of two <see cref="DateTime" /> objects to compare.</param>
		/// <param name="b">The second of two <see cref="DateTime" /> objects to compare.</param>
		/// <returns>
		/// Parameter <paramref name="a" /> or <paramref name="b" />, whichever is larger.
		/// </returns>
		public static DateTime Max(DateTime a, DateTime b)
		{
			return a > b ? a : b;
		}
		/// <summary>
		/// Returns the larger of two <see cref="TimeSpan" /> objects.
		/// </summary>
		/// <param name="a">The first of two <see cref="TimeSpan" /> objects to compare.</param>
		/// <param name="b">The second of two <see cref="TimeSpan" /> objects to compare.</param>
		/// <returns>
		/// Parameter <paramref name="a" /> or <paramref name="b" />, whichever is larger.
		/// </returns>
		public static TimeSpan Max(TimeSpan a, TimeSpan b)
		{
			return a > b ? a : b;
		}
		/// <summary>
		/// Returns the larger of two <see cref="IComparable" /> objects.
		/// </summary>
		/// <typeparam name="T">The type of the parameters to compare.</typeparam>
		/// <param name="a">The first of two <see cref="IComparable" /> objects to compare.</param>
		/// <param name="b">The second of two <see cref="IComparable" /> objects to compare.</param>
		/// <returns>
		/// Parameter <paramref name="a" /> or <paramref name="b" />, whichever is larger.
		/// </returns>
		public static T Max<T>(T a, T b) where T : IComparable
		{
			if (a == null) return b;
			else if (b == null) return a;
			else return a.CompareTo(b) > 0 ? a : b;
		}

		/// <summary>
		/// Returns a <see cref="byte" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="byte" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="byte" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="byte" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static byte Map(byte value, byte min, byte max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Math.Min(Math.Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="sbyte" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="sbyte" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="sbyte" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="sbyte" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static sbyte Map(sbyte value, sbyte min, sbyte max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Math.Min(Math.Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="decimal" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="decimal" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="decimal" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="decimal" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static decimal Map(decimal value, decimal min, decimal max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Math.Min(Math.Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="double" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="double" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="double" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="double" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static double Map(double value, double min, double max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Math.Min(Math.Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="float" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="float" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="float" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="float" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static float Map(float value, float min, float max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Math.Min(Math.Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="int" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="int" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="int" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static int Map(int value, int min, int max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Math.Min(Math.Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="uint" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="uint" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="uint" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="uint" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static uint Map(uint value, uint min, uint max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Math.Min(Math.Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="long" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="long" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="long" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="long" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static long Map(long value, long min, long max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Math.Min(Math.Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="ulong" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="ulong" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="ulong" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="ulong" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static ulong Map(ulong value, ulong min, ulong max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Math.Min(Math.Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="short" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="short" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="short" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="short" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static short Map(short value, short min, short max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Math.Min(Math.Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="ushort" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="ushort" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="ushort" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="ushort" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static ushort Map(ushort value, ushort min, ushort max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Math.Min(Math.Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="DateTime" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="DateTime" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="DateTime" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="DateTime" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static DateTime Map(DateTime value, DateTime min, DateTime max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Min(Max(value, min), max);
		}
		/// <summary>
		/// Returns a <see cref="TimeSpan" /> value that represents <paramref name="value" /> and is limited by the specified inclusive boundaries. The result is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="max" />.
		/// </summary>
		/// <param name="value">The <see cref="TimeSpan" /> value to be mapped by the specified boundaries.</param>
		/// <param name="min">The <see cref="TimeSpan" /> value representing the minimum of the result value.</param>
		/// <param name="max">The <see cref="TimeSpan" /> value representing the maximum of the result value.</param>
		/// <returns>
		/// The original value, if <paramref name="value" /> is greater than or equal to <paramref name="min" /> and less than or equal to <paramref name="min" />;
		/// <paramref name="min" />, if <paramref name="value" /> is less than <paramref name="min" />;
		/// and <paramref name="max" />, if <paramref name="value" /> is greater than <paramref name="max" />.
		/// </returns>
		public static TimeSpan Map(TimeSpan value, TimeSpan min, TimeSpan max)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqualValue(max, min, nameof(max), nameof(min));

			return Min(Max(value, min), max);
		}
		/// <summary>
		/// Interpolates the specified <see cref="decimal" /> value by scaling its expected range to another. The result is not clipped.
		/// </summary>
		/// <param name="value">The <see cref="decimal" /> value representing the initial value.</param>
		/// <param name="valueMin">A <see cref="decimal" /> value indicating the expected minimum of <paramref name="value" />.</param>
		/// <param name="valueMax">A <see cref="decimal" /> value indicating the expected maximum of <paramref name="value" />.</param>
		/// <param name="returnMin">A <see cref="decimal" /> value indicating the resulting minimum of the new value range.</param>
		/// <param name="returnMax">A <see cref="decimal" /> value indicating the resulting maximum of the new value range.</param>
		/// <returns>
		/// A <see cref="decimal" /> value, where the range of <paramref name="valueMin" /> through <paramref name="valueMax" /> has been scaled to <paramref name="returnMin" /> through <paramref name="returnMax" />. The result is not clipped.
		/// </returns>
		public static decimal Interpolate(decimal value, decimal valueMin, decimal valueMax, decimal returnMin, decimal returnMax)
		{
			return Interpolate(value, valueMin, valueMax, returnMin, returnMax, false);
		}
		/// <summary>
		/// Interpolates the specified <see cref="decimal" /> value by scaling its expected range to another. If <paramref name="mapToValueRange" /> is <see langword="true" />, the result clipped and therefore greater than or equal to <paramref name="returnMin" /> or less than or equal to <paramref name="valueMax" />.
		/// </summary>
		/// <param name="value">The <see cref="decimal" /> value representing the initial value.</param>
		/// <param name="valueMin">A <see cref="decimal" /> value indicating the expected minimum of <paramref name="value" />.</param>
		/// <param name="valueMax">A <see cref="decimal" /> value indicating the expected maximum of <paramref name="value" />.</param>
		/// <param name="returnMin">A <see cref="decimal" /> value indicating the resulting minimum of the new value range.</param>
		/// <param name="returnMax">A <see cref="decimal" /> value indicating the resulting maximum of the new value range.</param>
		/// <param name="mapToValueRange"><see langword="true" /> to clip the result to the new value range.</param>
		/// <returns>
		/// A <see cref="decimal" /> value, where the range of <paramref name="valueMin" /> through <paramref name="valueMax" /> has been scaled to <paramref name="returnMin" /> through <paramref name="returnMax" />.
		/// If <paramref name="mapToValueRange" /> is <see langword="true" />, the result clipped and therefore greater than or equal to <paramref name="returnMin" /> or less than or equal to <paramref name="valueMax" />.
		/// </returns>
		public static decimal Interpolate(decimal value, decimal valueMin, decimal valueMax, decimal returnMin, decimal returnMax, bool mapToValueRange)
		{
			decimal result = returnMin + (value - valueMin) * (returnMax - returnMin) / (valueMax - valueMin);
			return mapToValueRange ? Map(result, valueMin, valueMax) : result;
		}
		/// <summary>
		/// Interpolates the specified <see cref="double" /> value by scaling its expected range to another. The result is not clipped.
		/// </summary>
		/// <param name="value">The <see cref="double" /> value representing the initial value.</param>
		/// <param name="valueMin">A <see cref="double" /> value indicating the expected minimum of <paramref name="value" />.</param>
		/// <param name="valueMax">A <see cref="double" /> value indicating the expected maximum of <paramref name="value" />.</param>
		/// <param name="returnMin">A <see cref="double" /> value indicating the resulting minimum of the new value range.</param>
		/// <param name="returnMax">A <see cref="double" /> value indicating the resulting maximum of the new value range.</param>
		/// <returns>
		/// A <see cref="double" /> value, where the range of <paramref name="valueMin" /> through <paramref name="valueMax" /> has been scaled to <paramref name="returnMin" /> through <paramref name="returnMax" />. The result is not clipped.
		/// </returns>
		public static double Interpolate(double value, double valueMin, double valueMax, double returnMin, double returnMax)
		{
			return Interpolate(value, valueMin, valueMax, returnMin, returnMax, false);
		}
		/// <summary>
		/// Interpolates the specified <see cref="double" /> value by scaling its expected range to another. If <paramref name="mapToValueRange" /> is <see langword="true" />, the result clipped and therefore greater than or equal to <paramref name="returnMin" /> or less than or equal to <paramref name="valueMax" />.
		/// </summary>
		/// <param name="value">The <see cref="double" /> value representing the initial value.</param>
		/// <param name="valueMin">A <see cref="double" /> value indicating the expected minimum of <paramref name="value" />.</param>
		/// <param name="valueMax">A <see cref="double" /> value indicating the expected maximum of <paramref name="value" />.</param>
		/// <param name="returnMin">A <see cref="double" /> value indicating the resulting minimum of the new value range.</param>
		/// <param name="returnMax">A <see cref="double" /> value indicating the resulting maximum of the new value range.</param>
		/// <param name="mapToValueRange"><see langword="true" /> to clip the result to the new value range.</param>
		/// <returns>
		/// A <see cref="double" /> value, where the range of <paramref name="valueMin" /> through <paramref name="valueMax" /> has been scaled to <paramref name="returnMin" /> through <paramref name="returnMax" />.
		/// If <paramref name="mapToValueRange" /> is <see langword="true" />, the result clipped and therefore greater than or equal to <paramref name="returnMin" /> or less than or equal to <paramref name="valueMax" />.
		/// </returns>
		public static double Interpolate(double value, double valueMin, double valueMax, double returnMin, double returnMax, bool mapToValueRange)
		{
			double result = returnMin + (value - valueMin) * (returnMax - returnMin) / (valueMax - valueMin);
			return mapToValueRange ? Map(result, valueMin, valueMax) : result;
		}
		/// <summary>
		/// Interpolates the specified <see cref="float" /> value by scaling its expected range to another. The result is not clipped.
		/// </summary>
		/// <param name="value">The <see cref="float" /> value representing the initial value.</param>
		/// <param name="valueMin">A <see cref="float" /> value indicating the expected minimum of <paramref name="value" />.</param>
		/// <param name="valueMax">A <see cref="float" /> value indicating the expected maximum of <paramref name="value" />.</param>
		/// <param name="returnMin">A <see cref="float" /> value indicating the resulting minimum of the new value range.</param>
		/// <param name="returnMax">A <see cref="float" /> value indicating the resulting maximum of the new value range.</param>
		/// <returns>
		/// A <see cref="float" /> value, where the range of <paramref name="valueMin" /> through <paramref name="valueMax" /> has been scaled to <paramref name="returnMin" /> through <paramref name="returnMax" />. The result is not clipped.
		/// </returns>
		public static float Interpolate(float value, float valueMin, float valueMax, float returnMin, float returnMax)
		{
			return Interpolate(value, valueMin, valueMax, returnMin, returnMax, false);
		}
		/// <summary>
		/// Interpolates the specified <see cref="float" /> value by scaling its expected range to another. If <paramref name="mapToValueRange" /> is <see langword="true" />, the result clipped and therefore greater than or equal to <paramref name="returnMin" /> or less than or equal to <paramref name="valueMax" />.
		/// </summary>
		/// <param name="value">The <see cref="float" /> value representing the initial value.</param>
		/// <param name="valueMin">A <see cref="float" /> value indicating the expected minimum of <paramref name="value" />.</param>
		/// <param name="valueMax">A <see cref="float" /> value indicating the expected maximum of <paramref name="value" />.</param>
		/// <param name="returnMin">A <see cref="float" /> value indicating the resulting minimum of the new value range.</param>
		/// <param name="returnMax">A <see cref="float" /> value indicating the resulting maximum of the new value range.</param>
		/// <param name="mapToValueRange"><see langword="true" /> to clip the result to the new value range.</param>
		/// <returns>
		/// A <see cref="float" /> value, where the range of <paramref name="valueMin" /> through <paramref name="valueMax" /> has been scaled to <paramref name="returnMin" /> through <paramref name="returnMax" />.
		/// If <paramref name="mapToValueRange" /> is <see langword="true" />, the result clipped and therefore greater than or equal to <paramref name="returnMin" /> or less than or equal to <paramref name="valueMax" />.
		/// </returns>
		public static float Interpolate(float value, float valueMin, float valueMax, float returnMin, float returnMax, bool mapToValueRange)
		{
			float result = returnMin + (value - valueMin) * (returnMax - returnMin) / (valueMax - valueMin);
			return mapToValueRange ? Map(result, valueMin, valueMax) : result;
		}
		/// <summary>
		/// Interpolates the specified <see cref="int" /> value by scaling its expected range to another. The result is not clipped.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value representing the initial value.</param>
		/// <param name="valueMin">A <see cref="int" /> value indicating the expected minimum of <paramref name="value" />.</param>
		/// <param name="valueMax">A <see cref="int" /> value indicating the expected maximum of <paramref name="value" />.</param>
		/// <param name="returnMin">A <see cref="int" /> value indicating the resulting minimum of the new value range.</param>
		/// <param name="returnMax">A <see cref="int" /> value indicating the resulting maximum of the new value range.</param>
		/// <returns>
		/// A <see cref="int" /> value, where the range of <paramref name="valueMin" /> through <paramref name="valueMax" /> has been scaled to <paramref name="returnMin" /> through <paramref name="returnMax" />. The result is not clipped.
		/// </returns>
		public static int Interpolate(int value, int valueMin, int valueMax, int returnMin, int returnMax)
		{
			return Interpolate(value, valueMin, valueMax, returnMin, returnMax, false);
		}
		/// <summary>
		/// Interpolates the specified <see cref="int" /> value by scaling its expected range to another. If <paramref name="mapToValueRange" /> is <see langword="true" />, the result clipped and therefore greater than or equal to <paramref name="returnMin" /> or less than or equal to <paramref name="valueMax" />.
		/// </summary>
		/// <param name="value">The <see cref="int" /> value representing the initial value.</param>
		/// <param name="valueMin">A <see cref="int" /> value indicating the expected minimum of <paramref name="value" />.</param>
		/// <param name="valueMax">A <see cref="int" /> value indicating the expected maximum of <paramref name="value" />.</param>
		/// <param name="returnMin">A <see cref="int" /> value indicating the resulting minimum of the new value range.</param>
		/// <param name="returnMax">A <see cref="int" /> value indicating the resulting maximum of the new value range.</param>
		/// <param name="mapToValueRange"><see langword="true" /> to clip the result to the new value range.</param>
		/// <returns>
		/// A <see cref="int" /> value, where the range of <paramref name="valueMin" /> through <paramref name="valueMax" /> has been scaled to <paramref name="returnMin" /> through <paramref name="returnMax" />.
		/// If <paramref name="mapToValueRange" /> is <see langword="true" />, the result clipped and therefore greater than or equal to <paramref name="returnMin" /> or less than or equal to <paramref name="valueMax" />.
		/// </returns>
		public static int Interpolate(int value, int valueMin, int valueMax, int returnMin, int returnMax, bool mapToValueRange)
		{
			int result = returnMin + (value - valueMin) * (returnMax - returnMin) / (valueMax - valueMin);
			return mapToValueRange ? Map(result, valueMin, valueMax) : result;
		}

		/// <summary>
		/// Computes the greatest common divisor of two <see cref="int" /> values.
		/// </summary>
		/// <param name="a">The first <see cref="int" /> value.</param>
		/// <param name="b">The second <see cref="int" /> value.</param>
		/// <returns>
		/// The greatest common divisor of <paramref name="a" /> and <paramref name="b" />.
		/// </returns>
		public static int GreatestCommonDivisor(int a, int b)
		{
			while (b != 0)
			{
				int swap = b;
				b = a % b;
				a = swap;
			}

			return a;
		}
		/// <summary>
		/// Computes the greatest common divisor of a collection of <see cref="int" /> values.
		/// </summary>
		/// <param name="values">The collection of <see cref="int" /> values.</param>
		/// <returns>
		/// The greatest common divisor of the values in the <paramref name="values" /> parameter.
		/// </returns>
		public static int GreatestCommonDivisor(params int[] values)
		{
			Check.ArgumentNull(values, nameof(values));
			Check.Argument(values.Length >= 2, nameof(values), "The calculation requires at least 2 values.");

			return values.Aggregate(GreatestCommonDivisor);
		}
		/// <summary>
		/// Computes the greatest common divisor of two <see cref="long" /> values.
		/// </summary>
		/// <param name="a">The first <see cref="long" /> value.</param>
		/// <param name="b">The second <see cref="long" /> value.</param>
		/// <returns>
		/// The greatest common divisor of <paramref name="a" /> and <paramref name="b" />.
		/// </returns>
		public static long GreatestCommonDivisor(long a, long b)
		{
			while (b != 0)
			{
				long swap = b;
				b = a % b;
				a = swap;
			}

			return a;
		}
		/// <summary>
		/// Computes the greatest common divisor of a collection of <see cref="long" /> values.
		/// </summary>
		/// <param name="values">The collection of <see cref="long" /> values.</param>
		/// <returns>
		/// The greatest common divisor of the values in the <paramref name="values" /> parameter.
		/// </returns>
		public static long GreatestCommonDivisor(params long[] values)
		{
			Check.ArgumentNull(values, nameof(values));
			Check.Argument(values.Length >= 2, nameof(values), "The calculation requires at least 2 values.");

			return values.Aggregate(GreatestCommonDivisor);
		}
		/// <summary>
		/// Computes the least common multiple of two <see cref="int" /> values.
		/// </summary>
		/// <param name="a">The first <see cref="int" /> value.</param>
		/// <param name="b">The second <see cref="int" /> value.</param>
		/// <returns>
		/// The least common multiple of <paramref name="a" /> and <paramref name="b" />.
		/// </returns>
		public static int LeastCommonMultiple(int a, int b)
		{
			Check.DivideByZero(a, nameof(a));
			Check.DivideByZero(b, nameof(b));

			return a / GreatestCommonDivisor(a, b) * b;
		}
		/// <summary>
		/// Computes the least common multiple of an array of <see cref="int" /> values.
		/// </summary>
		/// <param name="values">The array of <see cref="int" /> values to compute the least common multiple from.</param>
		/// <returns>
		/// The least common multiple of all <see cref="int" /> values.
		/// </returns>
		public static int LeastCommonMultiple(params int[] values)
		{
			Check.ArgumentNull(values, nameof(values));
			Check.Argument(values.Length >= 2, nameof(values), "The calculation requires at least 2 values.");

			return values.Aggregate(LeastCommonMultiple);
		}
		/// <summary>
		/// Computes the least common multiple of two <see cref="long" /> values.
		/// </summary>
		/// <param name="a">The first <see cref="long" /> value.</param>
		/// <param name="b">The second <see cref="long" /> value.</param>
		/// <returns>
		/// The least common multiple of <paramref name="a" /> and <paramref name="b" />.
		/// </returns>
		public static long LeastCommonMultiple(long a, long b)
		{
			Check.DivideByZero(a, nameof(a));
			Check.DivideByZero(b, nameof(b));

			return a / GreatestCommonDivisor(a, b) * b;
		}
		/// <summary>
		/// Computes the least common multiple of an array of <see cref="long" /> values.
		/// </summary>
		/// <param name="values">The array of <see cref="long" /> values to compute the least common multiple from.</param>
		/// <returns>
		/// The least common multiple of all <see cref="long" /> values.
		/// </returns>
		public static long LeastCommonMultiple(params long[] values)
		{
			Check.ArgumentNull(values, nameof(values));
			Check.Argument(values.Length >= 2, nameof(values), "The calculation requires at least 2 values.");

			return values.Aggregate(LeastCommonMultiple);
		}
		/// <summary>
		/// Determines whether <paramref name="n" /> is a prime number.
		/// </summary>
		/// <param name="n">A <see cref="int" /> value specifying the number to test.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="n" /> is a prime number;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsPrimeNumber(int n)
		{
			if (n <= 1) return false;
			else if (n == 2) return true;
			else if ((n & 1) == 0) return false;

			int to = (int)Math.Floor(Math.Sqrt(n));
			for (int i = 3; i <= to; i += 2)
			{
				if (n % i == 0) return false;
			}

			return true;
		}
		/// <summary>
		/// Determines whether <paramref name="n" /> is a prime number.
		/// </summary>
		/// <param name="n">A <see cref="long" /> value specifying the number to test.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="n" /> is a prime number;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public static bool IsPrimeNumber(long n)
		{
			if (n <= 1) return false;
			else if (n == 2) return true;
			else if ((n & 1) == 0) return false;

			long to = (long)Math.Floor(Math.Sqrt(n));
			for (long i = 3; i <= to; i += 2)
			{
				if (n % i == 0) return false;
			}

			return true;
		}
		/// <summary>
		/// Computes the prime numbers between 0 and <paramref name="n" />. A <see cref="BitArray" /> with <paramref name="n" /> elements is returned, where a <see cref="bool" /> value at any given index indicates whether the number is a prime number.
		/// </summary>
		/// <param name="n">A <see cref="int" /> value specifying the range of numbers to check, where computation of numbers applies to values between 0 and <paramref name="n" />-1.</param>
		/// <returns>
		/// A <see cref="BitArray" /> with <paramref name="n" /> elements, where a <see cref="bool" /> value at any given index indicates whether the number is a prime number.
		/// </returns>
		public static BitArray ComputePrimeNumbers(int n)
		{
			Check.ArgumentOutOfRangeEx.Greater0(n, nameof(n));

			BitArray result = new BitArray(n, true);
			if (n >= 1) result[0] = false;
			if (n >= 2) result[1] = false;

			int to = (int)Math.Ceiling(Math.Sqrt(n));
			for (int i = 2; i < to; i++)
			{
				while (!result[i]) i++;
				for (int j = i + i; j < n; j += i) result[j] = false;
			}

			return result;
		}
		/// <summary>
		/// Computes all prime factors of the specified <see cref="int" /> value.
		/// </summary>
		/// <param name="n">The <see cref="int" /> to compute its prime factors from.</param>
		/// <returns>
		/// A <see cref="int" />[] containing all prime factors of <paramref name="n" />.
		/// </returns>
		public static int[] ComputePrimeFactors(int n)
		{
			Check.ArgumentOutOfRangeEx.Greater0(n, nameof(n));

			List<int> result = new List<int>();

			for (int i = 2; n > 1; i++)
			{
				while (n % i == 0)
				{
					n /= i;
					result.Add(i);
				}
			}

			return result.ToArray();
		}
		/// <summary>
		/// Computes all prime factors of the specified <see cref="long" /> value.
		/// </summary>
		/// <param name="n">The <see cref="long" /> to compute its prime factors from.</param>
		/// <returns>
		/// A <see cref="long" />[] containing all prime factors of <paramref name="n" />.
		/// </returns>
		public static long[] ComputePrimeFactors(long n)
		{
			Check.ArgumentOutOfRangeEx.Greater0(n, nameof(n));

			List<long> result = new List<long>();

			for (long i = 2; n > 1; i++)
			{
				while (n % i == 0)
				{
					n /= i;
					result.Add(i);
				}
			}

			return result.ToArray();
		}
		/// <summary>
		/// Computes the factorial of a number.
		/// <para>Example: Factorial(6) = 1*2*3*4*5*6</para>
		/// </summary>
		/// <param name="n">The value to compute the factorial from. If <paramref name="n" /> is greater than 20, an <see cref="OverflowException" /> will be raised. For larger numbers, use <see cref="ComputeFactorialBig(int)" />.</param>
		/// <returns>
		/// The factorial of <paramref name="n" />.
		/// </returns>
		public static long ComputeFactorial(int n)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(n, nameof(n));
			Check.Overflow(n <= 20);

			long result = 1;
			for (int i = 1; i <= n; i++) result *= i;
			return result;
		}
		/// <summary>
		/// Computes the factorial of a number and returns a <see cref="BigInteger" /> value.
		/// <para>Example: Factorial(6) = 1*2*3*4*5*6</para>
		/// </summary>
		/// <param name="n">The value to compute the factorial from.</param>
		/// <returns>
		/// The factorial of <paramref name="n" />, represented as a <see cref="BigInteger" /> value.
		/// </returns>
		public static BigInteger ComputeFactorialBig(int n)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(n, nameof(n));

			BigInteger result = 1;
			for (int i = 1; i <= n; i++) result *= i;
			return result;
		}
		/// <summary>
		/// Computes the Fibonacci value at position <paramref name="n" />.
		/// </summary>
		/// <param name="n">A <see cref="int" /> value specifying the position of the Fibonacci value. If <paramref name="n" /> is greater than 91, an <see cref="OverflowException" /> will be raised.</param>
		/// <returns>
		/// The Fibonacci value at position <paramref name="n" />.
		/// </returns>
		public static long Fibonacci(int n)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(n, nameof(n));
			Check.Overflow(n <= 91);

			long a = 0;
			long b = 1;

			for (int i = 0; i < n; i++)
			{
				long swap = a;
				a = b;
				b = swap + b;
			}

			return a;
		}
		/// <summary>
		/// Computes the Fibonacci value at position <paramref name="n" /> and returns a <see cref="BigInteger" /> value.
		/// </summary>
		/// <param name="n">A <see cref="int" /> value specifying the position of the Fibonacci value.</param>
		/// <returns>
		/// The Fibonacci value at position <paramref name="n" />, represented as a <see cref="BigInteger" /> value.
		/// </returns>
		public static BigInteger FibonacciBig(int n)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(n, nameof(n));

			BigInteger a = 0;
			BigInteger b = 1;

			for (int i = 0; i < n; i++)
			{
				BigInteger swap = a;
				a = b;
				b = swap + b;
			}

			return a;
		}
		/// <summary>
		/// Returns an enumerable of Fibonacci values that yields <paramref name="n" /> elements.
		/// </summary>
		/// <param name="n">A <see cref="int" /> value specifying the number of Fibonacci values to compute. If <paramref name="n" /> is greater than 91, an <see cref="OverflowException" /> will be raised.</param>
		/// <returns>
		/// An enumerable of Fibonacci values that yields <paramref name="n" /> elements.
		/// </returns>
		public static IEnumerable<long> EnumerateFibonacci(int n)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(n, nameof(n));
			Check.Overflow(n <= 91);

			long a = 0;
			long b = 1;

			for (int i = 0; i < n; i++)
			{
				yield return a;
				long swap = a;
				a = b;
				b = swap + b;
			}
		}
		/// <summary>
		/// Returns an enumerable of Fibonacci values that yields <paramref name="n" /> <see cref="BigInteger" /> elements.
		/// </summary>
		/// <param name="n">A <see cref="int" /> value specifying the number of Fibonacci values to compute.</param>
		/// <returns>
		/// An enumerable of Fibonacci values that yields <paramref name="n" /> <see cref="BigInteger" /> elements.
		/// </returns>
		public static IEnumerable<BigInteger> EnumerateFibonacciBig(int n)
		{
			Check.ArgumentOutOfRangeEx.GreaterEqual0(n, nameof(n));

			BigInteger a = 0;
			BigInteger b = 1;

			for (int i = 0; i < n; i++)
			{
				yield return a;
				BigInteger swap = a;
				a = b;
				b = swap + b;
			}
		}
	}
}