using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BytecodeApi.Wpf.Cui.Controls;

/// <summary>
/// Represents a property grid group containing <see cref="UiPropertyGridItem" /> children.
/// </summary>
public class UiPropertyGridGroup : ItemsControl
{
	/// <summary>
	/// Identifies the <see cref="Header" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty HeaderProperty = DependencyPropertyEx.Register(nameof(Header));
	/// <summary>
	/// Identifies the <see cref="IsExpanded" /> dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty IsExpandedProperty = DependencyPropertyEx.Register(nameof(IsExpanded), new(true));
	/// <summary>
	/// Gets or sets the header for the property grid group.
	/// </summary>
	public object? Header
	{
		get => GetValue(HeaderProperty);
		set => SetValue(HeaderProperty, value);
	}
	/// <summary>
	/// Gets or sets a <see cref="bool" /> value indicating whether the property grid group is expanded.
	/// </summary>
	public bool IsExpanded
	{
		get => this.GetValue<bool>(IsExpandedProperty);
		set => SetValue(IsExpandedProperty, value);
	}
	private Button? HeaderButton;

	static UiPropertyGridGroup()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(UiPropertyGridGroup), new FrameworkPropertyMetadata(typeof(UiPropertyGridGroup)));
	}

	/// <summary>
	/// Applies the control template to this <see cref="UiPropertyGridGroup" />.
	/// </summary>
	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();

		if (HeaderButton != null)
		{
			HeaderButton.MouseDoubleClick -= HeaderButton_MouseDoubleClick;
		}

		HeaderButton = GetTemplateChild("PART_HeaderButton") as Button;

		if (HeaderButton != null)
		{
			HeaderButton.MouseDoubleClick += HeaderButton_MouseDoubleClick;
		}
	}

	private void HeaderButton_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		IsExpanded = !IsExpanded;
	}
}