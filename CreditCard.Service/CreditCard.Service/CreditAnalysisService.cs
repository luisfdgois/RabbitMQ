using CreditCard.Service.Models;

namespace CreditCard.Service
{
    public class CreditAnalysisService
    {
        public static ProcessedCreditMessage Analyze(CreditCardMessage creditCard)
        {
            return new ProcessedCreditMessage { OrderId = creditCard.OrderId, PaymentApproved = CreditApproved() };
        }

        private static bool CreditApproved() =>
            (new Random().Next(0, 1) is 1);
    }
}
