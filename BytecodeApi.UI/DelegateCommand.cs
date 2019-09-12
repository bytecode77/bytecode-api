using System;
using System.Windows.Input;

namespace BytecodeApi.UI
{
	/// <summary>
	/// Represents a command which uses specified delegates for <see cref="ICommand.Execute(object)" /> and <see cref="ICommand.CanExecute(object)" />.
	/// </summary>
	public sealed class DelegateCommand : ICommand
	{
		private readonly Action ExecuteDelegate;
		private readonly Func<bool> CanExecuteDelegate;
		event EventHandler ICommand.CanExecuteChanged
		{
			add
			{
				if (CanExecuteDelegate != null) CommandManager.RequerySuggested += value;
			}
			remove
			{
				if (CanExecuteDelegate != null) CommandManager.RequerySuggested -= value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateCommand" /> class with the specified execute delegate.
		/// </summary>
		/// <param name="execute">The <see cref="Action" /> to be called when the command is invoked.</param>
		public DelegateCommand(Action execute)
		{
			Check.ArgumentNull(execute, nameof(execute));

			ExecuteDelegate = execute;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateCommand" /> class with the specified execute and canExecute delegates.
		/// </summary>
		/// <param name="execute">The <see cref="Action" /> to be called when the command is invoked.</param>
		/// <param name="canExecute">The <see cref="Func{TResult}" /> that determines whether the command can execute in its current state.</param>
		public DelegateCommand(Action execute, Func<bool> canExecute) : this(execute)
		{
			CanExecuteDelegate = canExecute;
		}

		/// <summary>
		/// Executes the command. A call to <see cref="CanExecute" /> is performed to check if the command can be executed.
		/// </summary>
		public void Execute()
		{
			if (CanExecute()) ExecuteDelegate();
		}
		/// <summary>
		/// Cetermines whether the command can execute in its current state.
		/// </summary>
		/// <returns>
		/// <see langword="true" />, if the command can be executed;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool CanExecute()
		{
			return CanExecuteDelegate?.Invoke() ?? true;
		}
		void ICommand.Execute(object parameter)
		{
			Execute();
		}
		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute();
		}
	}

	/// <summary>
	/// Represents a parameterized command which uses specified delegates for <see cref="ICommand.Execute(object)" /> and <see cref="ICommand.CanExecute(object)" />.
	/// </summary>
	/// <typeparam name="TParameter">The type of the command parameter.</typeparam>
	public sealed class DelegateCommand<TParameter> : ICommand
	{
		private readonly Action<TParameter> ExecuteDelegate;
		private readonly Predicate<TParameter> CanExecuteDelegate;
		event EventHandler ICommand.CanExecuteChanged
		{
			add
			{
				if (CanExecuteDelegate != null) CommandManager.RequerySuggested += value;
			}
			remove
			{
				if (CanExecuteDelegate != null) CommandManager.RequerySuggested -= value;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateCommand{TParameter}" /> class with the specified execute delegate.
		/// </summary>
		/// <param name="execute">The <see cref="Action{T}" /> to be called when the command is invoked. The first argument of <paramref name="execute" /> is the command parameter.</param>
		public DelegateCommand(Action<TParameter> execute)
		{
			Check.ArgumentNull(execute, nameof(execute));

			ExecuteDelegate = execute;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateCommand{TParameter}" /> class with the specified execute and canExecute delegates.
		/// </summary>
		/// <param name="execute">The <see cref="Action{T}" /> to be called when the command is invoked. The first argument of <paramref name="execute" /> is the command parameter.</param>
		/// <param name="canExecute">The <see cref="Predicate{T}" /> that determines whether the command can execute in its current state. The first argument of <paramref name="canExecute" /> is the command parameter.</param>
		public DelegateCommand(Action<TParameter> execute, Predicate<TParameter> canExecute) : this(execute)
		{
			CanExecuteDelegate = canExecute;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateCommand{TParameter}" /> class with the specified execute and canExecute delegates.
		/// </summary>
		/// <param name="execute">The <see cref="Action{T}" /> to be called when the command is invoked. The first argument of <paramref name="execute" /> is the command parameter.</param>
		/// <param name="canExecute">The <see cref="Func{TResult}" /> that determines whether the command can execute in its current state.</param>
		public DelegateCommand(Action<TParameter> execute, Func<bool> canExecute) : this(execute, parameter => canExecute())
		{
		}

		/// <summary>
		/// Executes the command with the specified parameter. A call to <see cref="CanExecute(TParameter)" /> is performed to check if the command can be executed.
		/// </summary>
		/// <param name="parameter">The parameter which is passed to the execute and canExecute delegate.</param>
		public void Execute(TParameter parameter)
		{
			if (CanExecute(parameter)) ExecuteDelegate(parameter);
		}
		/// <summary>
		/// Cetermines whether the command can execute in its current state.
		/// </summary>
		/// <param name="parameter">The parameter which is passed to the canExecute delegate.</param>
		/// <returns>
		/// <see langword="true" />, if the command can be executed;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool CanExecute(TParameter parameter)
		{
			return CanExecuteDelegate?.Invoke(parameter) ?? true;
		}
		void ICommand.Execute(object parameter)
		{
			Execute(CSharp.CastOrDefault<TParameter>(parameter));
		}
		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute(CSharp.CastOrDefault<TParameter>(parameter));
		}
	}
}