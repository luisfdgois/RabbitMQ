using Application.UseCases.Orders.GetOrderById;

namespace API.GraphQL.Queries.GetOrderById
{
    public class GetOrderByIdResponseType : ObjectType<GetOrderByIdResponse>
    {
        protected override void Configure(IObjectTypeDescriptor<GetOrderByIdResponse> descriptor) { }
    }
}
