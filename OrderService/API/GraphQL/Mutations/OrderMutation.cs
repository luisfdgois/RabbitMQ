using API.GraphQL.Mutations.RegisterOrder;
using Application.UseCases.Orders.RegisterOrder;
using MediatR;

namespace API.GraphQL.Mutations
{
    public class OrderMutation : ObjectType
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Name("order_mutations");

            descriptor.Field("add")
                      .Argument("input", a => a.Type<RegisterOrderRequestType>())
                      .Type<BooleanType>()
                      .Resolve(async context =>
                      {
                          var request = context.ArgumentValue<RegisterOrderRequest>("input");

                          var mediator = context.Service<IMediator>();

                          await mediator.Send(request);

                          return true;
                      });
        }
    }
}
