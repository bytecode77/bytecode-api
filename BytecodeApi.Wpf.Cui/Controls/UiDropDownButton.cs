using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// This control represents a button that displays either a drop-down menu or popup when clicked.
/// </summary>
public class UiDropDownButton : Button
{
    private static readonly DependencyPropertyKey IsDropDownOpenPropertyKey = DependencyPropertyEx.RegisterReadOnly(nameof(IsDropDownOpen), new FrameworkPropertyMetadata(false, IsDropDownOpen_Changed));
    private static readonly DependencyPropertyKey IsDropDownContextMenuPropertyKey = DependencyPropertyEx.RegisterReadOnly(nameof(IsDropDownContextMenu), new FrameworkPropertyMetadata(false));
    /// <summary>
    /// Identifies the <see cref="IsDropDownOpen" /> dependency property. This field is read-only.
    /// </summary>
	public static readonly DependencyProperty IsDropDownOpenProperty = IsDropDownOpenPropertyKey.DependencyProperty;
    /// <summary>
    /// Identifies the <see cref="DropDownContent" /> dependency property. This field is read-only.
    /// </summary>
	public static readonly DependencyProperty DropDownContentProperty = DependencyPropertyEx.Register(nameof(DropDownContent), new(DropDownContent_Changed));
    /// <summary>
    /// Identifies the <see cref="IsDropDownContextMenu" /> dependency property. This field is read-only.
    /// </summary>
    public static readonly DependencyProperty IsDropDownContextMenuProperty = IsDropDownContextMenuPropertyKey.DependencyProperty;
    /// <summary>
    /// Identifies the <see cref="UiDropDownButton" />.CloseOnClick dependency property. This field is read-only.
    /// </summary>
	public static readonly DependencyProperty CloseOnClickProperty = DependencyPropertyEx.RegisterAttached<UiDropDownButton, bool>("CloseOnClick", new FrameworkPropertyMetadata(CloseOnClick_Changed));
    /// <summary>
    /// Gets a <see cref="bool" /> value indicating whether the drop-down is currently open.
    /// </summary>
    public bool IsDropDownOpen
    {
        get => this.GetValue<bool>(IsDropDownOpenProperty);
        private set => SetValue(IsDropDownOpenPropertyKey, value);
    }
    /// <summary>
    /// Gets or sets the content to be displayed in the drop-down. This can be a <see cref="ContextMenu" />, or a <see cref="UIElement" />, which will be displayed in a <see cref="System.Windows.Controls.Primitives.Popup" />.
    /// </summary>
    public object? DropDownContent
    {
        get => GetValue(DropDownContentProperty);
        set => SetValue(DropDownContentProperty, value);
    }
    /// <summary>
    /// Gets a <see cref="bool" /> value indicating whether the drop-down content is a <see cref="ContextMenu" />.
    /// </summary>
    public bool IsDropDownContextMenu
    {
        get => this.GetValue<bool>(IsDropDownContextMenuProperty);
        private set => SetValue(IsDropDownContextMenuPropertyKey, value);
    }
    /// <summary>
    /// Returns <see langword="true" />, if the specified <see cref="DependencyObject" /> will close the currently open <see cref="UiDropDownButton" /> when clicked; otherwise, <see langword="false" />.
    /// </summary>
    /// <param name="dependencyObject">The <see cref="DependencyObject" /> to check.</param>
    /// <returns>
    /// <see langword="true" />, if the specified <see cref="DependencyObject" /> will close the currently open <see cref="UiDropDownButton" /> when clicked;
    /// otherwise, <see langword="false" />.
    /// </returns>
    public static bool GetCloseOnClick(DependencyObject dependencyObject)
    {
        return dependencyObject.GetValue<bool>(CloseOnClickProperty);
    }
    /// <summary>
    /// Sets a <see cref="bool" /> value indicating whether the specified <see cref="DependencyObject" /> will close the currently open <see cref="UiDropDownButton" /> when clicked.
    /// </summary>
    /// <param name="dependencyObject">The <see cref="DependencyObject" /> to apply this property to.</param>
    /// <param name="value"><see langword="true" />, if the specified <see cref="DependencyObject" /> will close the currently open <see cref="UiDropDownButton" /> when clicked; otherwise, <see langword="false" />.</param>
    public static void SetCloseOnClick(DependencyObject dependencyObject, bool value)
    {
        dependencyObject.SetValue(CloseOnClickProperty, value);
    }
    private Popup? Popup;
    private Window? Window;
    private static UiDropDownButton? OpenInstance;

    static UiDropDownButton()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(UiDropDownButton), new FrameworkPropertyMetadata(typeof(UiDropDownButton)));
    }

    /// <summary>
    /// Applies the control template to this <see cref="UiDropDownButton" />.
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (Popup != null)
        {
            Popup.Closed -= Popup_Closed;
        }

        Popup = GetTemplateChild("PART_Popup") as Popup;

        if (Popup != null)
        {
            Popup.Closed += Popup_Closed;
        }
    }
    protected override void OnClick()
    {
        IsDropDownOpen = !IsDropDownOpen;

        base.OnClick();
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
        UiDropDownButton dropDownButton = (UiDropDownButton)dependencyObject;

        if (dropDownButton.IsDropDownContextMenu)
        {
            if (dropDownButton.ContextMenu != null)
            {
                if (dropDownButton.IsDropDownOpen)
                {
                    OpenInstance = dropDownButton;
                    dropDownButton.ContextMenu.MinWidth = Math.Max(dropDownButton.ContextMenu.MinWidth, dropDownButton.ActualWidth + 10);
                    dropDownButton.ContextMenu.PlacementTarget = dropDownButton;
                    dropDownButton.ContextMenu.Placement = PlacementMode.Bottom;
                    dropDownButton.ContextMenu.IsOpen = true;
                }
                else
                {
                    if (OpenInstance == dropDownButton)
                    {
                        OpenInstance = null;
                    }

                    dropDownButton.ContextMenu.IsOpen = false;
                }
            }
        }
        else
        {
            if (dropDownButton.Popup != null)
            {
                if (dropDownButton.IsDropDownOpen)
                {
                    OpenInstance = dropDownButton;
                    dropDownButton.Popup.IsOpen = true;
                    dropDownButton.HookOutsideClick();
                }
                else
                {
                    if (OpenInstance == dropDownButton)
                    {
                        OpenInstance = null;
                    }

                    dropDownButton.UnhookOutsideClick();
                    dropDownButton.Popup.IsOpen = false;
                }
            }
        }
    }
    private static void DropDownContent_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        UiDropDownButton dropDownButton = (UiDropDownButton)dependencyObject;

        if (dropDownButton.ContextMenu != null)
        {
            dropDownButton.ContextMenu.Closed -= dropDownButton.ContextMenu_Closed;
            dropDownButton.ContextMenu = null;
        }

        dropDownButton.IsDropDownContextMenu = e.NewValue is ContextMenu;

        if (dropDownButton.IsDropDownContextMenu)
        {
            dropDownButton.ContextMenu = (ContextMenu)e.NewValue;
            dropDownButton.ContextMenu.Closed += dropDownButton.ContextMenu_Closed;
        }

        if (dropDownButton.IsDropDownOpen)
        {
            dropDownButton.IsDropDownOpen = false;
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
            OpenInstance.IsDropDownOpen = false;
        }
    }
    private static void CloseOnClick_MouseLeftButtonUp(object? sender, MouseButtonEventArgs e)
    {
        if (OpenInstance != null)
        {
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
            IsDropDownOpen = false;
        }
    }
    private void Window_Deactivated(object? sender, EventArgs e)
    {
        IsDropDownOpen = false;
    }
}