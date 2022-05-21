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
