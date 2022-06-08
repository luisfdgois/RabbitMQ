namespace CreditCard.Service.Models
{
    public class CreditCardMessage
    {
        public Guid OrderId { get; set; }
        public string Number { get; private set; }
        public string CVV { get; private set; }
        public int NumberOfInstallment { get; private set; }
        public decimal ValuePerInstallment { get; private set; }
    }
}
