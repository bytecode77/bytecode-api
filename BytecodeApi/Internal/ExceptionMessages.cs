namespace BytecodeApi
{
	internal static class ExceptionMessages
	{
		public const string InvalidCast = "Specified cast is not valid. Parameter name: {0}";
		public const string DivideByZero = "Attempted to divide by zero. Parameter name: {0}";
		public const string DirectoryNotFound = "Could not find a part of the path '{0}'.";
		public const string UnsupportedType = "The type is not supported. Parameter name: {0}";
		public const string Win32 = "An unmanaged exception was raised.";

		public static class Argument
		{
			public const string StringNotEmpty = "String must not be empty.";
			public const string StringNotEmptyOrWhiteSpace = "String must not be empty or whitespace.";
			public const string OffsetAndLengthOutOfBounds = "Offset and length were out of bounds.";
			public const string ArrayElementsRequired = "Array must have elements.";
			public const string EnumerableElementsRequired = "Sequence contains no elements.";
			public const string ArrayValuesNotNull = "Array must not contain null values.";
			public const string ArrayValuesNotStringEmpty = "Array must not contain empty strings.";
			public const string InvalidHandle = "Invalid handle.";
		}
		public static class ArgumentOutOfRange
		{
			public const string GreaterEqual0 = "Non-negative number required.";
			public const string Greater0 = "Positive number required.";
			public const string Between0And1 = "Value must be in range of 0...1.";
			public const string GreaterEqualValue = "Value must be greater than or eqaul to {0}";
		}
		public static class Security
		{
			public const string WrongPassword = "Wrong password.";
		}
	}
}