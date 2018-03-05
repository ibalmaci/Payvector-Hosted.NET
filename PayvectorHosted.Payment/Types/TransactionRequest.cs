using PayvectorHosted.Payment.Enums;
using PayvectorHosted.Payment.Helpers;
using System;
using System.Collections.Specialized;

namespace PayvectorHosted.Payment.Types
{
	public class TransactionRequest
	{
		public string Address1 { get; set; }
		public bool Address1Mandatory { get; set; }
		public string Address2 { get; set; }
		public string Address3 { get; set; }
		public string Address4 { get; set; }
		public int Amount { get; set; }
		public string CallbackURL { get; set; }
		public string City { get; set; }
		public bool CityMandatory { get; set; }
		public string CountryCode { get; set; }
		public bool CountryMandatory { get; set; }
		public string CurrencyCode { get; set; }
		public string CustomerName { get; set; }
		public bool CV2Mandatory { get; set; }
		public bool EchoAVSCheckResult { get; set; }
		public bool EchoCardType { get; set; }
		public bool EchoCV2CheckResult { get; set; }
		public bool EchoThreeDSecureAuthenticationCheckResult { get; set; }
		public string EmailAddress { get; set; }
		public bool EmailAddressEditable { get; set; }
		public string OrderDescription { get; set; }
		public string OrderID { get; set; }
		public bool PaymentFormDisplaysResult { get; set; }
		public string PhoneNumber { get; set; }
		public bool PhoneNumberEditable { get; set; }
		public string PostCode { get; set; }
		public bool PostCodeMandatory { get; set; }
		public ResultDeliveryMethod ResultDeliveryMethod { get; set; }
		public string ServerResultURL { get; set; }
		public NameValueCollection ServerResultURLCookieVariables { get; set; }
		public NameValueCollection ServerResultURLFormVariables { get; set; }
		public NameValueCollection ServerResultURLQueryStringVariables { get; set; }
		public string State { get; set; }
		public bool StateMandatory { get; set; }
		public string TransactionDateTime { get; set; }
		public TransactionType TransactionType { get; set; }

		public TransactionRequest()
		{
			TransactionType = TransactionType.SALE;
			ResultDeliveryMethod = ResultDeliveryMethod.SERVER;
			CV2Mandatory = true;
			TransactionDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss zzzz");
			ServerResultURLCookieVariables = new NameValueCollection();
			ServerResultURLFormVariables = new NameValueCollection();
			ServerResultURLQueryStringVariables = new NameValueCollection();
		}

		public NameValueCollection ToNameValueCollection()
		{
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection.AddProperty(this, r => r.Amount);
			nameValueCollection.AddProperty(this, r => r.CurrencyCode);
			nameValueCollection.AddProperty(this, r => r.EchoAVSCheckResult);
			nameValueCollection.AddProperty(this, r => r.EchoCV2CheckResult);
			nameValueCollection.AddProperty(this, r => r.EchoThreeDSecureAuthenticationCheckResult);
			nameValueCollection.AddProperty(this, r => r.EchoCardType);
			nameValueCollection.AddProperty(this, r => r.OrderID);
			nameValueCollection.AddProperty(this, r => r.TransactionType);
			nameValueCollection.AddProperty(this, r => r.TransactionDateTime);
			nameValueCollection.AddProperty(this, r => r.CallbackURL);
			nameValueCollection.AddProperty(this, r => r.OrderDescription);
			nameValueCollection.AddProperty(this, r => r.CustomerName);
			nameValueCollection.AddProperty(this, r => r.Address1);
			nameValueCollection.AddProperty(this, r => r.Address2);
			nameValueCollection.AddProperty(this, r => r.Address3);
			nameValueCollection.AddProperty(this, r => r.Address4);
			nameValueCollection.AddProperty(this, r => r.City);
			nameValueCollection.AddProperty(this, r => r.State);
			nameValueCollection.AddProperty(this, r => r.PostCode);
			nameValueCollection.AddProperty(this, r => r.CountryCode);
			nameValueCollection.AddProperty(this, r => r.EmailAddress);
			nameValueCollection.AddProperty(this, r => r.PhoneNumber);
			nameValueCollection.AddProperty(this, r => r.EmailAddressEditable);
			nameValueCollection.AddProperty(this, r => r.PhoneNumberEditable);
			nameValueCollection.AddProperty(this, r => r.CV2Mandatory);
			nameValueCollection.AddProperty(this, r => r.Address1Mandatory);
			nameValueCollection.AddProperty(this, r => r.CityMandatory);
			nameValueCollection.AddProperty(this, r => r.PostCodeMandatory);
			nameValueCollection.AddProperty(this, r => r.StateMandatory);
			nameValueCollection.AddProperty(this, r => r.CountryMandatory);
			nameValueCollection.AddProperty(this, r => r.ResultDeliveryMethod);
			nameValueCollection.AddProperty(this, r => r.ServerResultURL);
			nameValueCollection.AddProperty(this, r => r.PaymentFormDisplaysResult);

			string strForm = ServerResultURLFormVariables.ToQueryString();
			string strQuery = ServerResultURLQueryStringVariables.ToQueryString();
			string strCookie = ServerResultURLCookieVariables.ToQueryString();

			nameValueCollection.Add("ServerResultURLCookieVariables", strCookie);
			nameValueCollection.Add("ServerResultURLFormVariables", strForm);
			nameValueCollection.Add("ServerResultURLQueryStringVariables", strQuery);
			return nameValueCollection;
		}
	}
}
