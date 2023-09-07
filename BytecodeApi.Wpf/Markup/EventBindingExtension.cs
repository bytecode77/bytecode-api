using System.Reflection;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace BytecodeApi.Wpf.Markup;

/// <summary>
/// Implements event binding to <see cref="ICommand" /> objects support for .NET Framework XAML Services.
/// </summary>
[MarkupExtensionReturnType(typeof(Delegate))]
public sealed class EventBindingExtension : MarkupExtension
{
	private static readonly MethodInfo? EventHandlerImplMethod = typeof(EventBindingExtension).GetMethod(nameof(EventHandlerImpl), new[] { typeof(object), typeof(string) });
	/// <summary>
	/// Gets or sets the <see cref="ICommand" /> that is invoked when the event is fired. The <see cref="ICommandSource.CommandParameter" /> property of the <see cref="FrameworkElement" /> is passed as parameters to the <see cref="ICommand" /> instance.
	/// </summary>
	public string Command { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="EventBindingExtension" /> class, initializing <see cref="Command" /> based on the provided <see cref="string" /> value.
	/// </summary>
	/// <param name="command">A <see cref="string" /> value that is assigned to <see cref="Command" />.</param>
	public EventBindingExtension(string command)
	{
		Check.ArgumentNull(command);
		Check.ArgumentEx.StringNotEmpty(command);

		Command = command;
	}

	/// <summary>
	/// Do not use! Implements the method that is called by the underlying event handler.
	/// </summary>
	/// <param name="sender">The sender argument of the original event handler.</param>
	/// <param name="commandName">The name of the command delegated by the original event handler.</param>
	public static void EventHandlerImpl(object sender, string commandName)
	{
		if (sender is FrameworkElement frameworkElement &&
			frameworkElement.DataContext is object dataContext &&
			dataContext.GetType().GetProperty(commandName)?.GetValue(dataContext) is ICommand command)
		{
			object? commandParameter = (frameworkElement as ICommandSource)?.CommandParameter;
			if (command.CanExecute(commandParameter))
			{
				command.Execute(commandParameter);
			}
		}
	}

	/// <summary>
	/// Returns a <see cref="Delegate" /> that is created from the parameters supplied in the constructor of <see cref="EventBindingExtension" />.
	/// </summary>
	/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
	/// <returns>
	/// A <see cref="Delegate" /> value that is created from the parameters supplied in the constructor of <see cref="EventBindingExtension" />.
	/// </returns>
	public override object? ProvideValue(IServiceProvider serviceProvider)
	{
		if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget targetProvider &&
			targetProvider.TargetObject is FrameworkElement &&
			targetProvider.TargetProperty is MemberInfo memberInfo)
		{
			Type eventHandlerType;
			if (memberInfo is EventInfo eventInfo)
			{
				eventHandlerType = eventInfo.EventHandlerType ?? throw CreateException();
			}
			else if (memberInfo is MethodInfo methodInfo)
			{
				eventHandlerType = methodInfo.GetParameters()[1].ParameterType;
			}
			else
			{
				return null;
			}

			MethodInfo handler = eventHandlerType.GetMethod("Invoke") ?? throw CreateException();
			DynamicMethod method = new("", handler.ReturnType, new[] { typeof(object), typeof(object) });

			ILGenerator ilGenerator = method.GetILGenerator();
			ilGenerator.Emit(OpCodes.Ldarg, 0);
			ilGenerator.Emit(OpCodes.Ldstr, Command);
			ilGenerator.Emit(OpCodes.Call, EventHandlerImplMethod ?? throw CreateException());
			ilGenerator.Emit(OpCodes.Ret);

			return method.CreateDelegate(eventHandlerType);
		}
		else
		{
			throw CreateException();
		}

		static Exception CreateException()
		{
			return Throw.InvalidOperation("Could not create event binding.");
		}
	}
}