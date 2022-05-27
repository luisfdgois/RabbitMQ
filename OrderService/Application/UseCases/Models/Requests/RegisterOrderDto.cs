using Application.UseCases.Models.Enums;
using Newtonsoft.Json.Linq;

namespace Application.UseCases.Models.Requests
{
    public class RegisterOrderDto
    {
        public string ProductDescription { get; set; }
        public decimal ProductValue { get; set; }
        public int ProductQuantity { get; set; }
        public string UserEmail { get; set; }
        public PaymentTypeDto PaymentType { get; set; }
        public JObject Payment { get; set; }
    }
}
