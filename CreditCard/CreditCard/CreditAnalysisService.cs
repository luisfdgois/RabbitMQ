using CreditCard.Models;

namespace CreditCard
{
    public class CreditAnalysisService
    {
        public static void Analyze(CreditCardMessage creditCard, out ProcessedCreditMessage processedCreditMessage)
        {
            Thread.Sleep(10000);

            processedCreditMessage = new ProcessedCreditMessage { OrderId = creditCard.OrderId, PaymentApproved = CreditApproved() };
        }

        private static bool CreditApproved() =>
            (new Random().Next(0, 1) is 1);
    }
}
