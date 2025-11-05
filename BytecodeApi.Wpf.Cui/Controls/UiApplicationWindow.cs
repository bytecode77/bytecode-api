using BytecodeApi.Threading;
using BytecodeApi.Wpf.Controls;
using BytecodeApi.Wpf.Extensions;
using BytecodeApi.Wpf.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Shell;

namespace BytecodeApi.Wpf.Cui.Controls;

public class UiApplicationWindow : ObservableWindow
{
	private const int CaptionHeight = 37;
	private const int CaptionHeightWithMenu = 37;
	private const int CaptionHeightWithToolBar = 66;
	private const int CaptionHeightWithMenuAndToolBar = 66;
	public static readonly DependencyProperty IconControlProperty = DependencyPropertyEx.Register(nameof(IconControl));
	public static readonly DependencyProperty MenuProperty = DependencyPropertyEx.Register(nameof(Menu), new(Menu_Changed));
	public static readonly DependencyProperty ToolBarTrayProperty = DependencyPropertyEx.Register(nameof(ToolBarTray), new(ToolBar_Changed));
	public static readonly DependencyProperty StatusBarProperty = DependencyPropertyEx.Register(nameof(StatusBar));
	private static readonly DependencyPropertyKey ShowResizeGripPropertyKey = DependencyPropertyEx.RegisterReadOnly(nameof(ShowResizeGrip), new FrameworkPropertyMetadata(false));
	public static readonly DependencyProperty ShowResizeGripProperty = ShowResizeGripPropertyKey.DependencyProperty;
	public object? IconControl
	{
		get => GetValue(IconControlProperty);
		set => SetValue(IconControlProperty, value);
	}
	public Menu? Menu
	{
		get => this.GetValue<Menu?>(MenuProperty);
		set => SetValue(MenuProperty, value);
	}
	public ToolBarTray? ToolBarTray
	{
		get => this.GetValue<ToolBarTray?>(ToolBarTrayProperty);
		set => SetValue(ToolBarTrayProperty, value);
	}
	public StatusBar? StatusBar
	{
		get => this.GetValue<StatusBar?>(StatusBarProperty);
		set => SetValue(StatusBarProperty, value);
	}
	public bool ShowResizeGrip
	{
		get => this.GetValue<bool>(ShowResizeGripProperty);
		private set => SetValue(ShowResizeGripPropertyKey, value);
	}
	private ResizeGrip? ResizeGrip;
	private CriticalSection ResizeModeCriticalSection = new();

	static UiApplicationWindow()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiApplicationWindow), new FrameworkPropertyMetadata(typeof(UiApplicationWindow)));
		ResizeModeProperty.OverrideMetadata(typeof(UiApplicationWindow), new FrameworkPropertyMetadata(ResizeMode_Changed));
		BorderBrushProperty.OverrideMetadata(typeof(UiApplicationWindow), new FrameworkPropertyMetadata(BorderBrush_Changed));
	}
	public UiApplicationWindow()
	{
		CommandBindings.Add(new(SystemCommands.CloseWindowCommand, (sender, e) => SystemCommands.CloseWindow(this)));
		CommandBindings.Add(new(SystemCommands.MinimizeWindowCommand, (sender, e) => SystemCommands.MinimizeWindow(this)));
		CommandBindings.Add(new(SystemCommands.MaximizeWindowCommand, (sender, e) => SystemCommands.MaximizeWindow(this)));
		CommandBindings.Add(new(SystemCommands.RestoreWindowCommand, (sender, e) => SystemCommands.RestoreWindow(this)));

		UpdateWindowChrome();
	}

	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();

		ResizeGrip = GetTemplateChild("PART_ResizeGrip") as ResizeGrip;
	}
	protected override void OnSourceInitialized(EventArgs e)
	{
		base.OnSourceInitialized(e);

		UpdateBorder();

		if (ResizeGrip != null)
		{
			WindowChrome.SetResizeGripDirection(ResizeGrip, ResizeGripDirection.BottomRight);
		}
	}
	private static void Menu_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		UiApplicationWindow window = (UiApplicationWindow)dependencyObject;
		window.UpdateCaptionHeight();
	}
	private static void ToolBar_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		UiApplicationWindow window = (UiApplicationWindow)dependencyObject;
		window.UpdateCaptionHeight();
	}
	private static void ResizeMode_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		UiApplicationWindow window = (UiApplicationWindow)dependencyObject;

		if (!window.ResizeModeCriticalSection.IsRunning)
		{
			window.ShowResizeGrip = window.ResizeMode == ResizeMode.CanResizeWithGrip;
			window.UpdateWindowChrome();
		}
	}
	private static void BorderBrush_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		UiApplicationWindow window = (UiApplicationWindow)dependencyObject;
		window.UpdateBorder();
	}

	private void UpdateWindowChrome()
	{
		if (ShowResizeGrip)
		{
			// ResizeMode.CanResizeWithGrip cannot be used together with WindowChrome.
			ResizeModeCriticalSection.Invoke(() => ResizeMode = ResizeMode.CanResize);
		}

		WindowChrome.SetWindowChrome(this, new()
		{
			CaptionHeight = CaptionHeight - 6,
			ResizeBorderThickness = new(6),
			UseAeroCaptionButtons = false
		});

		UpdateCaptionHeight();
	}
	private void UpdateCaptionHeight()
	{
		WindowChrome? windowChrome = WindowChrome.GetWindowChrome(this);

		if (windowChrome != null)
		{
			windowChrome.CaptionHeight = (Menu, ToolBarTray) switch
			{
				(not null, null) => CaptionHeightWithMenu,
				(null, not null) => CaptionHeightWithToolBar,
				(not null, not null) => CaptionHeightWithMenuAndToolBar,
				(null, null) => CaptionHeight
			} - windowChrome.ResizeBorderThickness.Top;

			WindowChrome.SetWindowChrome(this, windowChrome);
		}
	}
	private void UpdateBorder()
	{
		if (BorderBrush is SolidColorBrush borderBrush)
		{
			WindowService.SetBorderBrush(this, borderBrush);
		}
	}
}