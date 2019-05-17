using BytecodeApi.Extensions;
using BytecodeApi.Text;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace BytecodeApi.IO.Wmi
{
	/// <summary>
	/// Represents a WMI namespace.
	/// </summary>
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public class WmiNamespace
	{
		private string DebuggerDisplay => CSharp.DebuggerDisplay<WmiNamespace>("{0}", new QuotedString(FullName));
		/// <summary>
		/// Gets the name of the <see cref="WmiNamespace" />. The "ROOT" namespace name is represented as <see cref="string.Empty" />.
		/// </summary>
		public string Name { get; private set; }
		/// <summary>
		/// Gets the path of the <see cref="WmiNamespace" />, including all parent namespaces except "ROOT", separated by a backslash.
		/// </summary>
		public string Path { get; private set; }
		/// <summary>
		/// Gets the full path of the <see cref="WmiNamespace" />, including all parent namespaces, the "ROOT" element and the workstation name.
		/// <para>Example: \\WORKSTATION-NAME\ROOT\CIMV2</para>
		/// </summary>
		public string FullName => System.IO.Path.Combine(@"\\", Environment.MachineName, "ROOT", Path);
		/// <summary>
		/// Gets a collection containing all child namespaces. This property is <see langword="null" />, if the loadNamespaces parameter was set to <see langword="false" /> in the constructor.
		/// </summary>
		public ReadOnlyCollection<WmiNamespace> Namespaces { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="WmiNamespace" /> class from the specified path. The <paramref name="path" /> parameter starts after "\\WORKSTATION-NAME\ROOT\" and excludes "ROOT". Child namespaces are not loaded.
		/// <para>Example: CIMV2</para>
		/// </summary>
		/// <param name="path">The relative path of the <see cref="WmiNamespace" />, excluding workstation name and "ROOT".</param>
		public WmiNamespace(string path) : this(path, false)
		{
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="WmiNamespace" /> class from the specified path. The <paramref name="path" /> parameter starts after "\\WORKSTATION-NAME\ROOT\" and excludes "ROOT".
		/// <para>Example: CIMV2</para>
		/// </summary>
		/// <param name="path">The relative path of the <see cref="WmiNamespace" />, excluding workstation name and "ROOT".</param>
		/// <param name="loadNamespaces"><see langword="true" /> to load child namespaces recursively.</param>
		public WmiNamespace(string path, bool loadNamespaces) : this(path, loadNamespaces, true)
		{
		}
		internal WmiNamespace(string path, bool loadNamespaces, bool checkIfExists)
		{
			Path = path ?? "";
			Name = System.IO.Path.GetFileName(Path);

			if (loadNamespaces)
			{
				try
				{
					using (ManagementClass managementClass = GetManagementClass())
					using (ManagementObjectCollection objectCollection = managementClass.GetInstances())
					{
						Namespaces = objectCollection
							.Cast<ManagementObject>()
							.Select(item => new WmiNamespace(System.IO.Path.Combine(Path, item["Name"].ToString()), true))
							.ToReadOnlyCollection();
					}
				}
				catch
				{
					throw CreateManagementException();
				}
			}
			else if (checkIfExists)
			{
				using (ManagementClass managementClass = GetManagementClass())
				{
					try
					{
						using (managementClass.GetInstances())
						{
						}
					}
					catch
					{
						throw CreateManagementException();
					}
				}
			}

			ManagementClass GetManagementClass()
			{
				return new ManagementClass(new ManagementScope(FullName), new ManagementPath("__namespace"), null);
			}
			Exception CreateManagementException()
			{
				return Throw.Management((Name == "" ? "Root namespace" : "Namespace '" + Name + "'") + " could not be retrieved.");
			}
		}
		/// <summary>
		/// Retrieves all WMI namespaces, starting from ROOT\ recursively. This is equivalent to <see langword="new" /> <see cref="WmiNamespace" />(<see langword="null" />).
		/// </summary>
		/// <returns>
		/// A <see cref="WmiNamespace" /> that is equivalent to <see langword="new" /> <see cref="WmiNamespace" />(<see langword="null" />).
		/// </returns>
		public static WmiNamespace GetRoot()
		{
			return new WmiNamespace(null, true);
		}

		/// <summary>
		/// Retrieves all classes of this <see cref="WmiNamespace" />.
		/// </summary>
		/// <returns>
		/// A new <see cref="WmiClass" />[] containing all classes of this <see cref="WmiNamespace" />.
		/// </returns>
		public WmiClass[] GetClasses()
		{
			using (ManagementObjectSearcher objectSearcher = new ManagementObjectSearcher(new ManagementScope(FullName), new WqlObjectQuery("SELECT * FROM meta_class")))
			using (ManagementObjectCollection objectCollection = objectSearcher.Get())
			{
				return objectCollection
					.Cast<ManagementObject>()
					.Select(item => item.ToString().SubstringFrom(":"))
					.Select(className => new WmiClass(this, className, false))
					.ToArray();
			}
		}
		/// <summary>
		/// Finds a class of this <see cref="WmiNamespace" /> by the specified name. An exception is thrown, if the class could not be created.
		/// </summary>
		/// <returns>
		/// A new <see cref="WmiClass" /> created from the specified name. An exception is thrown, if the class could not be created.
		/// </returns>
		public WmiClass GetClass(string name)
		{
			return GetClass(name, true);
		}
		internal WmiClass GetClass(string name, bool checkIfExists)
		{
			Check.ArgumentNull(name, nameof(name));
			Check.ArgumentEx.StringNotEmpty(name, nameof(name));

			return new WmiClass(this, name, checkIfExists);
		}

		/// <summary>
		/// Returns the name of this <see cref="WmiNamespace" />.
		/// </summary>
		/// <returns>
		/// The name of this <see cref="WmiNamespace" />.
		/// </returns>
		public override string ToString()
		{
			return Name;
		}
	}
}