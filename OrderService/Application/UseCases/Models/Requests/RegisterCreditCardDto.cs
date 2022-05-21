namespace Application.UseCases.Models.Requests
{
    public class RegisterCreditCardDto : RegisterPaymentDto
    {
        public string Number { get; set; }
        public string CVV { get; set; }
        public int NumberOfInstallment { get; set; }
        public decimal ValuePerInstallment { get; set; }
    }
}
