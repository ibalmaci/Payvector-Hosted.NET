using PayvectorHosted.Payment.Enums;
using PayvectorHosted.Payment.Helpers;
using PayvectorHosted.Payment.Interfaces;
using PayvectorHosted.Payment.Types;
using System;
using System.Collections.Specialized;
using System.Web;

namespace PayvectorHosted.Payment.Implementations
{
	public class PaymentProcessor : IPaymentProcessor
	{
		private readonly HttpContext _context;

		public string MerchantId { get; internal set; }

		public PaymentProcessor(string merchantId, HttpContext context)
		{
			if (string.IsNullOrEmpty(merchantId))
			{
				throw new ArgumentNullException("MerchantId Error!");
			}
			_context = context;
			MerchantId = merchantId;
		}

		public void SubmitTransaction(TransactionRequest request, string merchantPassword, string preSharedKey, string postUrl, HashMethod hashMethod = HashMethod.Sha1)
		{
			if (request == null)
			{
				throw new ArgumentNullException("Request Error!");
			}
			string[] strArrays = { merchantPassword, preSharedKey, postUrl };

			var remotePost = new RemotePost(_context, postUrl, FormMethod.Post);

			var nvCollection = new NameValueCollection();

			if (hashMethod == HashMethod.Sha1 || hashMethod == HashMethod.Md5)
				nvCollection.Add("PreSharedKey", preSharedKey);

			nvCollection.Add("MerchantID", MerchantId);
			nvCollection.Add("Password", merchantPassword);

			var requestNVCol = request.ToNameValueCollection();
			for (int i = 0; i < requestNVCol.AllKeys.Length; i++)
			{
				var key = requestNVCol.AllKeys[i];
				nvCollection.Add(key, requestNVCol.GetValues(key)[0]);
				remotePost.AddInput(key, requestNVCol.GetValues(key)[0]);
			}
			var qStr = nvCollection.ToQueryString(false, false);
			var digest = HashUtil.ComputeHashDigest(qStr, preSharedKey, hashMethod);

			remotePost.AddInput("HashDigest", digest);
			remotePost.AddInput("MerchantID", MerchantId);
			remotePost.AddInput("ThreeDSecureCompatMode", "false");
			remotePost.AddInput("ServerResultCompatMode", "false");
			remotePost.Post("CardsavePaymentForm");
		}

	}
}
