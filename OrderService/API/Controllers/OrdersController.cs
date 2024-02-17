using Application.UseCases.Orders.GetOrderById;
using Application.UseCases.Orders.ListOrders;
using Application.UseCases.Orders.RegisterOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GetOrderByIdResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetOrderByIdRequest(id));

            if (response is null) return NotFound();

            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ListOrdersResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrders(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ListOrdersRequest(), cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// This function registers a new order
        /// </summary>
        /// <remarks>
        /// ### PaymentType = 1 (CreditCard):
        /// ####   Payment Properties: 
        /// *   { Number: "", CVV: "", NumberOfInstallment: 0, ValuePerInstallment: 0 }
        /// </remarks>
        [HttpPost]
        public async Task<IActionResult> RegisterOrder([FromBody] RegisterOrderRequest request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);

            return Ok();
        }
    }
}
