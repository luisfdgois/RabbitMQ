namespace Application.UseCases.Models.Responses
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string ProductDescription { get; private set; }
        public decimal ProductValue { get; private set; }
        public int ProductQuantity { get; private set; }
        public string UserEmail { get; private set; }
        public DateTime RegistrationDate { get; set; }
    }
}
