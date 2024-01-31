using Application.UseCases.Orders.RegisterOrder;

namespace API.GraphQL.Mutations.RegisterOrder
{
    public class RegisterOrderRequestType : InputObjectType<RegisterOrderRequest>
    {
        protected override void Configure(IInputObjectTypeDescriptor<RegisterOrderRequest> descriptor)
        {
            descriptor.Field(f => f.Payment).Type<AnyType>();
        }
    }
}
