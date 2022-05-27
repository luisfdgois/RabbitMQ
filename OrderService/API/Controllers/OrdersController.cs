using Application.UseCases.Models.Requests;
using Application.UseCases.Orders.ListOrders;
using Application.UseCases.Orders.RegisterOrder;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOrders(
            [FromServices] IListOrdersUseCase useCase)
        {
            return Ok(await useCase.Execute());
        }

        /// <summary>
        /// This function registers a new order
        /// </summary>
        /// <remarks>
        /// ### Payment Type = CreditCard:
        /// ####   Payment: 
        /// *   { Number: "", CVV: "", NumberOfInstallment: 0, ValuePerInstallment: 0 }
        /// </remarks>
        /// <param name="useCase"></param>
        /// <param name="dto"></param>
        [HttpPost]
        public async Task<IActionResult> RegisterOrder(
            [FromServices] IRegisterOrderUseCase useCase,
            [FromBody] RegisterOrderDto dto)
        {
            await useCase.Execute(dto);

            return Ok();
        }
    }
}
