using API.GraphQL.Mutations.RegisterOrder;
using Application.UseCases.Orders.RegisterOrder;
using System.Text.Json;

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
                          var input = context.ArgumentValue<RegisterOrderRequest>("input");
                          
                          var useCase = context.Service<IRegisterOrderUseCase>();

                          await useCase.Execute(input);

                          return true;
                      });
        }
    }
}
