using PayvectorHosted.Payment.Enums;
using PayvectorHosted.Payment.Types;

namespace PayvectorHosted.Payment.Interfaces
{
	public interface IPaymentProcessor
	{
		string MerchantId { get; }

		void SubmitTransaction(TransactionRequest request, string merchantPassword, string preSharedKey, string postUrlHashMethod, HashMethod hashMethod = HashMethod.Sha1);
	}
}
