using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace BytecodeApi.IO.Wmi
{
	/// <summary>
	/// Represents a WMI class.
	/// </summary>
	[DebuggerDisplay(CSharp.DebuggerDisplayString)]
	public sealed class WmiClass
	{
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string DebuggerDisplay => CSharp.DebuggerDisplay<WmiClass>("Name = {0}, Namespace = {1}", new QuotedString(Name), new QuotedString(Namespace.FullName));
		/// <summary>
		/// Gets the <see cref="WmiNamespace" /> that this <see cref="WmiClass" /> was created from.
		/// </summary>
		public WmiNamespace Namespace { get; private set; }
		/// <summary>
		/// Gets the name of the <see cref="WmiClass" />.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="WmiClass" /> class with the specified <see cref="WmiNamespace" /> and the specified name.
		/// </summary>
		/// <param name="wmiNamespace">The WMI namespace of this class, in accordance to the <paramref name="name" /> parameter.</param>
		/// <param name="name">The name of the <see cref="WmiClass" />.</param>
		public WmiClass(WmiNamespace wmiNamespace, string name) : this(wmiNamespace, name, true)
		{
		}
		internal WmiClass(WmiNamespace wmiNamespace, string name, bool checkIfExists)
		{
			Check.ArgumentNull(wmiNamespace, nameof(wmiNamespace));
			Check.ArgumentNull(name, nameof(name));
			Check.ArgumentEx.StringNotEmpty(name, nameof(name));

			Namespace = wmiNamespace;
			Name = name;

			if (checkIfExists && !CSharp.Try(() => GetManagementClass().ClassPath != null))
			{
				throw Throw.Management("Class '" + name + "' could not be retrieved.");
			}
		}

		/// <summary>
		/// Retrieves all objects of this <see cref="WmiClass" /> including all columns.
		/// </summary>
		/// <returns>
		/// A new <see cref="WmiObject" />[] containing all objects of this <see cref="WmiClass" /> including all columns.
		/// </returns>
		public WmiObject[] GetObjects()
		{
			return GetObjects(null);
		}
		/// <summary>
		/// Retrieves all objects of this <see cref="WmiClass" /> including the specified columns. If <paramref name="columns" /> is <see langword="null" />, all columns are retrieved.
		/// </summary>
		/// <param name="columns">A <see cref="string" />[] specifying the columns to select. If <see langword="null" />, all columns are retrieved.</param>
		/// <returns>
		/// A new <see cref="WmiObject" />[] containing all objects of this <see cref="WmiClass" /> including the specified columns.
		/// </returns>
		public WmiObject[] GetObjects(params string[] columns)
		{
			return GetObjects(columns, null);
		}
		/// <summary>
		/// Retrieves objects of this <see cref="WmiClass" /> including the specified columns using a WHERE condition. If <paramref name="columns" /> is <see langword="null" />, all columns are retrieved. If <paramref name="where" /> is <see langword="null" /> or empty, no filter is applied.
		/// </summary>
		/// <param name="columns">A <see cref="string" />[] specifying the columns to select. If <see langword="null" />, all columns are retrieved.</param>
		/// <param name="where">A <see cref="string" /> specifying a WMI query that is appended after a "WHERE". If <see langword="null" /> or empty, no filter is applied.</param>
		/// <returns>
		/// A new <see cref="WmiObject" />[] containing objects of this <see cref="WmiClass" /> including the specified columns using the specified WHERE condition.
		/// </returns>
		public WmiObject[] GetObjects(string[] columns, string where)
		{
			string columnsString = columns.IsNullOrEmpty() ? "*" : columns.AsString(", ");
			string whereString = where.IsNullOrEmpty() ? null : " WHERE " + where;

			using ManagementObjectSearcher searcher = new ManagementObjectSearcher(Namespace.FullName, "SELECT " + columnsString + " FROM " + Name + whereString);

			return searcher
				.Get()
				.Cast<ManagementObject>()
				.Select(item => new WmiObject
				(
					this,
					item.Properties
						.Cast<PropertyData>()
						.OrderBy(property => property.Name)
						.Select(property => new WmiProperty(property.Name, property.Value))
						.ToArray()
				))
				.ToArray();
		}
		/// <summary>
		/// Invokes a method on this <see cref="WmiClass" />.
		/// </summary>
		/// <param name="methodName">The name of the method to invoke.</param>
		/// <param name="args">An array containing parameter values.</param>
		/// <returns>
		/// The <see cref="object" /> value returned by the method.
		/// </returns>
		public object InvokeMethod(string methodName, params object[] args)
		{
			Check.ArgumentNull(methodName, nameof(methodName));

			return GetManagementClass().InvokeMethod(methodName, args);
		}
		/// <summary>
		/// Invokes a method on this <see cref="WmiClass" />.
		/// </summary>
		/// <param name="methodName">The name of the method to invoke.</param>
		/// <param name="args">An array containing parameter values.</param>
		/// <typeparam name="T">The type of the returned value.</typeparam>
		/// <returns>
		/// The value returned by the method, casted to <typeparamref name="T" />.
		/// </returns>
		public T InvokeMethod<T>(string methodName, params object[] args)
		{
			return (T)InvokeMethod(methodName, args);
		}
		private ManagementClass GetManagementClass()
		{
			return new ManagementClass(new ManagementScope(Namespace.FullName), new ManagementPath(Name), null);
		}

		/// <summary>
		/// Returns the name of this <see cref="WmiClass" />.
		/// </summary>
		/// <returns>
		/// The name of this <see cref="WmiClass" />.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}
	}
}