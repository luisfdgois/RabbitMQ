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

        private Order() { }

        public Order(string productDescription, decimal productValue, int productQuantity, string userEmail)
        {
            ProductDescription = productDescription;
            ProductValue = productValue;
            ProductQuantity = productQuantity;
            UserEmail = userEmail;
        }

        public Order AddPaymentMethod(Payment payment)
        {
            Payment = payment;

            return this;
        }

        public void UpdatePaymentStatus(bool approved)
        {
            // Add a custom Exception
            if (Payment is null)
                throw new NullReferenceException("Payment information not found.");

            Payment.UpdateStatus(approved);
        }
    }
}
