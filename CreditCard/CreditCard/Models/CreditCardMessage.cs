namespace CreditCard.Models
{
    public record CreditCardMessage
    {
        public Guid OrderId { get; set; }
        public string Number { get; init; } = null!;
        public string CVV { get; init; } = null!;
        public int NumberOfInstallment { get; init; }
        public string ValuePerInstallment { get; init; }
    }
}
