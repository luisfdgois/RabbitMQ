using System;

namespace Domain.Exceptions
{
    public class InvalidPaymentTypeConversionException : Exception
    {
        public InvalidPaymentTypeConversionException() : base("It could not deserialize an unmapped type. Set the payment type and try again.") { }
    }
}
