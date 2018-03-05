using PayvectorHosted.Payment.Enums;
using System.Collections.Specialized;

namespace PayvectorHosted.Payment.Interfaces
{
	public interface IRemotePost
	{
		NameValueCollection InputValues { get; set; }
		FormMethod Method { get; set; }
		string Url { get; set; }

		void AddInput(string name, object value);
		void Post(string formName);
	}
}
