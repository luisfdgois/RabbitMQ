namespace Application.UseCases.Models.Requests
{
    public class RegisterOrderDto
    {
        public string ProductDescription { get; set; }
        public decimal ProductValue { get; set; }
        public int ProductQuantity { get; set; }
        public string UserEmail { get; set; }
        public RegisterPaymentDto Payment { get; set; }
    }
}
