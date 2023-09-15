using Application.UseCases.Models.Requests;
using System.Threading.Tasks;

namespace Application.UseCases.Orders.RegisterOrder
{
    public interface IRegisterOrderUseCase
    {
        Task Execute(RegisterOrderDto dto);
    }
}
