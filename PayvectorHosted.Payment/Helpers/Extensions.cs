using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;

namespace PayvectorHosted.Payment.Helpers
{
	public static class Extensions
	{
		public static StringBuilder AppendLineFormat(this StringBuilder sb, string format, params object[] args)
		{
			sb.AppendLine(string.Format(format, args));
			return sb;
		}

		public static void Add(this NameValueCollection collection, string name, object value)
		{
			collection.Add(name, (value == null ? "" : value.ToString()));
		}

		public static void AddProperty<TEntity, TProperty>(this NameValueCollection collection, TEntity entity, Expression<Func<TEntity, TProperty>> e, object value = null)
		{
			MemberExpression body = e.Body as MemberExpression;
			if (body == null)
			{
				throw new ArgumentException("Expression does not refer to a member");
			}
			PropertyInfo member = body.Member as PropertyInfo;
			if (member == null)
			{
				throw new ArgumentException("Expression does not refer to a property");
			}
			collection.Add(member.Name, value ?? member.GetValue(entity, null));
		}

		public static string ToQueryString(this NameValueCollection collection, bool omitEmpty = false, bool encode = true, string delimiter = "&")
		{
			var items = new List<string>();
			foreach (string key in collection.AllKeys)
			{
				var values = collection.GetValues(key);
				foreach (var value in values)
					if ((omitEmpty ? !string.IsNullOrEmpty(value) : true))
						items.Add(string.Concat(key, "=", (encode ? HttpUtility.UrlEncode(value) : value)));
			}
			return string.Join(delimiter, items.ToArray());
		}
	}
}
