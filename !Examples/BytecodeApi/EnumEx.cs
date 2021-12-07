using BytecodeApi;
using BytecodeApi.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public static class Program
{
	[STAThread]
	public static void Main()
	{
		// Get description
		string mondayDescription = Day.Monday.GetDescription();
		Day? monday = EnumEx.FindValueByDescription<Day>("Mon");

		// Get all enum values
		Day[] enumValues = EnumEx.GetValues<Day>();

		// Get all enum values with description
		Dictionary<Day, string> enumDescriptionLookup = EnumEx.GetDescriptionLookup<Day>();
	}
}

public enum Day
{
	[Description("Mon")]
	Monday,
	[Description("Tue")]
	Tuesday,
	[Description("Wed")]
	Wednesday,
	[Description("Thu")]
	Thursday,
	[Description("Fri")]
	Friday,
	[Description("Sat")]
	Saturday,
	[Description("Sun")]
	Sunday
}