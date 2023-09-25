namespace Application.UseCases.Models.Responses
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public bool Approved { get; set; }
        public string ProductDescription { get; private set; }
        public decimal ProductValue { get; private set; }
        public int ProductQuantity { get; private set; }
        public string UserEmail { get; private set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastUpdate { get; private set; }
    }
}
