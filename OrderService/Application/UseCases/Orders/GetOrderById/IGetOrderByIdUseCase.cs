namespace Application.UseCases.Orders.GetOrderById
{
    public interface IGetOrderByIdUseCase
    {
        Task<GetOrderByIdDto> Execute(Guid id, CancellationToken cancellationToken = default);
    }
}
