using BytecodeApi.Extensions;
using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a text box control.
/// </summary>
public class UiTextBox : TextBox
{
    /// <summary>
    /// Identifies the <see cref="Watermark" /> dependency property. This field is read-only.
    /// </summary>
    public static readonly DependencyProperty WatermarkProperty = DependencyPropertyEx.Register(nameof(Watermark));
    /// <summary>
    /// Identifies the <see cref="AutoCompleteItems" /> dependency property. This field is read-only.
    /// </summary>
    public static readonly DependencyProperty AutoCompleteItemsProperty = DependencyPropertyEx.Register(nameof(AutoCompleteItems));
    /// <summary>
    /// Identifies the <see cref="AutoCompleteMaxItems" /> dependency property. This field is read-only.
    /// </summary>
    public static readonly DependencyProperty AutoCompleteMaxItemsProperty = DependencyPropertyEx.Register(nameof(AutoCompleteMaxItems), new(10));
    /// <summary>
	/// Identifies the <see cref="Submit" /> dependency property. This field is read-only.
    /// </summary>
    public static readonly RoutedEvent SubmitEvent = EventManager.RegisterRoutedEvent(nameof(Submit), RoutingStrategy.Bubble, typeof(EventHandler<RoutedEventArgs>), typeof(UiTextBox));
    /// <summary>
    /// Gets or sets an object that represents the watermark displayed when no text is entered.
    /// </summary>
    public object? Watermark
    {
        get => GetValue(WatermarkProperty);
        set => SetValue(WatermarkProperty, value);
    }
    /// <summary>
    /// Gets or sets a collection of <see cref="string" /> values used as the source for complete suggestions.
    /// </summary>
    public IEnumerable<string>? AutoCompleteItems
    {
        get => this.GetValue<IEnumerable<string>?>(AutoCompleteItemsProperty);
        set => SetValue(AutoCompleteItemsProperty, value);
    }
    /// <summary>
    /// Gets or sets the maximum number of auto complete suggestions to display.
    /// </summary>
    public int AutoCompleteMaxItems
    {
        get => this.GetValue<int>(AutoCompleteMaxItemsProperty);
        set => SetValue(AutoCompleteMaxItemsProperty, value);
    }
    /// <summary>
    /// Gets or sets the event that is raised when the user submits the text by pressing the return key.
    /// </summary>
    public event RoutedEventHandler Submit
    {
        add => AddHandler(SubmitEvent, value);
        remove => RemoveHandler(SubmitEvent, value);
    }
    private Popup? AutoCompletePopup;
    private ListBox? AutoCompleteListBox;

    static UiTextBox()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(UiTextBox), new FrameworkPropertyMetadata(typeof(UiTextBox)));
    }

    /// <summary>
    /// Applies the control template to this <see cref="UiTextBox" />.
    /// </summary>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (AutoCompleteListBox != null)
        {
            AutoCompleteListBox.PreviewMouseLeftButtonUp -= AutoCompleteListBox_PreviewMouseLeftButtonUp;
        }

        AutoCompletePopup = GetTemplateChild("PART_AutoCompletePopup") as Popup;
        AutoCompleteListBox = GetTemplateChild("PART_AutoCompleteListBox") as ListBox;

        if (AutoCompleteListBox != null)
        {
            AutoCompleteListBox.PreviewMouseLeftButtonUp += AutoCompleteListBox_PreviewMouseLeftButtonUp;
        }
    }
    protected override void OnGotFocus(RoutedEventArgs e)
    {
        base.OnGotFocus(e);

        if (AutoCompletePopup != null)
        {
            AutoCompletePopup.IsOpen = UpdateAutoCompleteItems();
        }
    }
    protected override void OnLostFocus(RoutedEventArgs e)
    {
        base.OnLostFocus(e);

        if (AutoCompletePopup != null && AutoCompleteListBox?.IsKeyboardFocusWithin == false)
        {
            AutoCompletePopup.IsOpen = false;
        }
    }
    protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
    {
        base.OnPreviewMouseDown(e);

        if (AutoCompletePopup != null &&
            e.OriginalSource is FrameworkElement originalSource &&
            originalSource.FindParent<TextBox>(UITreeType.Visual) != null)
        {
            if (AutoCompletePopup.IsOpen)
            {
                AutoCompletePopup.IsOpen = false;
            }
            else
            {
                AutoCompletePopup.IsOpen = UpdateAutoCompleteItems();
            }
        }
    }
    protected override void OnTextChanged(TextChangedEventArgs e)
    {
        base.OnTextChanged(e);

        if (AutoCompletePopup != null)
        {
            AutoCompletePopup.IsOpen = UpdateAutoCompleteItems();
        }
    }
    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
        base.OnPreviewKeyDown(e);

        if (e.Key == Key.Return && !IsReadOnly && AcceptsReturn && SelectionStart >= 0 && SelectionLength == 0)
        {
            // When Return is pressed, copy the indentation from the previous line (useful for code editors)
            string[] lines = Text[..SelectionStart].SplitToLines();
            string leadingWhiteSpaces = (lines.LastOrDefault() ?? "").TakeWhile(c => c is ' ' or '\t').AsString();

            if (leadingWhiteSpaces != "")
            {
                e.Handled = true;

                int newSelectionStart = SelectionStart + 2 + leadingWhiteSpaces.Length;
                Text = Text[..SelectionStart] + "\r\n" + leadingWhiteSpaces + Text[SelectionStart..];
                SelectionStart = newSelectionStart;
            }
        }
    }
    protected override void OnKeyUp(KeyEventArgs e)
    {
        base.OnKeyUp(e);

        if (e.Key == Key.Return)
        {
            if (AutoCompletePopup?.IsOpen == true)
            {
                // When auto complete popup is open, Return chooses the selected auto complete item.
                ApplySelectedAutoCompleteItem();
            }
            else
            {
                bool isCtrl = Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
                if (!AcceptsReturn || isCtrl)
                {
                    // Single-line TextBox: Return or Ctrl+Return
                    // Multi-line TextBox: Ctrl+Return
                    RaiseEvent(new(SubmitEvent, this));
                }
            }
        }
        else if (e.Key == Key.Escape)
        {
            if (AutoCompletePopup != null)
            {
                AutoCompletePopup.IsOpen = false;
            }
        }
        else if (e.Key is Key.Up or Key.Down)
        {
            if (AutoCompleteListBox?.ItemsSource is IEnumerable<string> itemsSource && itemsSource.Any())
            {
                int offset = e.Key == Key.Up ? -1 : 1;
                AutoCompleteListBox.SelectedIndex = Math.Clamp(AutoCompleteListBox.SelectedIndex + offset, 0, itemsSource.Count() - 1);
            }
        }
    }
    private void AutoCompleteListBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        ApplySelectedAutoCompleteItem();
    }

    private bool UpdateAutoCompleteItems()
    {
        if (AutoCompleteListBox != null)
        {
            string[]? items = AutoCompleteItems
                ?.Where(str => str?.Contains((Text ?? "").Trim(), StringComparison.OrdinalIgnoreCase) == true)
                .Where(str => !string.Equals(str ?? "", Text ?? "", StringComparison.OrdinalIgnoreCase))
                .Take(AutoCompleteMaxItems)
                .ToArray();

            AutoCompleteListBox.ItemsSource = items;
            return items?.Any() == true;
        }
        else
        {
            return false;
        }
    }
    private void ApplySelectedAutoCompleteItem()
    {
        if (AutoCompleteListBox?.SelectedItem != null)
        {
            Text = AutoCompleteListBox.SelectedItem.ToString();
        }
    }
}