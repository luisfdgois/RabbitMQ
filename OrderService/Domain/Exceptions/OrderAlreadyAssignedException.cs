namespace Domain.Exceptions
{
    public class OrderAlreadyAssignedException : Exception
    {
        public OrderAlreadyAssignedException() : base("An order has already been assigned to this payment.") { }
    }
}
