using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace BytecodeApi.UI.Extensions
{
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
			Check.ArgumentNull(dependencyObject, nameof(dependencyObject));
			Check.ArgumentNull(dependencyProperty, nameof(dependencyProperty));

			return (T)dependencyObject.GetValue(dependencyProperty);
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
		public static T FindParent<T>(this DependencyObject dependencyObject, UITreeType treeType) where T : DependencyObject
		{
			Check.ArgumentNull(dependencyObject, nameof(dependencyObject));

			if (dependencyObject is Visual)
			{
				DependencyObject parent;
				switch (treeType)
				{
					case UITreeType.Logical:
						parent = LogicalTreeHelper.GetParent(dependencyObject);
						break;
					case UITreeType.Visual:
						parent = VisualTreeHelper.GetParent(dependencyObject);
						break;
					default:
						throw Throw.InvalidEnumArgument(nameof(treeType));
				}

				return parent is T visualParent ? visualParent : parent?.FindParent<T>(treeType);
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
			Check.ArgumentNull(dependencyObject, nameof(dependencyObject));

			List<T> children = new List<T>();
			IEnumerable<DependencyObject> subChildren;

			switch (treeType)
			{
				case UITreeType.Logical:
					subChildren = LogicalTreeHelper.GetChildren(dependencyObject).OfType<DependencyObject>();
					break;
				case UITreeType.Visual:
					subChildren = Create.Enumerable(VisualTreeHelper.GetChildrenCount(dependencyObject), i => VisualTreeHelper.GetChild(dependencyObject, i));
					break;
				default:
					throw Throw.InvalidEnumArgument(nameof(treeType));
			}

			foreach (DependencyObject child in subChildren)
			{
				if (child is T convertedChild) children.Add(convertedChild);
				children.AddRange(child.FindChildren<T>(treeType));
			}

			return children.ToArray();
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
			Check.ArgumentNull(dependencyObject, nameof(dependencyObject));

			return !Validation.GetHasError(dependencyObject) && (!validateChildren || dependencyObject.FindChildren<DependencyObject>(UITreeType.Logical).All(Validate));
		}
	}
}