using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDTVWebAPI
{
	public partial class DDTVServer
	{
		[System.Serializable]
		public class RequstException : System.Exception
		{
			public Code Code { get; init; }
			public string ServerMessage { get; init; }

			public RequstException() { Code = Code.NotUsed; ServerMessage = "错误"; }
			public RequstException(string message) : base(message) { Code = Code.NotUsed; ServerMessage = message; }
			public RequstException(string message, System.Exception inner) : base(message, inner) { Code = Code.NotUsed; ServerMessage = message; }
			protected RequstException(
				System.Runtime.Serialization.SerializationInfo info,
				System.Runtime.Serialization.StreamingContext context) : base(info, context) { Code = Code.NotUsed; ServerMessage = "错误"; }
			public RequstException(string Servermessage, Code code)
			{
				ServerMessage = Servermessage;
				Code = code;
			}
		}
		[System.Serializable]
		public class NotLoginException : System.Exception
		{
			public NotLoginException() { }
			public NotLoginException(string message) : base(message) { }
			public NotLoginException(string message, System.Exception inner) : base(message, inner) { }
			protected NotLoginException(
				System.Runtime.Serialization.SerializationInfo info,
				System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
		}
		[System.Serializable]
		public class NeedParamException : System.Exception
		{
			public NeedParamException() { }
			public NeedParamException(string message) : base(message) { }
			public NeedParamException(string message, System.Exception inner) : base(message, inner) { }
			protected NeedParamException(
				System.Runtime.Serialization.SerializationInfo info,
				System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
		}

	}
}
