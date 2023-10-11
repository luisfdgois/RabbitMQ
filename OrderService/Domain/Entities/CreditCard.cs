namespace Domain.Entities
{
    public class CreditCard : Payment
    {
        public string Number { get; private set; }
        public string CVV { get; private set; }
        public int NumberOfInstallment { get; private set; }
        public decimal ValuePerInstallment { get; private set; }

        protected CreditCard() { }

        public CreditCard(string number, string cVV, int numberOfInstallment, decimal valuePerInstallment) : base()
        {
            Number = number;
            CVV = cVV;
            NumberOfInstallment = numberOfInstallment;
            ValuePerInstallment = valuePerInstallment;
        }
    }
}
