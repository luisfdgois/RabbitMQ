using Application.UseCases.Orders.GetOrderById;

namespace API.GraphQL.Queries.GetOrderById
{
    public class GetOrderByIdDtoType : ObjectType<GetOrderByIdDto>
    {
        protected override void Configure(IObjectTypeDescriptor<GetOrderByIdDto> descriptor) { }
    }
}
