﻿using MediatR;
using Application.Shared;

namespace Application.UseCases.Orders.ListOrders
{
    public record ListOrdersRequest : PagedFilter, IRequest<List<ListOrdersResponse>>;
}
