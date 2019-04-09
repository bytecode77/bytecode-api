using BytecodeApi.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BytecodeApi
{
	/// <summary>
	/// Represents the base class for singleton buckets. Inherited classes can specify properties that use the <see cref="Get{T}" /> and <see cref="Set{T}(T)" /> methods. Each class that inherits <see cref="SingletonBucketBase" /> has its own scope for singleton objects.
	/// </summary>
	public abstract class SingletonBucketBase
	{
		private static readonly Dictionary<Tuple<Type, Type>, object> Singletons = new Dictionary<Tuple<Type, Type>, object>();

		/// <summary>
		/// Gets the singleton <see cref="object" /> of the specified type. If not found, an exception is thrown.
		/// </summary>
		/// <typeparam name="T">The type that identifies the class of the singleton <see cref="object" />.</typeparam>
		/// <returns>
		/// The singleton <see cref="object" /> of the specified type. If not found, an exception is thrown.
		/// </returns>
		protected static T Get<T>()
		{
			Tuple<Type, Type> key = Tuple.Create(GetInheritedType(), typeof(T));

			lock (Singletons)
			{
				if (Singletons.ContainsKey(key))
				{
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
			Tuple<Type, Type> key = Tuple.Create(GetInheritedType(), typeof(T));

			lock (Singletons)
			{
				if (!overwrite && Singletons.ContainsKey(key))
				{
					throw Throw.InvalidOperation("Singleton was already set.");
				}
				else
				{
					Singletons[key] = obj;
				}
			}
		}

		private static Type GetInheritedType()
		{
			return new StackTrace()
				.GetFrames()
				.FirstOrDefault(frame => typeof(SingletonBucketBase).IsAssignableFrom(frame.GetMethod().DeclaringType, true))
				?.GetMethod()
				.DeclaringType ?? throw Throw.InvalidOperation("Could not determine class type.");
		}
	}
}