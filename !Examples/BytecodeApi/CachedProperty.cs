using BytecodeApi;
using System;
using System.Threading;

public static class Program
{
	private static int TestBackingField = 0;
	private static readonly CachedProperty<int> MyProperty = new CachedProperty<int>(() =>
	{
		// getter
		// This property increments its value each time the getter is called
		return ++TestBackingField;
	}, TimeSpan.FromSeconds(1));

	//          ^---- Timeout = 1 second; Specify null to call the getter only once.

	[STAThread]
	public static void Main()
	{
		// CachedProperty<T> serves as cache for values stored in memory,
		// but retrieved only periodically after a timeout has been exceeded

		for (int i = 0; i < 30; i++)
		{
			// The actual getter is called only 1x per second as specified by the timeout property of our CachedProperty<T>
			Console.WriteLine(MyProperty.Get());
			Thread.Sleep(100);
		}

		Console.ReadKey();
	}
}