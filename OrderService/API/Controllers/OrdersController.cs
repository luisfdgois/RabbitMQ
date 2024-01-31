using Application.UseCases.Orders.GetOrderById;
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
            [FromServices] IListOrdersUseCase useCase,
            [FromQuery] ListOrdersRequest request)
        {
            return Ok(await useCase.Execute(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(
            [FromServices] IGetOrderByIdUseCase useCase, 
            [FromRoute] Guid id)
        {
            var result = await useCase.Execute(id);

            if (result is null) return NotFound();

            return Ok(result);
        }

        /// <summary>
        /// This function registers a new order
        /// </summary>
        /// <remarks>
        /// ### PaymentType = 1 (CreditCard):
        /// ####   Payment Properties: 
        /// *   { Number: "", CVV: "", NumberOfInstallment: 0, ValuePerInstallment: 0 }
        /// </remarks>
        /// <param name="useCase"></param>
        /// <param name="dto"></param>
        [HttpPost]
        public async Task<IActionResult> RegisterOrder(
            [FromServices] IRegisterOrderUseCase useCase,
            [FromBody] RegisterOrderRequest dto)
        {
            await useCase.Execute(dto);

            return Ok();
        }
    }
}
