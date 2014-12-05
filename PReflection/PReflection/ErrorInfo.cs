using System;

namespace PReflection
{
	public class ErrorInfo
	{
		public ErrorInfo ()
		{
			Property = property;
			Message = Message;
		}
		public string Property {
			get {return property;}
		}
		public string Message {
			get{return Message;}
	}
	}
}

