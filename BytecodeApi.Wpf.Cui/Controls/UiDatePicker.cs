using BytecodeApi.Extensions;
using BytecodeApi.Threading;
using BytecodeApi.Wpf.Extensions;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a date picker control.
/// </summary>
public class UiDatePicker : DatePicker
{
	/// <summary>
	/// Identifies the <see cref="Watermark" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty WatermarkProperty = DependencyProperty.Register(nameof(Watermark));
	/// <summary>
	/// Identifies the <see cref="ShowResetButton" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty ShowResetButtonProperty = DependencyProperty.Register(nameof(ShowResetButton));
	/// <summary>
	/// Gets or sets an object that represents the watermark displayed when no date is selected.
	/// </summary>
	public object? Watermark
	{
		get => GetValue(WatermarkProperty);
		set => SetValue(WatermarkProperty, value);
	}
	/// <summary>
	/// Gets or sets a <see cref="bool" /> value that indicates whether a reset button is shown to clear the selected date.
	/// </summary>
	public bool ShowResetButton
	{
		get => this.GetValue<bool>(ShowResetButtonProperty);
		set => SetValue(ShowResetButtonProperty, value);
	}
	private readonly CriticalSection FocusCriticalSection = new();
	private TextBox? TextBox;
	private Popup? Popup;
	private Button? ResetButton;

	static UiDatePicker()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiDatePicker), new FrameworkPropertyMetadata(typeof(UiDatePicker)));
	}

	/// <summary>
	/// Applies the control template to this <see cref="UiDatePicker" />.
	/// </summary>
	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();

		TextBox?.PreviewMouseLeftButtonUp -= TextBox_PreviewMouseLeftButtonUp;
		TextBox?.PreviewKeyDown -= TextBox_PreviewKeyDown;
		ResetButton?.Click -= ResetButton_Click;

		TextBox? datePickerTextBox = GetTemplateChild("PART_TextBox") as TextBox;
		datePickerTextBox?.ApplyTemplate();

		TextBox = datePickerTextBox?.Template?.FindName("Part_InnerTextBox", datePickerTextBox) as TextBox;
		Popup = GetTemplateChild("PART_Popup") as Popup;
		ResetButton = GetTemplateChild("PART_ResetButton") as Button;

		TextBox?.PreviewMouseLeftButtonUp += TextBox_PreviewMouseLeftButtonUp;
		TextBox?.PreviewKeyDown += TextBox_PreviewKeyDown;
		ResetButton?.Click += ResetButton_Click;
	}
	/// <summary>
	/// Invoked when an unhandled <see cref="Keyboard.GotKeyboardFocusEvent" /> attached event reaches an element in its route that is derived from this class.
	/// </summary>
	/// <param name="e">A <see cref="KeyboardFocusChangedEventArgs" /> that contains the event data.</param>
	protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
	{
		if (e.OriginalSource is UiDatePicker or DatePickerTextBox)
		{
			FocusCriticalSection.Invoke(() =>
			{
				if (TextBox?.Focus() != true)
				{
					base.OnGotKeyboardFocus(e);
				}
			});
		}
	}
	/// <summary>
	/// Raises the <see cref="DatePicker.CalendarOpened" /> routed event.
	/// </summary>
	/// <param name="e">A <see cref="RoutedEventArgs" /> that contains the event data.</param>
	protected override void OnCalendarOpened(RoutedEventArgs e)
	{
		base.OnCalendarOpened(e);

		if (Popup?.Child is Calendar calendar)
		{
			HookTodayButton(calendar);
		}
	}
	private void Calendar_Loaded(object sender, RoutedEventArgs e)
	{
		if (sender is Calendar cal)
		{
			cal.Loaded -= Calendar_Loaded;
			HookTodayButton(cal);
		}
	}
	private void TextBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		if (TextBox != null && TextBox.SelectionLength == 0 && Regex.IsMatch(TextBox.Text, @"[0-9]+\.[0-9]+\.[0-9]+"))
		{
			int start = TextBox.Text[..TextBox.SelectionStart].LastIndexOf('.') + 1;
			int end = TextBox.Text.IndexOf('.', TextBox.SelectionStart);

			if (end == -1)
			{
				end = TextBox.Text.Length;
			}

			TextBox.SelectionStart = start;
			TextBox.SelectionLength = end - start;
		}
	}
	private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (TextBox != null)
		{
			int direction = e.Key switch
			{
				Key.Up or Key.Subtract or Key.OemMinus => -1,
				Key.Down or Key.Add or Key.OemPlus => 1,
				_ => 0
			};

			if (direction != 0 && TextBox.Text.ToDateTime("dd.MM.yyyy") != null)
			{
				int selectionStart = TextBox.SelectionStart;

				if (selectionStart < 3)
				{
					SelectedDate = SelectedDate?.AddDays(direction);
				}
				else if (selectionStart < 6)
				{
					SelectedDate = SelectedDate?.AddMonths(direction);
				}
				else
				{
					SelectedDate = SelectedDate?.AddYears(direction);
				}

				TextBox.SelectionStart = selectionStart;
				e.Handled = true;
			}
		}
	}
	private void TodayButton_Click(object sender, RoutedEventArgs e)
	{
		SelectedDate = DateTime.Today;
		IsDropDownOpen = false;
	}
	private void ResetButton_Click(object sender, RoutedEventArgs e)
	{
		SelectedDate = null;
	}

	private void HookTodayButton(Calendar calendar)
	{
		calendar.ApplyTemplate();

		if (calendar.Template.FindName("PART_CalendarItem", calendar) is CalendarItem calendarItem)
		{
			calendarItem.ApplyTemplate();

			if (calendarItem.Template.FindName("PART_TodayButton", calendarItem) is Button todayButton)
			{
				todayButton.Click -= TodayButton_Click;
				todayButton.Click += TodayButton_Click;
			}
		}
		else
		{
			calendar.Loaded -= Calendar_Loaded;
			calendar.Loaded += Calendar_Loaded;
		}
	}
}