using System;

namespace PReflection
{
	public static class Validator
	{
		public ErrorInfo[] Validate(object obj){
			List<ErrorInfo> errorInfoList = new List<ErrorInfo> ();
			Type type = obj.GetType ();
			FieldInfo[] fields = type.GetFields (BlindingFlags.Instance | BindingFlangs.NonPublic);
			foreach ( FieldInfo field in fields)
				if(filed.IsDefined (typeof(ValidationAttribute), true)){
					ValidationAttribute validationAttribute =
					(ValidationAttribute)Attribute.GetCustomAttribute (type, typeof(ValidationAttribute));
					object value = field.GetValue (obj);
					string message = validationAttribute.Validate (value);
					if (message != null)
						errorInfoList.Add (new ErrorInfo (fild.Name, message));
				}
			{return errorInfoList.ToArray ();
		}
	}
}


