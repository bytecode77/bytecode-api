using BytecodeApi.Extensions;
using BytecodeApi.Threading;
using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiPasswordBox : Control
{
	public static readonly DependencyProperty PasswordProperty = DependencyPropertyEx.Register(nameof(Password), new(Password_Changed));
	public static readonly DependencyProperty IsReadOnlyProperty = DependencyPropertyEx.Register(nameof(IsReadOnly));
	public static readonly DependencyProperty CanPreviewProperty = DependencyPropertyEx.Register(nameof(CanPreview), new(true));
	public static readonly DependencyProperty CanCopyProperty = DependencyPropertyEx.Register(nameof(CanCopy));
	public static readonly RoutedUICommand CopyPasswordCommand = new RoutedUICommand("Copy Password", "CopyPassword", typeof(UiPasswordBox), [new KeyGesture(Key.C, ModifierKeys.Control)]);
	public static readonly RoutedUICommand CutPasswordCommand = new RoutedUICommand("Cut Password", "CutPassword", typeof(UiPasswordBox), [new KeyGesture(Key.C, ModifierKeys.Control)]);
	private static readonly DependencyPropertyKey PreviewPasswordPropertyKey = DependencyPropertyEx.RegisterReadOnly(nameof(PreviewPassword), new FrameworkPropertyMetadata(false));
	public static readonly DependencyProperty PreviewPasswordProperty = PreviewPasswordPropertyKey.DependencyProperty;
	public string? Password
	{
		get => this.GetValue<string?>(PasswordProperty);
		set => SetValue(PasswordProperty, value);
	}
	public bool IsReadOnly
	{
		get => this.GetValue<bool>(IsReadOnlyProperty);
		set => SetValue(IsReadOnlyProperty, value);
	}
	public bool CanPreview
	{
		get => this.GetValue<bool>(CanPreviewProperty);
		set => SetValue(CanPreviewProperty, value);
	}
	public bool CanCopy
	{
		get => this.GetValue<bool>(CanCopyProperty);
		set => SetValue(CanCopyProperty, value);
	}
	public bool PreviewPassword
	{
		get => this.GetValue<bool>(PreviewPasswordProperty);
		private set => SetValue(PreviewPasswordPropertyKey, value);
	}
	private readonly CriticalSection PasswordChangeCriticalSection = new();
	private PasswordBox? PasswordBox;
	private Button? PreviewButton;

	static UiPasswordBox()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiPasswordBox), new FrameworkPropertyMetadata(typeof(UiPasswordBox)));
	}

	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();

		CommandBindings.Add(new CommandBinding(CopyPasswordCommand, OnCopyPasswordExecuted, OnCopyPasswordCanExecute));
		CommandBindings.Add(new CommandBinding(CutPasswordCommand, OnCutPasswordExecuted, OnCutPasswordCanExecute));

		if (PasswordBox != null)
		{
			PasswordBox.PasswordChanged -= PasswordBox_PasswordChanged;
		}

		if (PreviewButton != null)
		{
			PreviewButton.PreviewMouseLeftButtonDown -= PreviewButton_PreviewMouseLeftButtonDown;
			PreviewButton.PreviewMouseLeftButtonUp -= PreviewButton_PreviewMouseLeftButtonUp;
		}

		PasswordBox = GetTemplateChild("PART_PasswordBox") as PasswordBox;
		PreviewButton = GetTemplateChild("PART_PreviewButton") as Button;

		if (PasswordBox != null)
		{
			PasswordBox.Password = Password;
			PasswordBox.PasswordChanged += PasswordBox_PasswordChanged;
		}

		if (PreviewButton != null)
		{
			PreviewButton.PreviewMouseLeftButtonDown += PreviewButton_PreviewMouseLeftButtonDown;
			PreviewButton.PreviewMouseLeftButtonUp += PreviewButton_PreviewMouseLeftButtonUp;
		}
	}
	protected override void OnPreviewGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
	{
		base.OnPreviewGotKeyboardFocus(e);

		if (e.NewFocus == this)
		{
			if (PasswordBox != null)
			{
				e.Handled = true;

				Dispatcher.BeginInvoke(DispatcherPriority.Input, () =>
				{
					PasswordBox.Focus();
					PasswordBox.MoveCaretToEnd();
				});
			}
		}
	}

	private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
	{
		ArgumentNullException.ThrowIfNull(PasswordBox);

		PasswordChangeCriticalSection.Invoke(() =>
		{
			if (IsReadOnly)
			{
				// Workaround to setting PasswordBox.IsEnabled to false, because this would change the foreground color of the PasswordBox
				if (PasswordBox.Password != Password)
				{
					PasswordBox.Password = Password;
					PasswordBox.MoveCaretToEnd();
				}
			}
			else
			{
				Password = PasswordBox.Password;
			}
		});
	}
	private void PreviewButton_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		PreviewPassword = true;
	}
	private void PreviewButton_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		PreviewPassword = false;
	}
	private static void Password_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		UiPasswordBox passwordBox = (UiPasswordBox)dependencyObject;

		passwordBox.PasswordChangeCriticalSection.Invoke(() =>
		{
			if (passwordBox.PasswordBox != null)
			{
				passwordBox.PasswordBox.Password = passwordBox.Password;
			}
		});
	}
	private void OnCopyPasswordCanExecute(object sender, CanExecuteRoutedEventArgs e)
	{
		e.CanExecute = CanCopy && PasswordBox?.GetSelectionLength() > 0;
	}
	private void OnCopyPasswordExecuted(object sender, ExecutedRoutedEventArgs e)
	{
		if (PasswordBox != null && !Password.IsNullOrEmpty())
		{
			Clipboard.SetDataObject(Password.Substring(PasswordBox.GetSelectionStart(), PasswordBox.GetSelectionLength()));
		}
	}
	private void OnCutPasswordCanExecute(object sender, CanExecuteRoutedEventArgs e)
	{
		e.CanExecute = CanCopy && !IsReadOnly && PasswordBox?.GetSelectionLength() > 0;
	}
	private void OnCutPasswordExecuted(object sender, ExecutedRoutedEventArgs e)
	{
		if (PasswordBox != null && !Password.IsNullOrEmpty())
		{
			Clipboard.SetDataObject(Password.Substring(PasswordBox.GetSelectionStart(), PasswordBox.GetSelectionLength()));
			Password = Password[..PasswordBox.GetSelectionStart()] + Password[(PasswordBox.GetSelectionStart() + PasswordBox.GetSelectionLength())..];
		}
	}
}