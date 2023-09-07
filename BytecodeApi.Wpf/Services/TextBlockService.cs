using BytecodeApi.Wpf.Extensions;
using System.Windows;
using System.Windows.Controls;

namespace BytecodeApi.Wpf.Services;

/// <summary>
/// Helper class for the WPF <see cref="TextBlock" /> control.
/// </summary>
public static class TextBlockService
{
	/// <summary>
	/// Identifies the <see cref="TextBlockService" />.TextTrimming dependency property. This field is read-only.
	/// </summary>
	public static readonly DependencyProperty TextTrimmingProperty = DependencyPropertyEx.RegisterAttached<ContentControl, TextTrimming>("TextTrimming", new FrameworkPropertyMetadata(default(TextTrimming), FrameworkPropertyMetadataOptions.Inherits, TextTrimming_Changed));
	/// <summary>
	/// Returns the <see cref="TextBlock.TextTrimming" /> property.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="TextBlock" /> to check.</param>
	/// <returns>
	/// The <see cref="TextBlock.TextTrimming" /> property.
	/// </returns>
	public static TextTrimming GetTextTrimming(DependencyObject dependencyObject)
	{
		Check.ArgumentNull(dependencyObject);

		return dependencyObject.GetValue<TextTrimming>(TextTrimmingProperty);
	}
	/// <summary>
	/// Sets the <see cref="TextBlock.TextTrimming" /> property.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="TextBlock" /> to modify.</param>
	/// <param name="value">A <see cref="TextTrimming" /> value for the <see cref="TextBlock" />.</param>
	public static void SetTextTrimming(DependencyObject dependencyObject, TextTrimming value)
	{
		Check.ArgumentNull(dependencyObject);

		dependencyObject.SetValue(TextTrimmingProperty, value);
	}

	private static void TextTrimming_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
	{
		if (dependencyObject is TextBlock textBlock)
		{
			textBlock.TextTrimming = (TextTrimming)e.NewValue;
		}
	}
}