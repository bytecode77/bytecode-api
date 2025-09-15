using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BytecodeApi.Wpf.Extensions;

/// <summary>
/// Provides a set of <see langword="static" /> methods for interaction with <see cref="DependencyObject" /> objects.
/// </summary>
public static class DependencyObjectExtensions
{
	/// <summary>
	/// Returns the current effective value of a <see cref="DependencyProperty" /> on this instance of a <see cref="DependencyObject" />.
	/// </summary>
	/// <typeparam name="T">The return type of the <see cref="DependencyProperty" />.</typeparam>
	/// <param name="dependencyObject">The <see cref="DependencyObject" /> to retrieve the value from.</param>
	/// <param name="dependencyProperty">The <see cref="DependencyProperty" /> identifier of the property to retrieve the value for.</param>
	/// <returns>
	/// The effective value of <paramref name="dependencyProperty" /> on this instance of a <see cref="DependencyObject" />.
	/// </returns>
	public static T GetValue<T>(this DependencyObject dependencyObject, DependencyProperty dependencyProperty)
	{
		Check.ArgumentNull(dependencyObject);
		Check.ArgumentNull(dependencyProperty);

		return (T)dependencyObject.GetValue(dependencyProperty);
	}
	/// <summary>
	/// Returns an array with all parents of this <see cref="DependencyObject" /> matching the specified type by traversing either the visual or the logical tree.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="DependencyObject" /> to traverse the tree from.</param>
	/// <param name="treeType">A <see cref="UITreeType" /> value indicating whether to use the <see cref="LogicalTreeHelper" /> or the <see cref="VisualTreeHelper" />.</param>
	/// <returns>
	/// An array with all parents of this <see cref="DependencyObject" />, depending on <paramref name="treeType" />.
	/// </returns>
	public static DependencyObject[] GetParents(this DependencyObject dependencyObject, UITreeType treeType)
	{
		Check.ArgumentNull(dependencyObject);

		List<DependencyObject> result = [];

		for (DependencyObject? obj = dependencyObject; obj != null;)
		{
			DependencyObject? parent = treeType switch
			{
				UITreeType.Logical => LogicalTreeHelper.GetParent(obj),
				UITreeType.Visual => VisualTreeHelper.GetParent(obj),
				_ => throw Throw.InvalidEnumArgument(nameof(treeType), treeType)
			};

			if (parent != null)
			{
				result.Add(parent);
			}

			obj = parent;
		}

		return result.ToArray();
	}
	/// <summary>
	/// Tries to find the closest parent of this <see cref="DependencyObject" /> matching the specified type by traversing either the visual or the logical tree.
	/// </summary>
	/// <typeparam name="T">The explicit type of the parent to search for.</typeparam>
	/// <param name="dependencyObject">The <see cref="DependencyObject" /> to traverse the tree from.</param>
	/// <param name="treeType">A <see cref="UITreeType" /> value indicating whether to use the <see cref="LogicalTreeHelper" /> or the <see cref="VisualTreeHelper" />.</param>
	/// <returns>
	/// The closest visual or logical parent of this <see cref="DependencyObject" />, depending on <paramref name="treeType" />, that is of type <typeparamref name="T" />, if found;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static T? FindParent<T>(this DependencyObject dependencyObject, UITreeType treeType) where T : DependencyObject
	{
		return dependencyObject.FindParent<T>(treeType, null);
	}
	/// <summary>
	/// Tries to find the closest parent of this <see cref="DependencyObject" /> matching the specified type and satisfying a specified condition by traversing either the visual or the logical tree.
	/// </summary>
	/// <typeparam name="T">The explicit type of the parent to search for.</typeparam>
	/// <param name="dependencyObject">The <see cref="DependencyObject" /> to traverse the tree from.</param>
	/// <param name="treeType">A <see cref="UITreeType" /> value indicating whether to use the <see cref="LogicalTreeHelper" /> or the <see cref="VisualTreeHelper" />.</param>
	/// <param name="predicate">The <see cref="Func{T, TResult}" /> that determines whether the parent of the specified type is returned.</param>
	/// <returns>
	/// The closest visual or logical parent of this <see cref="DependencyObject" />, depending on <paramref name="treeType" /> and <paramref name="predicate" />, that is of type <typeparamref name="T" />, if found;
	/// otherwise, <see langword="null" />.
	/// </returns>
	public static T? FindParent<T>(this DependencyObject dependencyObject, UITreeType treeType, Func<T, bool>? predicate) where T : DependencyObject
	{
		Check.ArgumentNull(dependencyObject);

		if (dependencyObject is Visual)
		{
			DependencyObject parent = treeType switch
			{
				UITreeType.Logical => LogicalTreeHelper.GetParent(dependencyObject),
				UITreeType.Visual => VisualTreeHelper.GetParent(dependencyObject),
				_ => throw Throw.InvalidEnumArgument(nameof(treeType), treeType)
			};

			if (parent is T visualParent && predicate?.Invoke(visualParent) != false)
			{
				return visualParent;
			}
			else
			{
				return parent?.FindParent(treeType, predicate);
			}
		}
		else
		{
			return null;
		}
	}
	/// <summary>
	/// Finds all children of this <see cref="DependencyObject" /> matching the specified type by traversing either the visual or the logical tree recursively. If no children have been found, an empty array is returned.
	/// </summary>
	/// <typeparam name="T">The explicit type of the children to search for.</typeparam>
	/// <param name="dependencyObject">The <see cref="DependencyObject" /> to traverse the tree recursively.</param>
	/// <param name="treeType">A <see cref="UITreeType" /> value indicating whether to use the <see cref="LogicalTreeHelper" /> or the <see cref="VisualTreeHelper" />.</param>
	/// <returns>
	/// An array of the specified type with all children, depending on <paramref name="treeType" />, that can be casted to <typeparamref name="T" />. If no children have been found, an empty array is returned.
	/// </returns>
	public static T[] FindChildren<T>(this DependencyObject dependencyObject, UITreeType treeType) where T : DependencyObject
	{
		return dependencyObject.FindChildren<T>(treeType, null);
	}
	/// <summary>
	/// Finds all children of this <see cref="DependencyObject" /> matching the specified type and satisfying a specified condition by traversing either the visual or the logical tree recursively. If no children have been found, an empty array is returned.
	/// </summary>
	/// <typeparam name="T">The explicit type of the children to search for.</typeparam>
	/// <param name="dependencyObject">The <see cref="DependencyObject" /> to traverse the tree recursively.</param>
	/// <param name="treeType">A <see cref="UITreeType" /> value indicating whether to use the <see cref="LogicalTreeHelper" /> or the <see cref="VisualTreeHelper" />.</param>
	/// <param name="predicate">The <see cref="Func{T, TResult}" /> that determines whether the child is included in the result.</param>
	/// <returns>
	/// An array of the specified type with all children, depending on <paramref name="treeType" /> and <paramref name="predicate" />, that can be casted to <typeparamref name="T" />. If no children have been found, an empty array is returned.
	/// </returns>
	public static T[] FindChildren<T>(this DependencyObject dependencyObject, UITreeType treeType, Func<T, bool>? predicate) where T : DependencyObject
	{
		Check.ArgumentNull(dependencyObject);

		List<T> result = [];

		IEnumerable<DependencyObject> children = treeType switch
		{
			UITreeType.Logical => LogicalTreeHelper.GetChildren(dependencyObject).OfType<DependencyObject>(),
			UITreeType.Visual => Create.Enumerable(VisualTreeHelper.GetChildrenCount(dependencyObject), i => VisualTreeHelper.GetChild(dependencyObject, i)),
			_ => throw Throw.InvalidEnumArgument(nameof(treeType), treeType)
		};

		foreach (DependencyObject child in children)
		{
			if (child is T convertedChild && predicate?.Invoke(convertedChild) != false)
			{
				result.Add(convertedChild);
			}

			result.AddRange(child.FindChildren(treeType, predicate));
		}

		return result.ToArray();
	}
	/// <summary>
	/// Finds the first child of this <see cref="DependencyObject" /> matching the specified type by traversing either the visual or the logical tree recursively. If no child was found, <see langword="null" /> is returned.
	/// </summary>
	/// <typeparam name="T">The explicit type of the children to search for.</typeparam>
	/// <param name="dependencyObject">The <see cref="DependencyObject" /> to traverse the tree recursively.</param>
	/// <param name="treeType">A <see cref="UITreeType" /> value indicating whether to use the <see cref="LogicalTreeHelper" /> or the <see cref="VisualTreeHelper" />.</param>
	/// <returns>
	/// The first child of the specified type, depending on <paramref name="treeType" />, that can be casted to <typeparamref name="T" />. If no child was found, <see langword="null" /> is returned.
	/// </returns>
	public static T? FindChild<T>(this DependencyObject dependencyObject, UITreeType treeType) where T : DependencyObject
	{
		return dependencyObject.FindChild<T>(treeType, null);
	}
	/// <summary>
	/// Finds the first child of this <see cref="DependencyObject" /> matching the specified type and satisfying a specified condition by traversing either the visual or the logical tree recursively. If no child was found, <see langword="null" /> is returned.
	/// </summary>
	/// <typeparam name="T">The explicit type of the children to search for.</typeparam>
	/// <param name="dependencyObject">The <see cref="DependencyObject" /> to traverse the tree recursively.</param>
	/// <param name="treeType">A <see cref="UITreeType" /> value indicating whether to use the <see cref="LogicalTreeHelper" /> or the <see cref="VisualTreeHelper" />.</param>
	/// <param name="predicate">The <see cref="Func{T, TResult}" /> that determines whether the child is included in the result.</param>
	/// <returns>
	/// The first child of the specified type that satisfies a specified condition, depending on <paramref name="treeType" />, that can be casted to <typeparamref name="T" />. If no child was found, <see langword="null" /> is returned.
	/// </returns>
	public static T? FindChild<T>(this DependencyObject dependencyObject, UITreeType treeType, Func<T, bool>? predicate) where T : DependencyObject
	{
		Check.ArgumentNull(dependencyObject);

		IEnumerable<DependencyObject> children = treeType switch
		{
			UITreeType.Logical => LogicalTreeHelper.GetChildren(dependencyObject).OfType<DependencyObject>(),
			UITreeType.Visual => Create.Enumerable(VisualTreeHelper.GetChildrenCount(dependencyObject), i => VisualTreeHelper.GetChild(dependencyObject, i)),
			_ => throw Throw.InvalidEnumArgument(nameof(treeType), treeType)
		};

		foreach (DependencyObject child in children)
		{
			if (child is T convertedChild && predicate?.Invoke(convertedChild) != false)
			{
				return convertedChild;
			}
			else if (child.FindChild(treeType, predicate) is T convertedSubChild)
			{
				return convertedSubChild;
			}
		}

		return null;
	}
	/// <summary>
	/// Validates this <see cref="DependencyObject" /> and all of its children and returns <see langword="true" />, if validation succeeded.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="DependencyObject" /> to validate.</param>
	/// <returns>
	/// <see langword="true" />, if validation succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool Validate(this DependencyObject dependencyObject)
	{
		return Validate(dependencyObject, true);
	}
	/// <summary>
	/// Validates this <see cref="DependencyObject" /> and returns <see langword="true" />, if validation succeeded. If <paramref name="validateChildren" /> is set to <see langword="true" />, logical children are validated recursively.
	/// </summary>
	/// <param name="dependencyObject">The <see cref="DependencyObject" /> to validate.</param>
	/// <param name="validateChildren"><see langword="true" /> to validate children recursively in addition to this <see cref="DependencyObject" />.</param>
	/// <returns>
	/// <see langword="true" />, if validation succeeded;
	/// otherwise, <see langword="false" />.
	/// </returns>
	public static bool Validate(this DependencyObject dependencyObject, bool validateChildren)
	{
		Check.ArgumentNull(dependencyObject);

		return !Validation.GetHasError(dependencyObject) && (!validateChildren || dependencyObject.FindChildren<DependencyObject>(UITreeType.Logical).All(Validate));
	}
}