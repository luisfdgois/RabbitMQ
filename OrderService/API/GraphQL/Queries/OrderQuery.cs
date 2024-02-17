using API.GraphQL.Queries.GetOrderById;
using API.GraphQL.Queries.ListOrders;
using Application.UseCases.Orders.GetOrderById;
using Application.UseCases.Orders.ListOrders;
using Application.UseCases.Orders.ListOrders.GraphQL;
using MediatR;

namespace API.GraphQL.Queries
{
    public class OrderQuery : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("order_queries");

            descriptor.Field("orders")
                      .Argument("filter", a => a.Type<ListOrdersRequestType>())
                      .Type<ListType<ListOrdersResponseType>>()
                      .Resolve(async context =>
                      {
                          var request = context.ArgumentValue<ListOrdersRequest>("filter");

                          var mediator = context.Service<IMediator>();

                          var response = await mediator.Send(request);

                          return response;
                      });

            descriptor.Field("orderById")
                      .Argument("id", a => a.Type<StringType>())
                      .Type<GetOrderByIdResponseType>()
                      .Resolve(async context =>
                      {
                          var idString = context.ArgumentValue<string>("id");
                          var id = new Guid(idString);

                          var mediator = context.Service<IMediator>();

                          var response = await mediator.Send(new GetOrderByIdRequest(id));

                          return response;
                      });

        }
    }
}
