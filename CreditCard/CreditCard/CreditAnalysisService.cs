using Messages;

namespace CreditCard
{
    public class CreditAnalysisService
    {
        public static void Analyze(CreditRequestedMessage creditCard, out PaymentProcessedMessage processedCreditMessage)
        {
            Thread.Sleep(10000);

            processedCreditMessage = new PaymentProcessedMessage(creditCard.OrderId, CreditApproved());
        }

        private static bool CreditApproved() =>
            (new Random().Next(0, 2) is 1);
    }
}
