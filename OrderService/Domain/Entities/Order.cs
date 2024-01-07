using System;

namespace Domain.Entities
{
    public class Order : Entity
    {
        public string ProductDescription { get; private set; }
        public decimal ProductValue { get; private set; }
        public int ProductQuantity { get; private set; }
        public string UserEmail { get; private set; }
        public Payment Payment { get; private set; }

        protected Order() { }

        public Order(string productDescription, decimal productValue, int productQuantity, string userEmail, Payment payment)
        {
            ProductDescription = productDescription;
            ProductValue = productValue;
            ProductQuantity = productQuantity;
            UserEmail = userEmail;
            Payment = payment;
        }

        public void UpdatePaymentStatus(bool approved)
        {
            Payment.UpdateStatus(approved);

            UpdatedOn = DateTime.UtcNow;
        }
    }
}
