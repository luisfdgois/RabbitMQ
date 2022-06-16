using CreditCard.Service.Models;

namespace CreditCard.Service
{
    public class CreditAnalysisService
    {
        public static void Analyze(CreditCardMessage creditCard, out ProcessedCreditMessage processedCreditMessage)
        {
            Task.Delay(5000);

            processedCreditMessage = new ProcessedCreditMessage { OrderId = creditCard.OrderId, PaymentApproved = CreditApproved() };
        }

        private static bool CreditApproved() =>
            (new Random().Next(0, 1) is 1);
    }
}
