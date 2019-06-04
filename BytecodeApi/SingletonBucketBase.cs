using System;
using System.Collections.Generic;
using System.Windows;

namespace BytecodeApi
{
	/// <summary>
	/// Represents the base class for singleton buckets. Derived classes can specify <see langword="static" /> properties that use the <see cref="Get{T}()" /> and <see cref="Set{T}(T)" /> methods. Each class that inherits <see cref="SingletonBucketBase{TBucket}" /> has its own scope for singleton objects, defined by <typeparamref name="TBucket" />. Each property must be of a different type. This is an abstract class.
	/// <para>Example class: <see langword="public" /> <see langword="class" /> AppSingletons : SingletonBucketBase&lt;AppSingletons&gt;</para>
	/// <para>Example property: <see langword="public" /> <see langword="static" /> <see cref="Window" /> MainWindow { <see langword="get" /> => Get&lt;Window&gt;(); <see langword="set" /> => Set(value); }</para>
	/// </summary>
	/// <typeparam name="TBucket">The class type that defines the scope for the derived class. This type parameter should be the derived class itself.</typeparam>
	public abstract class SingletonBucketBase<TBucket> where TBucket : SingletonBucketBase<TBucket>
	{
		private static readonly Dictionary<Tuple<Type, Type>, object> Singletons = new Dictionary<Tuple<Type, Type>, object>();

		/// <summary>
		/// Initializes a new instance of the <see cref="SingletonBucketBase{TBucket}" /> class.
		/// </summary>
		protected SingletonBucketBase()
		{
		}

		/// <summary>
		/// Gets a value indicating whether the singleton <see cref="object" /> of the specified type exists.
		/// </summary>
		/// <typeparam name="T">The type that identifies the class of the singleton <see cref="object" />.</typeparam>
		/// <returns>
		/// <see langword="true" />, if the singleton <see cref="object" /> of the specified type exists;
		/// otherwise, <see langword="false" />.
		/// </returns>
		protected static bool Exists<T>()
		{
			lock (Singletons)
			{
				return Singletons.ContainsKey(GetKey<T>());
			}
		}
		/// <summary>
		/// Gets the singleton <see cref="object" /> of the specified type. If not found, an exception is thrown.
		/// </summary>
		/// <typeparam name="T">The type that identifies the class of the singleton <see cref="object" />.</typeparam>
		/// <returns>
		/// The singleton <see cref="object" /> of the specified type. If not found, an exception is thrown.
		/// </returns>
		protected static T Get<T>()
		{
			return Get<T>(false);
		}
		/// <summary>
		/// Gets the singleton <see cref="object" /> of the specified type. If not found, an exception is thrown. <paramref name="create" /> can be set to <see langword="true" /> to create an instance, if it does not yet exist.
		/// </summary>
		/// <typeparam name="T">The type that identifies the class of the singleton <see cref="object" />.</typeparam>
		/// <returns>
		/// The singleton <see cref="object" /> of the specified type. If not found and <paramref name="create" /> is <see langword="false" />, an exception is thrown.
		/// </returns>
		protected static T Get<T>(bool create)
		{
			Tuple<Type, Type> key = GetKey<T>();

			lock (Singletons)
			{
				if (Singletons.ContainsKey(key))
				{
					return (T)Singletons[key];
				}
				else if (create)
				{
					Singletons[key] = Activator.CreateInstance<T>();
					return (T)Singletons[key];
				}
				else
				{
					throw Throw.InvalidOperation("Singleton was not set.");
				}
			}
		}
		/// <summary>
		/// Sets the singleton <see cref="object" /> of the specified type. If it was already set, an exception is thrown.
		/// </summary>
		/// <typeparam name="T">The type that identifies the class of the singleton <see cref="object" />.</typeparam>
		/// <param name="obj">The <see cref="object" /> that represents the singleton of <typeparamref name="T" />.</param>
		protected static void Set<T>(T obj)
		{
			Set(obj, false);
		}
		/// <summary>
		/// Sets the singleton <see cref="object" /> of the specified type. If it was already set, an exception is thrown. <paramref name="overwrite" /> can be set to <see langword="true" /> to allow the singleton <see cref="object" /> to be mutable.
		/// </summary>
		/// <typeparam name="T">The type that identifies the class of the singleton <see cref="object" />.</typeparam>
		/// <param name="obj">The <see cref="object" /> that represents the singleton of <typeparamref name="T" />.</param>
		/// <param name="overwrite"><see langword="true" /> to allow the singleton <see cref="object" /> to be mutable; <see langword="false" /> to throw an exception, if the singleton <see cref="object" /> was already set.</param>
		protected static void Set<T>(T obj, bool overwrite)
		{
			Tuple<Type, Type> key = GetKey<T>();

			lock (Singletons)
			{
				if (!overwrite && Singletons.ContainsKey(key))
				{
					throw Throw.InvalidOperation("Singleton was already set.");
				}
				else
				{
					if (obj == null) Singletons.Remove(key); else Singletons[key] = obj;
				}
			}
		}

		private static Tuple<Type, Type> GetKey<T>()
		{
			return Tuple.Create(typeof(TBucket), typeof(T));
		}
	}
}