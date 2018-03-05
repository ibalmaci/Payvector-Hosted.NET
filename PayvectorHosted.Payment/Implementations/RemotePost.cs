using PayvectorHosted.Payment.Enums;
using PayvectorHosted.Payment.Helpers;
using PayvectorHosted.Payment.Interfaces;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace PayvectorHosted.Payment.Implementations
{
	public class RemotePost : IRemotePost
	{
		private readonly HttpContext _httpContext;

		public NameValueCollection InputValues { get; set; }
		public FormMethod Method { get; set; }
		public string Url { get; set; }

		public RemotePost(HttpContext httpContext, string url, FormMethod method)
			: this(httpContext)
		{
			Url = url;
			Method = method;
		}

		public RemotePost(HttpContext httpContext)
		{
			InputValues = new NameValueCollection();
			_httpContext = httpContext;
		}

		public void AddInput(string name, object value)
		{
			InputValues.Add(name, value.ToString());
		}

		public void Post(string formName)
		{
			if (string.IsNullOrEmpty(formName))
				throw new ArgumentNullException("Form Name Error!");

			var html = new StringBuilder();
			html.AppendLine("<html><head>");
			html.AppendLineFormat("</head><body onload=\"document.{0}.submit()\">", formName);
			html.AppendLineFormat("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", formName, Method.ToString(), Url);
			for (int i = 0; i < InputValues.Keys.Count; i++)
				html.AppendLineFormat("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">",
						HttpUtility.HtmlEncode(InputValues.Keys[i]),
						HttpUtility.HtmlEncode(InputValues[InputValues.Keys[i]])
					);
			html.AppendLine("</form>");
			html.AppendLine("</body></html>");
			_httpContext.Response.Clear();
			_httpContext.Response.Write(html.ToString());
			_httpContext.Response.End();
		}
	}
}
