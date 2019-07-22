using BytecodeApi.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BytecodeApi.FileFormats.Ini
{
	/// <summary>
	/// Represents a collection of INI file sections.
	/// </summary>
	public sealed class IniSectionCollection : ICollection<IniSection>
	{
		private readonly List<IniSection> Sections;
		/// <summary>
		/// Gets the <see cref="IniSection" /> at the specified index.
		/// </summary>
		/// <param name="index">The index at which to retrieve the <see cref="IniSection" />.</param>
		public IniSection this[int index]
		{
			get
			{
				Check.IndexOutOfRange(index, Count);
				return Sections[index];
			}
		}
		/// <summary>
		/// Gets the <see cref="IniSection" /> with the specified name.
		/// </summary>
		/// <param name="name">A <see cref="string" /> specifying the name of the section.</param>
		public IniSection this[string name] => this[name, false];
		/// <summary>
		/// Gets the <see cref="IniSection" /> with the specified name.
		/// </summary>
		/// <param name="name">A <see cref="string" /> specifying the name of the section.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during name comparison.</param>
		public IniSection this[string name, bool ignoreCase]
		{
			get
			{
				Check.ArgumentNull(name, nameof(name));
				Check.KeyNotFoundException(HasSection(name, ignoreCase), "The section '" + name + "' was not found.");
				return Sections.First(section => section.Name.Equals(name, ignoreCase ? SpecialStringComparisons.IgnoreCase : SpecialStringComparisons.None));
			}
		}
		/// <summary>
		/// Gets the number of elements contained in the <see cref="IniSectionCollection" />.
		/// </summary>
		public int Count => Sections.Count;
		/// <summary>
		/// Gets a value indicating whether the <see cref="IniSectionCollection" /> is read-only.
		/// </summary>
		public bool IsReadOnly => false;

		/// <summary>
		/// Initializes a new instance of the <see cref="IniSectionCollection" /> class.
		/// </summary>
		public IniSectionCollection()
		{
			Sections = new List<IniSection>();
		}

		/// <summary>
		/// Determines whether a section with the specified name exists in this collection.
		/// </summary>
		/// <param name="name">The name of the section to check.</param>
		/// <returns>
		/// <see langword="true" />, if the section with the specified name exists;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool HasSection(string name)
		{
			return HasSection(name, false);
		}
		/// <summary>
		/// Determines whether a section with the specified name exists in this collection.
		/// </summary>
		/// <param name="name">The name of the section to check.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during comparison.</param>
		/// <returns>
		/// <see langword="true" />, if the section with the specified name exists;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool HasSection(string name, bool ignoreCase)
		{
			return Sections.Any(section => section.Name.Equals(name, ignoreCase ? SpecialStringComparisons.IgnoreCase : SpecialStringComparisons.None));
		}
		/// <summary>
		/// Processes all duplicate sections within this collection according to the specified behavior.
		/// </summary>
		/// <param name="behavior">An <see cref="IniDuplicateSectionNameBehavior" /> object specifying how duplicates are processed.</param>
		public void ProcessDuplicates(IniDuplicateSectionNameBehavior behavior)
		{
			ProcessDuplicates(behavior, false);
		}
		/// <summary>
		/// Processes all duplicate sections within this collection according to the specified behavior.
		/// </summary>
		/// <param name="behavior">An <see cref="IniDuplicateSectionNameBehavior" /> object specifying how duplicates are processed.</param>
		/// <param name="ignoreCase"><see langword="true" /> to ignore character casing during name comparison.</param>
		public void ProcessDuplicates(IniDuplicateSectionNameBehavior behavior, bool ignoreCase)
		{
			List<IniSection> removedSections = new List<IniSection>();

			switch (behavior)
			{
				case IniDuplicateSectionNameBehavior.Abort:
					if (Sections.DistinctBy(section => ignoreCase ? section.Name.ToLower() : section.Name).Count() != Count)
					{
						throw Throw.Format("Duplicate section found.");
					}
					break;
				case IniDuplicateSectionNameBehavior.Ignore:
					for (int i = 1; i < Count; i++)
					{
						if (Sections.Take(i).Any(section => section.Name.Equals(Sections[i].Name, ignoreCase ? SpecialStringComparisons.IgnoreCase : SpecialStringComparisons.None)))
						{
							removedSections.Add(Sections[i]);
						}
					}
					break;
				case IniDuplicateSectionNameBehavior.Merge:
					for (int i = 1; i < Count; i++)
					{
						IniSection firstSection = Sections.Take(i).FirstOrDefault(section => section.Name.Equals(Sections[i].Name, ignoreCase ? SpecialStringComparisons.IgnoreCase : SpecialStringComparisons.None));
						if (firstSection != null)
						{
							firstSection.Properties.AddRange(Sections[i].Properties);
							removedSections.Add(Sections[i]);
						}
					}
					break;
				case IniDuplicateSectionNameBehavior.Duplicate:
					break;
				default:
					throw Throw.InvalidEnumArgument(nameof(behavior), behavior);
			}

			Sections.RemoveRange(removedSections);
		}

		/// <summary>
		/// Adds an <see cref="IniSection" /> to the end of the <see cref="IniSectionCollection" />.
		/// </summary>
		/// <param name="item">The <see cref="IniSection" /> to be added to the end of the <see cref="IniSectionCollection" />.</param>
		public void Add(IniSection item)
		{
			Check.ArgumentNull(item, nameof(item));

			Sections.Add(item);
		}
		/// <summary>
		/// Removes the first occurrence of a specific <see cref="IniSection" /> from the <see cref="IniSectionCollection" />.
		/// </summary>
		/// <param name="item">The <see cref="IniSection" /> to remove from the <see cref="IniSectionCollection" />.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="item" /> is successfully removed;
		/// otherwise, <see langword="false" />.
		/// This method also returns <see langword="false" />, if <paramref name="item" /> was not found in the <see cref="IniSectionCollection" />.</returns>
		public bool Remove(IniSection item)
		{
			return Sections.Remove(item);
		}
		/// <summary>
		/// Removes all elements from the <see cref="IniSectionCollection" />.
		/// </summary>
		public void Clear()
		{
			Sections.Clear();
		}
		/// <summary>
		/// Determines whether an element is in the <see cref="IniSectionCollection" />.
		/// </summary>
		/// <param name="item">The <see cref="IniSection" /> to locate in the <see cref="IniSectionCollection" />.</param>
		/// <returns>
		/// <see langword="true" />, if <paramref name="item" /> is found in the <see cref="IniSectionCollection" />;
		/// otherwise, <see langword="false" />.
		/// </returns>
		public bool Contains(IniSection item)
		{
			return Sections.Contains(item);
		}
		void ICollection<IniSection>.CopyTo(IniSection[] array, int arrayIndex)
		{
			Check.ArgumentNull(array, nameof(array));
			Check.IndexOutOfRange(arrayIndex, array.Length - Count + 1);

			Sections.CopyTo(array, arrayIndex);
		}
		/// <summary>
		/// Returns an enumerator that iterates through the <see cref="IniSectionCollection" />.
		/// </summary>
		/// <returns>
		/// An enumerator that can be used to iterate through the <see cref="IniSectionCollection" />.
		/// </returns>
		public IEnumerator<IniSection> GetEnumerator()
		{
			return Sections.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return Sections.GetEnumerator();
		}
	}
}