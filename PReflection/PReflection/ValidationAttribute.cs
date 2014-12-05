using System;

namespace PReflection
{
	public class ValidationAttribute : Attribute
	{
		public abstract string Validate(object value);{

		public ValidationAttribute ()
		{
		}
	}
}

