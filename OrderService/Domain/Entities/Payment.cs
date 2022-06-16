namespace Domain.Entities
{
    public class Payment : Entity
    {
        public bool Approved { get; private set; }

        public Guid OrderId { get; private set; }
        public Order? Order { get; private set; }

        protected Payment() : base() { }

        public void UpdateStatus(bool approved)
        {
            Approved = approved;
        }
    }
}
