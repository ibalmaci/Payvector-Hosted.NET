using PayvectorHosted.Payment.Enums;
using PayvectorHosted.Payment.Implementations;
using PayvectorHosted.Payment.Types;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;

namespace PayvectorHosted.SampleWeb
{
	public partial class Default : Page
	{
		protected void btnPayNow_Click(object sender, EventArgs e)
		{
			string postUrl = "https://mms.payvector.net/Pages/PublicPages/PaymentForm.aspx";
			string merchantId = WebConfigurationManager.AppSettings["MerchantId"];
			string merchantPassword = WebConfigurationManager.AppSettings["MerchantPassword"];
			string preSharedKey = WebConfigurationManager.AppSettings["PreSharedKey"];

			var pRequest = new TransactionRequest
			{
				//Order Details
				Amount = 101,
				CurrencyCode = "826", //GBP
				OrderID = "Order-123456",
				OrderDescription = "A test order",
				TransactionType = TransactionType.SALE,
				CallbackURL = WebConfigurationManager.AppSettings["CallBackUrl"],
				ServerResultURL = WebConfigurationManager.AppSettings["ServerCallBackUrl"],

				EchoAVSCheckResult = true,
				EchoCV2CheckResult = true,
				EchoThreeDSecureAuthenticationCheckResult = true,
				EchoCardType = true,

				//Payment Details
				CustomerName = "A Customer",

				//Billing Address
				Address1 = "1 Some Street",
				Address2 = "",
				Address3 = "",
				Address4 = "",
				City = "Some City",
				State = "Some State",
				PostCode = "GU14 666",
				CountryCode = "826", //UK

				//Customer Details
				EmailAddress = "test@test.com",
				PhoneNumber = "12345678",

				EmailAddressEditable = false,
				PhoneNumberEditable = false,

				CV2Mandatory = true,
				Address1Mandatory = true,
				CityMandatory = true,
				PostCodeMandatory = true,
				StateMandatory = true,
				CountryMandatory = true
			};

			//Custom variables
			var ResultCookieVariables = new NameValueCollection();
			var ResultFormVariables = new NameValueCollection();
			var ResultQueryStringVariables = new NameValueCollection();
			pRequest.ServerResultURLCookieVariables = ResultCookieVariables;
			pRequest.ServerResultURLFormVariables = ResultFormVariables;
			pRequest.ServerResultURLQueryStringVariables = ResultQueryStringVariables;

			var http = HttpContext.Current;
			var hpProcess = new PaymentProcessor(merchantId, http);
			hpProcess.SubmitTransaction(pRequest, merchantPassword, preSharedKey, postUrl);
		}
	}
}