using BytecodeApi;
using System;
using static System.Console;

namespace Playground.Console
{
	/// <summary>
	/// Playground project for development and case testing of class libraries.
	/// </summary>
	public static class Program
	{
		[STAThread]
		public static void Main(string[] args)
		{
			Action test = Test1;

			WriteLine("Time elapsed: " + (int)CSharp.MeasureTime(test).TotalMilliseconds);
			ReadKey();
		}

		public static void Test1()
		{
		}
		public static void Test2()
		{
		}
		public static void Test3()
		{
		}
		public static void Test4()
		{
		}
		public static void Test5()
		{
		}
	}
}