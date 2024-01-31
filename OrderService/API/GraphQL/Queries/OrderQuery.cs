using API.GraphQL.Queries.GetOrderById;
using Application.UseCases.Orders.GetOrderById;
using Application.UseCases.Orders.ListOrders;
using Application.UseCases.Orders.ListOrders.GraphQL;

namespace API.GraphQL.Queries
{
    public class OrderQuery : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("order_queries");

            descriptor.Field("orders")
                      .Argument("filter", a => a.Type<ListOrdersRequestType>())
                      .Type<ListType<ListOrdersDtoType>>()
                      .Resolve(async context =>
                      {
                          var filters = context.ArgumentValue<ListOrdersRequest>("filter");

                          var useCase = context.Service<IListOrdersUseCase>();

                          return await useCase.Execute(filters);
                      });

            descriptor.Field("orderById")
                      .Argument("id", a => a.Type<StringType>())
                      .Type<GetOrderByIdDtoType>()
                      .Resolve(async context =>
                      {
                          var idString = context.ArgumentValue<string>("id");
                          var id = new Guid(idString);

                          var useCase = context.Service<IGetOrderByIdUseCase>();

                          return await useCase.Execute(id);
                      });

        }
    }
}
