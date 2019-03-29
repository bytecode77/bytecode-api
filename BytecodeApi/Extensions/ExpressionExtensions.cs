using System;
using System.Linq.Expressions;
using System.Reflection;

namespace BytecodeApi.Extensions
{
	/// <summary>
	/// Provides a set of <see langword="static" /> methods for interaction with <see cref="Expression" /> objects.
	/// </summary>
	public static class ExpressionExtensions
	{
		/// <summary>
		/// Returns the member name of this <see cref="Expression{TDelegate}" />. If this instance does not resolve to a <see cref="MemberExpression" />, <see langword="null" /> is returned.
		/// </summary>
		/// <typeparam name="T">The member type.</typeparam>
		/// <param name="member">The member to retrieve its name from.</param>
		/// <returns>
		/// The member name of this <see cref="Expression{TDelegate}" />, or <see langword="null" />, if this instance does not resolve to a <see cref="MemberExpression" />.
		/// </returns>
		public static string GetMemberName<T>(this Expression<Func<T>> member)
		{
			Check.ArgumentNull(member, nameof(member));

			return (member.Body as MemberExpression)?.Member.Name;
		}
		/// <summary>
		/// Retrieves the value of the member of this <see cref="Expression{TDelegate}" />.
		/// </summary>
		/// <typeparam name="T">The member type.</typeparam>
		/// <param name="member">The member to retrieve its value from.</param>
		/// <returns>
		/// The value of the member of this <see cref="Expression{TDelegate}" />.
		/// </returns>
		public static T GetMemberValue<T>(this Expression<Func<T>> member)
		{
			Check.ArgumentNull(member, nameof(member));

			return (T)Expression.Lambda<Func<object>>(Expression.Convert((MemberExpression)member.Body, typeof(object))).Compile()();
		}
		/// <summary>
		/// Sets the value of the member of this <see cref="Expression{TDelegate}" />.
		/// </summary>
		/// <typeparam name="T">The member type.</typeparam>
		/// <param name="member">The member from which to update the value.</param>
		/// <param name="obj">The class instance of <paramref name="member" />.</param>
		/// <param name="value">The new value to be assigned to <paramref name="member" />.</param>
		public static void SetMemberValue<T>(this Expression<Func<T>> member, object obj, T value)
		{
			//IMPORTANT: Omit "obj" parameter; get automatically
			MemberInfo memberInfo = (member.Body as MemberExpression)?.Member;

			if (memberInfo is PropertyInfo property) property.SetValue(obj, value);
			else if (memberInfo is FieldInfo field) field.SetValue(obj, value);
			else throw Throw.UnsupportedType(nameof(member));
		}
	}
}