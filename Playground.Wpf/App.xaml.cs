using BytecodeApi.Wpf.Interop;
using System.Windows;

namespace Playground.Wpf;

public partial class App : Application
{
	public static SingleInstance SingleInstance { get; private set; } = null!;

	private void Application_Startup(object sender, StartupEventArgs e)
	{
		SingleInstance = new SingleInstance("EXAMPLE_SINGLE_INSTANCE");
		if (SingleInstance.CheckInstanceRunning())
		{
			SingleInstance.SendActivationMessage();
			Shutdown();
		}
	}
}