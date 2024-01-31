namespace Application.UseCases.Orders.ListOrders.GraphQL
{
    public class ListOrdersRequestType : InputObjectType<ListOrdersRequest>
    {
        protected override void Configure(IInputObjectTypeDescriptor<ListOrdersRequest> descriptor)
        {
            descriptor.Field(f => f.PageLength).Type<IntType>();
            descriptor.Field(f => f.PageNumber).Type<IntType>();
        }
    }
}

