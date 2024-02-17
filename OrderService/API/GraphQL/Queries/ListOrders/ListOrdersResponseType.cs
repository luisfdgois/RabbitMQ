using Application.UseCases.Orders.ListOrders;

namespace API.GraphQL.Queries.ListOrders
{
    public class ListOrdersResponseType : ObjectType<ListOrdersResponse>
    {
        protected override void Configure(IObjectTypeDescriptor<ListOrdersResponse> descriptor) { }
    }
}
