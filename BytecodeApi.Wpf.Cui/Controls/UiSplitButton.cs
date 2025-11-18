using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiSplitButton : Button
{
	private static readonly DependencyPropertyKey IsDropDownOpenPropertyKey = DependencyPropertyEx.RegisterReadOnly(nameof(IsDropDownOpen), new FrameworkPropertyMetadata(false, IsDropDownOpen_Changed));
	private static readonly DependencyPropertyKey IsDropDownContextMenuPropertyKey = DependencyPropertyEx.RegisterReadOnly(nameof(IsDropDownContextMenu), new FrameworkPropertyMetadata(false));
	public static readonly DependencyProperty IsDropDownOpenProperty = IsDropDownOpenPropertyKey.DependencyProperty;
	public static readonly DependencyProperty DropDownContentProperty = DependencyPropertyEx.Register(nameof(DropDownContent), new(DropDownContent_Changed));
	public static readonly DependencyProperty IsDropDownContextMenuProperty = IsDropDownContextMenuPropertyKey.DependencyProperty;
	public static readonly DependencyProperty CloseOnClickProperty = DependencyPropertyEx.RegisterAttached<UiSplitButton, bool>("CloseOnClick", new FrameworkPropertyMetadata(CloseOnClick_Changed));
	public bool IsDropDownOpen
	{
		get => this.GetValue<bool>(IsDropDownOpenProperty);
		private set => SetValue(IsDropDownOpenPropertyKey, value);
	}
	public object? DropDownContent
	{
		get => GetValue(DropDownContentProperty);
		set => SetValue(DropDownContentProperty, value);
	}
	public bool IsDropDownContextMenu
	{
		get => this.GetValue<bool>(IsDropDownContextMenuProperty);
		private set => SetValue(IsDropDownContextMenuPropertyKey, value);
	}
	public static bool GetCloseOnClick(DependencyObject dependencyObject)
	{
		return dependencyObject.GetValue<bool>(CloseOnClickProperty);
	}
	public static void SetCloseOnClick(DependencyObject dependencyObject, bool value)
	{
		dependencyObject.SetValue(CloseOnClickProperty, value);
	}
	private Button? DropDownButton;
	private Popup? Popup;
	private Border? PopupBorder;
	private Window? Window;
	private bool SuppressNextClick;
	private static UiSplitButton? OpenInstance;

	static UiSplitButton()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiSplitButton), new FrameworkPropertyMetadata(typeof(UiSplitButton)));
		EventManager.RegisterClassHandler(typeof(UiSplitButton), ClickEvent, new RoutedEventHandler(UiSplitButton_Click), true);
	}

    /// <summary>
    /// Applies the control template to this <see cref="UiSplitButton" />.
    /// </summary>
    public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();

		if (DropDownButton != null)
		{
			DropDownButton.Click -= DropDownButton_Click;
		}

		if (Popup != null)
		{
			Popup.Closed -= Popup_Closed;
			Popup.PreviewMouseLeftButtonDown -= Popup_PreviewMouseLeftButtonDown;
		}

		if (PopupBorder != null)
		{
			PopupBorder.MouseLeftButtonDown -= PopupBorder_MouseLeftButtonDown;
		}

		DropDownButton = GetTemplateChild("PART_DropDownButton") as Button;
		Popup = GetTemplateChild("PART_Popup") as Popup;
		PopupBorder = GetTemplateChild("PART_PopupBorder") as Border;

		if (DropDownButton != null)
		{
			DropDownButton.Click += DropDownButton_Click;
		}

		if (Popup != null)
		{
			Popup.Closed += Popup_Closed;
			Popup.PreviewMouseLeftButtonDown += Popup_PreviewMouseLeftButtonDown;
		}

		if (PopupBorder != null)
		{
			PopupBorder.MouseLeftButtonDown += PopupBorder_MouseLeftButtonDown;
		}
	}
	protected override void OnClick()
	{
		if (SuppressNextClick)
		{
			SuppressNextClick = false;
			return;
		}

		base.OnClick();
	}
	private static void UiSplitButton_Click(object sender, RoutedEventArgs e)
	{
		if (e.OriginalSource != sender)
		{
			e.Handled = true;
		}
	}
	private void DropDownButton_Click(object sender, RoutedEventArgs e)
	{
		IsDropDownOpen = !IsDropDownOpen;
		SuppressNextClick = true;
		e.Handled = true;
	}
	protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
	{
		base.OnPreviewMouseLeftButtonDown(e);

		DependencyObject? src = e.OriginalSource as DependencyObject;
		bool inDropDownButton = DropDownButton != null && IsDescendantOf(DropDownButton, src);
		bool inSelf = IsDescendantOf(this, src);

		if (inSelf && !inDropDownButton)
		{
			SuppressNextClick = false;
		}

		static bool IsDescendantOf(DependencyObject root, DependencyObject? node)
		{
			while (node != null)
			{
				if (node == root)
				{
					return true;
				}

				node = VisualTreeHelper.GetParent(node);
			}

			return false;
		}
	}
	protected override void OnPreviewKeyDown(KeyEventArgs e)
	{
		base.OnPreviewKeyDown(e);

		if (e.Key == Key.Escape)
		{
			IsDropDownOpen = false;
			e.Handled = true;
		}
	}
	protected override void OnContextMenuOpening(ContextMenuEventArgs e)
	{
		if (IsDropDownContextMenu)
		{
			e.Handled = true;
			return;
		}

		base.OnContextMenuOpening(e);
	}
	private void Popup_Closed(object? sender, EventArgs e)
	{
		if (IsDropDownOpen)
		{
			IsDropDownOpen = false;
		}

		if (OpenInstance == this)
		{
			OpenInstance = null;
		}

		UnhookOutsideClick();
	}
	private void Popup_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		SuppressNextClick = true;
	}
	private void PopupBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		if (e.OriginalSource == sender)
		{
			SuppressNextClick = true;
			IsDropDownOpen = false;
			e.Handled = true;
		}
	}
	private void ContextMenu_Closed(object? sender, RoutedEventArgs e)
	{
		if (IsDropDownOpen)
		{
			IsDropDownOpen = false;
		}

		if (OpenInstance == this)
		{
			OpenInstance = null;
		}
	}
	private static void IsDropDownOpen_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		UiSplitButton splitButton = (UiSplitButton)dependencyObject;

		if (splitButton.IsDropDownContextMenu)
		{
			if (splitButton.ContextMenu != null)
			{
				if (splitButton.IsDropDownOpen)
				{
					OpenInstance = splitButton;
					splitButton.ContextMenu.MinWidth = Math.Max(splitButton.ContextMenu.MinWidth, splitButton.ActualWidth + 10);
					splitButton.ContextMenu.PlacementTarget = splitButton;
					splitButton.ContextMenu.Placement = PlacementMode.Bottom;
					splitButton.ContextMenu.IsOpen = true;
				}
				else
				{
					if (OpenInstance == splitButton)
					{
						OpenInstance = null;
					}

					splitButton.ContextMenu.IsOpen = false;
				}
			}
		}
		else
		{
			if (splitButton.Popup != null)
			{
				if (splitButton.IsDropDownOpen)
				{
					OpenInstance = splitButton;
					splitButton.Popup.IsOpen = true;
					splitButton.HookOutsideClick();
				}
				else
				{
					if (OpenInstance == splitButton)
					{
						OpenInstance = null;
					}

					splitButton.UnhookOutsideClick();
					splitButton.Popup.IsOpen = false;
				}
			}
		}
	}
	private static void DropDownContent_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		UiSplitButton splitButton = (UiSplitButton)dependencyObject;

		if (splitButton.ContextMenu != null)
		{
			splitButton.ContextMenu.Closed -= splitButton.ContextMenu_Closed;
			splitButton.ContextMenu = null;
		}

		splitButton.IsDropDownContextMenu = e.NewValue is ContextMenu;

		if (splitButton.IsDropDownContextMenu)
		{
			splitButton.ContextMenu = (ContextMenu)e.NewValue;
			splitButton.ContextMenu.Closed += splitButton.ContextMenu_Closed;
		}

		if (splitButton.IsDropDownOpen)
		{
			splitButton.IsDropDownOpen = false;
		}
	}
	private static void CloseOnClick_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is ButtonBase button)
		{
			if ((bool)e.NewValue)
			{
				button.Click += CloseOnClick_Click;
			}
			else
			{
				button.Click -= CloseOnClick_Click;
			}
		}
		else if (d is UIElement element)
		{
			if ((bool)e.NewValue)
			{
				element.MouseLeftButtonUp += CloseOnClick_MouseLeftButtonUp;
			}
			else
			{
				element.MouseLeftButtonUp -= CloseOnClick_MouseLeftButtonUp;
			}
		}
	}
	private static void CloseOnClick_Click(object? sender, RoutedEventArgs e)
	{
		if (OpenInstance != null)
		{
			OpenInstance.SuppressNextClick = true;
			OpenInstance.IsDropDownOpen = false;
		}
	}
	private static void CloseOnClick_MouseLeftButtonUp(object? sender, MouseButtonEventArgs e)
	{
		if (OpenInstance != null)
		{
			OpenInstance.SuppressNextClick = true;
			OpenInstance.IsDropDownOpen = false;
		}
	}

	private void HookOutsideClick()
	{
		Window = Window.GetWindow(this);
		if (Window != null)
		{
			Window.PreviewMouseDown += Window_PreviewMouseDown;
			Window.Deactivated += Window_Deactivated;
		}
	}
	private void UnhookOutsideClick()
	{
		if (Window != null)
		{
			Window.PreviewMouseDown -= Window_PreviewMouseDown;
			Window.Deactivated -= Window_Deactivated;
			Window = null;
		}
	}
	private void Window_PreviewMouseDown(object? sender, MouseButtonEventArgs e)
	{
		if (IsMouseOver)
		{
			// Click on button.
			return;
		}
		else if (Popup != null && Popup.IsMouseOver)
		{
			// Click on popup.
			return;
		}
		else
		{
			SuppressNextClick = true;
			IsDropDownOpen = false;
			e.Handled = true;
		}
	}
	private void Window_Deactivated(object? sender, EventArgs e)
	{
		IsDropDownOpen = false;
	}
}