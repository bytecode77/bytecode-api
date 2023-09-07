using System.Windows;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="UIElement" /> objects.
/// </summary>
public static class UIElementExtensions
{
	/// <summary>
	/// Returns the converted <see cref="Visibility" /> of this <see cref="UIElement" />.
	/// </summary>
	/// <param name="uiElement">The <see cref="UIElement" /> to be tested.</param>
	/// <returns>
	/// <see langword="true" />, if the <see cref="UIElement.Visibility" /> property of this <see cref="UIElement" /> is equal to <see cref="Visibility.Visible" />;
	/// <see langword="false" />, if <see cref="UIElement.Visibility" /> is equal to <see cref="Visibility.Collapsed" />;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static bool? GetVisibility(this UIElement uiElement)
	{
		Check.ArgumentNull(uiElement);

		return uiElement.Visibility.ToBoolean();
	}
	/// <summary>
	/// Sets the <see cref="Visibility" /> of this <see cref="UIElement" />.
	/// </summary>
	/// <param name="uiElement">The <see cref="UIElement" /> to be changed.</param>
	/// <param name="visible"><see langword="true" /> to set <see cref="UIElement.Visibility" /> to <see cref="Visibility.Visible" />; <see langword="false" /> to use <see cref="Visibility.Collapsed" />.</param>
	public static void SetVisibility(this UIElement uiElement, bool visible)
	{
		uiElement.SetVisibility(visible, false);
	}
	/// <summary>
	/// Sets the <see cref="Visibility" /> of this <see cref="UIElement" />.
	/// </summary>
	/// <param name="uiElement">The <see cref="UIElement" /> to be changed.</param>
	/// <param name="visible"><see langword="true" /> to set <see cref="UIElement.Visibility" /> to <see cref="Visibility.Visible" />; <see langword="false" /> to use <see cref="Visibility.Collapsed" />, or <see cref="Visibility.Hidden" /> according to the <paramref name="preserveSpace" /> parameter.</param>
	/// <param name="preserveSpace"><see langword="true" /> to use <see cref="Visibility.Hidden" />; <see langword="false" /> to use <see cref="Visibility.Collapsed" />. Only applies if <paramref name="visible" /> is <see langword="false" />.</param>
	public static void SetVisibility(this UIElement uiElement, bool visible, bool preserveSpace)
	{
		Check.ArgumentNull(uiElement);

		uiElement.Visibility = visible.ToVisibility(preserveSpace);
	}
	/// <summary>
	/// Sets the <see cref="UIElement.Visibility" /> property of this <see cref="UIElement" /> to <see cref="Visibility.Visible" />.
	/// </summary>
	/// <param name="uiElement">The <see cref="UIElement" /> to be changed.</param>
	public static void Show(this UIElement uiElement)
	{
		Check.ArgumentNull(uiElement);

		uiElement.SetVisibility(true);
	}
	/// <summary>
	/// Sets the <see cref="UIElement.Visibility" /> property of this <see cref="UIElement" /> to <see cref="Visibility.Collapsed" />.
	/// </summary>
	/// <param name="uiElement">The <see cref="UIElement" /> to be changed.</param>
	public static void Hide(this UIElement uiElement)
	{
		uiElement.Hide(false);
	}
	/// <summary>
	/// Sets the <see cref="UIElement.Visibility" /> property of this <see cref="UIElement" /> to <see cref="Visibility.Collapsed" /> or <see cref="Visibility.Hidden" /> based on the <paramref name="preserveSpace" /> parameter.
	/// </summary>
	/// <param name="uiElement">The <see cref="UIElement" /> to be changed.</param>
	/// <param name="preserveSpace"><see langword="true" /> to use <see cref="Visibility.Hidden" />; <see langword="false" /> to use <see cref="Visibility.Collapsed" />.</param>
	public static void Hide(this UIElement uiElement, bool preserveSpace)
	{
		Check.ArgumentNull(uiElement);

		uiElement.SetVisibility(false, preserveSpace);
	}
}