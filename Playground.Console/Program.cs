using BytecodeApi;
using System;

namespace Playground
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

			Console.WriteLine("Time elapsed: " + (int)CSharp.MeasureTime(test).TotalMilliseconds);
			Console.ReadKey();
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