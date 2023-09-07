using BytecodeApi;
using BytecodeApi.Extensions;

/// <summary>
/// Playground project for development and case testing of class libraries.
/// All methods with the ExecuteAttribute will be executed.
/// </summary>
public static class Playground
{
	[Execute]
	public static void Test1(string[] args)
	{
	}
	//[Execute]
	public static void Test2(string[] args)
	{
	}
	//[Execute]
	public static void Test3(string[] args)
	{
	}
	//[Execute]
	public static async Task Task1(string[] args)
	{
		await Task.CompletedTask;
	}
	//[Execute]
	public static async Task Task2(string[] args)
	{
		await Task.CompletedTask;
	}
	//[Execute]
	public static async Task Task3(string[] args)
	{
		await Task.CompletedTask;
	}
}