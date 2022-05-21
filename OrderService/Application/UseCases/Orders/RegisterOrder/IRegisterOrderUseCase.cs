using Application.UseCases.Models.Requests;

namespace Application.UseCases.Orders.RegisterOrder
{
    public interface IRegisterOrderUseCase
    {
        Task Execute(RegisterOrderDto dto);
    }
}
